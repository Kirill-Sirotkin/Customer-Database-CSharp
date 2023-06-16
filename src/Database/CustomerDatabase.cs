using Utils;

namespace Database;

class CustomerDatabase
{
    private static List<string> fileNames = new();

    private List<Customer> _customers;
    private string _filePath;
    private ActionRecorder _recorder;

    public List<Customer> Customers
    {
        get { return _customers; }
    }

    public CustomerDatabase(string filePath)
    {
        if (fileNames.Contains(filePath)) throw ExceptionHandler.FileInUse();
        _filePath = filePath;
        _customers = ConvertCustomersToList(FileHelper.ReadFile(_filePath));
        fileNames.Add(_filePath);
        _recorder = new();
    }

    ~CustomerDatabase()
    {
        fileNames.Remove(_filePath);
    }

    public Guid CreateCustomer(CustomerDTO customerDTO)
    {
        if (GetCustomerByEmail(customerDTO.email) is not null)
            throw ExceptionHandler.EmailExists();

        Guid id = Guid.NewGuid();

        Customer customer;
        try
        {
            customer = new Customer(
                id,
                customerDTO.firstName,
                customerDTO.lastName,
                customerDTO.email,
                customerDTO.address
            );
        }
        catch (Exception e)
        {
            throw ExceptionHandler.CustomerNotCreated(e.ToString());
        }

        Create action = new(customer, _customers);
        _recorder.Record(action);
        return id;
    }

    public void DeleteCustomer(Guid id)
    {
        Delete action = new(GetCustomerById(id), _customers);
        _recorder.Record(action);
    }

    public void UpdateCustomer(Guid id, CustomerDTO customerDTO)
    {
        Update action = new(GetCustomerById(id), customerDTO);
        _recorder.Record(action);
    }

    public void UndoAction()
    {
        _recorder.Rewind();
    }

    public void RedoAction()
    {
        _recorder.Redo();
    }

    public Customer GetCustomerById(Guid id)
    {
        Customer? customer = _customers.Find(c => c.Id == id);
        return customer is not null ? customer : throw ExceptionHandler.CustomerNotFound();
    }

    public Customer? GetCustomerByEmail(string email)
    {
        return _customers.Find(c => c.Email == email);
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