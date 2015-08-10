<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="CacheMonitorLab.Demo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Button ID="btnAdd" runat="server" Text="Add Cache" OnClick="btnAdd_Click" />
            &nbsp;<asp:Button ID="btnNotify" runat="server" Text="Notify Monitor" OnClick="btnNotify_Click" />
        &nbsp;<asp:Button ID="btnShow" runat="server" Text="Show Cache" OnClick="btnShow_Click" />
            <br />
            <asp:Label ID="lblCaches" runat="server" />
            <br />
        </div>
    </form>
</body>
</html>
