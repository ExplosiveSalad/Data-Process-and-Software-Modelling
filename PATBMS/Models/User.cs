namespace PATBMS.Models
{
public class User
{
    //Private fields (only accessible by this class)
    private string userID;
    private string name;
    private string email;
    private string password; 
    private string role;

    //Public properties (accessible by external classes)
    public string UserID
        {
            get {return userID; }
            set {userID = value; }
        }

    public string Name
        {
            get {return name; }
            set {name = value; }
        }

    public string Email
        {
         get {return email; }
         set {email = value; }   
        }

    public string Password
        {
            get {return password; }
            set {password = value; }
        }

    public string Role
        {
            get {return role; }
            set {role = value; }
        }
    //Constructors
    public User(string userID, string name, string email, string password, string role)
        {
            //each parameter is assigned its field here
            this.userID = userID;
            this.name = name;
            this.email = email;
            this.password = password;
            this.role = role;
        }
    //Methods
    public void Login()
        {
            //Full authentication with credential verification will be implemented in Part 2
            Console.WriteLine("Welcome to the PATBMS, " + Name + "!");
        }
    
    public void Logout()
        {
            Console.WriteLine("Goodbye " + Name + "!");
        }
}
}