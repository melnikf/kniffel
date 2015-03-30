<%@ Page Language="C#" MasterPageFile="~/Pokker.Master" AutoEventWireup="true" CodeBehind="Play.aspx.cs" Inherits="Pokker.Play" %>

<asp:Content ID="bodyPlay" ContentPlaceHolderID="body" runat="server">

    <div class="poker-screen container">
        <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>
        <div class="row-std">
            <asp:UpdatePanel ID="updPlay" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txtState" runat="server" ReadOnly="True" TextMode="MultiLine" Height="100%" Width="200px"></asp:TextBox>
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
        <asp:Timer ID="updTimer" runat="server" OnTick="updTimer_Tick" Interval="10000"></asp:Timer>
    </div>

</asp:Content>
