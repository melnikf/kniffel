<%@ Page Language="C#" MasterPageFile="~/Pokker.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Pokker.Pages.Login" %>

<asp:Content ID="bodyLogin" ContentPlaceHolderID="body" runat="server">

    <script type="text/javascript">
        function NoReg()
        {
            alert('Что-то пошло нетак...');
        }
   </script>

    <div class="form-std container">
        <div class='col-std'>
            <div class='inp-std'>
                <label for='uname'>Имя:</label>
                <input id='uname' name='uname' type='text' runat='server'/>
            </div>
            <div class='inp-std'>
                <label for='upass'>Пароль:</label>
                <input id='upass' name='upass' type='text' runat='server'/>
                <br />
            </div>
            <label class="error" runat="server" id="lblError"></label>
            <input class='btn-std' type='submit' id='btnLogin' runat='server' value='Войти' onserverclick='btnLogin_Click'/>
        </div>
    </div>

</asp:Content>
