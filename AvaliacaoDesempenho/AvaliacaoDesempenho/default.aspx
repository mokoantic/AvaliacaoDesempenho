<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="AvaliacaoDesempenho._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <meta charset="iso-8859-1" />
	<title>Login</title>

    <link href="/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="/Content/normalize.min.css" rel="stylesheet" />
    <link href="/Content/style.css" rel="stylesheet" />
    <script src="/Scripts/prefixfree.min.js"></script>

</head>
<body>

    <form id="form1" runat="server">
        <div class="login">
            <h1>Login</h1>
            <asp:TextBox ID="TBUser" runat="server" placeholder="Nome de utilizador"></asp:TextBox>
            <asp:TextBox ID="TBPassword" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
            <asp:LinkButton ID="BTLogin" runat="server" class="btn btn-primary btn-block btn-large" type="submit" style="text-decoration:none;" OnClick="BTLogin_Click">Entrar</asp:LinkButton>
            <div id="DivErro" runat="server" style="margin-top:20px;"></div>
        </div>
    </form>

    <script src="/Scripts/index.js"></script>

</body>
</html>
