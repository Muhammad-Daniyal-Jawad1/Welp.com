<%@ Page Language="C#" CodeFile="search.aspx.cs" Inherits="search" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">


<head runat="server">
    <title>Search and Review Businesses</title>

    <style>
    body{
      background-color: #cccccc;
    background-image: linear-gradient(black 2%, #cccccc);
    background-repeat: repeat-x;
    font-family: Arial, sans-serif;
    margin: 0;
    padding: 0;
    }
    .search-container {
    text-align: center;
    margin-bottom: 20px;
}

    .search-container input[type=text] {
        background-color: rgb(22 22 22);
        color: whitesmoke;
        width: 300px;
        padding: 10px;
        font-size: 16px;
        display: block;
        margin-left: auto;
        margin-right: auto;
        margin-bottom: 10px;
        margin-top: 10px;
        border-radius: 5px;
        border: none;
        box-shadow: 0px 0px 5px 0px rgba(0, 0, 0, 0.2);
    }

    .search-container button {
       display: block;
    background-color: forestgreen;
    color: #fff;
    margin: auto;
    margin-top: 10px;
    margin-bottom: 10px;
    padding: 10px 20px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    }
    .search-container a{
        color: white;
    text-decoration: none;
    border-radius: 10px;
    background-color: rgb(64 158 64 / 0.90);
    padding: 10px;
    display: inline-block;
    }
.business-container {
    display: flex;
    color: whitesmoke;
    flex-wrap: wrap;
    justify-content: center;
}

.business-box {
    display: inline-block;
    background-color: rgb(22 22 22);
    color: whitesmoke;
    border-radius: 30px;
    transition: 0.3s;
    box-shadow: rgb(64 158 64 / 0.50);
    width: 25%;
    height: 40%;
    padding: 50px;
    margin: 50px;
}

    .business-box:hover {
        background-color: rgb(90 220 90);
    }
    .business-box a
    {
        color: whitesmoke;
    }
    header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 20px;
}
    nav ul {
    display: flex;
    list-style: none;
    justify-content:end;
    /*margin-left: 1240px ;*/
    margin-top:5px;
    padding: 0;
}
    nav li {
    margin-right: 20px;
}



nav a {
    color: white;
    text-decoration: none;
    border-radius: 10px;
    background-color: rgb(64 158 64 / 0.90);
    padding: 10px;
    display: inline-block;
}

</style>

</head>
<body>
    <form id="form1" runat="server">
        <header>
        <div class="logo">
                <a href="index.aspx">
                     <img src="welplogo.png" alt="welp.com" style="width:60px;height:60px;">
                </a>
            </div>
             <nav>
                <ul>
                    <li><a href="index.aspx">Logout</a></li>
                </ul>
            </nav>
        </header>
        <h2>Search and Review Businesses</h2>
        <div class="search-container">
            <input type="text" id="searchInput" placeholder="Search businesses..." />
            <!-- Add the dropdown menu for rating filter -->
            <select id="ratingFilter" onchange="searchBusinesses()">
                <option value="">All Ratings</option>
                <option value="1">1 Star</option>
                <option value="2">2 Stars</option>
                <option value="3">3 Stars</option>
                <option value="4">4 Stars</option>
                <option value="5">5 Stars</option>
            </select>
            <button type="button" id="searchButton" onclick="searchBusinesses()">Search</button>
            <a href="Dashboard.aspx">My Dashboard</a>
        </div>
        <div id="businessContainer" runat="server" class="business-container">
            <!-- Businesses will be dynamically added here -->
        </div>
        <div id="topBusinessesContainer" class="top-businesses-container">
            <!-- Top businesses will be dynamically added here -->
        </div>
        <!-- Rest of the code remains unchanged -->
        ...
    </form>


        <script>
            function searchBusinesses() {
                var searchInput = document.getElementById("searchInput").value;
                var ratingFilter = document.getElementById("ratingFilter").value;

                // Make an AJAX request to fetch search results
                var xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function () {
                    if (xhr.readyState === XMLHttpRequest.DONE) {
                        if (xhr.status === 200) {
                            var searchResults = JSON.parse(xhr.responseText);
                            renderSearchResults(searchResults);
                            fetchTopBusinesses();
                        } else {
                            console.error('Error: ' + xhr.status);
                        }
                    }
                };

                // Modify the URL to match your server endpoint for fetching search results
                var url = 'search.aspx?query=' + encodeURIComponent(searchInput) + '&rating=' + encodeURIComponent(ratingFilter);
                xhr.open('GET', url, true);
                xhr.send();
            }



            function fetchTopBusinesses() {
                // Make an AJAX request to fetch top businesses based on rating
                var xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function () {
                    if (xhr.readyState === XMLHttpRequest.DONE) {
                        if (xhr.status === 200) {
                            var topBusinesses = JSON.parse(xhr.responseText);
                            renderTopBusinesses(topBusinesses);
                        } else {
                            console.error('Error: ' + xhr.status);
                        }
                    }
                };
                // Modify the URL to match your server endpoint for fetching top businesses
                var url = 'topBusinesses.aspx';
                xhr.open('GET', url, true);
                xhr.send();
            }



            function renderSearchResults(searchResults) {
                var businessContainer = document.getElementById("businessContainer");

                // Clear existing business boxes only if there are search results
                if (searchResults.length > 0) {
                    businessContainer.innerHTML = "";

                    searchResults.forEach(function (result) {
                        // Create business boxes for the search results
                        var div = document.createElement("div");
                        div.className = "business-box";

                        var businessLink = document.createElement("a");
                        businessLink.href = "BusinessDetails.aspx?businessId=" + result.BusinessId;
                        businessLink.addEventListener("click", function (event) {
                            event.stopPropagation();
                        });

                        var h3 = document.createElement("h3");
                        h3.innerText = result.Name;

                        
                        var p = document.createElement("p");
                        p.innerText = result.Location;

                        businessLink.appendChild(h3);
                        businessLink.appendChild(p);
                        div.appendChild(businessLink);

                        businessContainer.appendChild(div);
                    });
                }
            }

            function renderTopBusinesses(topBusinesses) {
                var topBusinessesContainer = document.getElementById("topBusinessesContainer");

                // Clear existing top business boxes
                topBusinessesContainer.innerHTML = "";

                // Create a section for top businesses
                var section = document.createElement("section");
                section.className = "top-businesses-section";
                var h2 = document.createElement("h2");
                h2.innerText = "Top Businesses";
                section.appendChild(h2);

                // Add business boxes for the top businesses
                topBusinesses.forEach(function (business) {
                    var div = document.createElement("div");
                    div.className = "business-box";

                    var businessLink = document.createElement("a");
                    businessLink.href = "BusinessDetails.aspx?businessId=" + business.BusinessId;
                    businessLink.addEventListener("click", function (event) {
                        event.stopPropagation();
                    });

                    var h3 = document.createElement("h3");
                    h3.innerText = business.Name;

                    var p = document.createElement("p");
                    p.innerText = business.Location;

                    businessLink.appendChild(h3);
                    businessLink.appendChild(p);
                    div.appendChild(businessLink);

                    section.appendChild(div);
                });

                topBusinessesContainer.appendChild(section);
            }



            function openReviewModal(element) {
                var businessId = element.getAttribute("data-business-id");
                var modal = document.getElementById("reviewModal");
                var selectedBusinessId = document.getElementById("selectedBusinessId");
                selectedBusinessId.value = businessId;
                modal.style.display = "block";
            }

            function closeReviewModal() {
                var modal = document.getElementById("reviewModal");
                modal.style.display = "none";
            }

            function submitReview() {
                var businessId = document.getElementById("selectedBusinessId").value;
                var reviewInput = document.getElementById("reviewInput").value;

                

                // Make an AJAX request to submit the review
                var xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function () {
                    if (xhr.readyState === XMLHttpRequest.DONE) {
                        if (xhr.status === 200) {
                            console.log("Review submitted successfully");
                            closeReviewModal();
                        } else {
                            console.error('Error: ' + xhr.status);
                        }
                    }
                };

                // Modify the URL to match your server endpoint for submitting reviews
                var url = 'submitReview.aspx';
                xhr.open('POST', url, true);
                xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
                xhr.send('businessId=' + encodeURIComponent(businessId) + '&review=' + encodeURIComponent(reviewInput));
            }
        </script>
</form>
</body>
</html>
