<%@ Page Language="C#" Inherits="MuonLab.Web.Mvc.Xhtml.View<User>" %>
<%@ Import Namespace="MuonLab.Web.Mvc.Xhtml"%>
<%@ Import Namespace="MuonKit.Examples.Domain.Entities"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <div>
    
		<% using(Html.AntiForgeryForm()){ %>
		
			<%= TextBoxFor(u => u.Name) %><br />
			
			<%= TextBoxFor(u => u.Email) %>
		
			<input type="submit" value="Post" />
		
		<% } %>
    
    </div>
</body>
</html>
