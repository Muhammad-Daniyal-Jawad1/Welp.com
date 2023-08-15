<%@ Page Language="C#" CodeFile="BusinessDetails.aspx.cs" Inherits="BusinessDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Business Details</title>
    <style>
        body{
      background-color: #cccccc;
    background-image: linear-gradient(black 2%, #cccccc);
    background-repeat: repeat-x;
    font-family: Arial, sans-serif;
    color: whitesmoke;
    margin: 0;
    padding: 0;
    }
        .Button{
            display: block;
    background-color: forestgreen;
    color: #fff;
    margin-left:20px;
    margin-top: 10px;
    margin-bottom: 10px;
    padding: 10px 20px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
        }
        h2{
            text-align: center;
            font-weight: bold;
        }
    .business-details{
           max-width: 600px;
    margin: auto;
    padding: 30px;
    border: 2px solid #409E40;
    border-radius: 25px;
    background-color: var(--wowalagreen);
    box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.2);
    }
    .review-form textarea{
        background-color: #cccccc;
        border-color: forestgreen;
        color:black;
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
        </header>
        <h2>"Business Details"</h2>
        <div id="businessDetails" runat="server" class="business-details">
            <!-- Business details will be dynamically added here -->

            <h3>Add a Review</h3>
            <div class="review-form">
                <textarea id="reviewTextBox"  runat="server" rows="4" cols="50" placeholder="Enter your review"></textarea>
                <br />
                Rating:
                <asp:DropDownList ID="ratingDropDownList" runat="server">
                    <asp:ListItem Text="1" Value="1" />
                    <asp:ListItem Text="2" Value="2" />
                    <asp:ListItem Text="3" Value="3" />
                    <asp:ListItem Text="4" Value="4" />
                    <asp:ListItem Text="5" Value="5" />
                </asp:DropDownList>
                <br />
                <p>This business is <b>Promoted</b></p>
                <asp:Button ID="submitButton" CssClass="Button" runat="server" Text="Submit" OnClick="SubmitButton_Click" />
            </div>

            <h3>Make a Donation</h3>
            <div class="donation-form">
                <asp:Label ID="donationAmountLabel" runat="server" Text="Donation Amount:"></asp:Label>
                <asp:TextBox ID="donationAmountTextBox" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="donateButton" CssClass="Button" runat="server" Text="Donate" OnClick="DonateButton_Click" />
            </div>

            <asp:Button ID="followButton" CssClass="Button" runat="server" Text="Follow this business" OnClick="FollowButton_Click" Visible="true" />
            <asp:Label ID="followStatusLabel" runat="server" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>
