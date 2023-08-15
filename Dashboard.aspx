<%@ Page Language="C#" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Dashboard</title>
    <style>
      body{
    background-color: rgb(22 22 22);
    font-family: Arial, sans-serif;
    color: whitesmoke;
    margin: 0;
    padding: 0;
    }
      .infocard
      {
        max-width: 600px;
        margin: auto;
        padding: 30px;
        border: 2px solid #409E40;
        border-radius: 25px;
        background-color: rgb(64 158 64 / 0.90);
        box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.2);
      }
     h2, h3{
         font-weight: bold;
     }
     h2{
         text-align:center;
          margin-top: 20px;
     }
     .followlist{
         margin-left: 20px;
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
        <h2>"User Dashboard"</h2>
        <div id="userInfoContainer" class="infocard" runat="server">
            <h3>Welcome, <span id="usernameLabel" runat="server"></span>!</h3>
            <p>Email: <span id="emailLabel" runat="server"></span></p>
        </div>
        
        <h3>"Following":</h3>
        <div id="followingContainer" class="followlist" runat="server">
            <!-- Following businesses will be dynamically added here -->
        </div>
    </form>
</body>
</html>
