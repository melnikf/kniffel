using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pokker.Models;
using System.Web.Security;
using Pokker.Backend;

namespace Pokker.Pages
{
    public partial class Entrance : System.Web.UI.Page
    {
        PokerTable ptable;
        string name;

        int gameId = -1;

        private string GetUserName()
        {
            HttpCookie authCookie = Request.Cookies[".ASPXAUTH"];
            return FormsAuthentication.Decrypt(authCookie.Value).Name;
        }

        private void UpdateState()
        {
            string state = "";
            int i;

            state += this.GetRoundName(ptable.Round) + "\n";
            state += "Игроки: \n";
            for (i = 0; i < ptable.PlayersInGame; i++)
            {
                if (i == ptable.ActivePlayer)
                    state += "@   ";
                else
                    state += "    ";

                state += ptable.GetPlayerName(i) + " ";
                state += "Ставка: " + ptable.GetPlayerBet(i) + "\n";
            }
            state += "Открытые карты: \n";
            state += this.GetCards(ptable.ShowBoard()) + "\n";
            if (gameId >= 0)
            {
                state += "Рука: \n";
                state += this.GetCards(ptable.ShowHand((uint)gameId)) + "\n";
            }

            txtState.Text = state;
        }

        private string GetCards(Card[] cards)
        {
            int i;
            string res;

            res = "";
            for (i = 0; i < cards.Length; i++)
            {
                res += GetCard(cards[i]) + "; ";
            }
            return res;
        }

        private string GetCard(Card card)
        {
            string res = "";

            switch (card.weight)
            {
                case 0: res += "Двойка"; break;
                case 1: res += "Тройка"; break;
                case 2: res += "Четверка"; break;
                case 3: res += "Пятерка"; break;
                case 4: res += "Шестерка"; break;
                case 5: res += "Семерка"; break;
                case 6: res += "Восьмерка"; break;
                case 7: res += "Девятка"; break;
                case 8: res += "Десятка"; break;
                case 9: res += "Валет"; break;
                case 10: res += "Дама"; break;
                case 11: res += "Король"; break;
                case 12: res += "Туз"; break;
            }

            res += " ";

            switch (card.suit)
            {
                case 0: res += "Трефы"; break;
                case 1: res += "Пики"; break;
                case 2: res += "Червы"; break;
                case 3: res += "Бубны"; break;
            }

            return res;
        }

        private string GetRoundName(int n)
        {
            switch (ptable.Round)
            {
                case 0:
                    return "Игра скоро начнется";
                case 1:
                    return "Пре-флоп";
                case 2:
                    return "Флоп";
                case 3:
                    return "Терн";
                case 4:
                    return "Ривер";
                case 5:
                    return "Шоудаун";
                default:
                    return "Ждем игроков";
            }
        }

        private void AddCash(uint amount)
        {
            using (PokkerDbContext ctx = new PokkerDbContext())
            {
                var player = ctx.Players.FirstOrDefault(p => p.Name == this.name);
                player.Cash += (int)amount;
                ctx.SaveChanges();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Забираем свое имя из кукисов, оно нам нужно для соединения со столом.
            if(name == null)
                name = this.GetUserName();

            // Получаем ссылку на стол, если еще нет.
            if (ptable == null)
            {
                ptable = PokerTableProvider.GetTable();
                ptable.Updated += ptable_Updated;
            }

            // Получаем ид. если еще не сидим за столом.
            if (gameId == -1)
            {
                gameId = ptable.MyId(name);
            }

            // Если ид. не получили, то кнопки не будут работать, но состояние продолжит обновляться.


            // Загружаем статистику из базы.
            lblName.Text = name;
            lblEmail.Text = EntranceUtils.TakeEmail(name);
            lblCash.Text = EntranceUtils.GetCash(name).ToString();

            var games = EntranceUtils.TakeGames(name);
            for (int i = 0; i < games.Length + 1; i++)
            {
                TableRow tmp = new TableRow();
                for (int j = 0; j < 3; j++)
                {
                    TableCell tmp1 = new TableCell();
                    tmp.Cells.Add(tmp1);
                }
                tblGames.Rows.Add(tmp);
            }

            TableItemStyle tableStyle = new TableItemStyle();
            tableStyle.HorizontalAlign = HorizontalAlign.Center;
            tableStyle.VerticalAlign = VerticalAlign.Middle;
            tableStyle.BorderStyle = BorderStyle.Solid;

            foreach (TableRow rw in tblGames.Rows)
                foreach (TableCell cel in rw.Cells)
                    cel.ApplyStyle(tableStyle);

            for (int i = 0; i < tblGames.Rows.Count; i++)
            {
                if (i == 0)
                {
                    tblGames.Rows[i].Cells[0].Text = "Номер игры";
                    tblGames.Rows[i].Cells[1].Text = "Банк игры";
                    tblGames.Rows[i].Cells[2].Text = "Выигрыш";
                }
                else
                {
                    tblGames.Rows[i].Cells[0].Text = games[i - 1].Name;
                    tblGames.Rows[i].Cells[1].Text = (games[i - 1].Chips).ToString();
                    tblGames.Rows[i].Cells[2].Text = (games[i - 1].Result).ToString();
                }
            }

            this.UpdateState();
        }

        void ptable_Updated(object sender, EventArgs e)
        {
            this.UpdateState();
        }

        protected void btnJoin_Click(object sender, EventArgs e)
        {
            if (gameId < 0)
                gameId = ptable.Join(name);

            if (gameId >= 0)
                this.UpdateState();
        }

        protected void btnCash_Click(object sender, EventArgs e)
        {
            if (gameId < 0)
                this.AddCash(100);
        }

        protected void btnBet_Click(object sender, EventArgs e)
        {
            uint id, amount;

            if (gameId >= 0)
            {
                id = (uint)gameId;
                amount = uint.Parse(inpBet.Value);
                if (ptable.Bet(id, amount))
                    this.UpdateState();
                else
                    inpBet.Value = "Не удалось...";
            }
        }

        protected void btnFold_Click(object sender, EventArgs e)
        {
            if (gameId >= 0)
            {
                if(ptable.Fold((uint)gameId))
                    this.UpdateState();
            }
        }

        protected void updTimer_Tick(object sender, EventArgs e)
        {
            this.UpdateState();
        }
    }
}