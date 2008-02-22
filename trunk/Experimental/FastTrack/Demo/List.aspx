<%@ Page Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="List" %>
<%@ Register TagPrefix="puzzle" Namespace="Puzzle.FastTrack.Framework.Web.Controls" Assembly="Puzzle.FastTrack.Framework.Web" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <puzzle:ListView ID="list" runat="server"></puzzle:ListView>    
    </div>
    </form>
</body>
</html>
