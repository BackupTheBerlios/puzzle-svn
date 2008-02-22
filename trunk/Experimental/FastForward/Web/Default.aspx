<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Puzzle.FastForward.Web._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Name: <asp:TextBox ID="nameTextBox" runat="server"></asp:TextBox>
        <br />
        <asp:Button id="CreateClassButton" runat="Server"/>
        <asp:Repeater ID="testRepeater" runat="server">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "Id") %><br />
            </ItemTemplate>        
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
