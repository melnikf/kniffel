<%@ Page Language="C#" MasterPageFile="~/Pokker.Master" AutoEventWireup="true" CodeBehind="Entrance.aspx.cs" Inherits="Pokker.Pages.Entrance" %> 

<asp:Content ID="bodyEntrance" ContentPlaceHolderID="body" runat="server">
    <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>
    <asp:Timer ID="updTimer" runat="server" OnTick="updTimer_Tick" Interval="10000"></asp:Timer>
    <div class="row-std">
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
        <div class="col-std poker-cards">
            <div class="poker-cards-list">
                <img id="imgCard1" src="/Content/Cards/0.png" alt="board_1" runat='server'/>
                <img id="imgCard2" src="/Content/Cards/0.png" alt="board_2" runat='server'/>
                <img id="imgCard3" src="/Content/Cards/0.png" alt="board_3" runat='server'/>
                <img id="imgCard4" src="/Content/Cards/0.png" alt="board_4" runat='server'/>
                <img id="imgCard5" src="/Content/Cards/0.png" alt="board_5" runat='server'/>
            </div>
            <div class="poker-cards-list">
                <img id="imgCard6" src="/Content/Cards/0.png" alt="hand_1" runat='server'/>
                <img id="imgCard7" src="/Content/Cards/0.png" alt="hand_2" runat='server'/>
            </div>
        </div>
        <div class="col-std poker-screen container">
            <asp:UpdatePanel ID="updPlay" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txtState" runat="server" ReadOnly="True" TextMode="MultiLine" Rows="20" CssClass="poker-players"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="col-std">
                <div class="inp-std">
                    <label for="bet">Ставка:</label>
                    <input id="inpBet" name="bet" type="text" runat="server"/>
                </div>
                <input class="btn-std" type="submit" id="btnBet" runat="server" value="Принять ставку" onserverclick="btnBet_Click"/>
                <input class="btn-std" type="submit" id="btnFold" runat="server" value="Пас" onserverclick="btnFold_Click"/>
            </div>
        </div>
    </div>
    

</asp:Content>


