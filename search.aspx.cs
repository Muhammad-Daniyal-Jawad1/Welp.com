using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

public partial class search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string searchQuery = Request.QueryString["query"];
            string ratingFilter = Request.QueryString["rating"];

            DataTable searchResults = GetBusinesses(searchQuery, ratingFilter);
            RenderSearchResults(searchResults);

            if (!string.IsNullOrEmpty(searchQuery))
            {
                List<Business> businesses = GetBusinessesFromDatabase(searchQuery);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string jsonResults = serializer.Serialize(businesses);
                Response.Write(jsonResults);
                Response.End();
            }
        }
    }

    protected List<Business> GetBusinessesFromDatabase(string searchQuery)
    {
        List<Business> businesses = new List<Business>();
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT BusinessId, Name, Location FROM Businesses WHERE Name LIKE @SearchQuery";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Business business = new Business();
                business.BusinessId = reader["BusinessId"].ToString();
                business.Name = reader["Name"].ToString();
                business.Location = reader["Location"].ToString();
                businesses.Add(business);
            }
        }

        return businesses;
    }

    protected DataTable GetBusinesses(string searchQuery, string ratingFilter)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT TOP 10 * FROM Businesses WHERE Name LIKE '%' + @SearchQuery + '%'";

            if (!string.IsNullOrEmpty(ratingFilter))
            {
                query = "SELECT * FROM Businesses WHERE Rating = @Rating";
            }

            query += " ORDER BY Rating DESC";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");

                if (!string.IsNullOrEmpty(ratingFilter))
                {
                    command.Parameters.AddWithValue("@Rating", ratingFilter);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
    }



    protected void RenderSearchResults(DataTable searchResults)
    {
        businessContainer.InnerHtml = "";

        foreach (DataRow row in searchResults.Rows)
        {
            string businessId = row["BusinessId"].ToString();
            string name = row["Name"].ToString();
            string location = row["Location"].ToString();

            string html = $@"
                <div class='business-box'>
                    <a href='BusinessDetails.aspx?businessId={businessId}' onclick='event.stopPropagation();'>
                        <h3>{name}</h3>
                        <p>{location}</p>
                    </a>
                </div>";

            businessContainer.InnerHtml += html;
        }
    }

    public class Business
    {
        public string BusinessId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
