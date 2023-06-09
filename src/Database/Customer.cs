namespace Database;

class Customer
{
    private Guid _id;
    private string _firstName = "defaultFirst";
    private string _lastName = "defaultLast";
    private string _email = "default@default.com";
    private string _address = "defaultAddress";

    public Guid Id
    {
        get { return _id; }
    }
    public string FirstName
    {
        get { return _firstName; }
        set
        {
            if (value.Length > 99) throw new Exception("First name must be less than 100 characters long");
            _firstName = value;
        }
    }
    public string LastName
    {
        get { return _lastName; }
        set
        {
            if (value.Length > 99) throw new Exception("Last name must be less than 100 characters long");
            _lastName = value;
        }
    }
    public string Email
    {
        get { return _email; }
        set
        {
            int atIndex = value.IndexOf('@');
            if (atIndex < 0) throw new Exception("Email must contain @ symbol");
            int dotIndex = value.LastIndexOf('.');
            if (dotIndex < 0) throw new Exception("Email must contain . symbol followed by domain");
            if (dotIndex < atIndex) throw new Exception("Email must contain . symbol followed by domain after @ symbol");

            _email = value;
        }
    }
    public string Address
    {
        get { return _address; }
        set
        {
            if (value.Length > 199) throw new Exception("Address must be less than 200 characters long");
            _address = value;
        }
    }

    public Customer(Guid id, string firstName, string lastName, string email, string address)
    {
        _id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Address = address;
    }

    public override string ToString()
    {
        return $"{_id},{_firstName},{_lastName},{_email},{_address}";
    }
}