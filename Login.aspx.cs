using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace WebApplication4
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            errorLabel.Text = string.Empty; // Clear any previous error messages
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string email = emailTextBox.Text.Trim();
            string password = passwordTextBox.Text;
            string userType = userTypeDropDownList.SelectedValue;

            bool isValid = false;

            if (userType == "customer")
            {
                isValid = ValidateCustomerCredentials(email, password);
            }
            else if (userType == "business owner")
            {
                isValid = ValidateBusinessOwnerCredentials(email, password);
            }

            if (isValid)
            {
                // Redirect to the appropriate page based on user type
                if (userType == "customer")
                {
                    Response.Redirect("search.aspx");
                }
                else if (userType == "business owner")
                {
                    Response.Redirect("search.aspx");
                }
            }
            else
            {
                errorLabel.Text = "Invalid credentials. Please try again.";
            }
        }

        protected bool ValidateCustomerCredentials(string email, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Customers WHERE Email = @Email AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        protected bool ValidateBusinessOwnerCredentials(string email, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM BusinessOwners WHERE Email = @Email AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
