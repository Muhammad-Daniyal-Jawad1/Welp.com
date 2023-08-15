using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using static BusinessDetails;

public partial class BusinessDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string businessId = Request.QueryString["businessId"];
            if (!string.IsNullOrEmpty(businessId))
            {
                Business business = GetBusinessFromDatabase(businessId);
                if (business != null)
                {
                    DisplayBusinessDetails(business);
                    LoadReviews(businessId);
                }
            }
        }
    }


    protected void LoadReviews(string businessId)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT Text, Rating FROM Reviews WHERE BusinessId = @BusinessId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BusinessId", businessId);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Review> reviews = new List<Review>(); // Create a list to store the reviews
            while (reader.Read())
            {
                string reviewText = reader["Text"].ToString();
                int rating = Convert.ToInt32(reader["Rating"]);
                Review review = new Review(reviewText, rating); // Create a Review object
                reviews.Add(review); // Add the review to the list
            }

            // Display all the reviews
            var businessDetailsDiv = (System.Web.UI.HtmlControls.HtmlGenericControl)FindControl("businessDetails");
            foreach (Review review in reviews)
            {
                var reviewParagraph = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
                reviewParagraph.InnerText = "Review: " + review.Text + " (Rating: " + review.Rating + ")";
                businessDetailsDiv.Controls.Add(reviewParagraph);
            }
        }
    }


    protected Business GetBusinessFromDatabase(string businessId)
    {
        Business business = null;
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT Name, Location, About, Contact, Rating, Speed, Quality, Satisfaction FROM Businesses WHERE BusinessId = @BusinessId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BusinessId", businessId);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                business = new Business();
                business.Name = reader["Name"].ToString();
                business.Location = reader["Location"].ToString();
                business.Description = reader["About"].ToString();
                business.Contact = reader["Contact"].ToString();
                business.Rating = Convert.ToInt32(reader["Rating"]);
                business.Speed = Convert.ToInt32(reader["Speed"]);
                business.Quality = Convert.ToInt32(reader["Quality"]);
                business.Satisfaction = Convert.ToInt32(reader["Satisfaction"]);
            }
        }
        return business;
    }

    protected void DisplayBusinessDetails(Business business)
    {
        var businessDetailsDiv = (System.Web.UI.HtmlControls.HtmlGenericControl)FindControl("businessDetails");

        // Create HTML elements to display the business details
        var nameHeader = new System.Web.UI.HtmlControls.HtmlGenericControl("h3");
        nameHeader.InnerText = "Name: " + business.Name;

        var locationParagraph = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
        locationParagraph.InnerText = "Location: " + business.Location;

        var descriptionParagraph = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
        descriptionParagraph.InnerText = "Description: " + business.Description;

        var contactParagraph = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
        contactParagraph.InnerText = "Contact: " + business.Contact;

        var ratingParagraph = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
        ratingParagraph.InnerText = "Rating: " + business.Rating;

        var speedParagraph = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
        speedParagraph.InnerText = "Speed: " + business.Speed;

        var qualityParagraph = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
        qualityParagraph.InnerText = "Quality: " + business.Quality;

        var satisfactionParagraph = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
        satisfactionParagraph.InnerText = "Satisfaction: " + business.Satisfaction;

        // Add the business details to the businessDetailsDiv
        businessDetailsDiv.Controls.Add(nameHeader);
        businessDetailsDiv.Controls.Add(locationParagraph);
        businessDetailsDiv.Controls.Add(descriptionParagraph);
        businessDetailsDiv.Controls.Add(contactParagraph);
        businessDetailsDiv.Controls.Add(ratingParagraph);
        businessDetailsDiv.Controls.Add(speedParagraph);
        businessDetailsDiv.Controls.Add(qualityParagraph);
        businessDetailsDiv.Controls.Add(satisfactionParagraph);
    }

 

    protected int GetNewReviewId()
    {
        // You can implement your logic here to generate a new unique review ID
        // This is just a placeholder
        return 1;
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        string reviewText = reviewTextBox.Value.Trim();
        int rating = int.Parse(ratingDropDownList.SelectedValue);

        if (!string.IsNullOrEmpty(reviewText))
        {
            // Save the review to the database
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Retrieve the latest CustomerId from the Customers table
                int customerId;
                using (SqlCommand customerIdCommand = new SqlCommand("SELECT TOP 1 CustomerId FROM Customers ORDER BY CustomerId DESC", connection))
                {
                    connection.Open();
                    object result = customerIdCommand.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int latestCustomerId))
                    {
                        customerId = latestCustomerId;
                    }
                    else
                    {
                        // Handle the case when no customers exist
                        // Set a default or display an error message
                        return;
                    }
                }

                // Insert the review into the Reviews table
                string query = "INSERT INTO Reviews (Text, BusinessId, CustomerId, Rating) VALUES (@Text, @BusinessId, @CustomerId, @Rating)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Text", reviewText);
                //command.Parameters.AddWithValue("@Id", GetNewReviewId());
                command.Parameters.AddWithValue("@BusinessId", Request.QueryString["businessId"]);
                command.Parameters.AddWithValue("@CustomerId", customerId);
                command.Parameters.AddWithValue("@Rating", rating);
                command.ExecuteNonQuery();
            }

            // Add the new review dynamically to the page
            var reviewParagraph = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
            reviewParagraph.InnerText = "Review: " + reviewText + " (Rating: " + rating + ")";

            var businessDetailsDiv = (System.Web.UI.HtmlControls.HtmlGenericControl)FindControl("businessDetails");
            businessDetailsDiv.Controls.Add(reviewParagraph);
        }
    }




    protected void FollowButton_Click(object sender, EventArgs e)
    {
        string businessId = Request.QueryString["businessId"];
        string username = GetLoggedInUsername(); // Replace this with your own logic to retrieve the logged-in username
        string email = GetLoggedInEmail();
        string id = GetLoggedInId();
        // Replace this with your own logic to retrieve the logged-in email

        // Save the follow information to the database
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Following (username, Email, BusinessId, CustomerId) VALUES (@Username, @Email, @BusinessId, @CustomerId)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@BusinessId", businessId);
            command.Parameters.AddWithValue("@CustomerId", id);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                // Display a success message
                followStatusLabel.Text = "You are now following this business.";
                followStatusLabel.Visible = true;
            }
            else
            {
                // Display an error message
                followStatusLabel.Text = "Failed to follow this business.";
                followStatusLabel.Visible = true;
            }
        }
    }


    protected void DonateButton_Click(object sender, EventArgs e)
    {
        // Retrieve the donation amount from the text box
        string donationAmount = donationAmountTextBox.Text;

        // Perform validation or additional processing on the donation amount if needed

        // Process the donation (e.g., interact with payment gateway or business's payment system)
        bool donationSuccessful = ProcessDonation(donationAmount);

        if (donationSuccessful)
        {
            // Donation was successful, provide appropriate feedback to the user
            // For example, you can display a success message or redirect to a confirmation page
            Response.Write("Donation Successful");
        }
        else
        {
            // Donation failed, provide appropriate feedback to the user
            // For example, you can display an error message or redirect to an error page
            Response.Write("Donation Failed");
        }
    }

    private bool ProcessDonation(string donationAmount)
    {
        // Perform necessary operations to process the donation
        // You can interact with a payment gateway or business's payment system here

        // Placeholder logic: Assume the donation is successful if the donation amount is a positive number
        decimal amount;
        bool isNumeric = decimal.TryParse(donationAmount, out amount);
        if (isNumeric && amount > 0)
        {
            // Perform actual donation processing and return the result
            return true;
        }

        return false; // Donation failed
    }





    protected string GetLoggedInEmail()
    {
        // Replace "YourConnectionString" with your actual connection string
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT TOP 1 Email FROM Customers ORDER BY CustomerId DESC";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            object result = command.ExecuteScalar();
            if (result != null && !string.IsNullOrEmpty(result.ToString()))
            {
                return result.ToString();
            }
        }
        return string.Empty;
    }



    protected string GetLoggedInId()
    {
        // Replace "YourConnectionString" with your actual connection string
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT TOP 1 CustomerId FROM Customers ORDER BY CustomerId DESC";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            object result = command.ExecuteScalar();
            if (result != null && !string.IsNullOrEmpty(result.ToString()))
            {
                return result.ToString();
            }
        }
        return string.Empty;
    }

    protected string GetLoggedInUsername()
    {
        // Replace "YourConnectionString" with your actual connection string
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT TOP 1 username FROM Customers ORDER BY CustomerId DESC";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            object result = command.ExecuteScalar();
            if (result != null && !string.IsNullOrEmpty(result.ToString()))
            {
                return result.ToString();
            }
        }
        return string.Empty;
    }





    public class Business
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Contact { get; set; }
        public int Rating { get; set; }
        public int Speed { get; set; }
        public int Quality { get; set; }
        public int Satisfaction { get; set; }
    }


    public class Review
    {
        public string Text { get; set; }
        public int Rating { get; set; }

        public Review(string text, int rating)
        {
            Text = text;
            Rating = rating;
        }
    }
}
