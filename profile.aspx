<%@ Page Language="C#" CodeFile="profile.aspx.cs" Inherits="profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Enter Details - Welp.com</title>
    <link rel="stylesheet" type="text/css" href="profile.css" />

</head>
<body>
    <form id="form1" runat="server">
        <header>
               <div class="logo" style="align-items:center;">
                <a>
                     <img src="welplogo.png" alt="welp.com" style="width:60px;height:60px;">
                </a>
            </div>
            <nav>
            
            </nav>
        </header>
        <main>
            <section>
                <h2>"Sign Up Details"</h2>
                <label for="userType">User Type:</label>
                <div class="Drpdn">
                <asp:DropDownList ID="userTypeDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="userTypeDropDown_SelectedIndexChanged">
                    <asp:ListItem Text="Select User Type" CssClass ="options" Value="" />
                    <asp:ListItem Text="Customer" CssClass ="options" Value="customer" />
                    <asp:ListItem Text="Business Owner" CssClass ="options" Value="businessOwner" />
                </asp:DropDownList>
                </div>
                <asp:Panel ID="customerPanel" runat="server" Visible="false">
                    <h3>"Customer Details"</h3>
                    <label style="color: black;font-weight:bold;">Preferences:</label>
                    <br />
                <div class = "checkboxes">
                    <asp:CheckBox ID="fashionCheckBox" runat="server" Text="Fashion" />
                    <br />
                    <asp:CheckBox ID="barberCheckBox" runat="server" Text="Barber" />
                    <br />
                    <asp:CheckBox ID="restaurantCheckBox" runat="server" Text="Restaurant" />
                    <br />
                    <asp:CheckBox ID="otherscheckbox" runat="server" Text="Others" />
                    <br />
                    <!-- Add more checkboxes as needed -->
                 </div>
                    <asp:Button ID="customerSubmitButton" CssClass = "Button" runat="server" Text="Submit" OnClick="customerSubmitButton_Click" />
                </asp:Panel>

                <asp:Panel ID="businessOwnerPanel" runat="server" Visible="false">
                    <h3>"Business Owner Details"</h3>
                    <label>Business:</label>
                    <asp:TextBox ID="businessTextBox" CssClass="TextBox" runat="server"></asp:TextBox>
                    <br />
                    <label>Location:</label>
                    <asp:TextBox ID="locationTextBox" CssClass="TextBox" runat="server"></asp:TextBox>
                    <br />
                    <label>About:</label>
                    <asp:TextBox ID="aboutTextBox" CssClass="TextBox" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <br />
                    <label>Contact:</label>
                    <asp:TextBox ID="contactTextBox" CssClass="TextBox" runat="server"></asp:TextBox>
                    <br />
                    <asp:Button ID="businessOwnerSubmitButton" CssClass = "Button" runat="server" Text="Submit" OnClick="businessOwnerSubmitButton_Click" />
                </asp:Panel>
            </section>
        </main>
       <%-- <footer>
            <p>&copy; <%= DateTime.Now.Year %> Welp.com. All rights reserved.</p>
        </footer>--%>
    </form>
    
</body>
</html>