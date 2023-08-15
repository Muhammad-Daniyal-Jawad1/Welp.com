<%@ Page Language="C#" CodeFile="signup.aspx.cs" Inherits="signup" %>


<!DOCTYPE html>

<html>
<head>
    <title>Sign Up - Welp.com</title>
    <link rel="stylesheet" type="text/css" href="signup.css" />
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
                    <li>
                        <a href="index.aspx">Home</a>
                    </li>
                    <li><a href="#">Reviews</a></li>
                    <li><a href="search.aspx">Business Listings</a></li>
                    <li><a href="Login.aspx">Log In</a></li>
                </ul>
            </nav>
        </header>
        <main>
            <section class="signup">
                <h1>"Sign Up"</h1>
                <label for="username">Username:</label>
                <asp:TextBox ID="usernameTextBox" CssClass="TextBox" runat="server" required />

                <label for="email">Email:</label>
                <asp:TextBox ID="emailTextBox" CssClass="TextBox" runat="server" required />

                <label for="password">Password:</label>
                <asp:TextBox ID="passwordTextBox" CssClass="TextBox" runat="server" TextMode="Password" required />

                <label for="confirm-password">Confirm Password:</label>
                <asp:TextBox ID="confirmpasswordTextBox" CssClass="TextBox" runat="server" TextMode="Password" required />
                <br />
                <asp:Button ID="btnSignUp" CssClass="Button" runat="server" Text="Sign Up" OnClick="btnSignUp_Click" />

                <asp:Label ID="lblErrorMessage" runat="server" CssClass="error-message" Visible="false"></asp:Label>
            </section>
        </main>
        <footer>
            <p>&copy; <%= DateTime.Now.Year %> Welp.com. All rights reserved.</p>
        </footer>
    </form>
</body>
</html>
