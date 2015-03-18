<%@ Page Language="C#" MasterPageFile="~/Pokker.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Pokker.Users.Login" %>

<asp:Content ID="bodyLogin" ContentPlaceHolderID="body" runat="server">

    <div class="form-std container">
        <h1>Вход</h1>
        <div class='col-std'>
            <div class='inp-std'>
                <label for='uname'>Имя:</label>
                <input id='uname' name='uname' type='text'/></div>
            <div class='inp-std'>
                <label for='upass'>Пароль:</label>
                <input id='upass' name='upass' type='text'/>
            </div>
        </div>
    </div>

</asp:Content>
