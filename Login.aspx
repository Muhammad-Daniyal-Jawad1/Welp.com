<%@ Page Language="C#" CodeFile="Login.aspx.cs" Inherits="WebApplication4.Login" %>


<!DOCTYPE html>

<html>
<head>
    <title>Log in</title>
    <link rel="stylesheet" type="text/css" href="Login.css" />
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
                    <li><a href="index.aspx">Home</a></li>
                    <li><a href="#">Reviews</a></li>
                    <li><a href="search.aspx">Business Listings</a></li>
                    <li><a href="signup.aspx">Sign Up</a></li>
                </ul>
            </nav>
        </header>

        <main>
            <section class="login-form">
                <h1>"Log in"</h1>
                <asp:Label ID="errorLabel" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <label>Email:</label>
                <asp:TextBox ID="emailTextBox" CssClass="TextBox" runat="server"></asp:TextBox>
                <br />
                <label>Password:</label>
                <asp:TextBox ID="passwordTextBox" CssClass="TextBox" runat="server" TextMode="Password"></asp:TextBox>
                <br />
                <label>User Type:</label>
                <asp:DropDownList ID="userTypeDropDownList" runat="server" style="display:flex;margin-left: 200px; cursor: pointer; width: 20%; padding: 5px;">
                    <asp:ListItem  Value="customer" style ="text-align: center;">Customer</asp:ListItem>
                    <asp:ListItem Value="business owner">Business Owner</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Button ID="loginButton" CssClass="Button" runat="server" Text="Login" OnClick="LoginButton_Click" />
            </section>
        </main>

        <footer>
            <p>&copy; <%= DateTime.Now.Year %> Welp.com. All rights reserved.</p>
        </footer>
    </form>

</body>
</html>