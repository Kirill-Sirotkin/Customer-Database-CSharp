using Utils;

namespace Database;

class Create : IAction
{
    private Customer _customer;
    private List<Customer> _customers;

    public Create(Customer customer, List<Customer> customers)
    {
        _customer = customer;
        _customers = customers;
    }

    public void Execute()
    {
        _customers.Add(_customer);
    }

    public void Undo()
    {
        _customers = _customers.Where(c => c.Id != _customer.Id).ToList();
    }
}

class Delete : IAction
{
    private Customer _customer;
    private List<Customer> _customers;

    public Delete(Customer customer, List<Customer> customers)
    {
        _customer = customer;
        _customers = customers;
    }

    public void Execute()
    {
        _customers = _customers.Where(c => c.Id != _customer.Id).ToList();
    }

    public void Undo()
    {
        _customers.Add(_customer);
    }
}

class Update : IAction
{
    private Customer _customer;
    private CustomerDTO _newCustomer;
    private CustomerDTO _oldCustomer;

    public Update(Customer customer, CustomerDTO newCustomer)
    {
        _customer = customer;
        _newCustomer = newCustomer;
        _oldCustomer = new(_customer.FirstName, _customer.LastName, _customer.Email, _customer.Address);
    }

    public void Execute()
    {
        _customer.FirstName = _newCustomer.firstName;
        _customer.LastName = _newCustomer.lastName;
        _customer.Email = _newCustomer.email;
        _customer.Address = _newCustomer.address;
    }

    public void Undo()
    {
        _customer.FirstName = _oldCustomer.firstName;
        _customer.LastName = _oldCustomer.lastName;
        _customer.Email = _oldCustomer.email;
        _customer.Address = _oldCustomer.address;
    }
}