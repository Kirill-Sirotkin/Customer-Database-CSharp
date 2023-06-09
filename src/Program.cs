using Database;

internal class Program
{
    private static void Main(string[] args)
    {
        CustomerDatabase db = new CustomerDatabase("./src/customers.csv");
        // CustomerDatabase db2 = new CustomerDatabase("./src/customers.csv"); --- throws error to prevent simultaneous editing of a file
        CustomerDTO customer1 = new CustomerDTO("Jonh", "Smith", "john@mail.com", "123");
        CustomerDTO customer2 = new CustomerDTO("Jane", "Jones", "jane@mail.com", "456");
        CustomerDTO customer3 = new CustomerDTO("Bob", "Lincoln", "bob@mail.com", "789");

        db.CreateCustomer(customer1);
        db.CreateCustomer(customer2);
        db.CreateCustomer(customer3);

        db.SaveChanges();
    }
}