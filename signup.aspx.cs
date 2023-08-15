using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class signup : System.Web.UI.Page
{
    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        // Retrieve form data
        string username = usernameTextBox.Text;
        string email = emailTextBox.Text;
        string password = passwordTextBox.Text;
        string confirmpassword = confirmpasswordTextBox.Text;

        // Establish a connection to the database
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Construct the SQL command
            string sql = "INSERT INTO SignUpDetails (username, email, password, confirmpassword) VALUES (@Username, @Email, @Password, @ConfirmPassword)";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                // Set parameter values
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@ConfirmPassword", confirmpassword);

                // Execute the command
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        // Redirect or show success message
        Response.Redirect("profile.aspx");
    }
}
