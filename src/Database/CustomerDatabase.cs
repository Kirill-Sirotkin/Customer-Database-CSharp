using Utils;

namespace Database;

class CustomerDatabase
{
    private static List<string> fileNames = new();

    private List<Customer> _customers;
    private string _filePath;

    public List<Customer> Customers
    {
        get { return _customers; }
    }

    public CustomerDatabase(string filePath)
    {
        if (fileNames.Contains(filePath)) throw new Exception("This file is already in use");
        _filePath = filePath;
        _customers = ConvertCustomersToList(FileHelper.ReadFile(_filePath));
        fileNames.Add(_filePath);
    }

    ~CustomerDatabase()
    {
        fileNames.Remove(_filePath);
    }

    public Guid CreateCustomer(CustomerDTO customerDTO)
    {
        if (GetCustomerByEmail(customerDTO.email) is not null)
            throw new Exception("Email already in use");

        Guid id = Guid.NewGuid();
        Customer customer = new Customer(
            id,
            customerDTO.firstName,
            customerDTO.lastName,
            customerDTO.email,
            customerDTO.address
        );
        _customers.Add(customer);
        return id;
    }

    public Customer? GetCustomerById(Guid id)
    {
        return _customers.Find(c => c.Id == id);
    }

    public Customer? GetCustomerByEmail(string email)
    {
        return _customers.Find(c => c.Email == email);
    }

    public void DeleteCustomer(Guid id)
    {
        _customers = _customers.Where(c => c.Id != id).ToList();
    }

    public void UpdateCustomer(Guid id, CustomerDTO customerDTO)
    {
        Customer? customer = GetCustomerById(id);
        if (customer is null) return;
        customer.FirstName = customerDTO.firstName;
        customer.LastName = customerDTO.lastName;
        customer.Email = customerDTO.email;
        customer.Address = customerDTO.address;
    }

    public void SaveChanges()
    {
        string[] data = new string[_customers.Count];
        for (int i = 0; i < _customers.Count; i++)
        {
            data[i] = _customers[i].ToString();
        }
        FileHelper.WriteFile(_filePath, data);
    }

    private List<Customer> ConvertCustomersToList(string[] customersArray)
    {
        List<Customer> customers = new();
        for (int i = 0; i < customersArray.Length; i++)
        {
            string[] userInfo = customersArray[i].Split(',');
            Customer customer = new Customer(
                Guid.Parse(userInfo[0]),
                userInfo[1],
                userInfo[2],
                userInfo[3],
                userInfo[4]
            );
            customers.Add(customer);
        }
        return customers;
    }
}