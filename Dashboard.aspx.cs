using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

public partial class Dashboard : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string userType = GetUserType(); // Assuming you have a method to retrieve the user type selection
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            // Retrieve the username and email based on the user type
            string username = string.Empty;
            string email = string.Empty;

            if (userType == "BusinessOwner")
            {
                username = GetLatestBusinessOwnerUsername(connectionString);
                email = GetLatestBusinessOwnerEmail(connectionString);
            }
            else if (userType == "Customer")
            {
                username = GetLatestCustomerUsername(connectionString);
                email = GetLatestCustomerEmail(connectionString);
            }

            // Update the HTML elements with the user's information
            usernameLabel.InnerText = username;
            emailLabel.InnerText = email;

            // Load the following businesses
            LoadFollowingBusinesses(connectionString);
        }
    }

    private void LoadFollowingBusinesses(string connectionString)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT Name FROM Following f JOIN Businesses b ON f.BusinessId = b.BusinessId WHERE f.CustomerId = @CustomerId";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                string customerId = GetLatestCustomerId(connectionString);
                command.Parameters.AddWithValue("@CustomerId", customerId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string businessName = reader["Name"].ToString();
                    AddFollowingBusiness(businessName);
                }
                reader.Close();
            }
        }
    }

    private void AddFollowingBusiness(string businessName)
    {
        var followingContainer = (System.Web.UI.HtmlControls.HtmlGenericControl)FindControl("followingContainer");

        var businessNameParagraph = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
        businessNameParagraph.InnerText = businessName;

        followingContainer.Controls.Add(businessNameParagraph);
    }

    // Existing code for retrieving the latest usernames and emails...

    private string GetLatestCustomerId(string connectionString)
    {
        string query = "SELECT TOP 1 CustomerId FROM Customers ORDER BY CustomerId DESC";
        return GetSingleValueFromDatabase(connectionString, query);
    }

    private string GetSingleValueFromDatabase(string connectionString, string query)
    {
        string result = string.Empty;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    result = reader[0].ToString();
                }
                reader.Close();
            }
        }

        return result;
    }



    private string GetLatestBusinessOwnerUsername(string connectionString)
    {
        string query = "SELECT TOP 1 username FROM BusinessOwners ORDER BY OwnerId DESC";
        return GetSingleValueFromDatabase(connectionString, query);
    }

    private string GetLatestBusinessOwnerEmail(string connectionString)
    {
        string query = "SELECT TOP 1 Email FROM BusinessOwners ORDER BY OwnerId DESC";
        return GetSingleValueFromDatabase(connectionString, query);
    }

    private string GetLatestCustomerUsername(string connectionString)
    {
        string query = "SELECT TOP 1 username FROM Customers ORDER BY CustomerId DESC";
        return GetSingleValueFromDatabase(connectionString, query);
    }

    private string GetLatestCustomerEmail(string connectionString)
    {
        string query = "SELECT TOP 1 Email FROM Customers ORDER BY CustomerId DESC";
        return GetSingleValueFromDatabase(connectionString, query);
    }

    private string GetUserType()
    {
        // Replace this with your own logic to retrieve the user type selection
        // You might store it in a session variable, cookie, or any other mechanism
        // Return the user type as a string ("BusinessOwner" or "Customer")
        return "Customer";
    }
}