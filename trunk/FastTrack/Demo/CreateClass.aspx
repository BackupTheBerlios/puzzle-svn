<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateClass.aspx.cs" Inherits="CreateClass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td colspan="3">
                    Create Class
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    Name
                </td>
                <td align="left" valign="top">
                    <asp:TextBox ID="nameTextBox" runat="server"></asp:TextBox>
                </td>
                <td align="left" valign="top">
                    The name of your new class
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    Table
                </td>
                <td align="left" valign="top">
                    <asp:TextBox ID="tableTextBox" runat="server"></asp:TextBox>
                </td>
                <td align="left" valign="top">
                    The name of the new table your class will be mapping to (leave blank to use class name)
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Button id="createButton" runat="server" Text="Create Class" OnClick="createButton_Click"/>
                </td>
            </tr>            
        </table>
    </div>
    </form>
</body>
</html>
