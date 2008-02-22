<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditClass.aspx.cs" Inherits="EditClass" %>

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
                    Edit Class
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
                    The name of your class
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    Properties
                </td>
                <td align="left" valign="top" colspan="2">
                
                    <asp:Panel id="propertiesPanel" runat="server" Visible="true"></asp:Panel> 
                    
                    <table>
                        <tr>
                            <td align="left" valign="top">
                                Add
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList id="addTypeDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="addTypeDropDownList_SelectedIndexChanged" >
                                    <asp:ListItem Text="Property" Value="Property" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Relationship" Value="Relationship"></asp:ListItem>
                                </asp:DropDownList>                                
                            </td>
                            <td align="left" valign="top">
                                Add a primitive property or a relationship.
                            </td>
                        </tr>                        
                        <tr>
                            <td align="left" valign="top">
                                Name
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="propertyNameTextBox" runat="server"></asp:TextBox>                                                            
                            </td>
                            <td align="left" valign="top">
                                The name of your new property
                            </td>
                        </tr>

                        <asp:Panel ID="propertyPanel" runat="server">

                        <tr>
                            <td align="left" valign="top">
                                Type
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList id="propertyTypeDropDownList" runat="server"></asp:DropDownList>
                            </td>
                            <td align="left" valign="top">
                                The type of your new property
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                Length
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="stringLengthTextBox" runat="server"></asp:TextBox>                                                            
                            </td>
                            <td align="left" valign="top">
                                The number of characters in a System.String property. Empty resolves to the default of 255.
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                Nullable
                            </td>
                            <td align="left" valign="top">
                                <asp:CheckBox ID="nullableCheckBox" runat="server" Checked="true" />
                            </td>
                            <td align="left" valign="top">
                                Select if you want your new property to be nullable.
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                Column
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="columnNameTextBox" runat="server"></asp:TextBox>                                                            
                            </td>
                            <td align="left" valign="top">
                                The name of the new column for your property
                            </td>
                        </tr>                        

                        <tr>
                            <td align="left" valign="top" colspan="3">
                                <asp:Button id="addPropertyButton" runat="server" Text="Add Value Property" OnClick="addPropertyButton_Click" />                                
                            </td>
                        </tr>

                        </asp:Panel>

                        <asp:Panel id="relationshipPanel" runat="server" Visible="false">

                        <tr>
                            <td align="left" valign="top">
                                Type
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList id="relationshipTypeDropDownList" runat="server"></asp:DropDownList>
                            </td>
                            <td align="left" valign="top">
                                The type of your new relatinship property
                            </td>
                        </tr>
                        
                        <tr>
                            <td align="left" valign="top">
                                List
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList id="relationshipMultiplicityDropDownList" runat="server"></asp:DropDownList>
                            </td>
                            <td align="left" valign="top">
                                Select the type of relationship you want.
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                Inverse name
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="inverseNameTextBox" runat="server"></asp:TextBox>                                                            
                            </td>
                            <td align="left" valign="top">
                                The name of the new inverse property
                            </td>
                        </tr>
                        
                        <tr>
                            <td align="left" valign="top" colspan="3">
                                <asp:Button id="addRelationshipButton" runat="server" Text="Add Relationship" OnClick="addRelationshipButton_Click" />                                
                            </td>
                        </tr>

                        </asp:Panel>
                        
                    </table>
                                        
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Button id="updateButton" runat="server" Text="Update Class" OnClick="updateButton_Click" />
                </td>
            </tr>            
        </table>    
    </div>
    </form>
</body>
</html>
