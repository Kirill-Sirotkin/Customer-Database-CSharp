using Database;

internal class Program
{
    private static void Main(string[] args)
    {
        CustomerDatabase db = new CustomerDatabase("./src/customers.csv");
        try
        {
            CustomerDatabase db2 = new CustomerDatabase("./src/customers.csv"); // throws error to prevent simultaneous editing of a file
        }
        catch (Utils.ExceptionHandler e)
        {
            Console.WriteLine(e.ToString());
        }
        CustomerDTO customer1 = new("Jonh", "Smith", "john@mail.com", "123");
        CustomerDTO customer1_1 = new("Jonh", "Smith", "john@mail.com", "123"); // Copy of previous customer to test for error
        CustomerDTO customer2 = new("Jane", "Jones", "jane@mail.com", "456");
        CustomerDTO customer3 = new("Bob", "Lincoln", "bob@mail.com", "789");
        CustomerDTO customer4 = new("Brad", "Pitt", "you know", ""); // Customer with invalid email to test for error

        Guid id1 = db.CreateCustomer(customer1);
        Guid id2 = db.CreateCustomer(customer2);
        Guid id3 = db.CreateCustomer(customer3);
        try
        {
            db.CreateCustomer(customer1_1); // throws error because email already in use
        }
        catch (Utils.ExceptionHandler e)
        {
            Console.WriteLine(e.ToString());
        }
        try
        {
            db.CreateCustomer(customer4); // throws error because email is invalid
        }
        catch (Utils.ExceptionHandler e)
        {
            Console.WriteLine(e.ToString());
        }


        db.SaveChanges();

        Console.WriteLine(db.GetCustomerById(id1).ToString());
        CustomerDTO customer1update = new("Ronald", "Macdonald", "ron@mail.com", "street");
        db.UpdateCustomer(id1, customer1update);
        Console.WriteLine(db.GetCustomerById(id1).ToString());
        db.UndoAction();
        Console.WriteLine(db.GetCustomerById(id1).ToString());
        db.RedoAction();
        Console.WriteLine(db.GetCustomerById(id1).ToString());

        db.SaveChanges();
    }
}