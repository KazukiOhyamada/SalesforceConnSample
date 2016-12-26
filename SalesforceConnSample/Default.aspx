<%@ Page Language="C#" Inherits="SalesforceConnSample.Default" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Default</title>
</head>
<body>
	<form id="form1" runat="server">
			<asp:Label id="label1" Text="text1" runat="server" />
			<asp:TextBox id="text1" Text="text1" runat="server" />
			<br />
			<asp:Label id="label2" Text="button1" runat="server" />
			<asp:Button id="button1" runat="server" Text="Click me!" OnClick="button1Clicked" />
	</form>
</body>
</html>
