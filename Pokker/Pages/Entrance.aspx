<%@ Page Language="C#" MasterPageFile="~/Pokker.Master" AutoEventWireup="true" CodeBehind="Entrance.aspx.cs" Inherits="Pokker.Pages.Entrance" %> 

<asp:Content ID="bodyEntrance" ContentPlaceHolderID="body" runat="server">

    <script type="text/javascript">
        function Reg()
        {
            alert('Вы успешно авторизовались!');
        }
    </script>

    <div class="col-std">
        <div class="status-bar">
            <ul>
                <li><asp:Label ID="lblName" runat="server" Text=""></asp:Label></li>
                <li><asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label></li>
                <li><asp:Label ID="lblCash" runat="server" Text="Кэш:"></asp:Label></li>
                <li><asp:Table ID="tblGames" runat="server" CssClass="tbl-stats" Width="100%"></asp:Table></li>
            </ul>
        </div>
        <div>
            <input class='btn-std' type='submit' id='btnCash' runat='server' value='Добавить деньжат' onserverclick='btnCash_Click'/>
            <label class="error" runat="server" id="lblError"></label>
            <input class='btn-std' type='submit' id='btnJoin' runat='server' value='Сесть за стол' onserverclick='btnJoin_Click'/>
        </div>
    </div>

</asp:Content>


