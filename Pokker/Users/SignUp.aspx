<%@ Page Language="C#" MasterPageFile="~/Pokker.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="Pokker.Pages.SignUp" %>

<asp:Content ID="bodyLogin" ContentPlaceHolderID="body" runat="server">

    <div class="form-std container">
        <div class='col-std'>
            <div class='inp-std'>
                <label for='uname'>Имя:</label>
                <input id='uname' name='uname' type='text' runat='server'/>
            </div>
            <div class='inp-std'>
                <label for='umail'>Емайл:</label>
                <input id='umail' name='umail' type='text' runat='server'/>
            </div>
            <div class='inp-std'>
                <label for='upass'>Пароль:</label>
                <input id='upass' name='upass' type='text' runat='server'/>
            </div>
            <div class='inp-std'>
                <label for='upassr'>Пароль еще раз:</label>
                <input id='upassr' name='upassr' type='text' runat='server'/>
            </div>
            <label class="error" runat="server" id="lblError"></label>
            <input class='btn-std' type='submit' id='btnSignUp' runat='server' value='Зарегистрироваться' onserverclick='btnSignUp_Click'/>
        </div>
    </div>

</asp:Content>
