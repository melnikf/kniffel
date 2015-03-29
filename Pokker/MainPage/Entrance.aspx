<%@ Page Language="C#" MasterPageFile="~/Pokker.Master" AutoEventWireup="true" CodeBehind="Entrance.aspx.cs" Inherits="Pokker.MainPage.Entrance" %> 

<asp:Content ID="bodyEntrance" ContentPlaceHolderID="body" runat="server">

    <script type="text/javascript">
        function Reg()
        {
            alert('Вы успешно авторизовались!');
        }
   </script>
    
    <div class="row-content">

        <div class="status-bar">
            <ul>
            <li>
                <asp:Label ID="lbLogin" runat="server" Text="Логин:"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lbEmail" runat="server" Text="Email:"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lbChips" runat="server" Text="Баланс:"></asp:Label>
            </li>
            <li>
                <asp:ListBox ID="lbGames" runat="server"></asp:ListBox>
            </li>
        </ul>
        </div>
        
        <div class="entrance-bar">
             <a class="btn-entrance" runat="server" href="/">Войти в стол</a>
        </div>

    </div>

   

    



</asp:Content>


