using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class profile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClearForm();
        }
    }

    protected void userTypeDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearForm();

        if (userTypeDropDown.SelectedValue == "customer")
        {
            customerPanel.Visible = true;
            businessOwnerPanel.Visible = false;
        }
        else if (userTypeDropDown.SelectedValue == "businessOwner")
        {
            businessOwnerPanel.Visible = true;
            customerPanel.Visible = false;
        }
    }


    protected void customerSubmitButton_Click(object sender, EventArgs e)
    {
        // Get the email and password from the latest record in the SignUpDetails table
        string username = string.Empty;
        string email = string.Empty;
        string password = string.Empty;
        GetLatestSignUpDetails(out username, out email, out password);

        // Get the selected preferences
        string preferences = GetSelectedPreferences();

        // Save the data into the Customer table
        SaveCustomerData(username,email, password, preferences);

        // Redirect or show success message
        Response.Redirect("search.aspx");
    }

    protected void businessOwnerSubmitButton_Click(object sender, EventArgs e)
    {
        string username = string.Empty;
        string email = string.Empty;
        string password = string.Empty;
        GetLatestSignUpDetails(out username,out email, out password);
        string business = businessTextBox.Text;
        string location = locationTextBox.Text;
        string about = aboutTextBox.Text;
        string contact = contactTextBox.Text;

        SaveBusinessOwnerData(username,email, password, business, location, about, contact);

        // Redirect or show success message
        Response.Redirect("search.aspx");
    }

    private void GetLatestSignUpDetails(out string username,out string email, out string password)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        string sql = "SELECT TOP 1 username,email, password FROM SignUpDetails ORDER BY signupid DESC";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        username = reader["username"].ToString();
                        email = reader["email"].ToString();
                        password = reader["password"].ToString();
                    }
                    else
                    {
                        // Handle if no record is found
                        username=string.Empty;
                        email = string.Empty;
                        password = string.Empty;
                    }
                }
            }

            connection.Close();
        }
    }

    private string GetSelectedPreferences()
    {
        string preferences = "";

        if (fashionCheckBox.Checked)
        {
            preferences += "Fashion,";
        }
        if (barberCheckBox.Checked)
        {
            preferences += "Barber,";
        }
        if (restaurantCheckBox.Checked)
        {
            preferences += "Restaurant,";
        }

        // Remove the trailing comma if any
        if (!string.IsNullOrEmpty(preferences))
        {
            preferences = preferences.TrimEnd(',');
        }

        return preferences;
    }

    private void SaveCustomerData(string username,string email, string password, string preferences)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sql = "INSERT INTO Customers (username,email, password, preferences) VALUES (@Username,@Email, @Password, @Preferences)";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Preferences", preferences);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }


    private void SaveBusinessOwnerData(string username, string email, string password, string business, string location, string about, string contact)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Insert the business owner record and retrieve the generated OwnerId
            string sqlBusinessOwner = "INSERT INTO BusinessOwners (username, email, password) OUTPUT INSERTED.OwnerId VALUES (@Username, @Email, @Password)";
            int ownerId;

            using (SqlCommand commandBusinessOwner = new SqlCommand(sqlBusinessOwner, connection))
            {
                commandBusinessOwner.Parameters.AddWithValue("@Username", username);
                commandBusinessOwner.Parameters.AddWithValue("@Email", email);
                commandBusinessOwner.Parameters.AddWithValue("@Password", password);

                // Execute the query and retrieve the OwnerId
                ownerId = (int)commandBusinessOwner.ExecuteScalar();
            }

            // Insert the business record with the retrieved OwnerId
            string sqlBusiness = "INSERT INTO Businesses (OwnerId, Name, Location, About, Contact) VALUES (@OwnerId, @Name, @Location, @About, @Contact)";

            using (SqlCommand commandBusiness = new SqlCommand(sqlBusiness, connection))
            {
                commandBusiness.Parameters.AddWithValue("@OwnerId", ownerId);
                commandBusiness.Parameters.AddWithValue("@Name", business);
                commandBusiness.Parameters.AddWithValue("@Location", location);
                commandBusiness.Parameters.AddWithValue("@About", about);
                commandBusiness.Parameters.AddWithValue("@Contact", contact);

                commandBusiness.ExecuteNonQuery();
            }

            connection.Close();
        }
    }



    private void ClearForm()
    {
        
        businessTextBox.Text = string.Empty;
        locationTextBox.Text = string.Empty;
        aboutTextBox.Text = string.Empty;
        contactTextBox.Text = string.Empty;

        fashionCheckBox.Checked = false;
        barberCheckBox.Checked = false;
        restaurantCheckBox.Checked = false;
    }
}
