namespace Database;

struct CustomerDTO
{
    public CustomerDTO(string firstName, string lastName, string email, string address)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.address = address;
    }

    public string firstName;
    public string lastName;
    public string email;
    public string address;
}