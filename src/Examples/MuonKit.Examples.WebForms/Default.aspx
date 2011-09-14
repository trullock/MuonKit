<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MuonKit.Examples.WebForms.Default" %>
<%@ Import Namespace="MuonKit.Examples.Domain.Entities"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<h1>Users:</h1>
		<asp:Repeater runat="server" ID="rptUsers">
			<HeaderTemplate>
				<ul>
			</HeaderTemplate>
			<ItemTemplate>
				<li><%# (Container.DataItem as User).Name %></li>
			</ItemTemplate>
			<FooterTemplate>
				</ul>
			</FooterTemplate>
		</asp:Repeater>
		<br />
		<h1>Add Person</h1>
		
		Name: <asp:TextBox runat="server" id="txtName" /><br />
		<asp:Button runat="server" ID="btnAdd" Text="Add person" />
    </div>
    </form>
</body>
</html>
