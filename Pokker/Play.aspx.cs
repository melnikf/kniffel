using Pokker.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pokker
{
    public partial class Play : System.Web.UI.Page
    {
        PokerTable ptable;
        int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            ptable = PokerTableProvider.GetTable();
            if (!Page.IsPostBack)
            {
                Page.Session.Add("pid", ptable.Join(Session["name"].ToString()));
                this.UpdateState();   
            }
            id = int.Parse(Page.Session["pid"].ToString());
        }

        #region old

        private void Showdown(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Betting(object sender, BettingEventArgs e)
        {
        }

        private void PlayerJoined(object sender, EventArgs e)
        {
        }

        private void Ready(object sender, ReadyEventArgs e)
        {
        } 

        #endregion

        private void UpdateState()
        {
            string state = "";
            int i;

            if (id < 0)
            {
                state = "Ошибка!";
            }
            else
            {
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
                state += this.GetCards(ptable.OpenCards());
                state += "Рука: \n";
                //state += this.GetCards(ptable.ShowHand((uint)id));
            }            
            
            txtState.Text = state;
        }

        private string GetCards(Card[] cards)
        {
            int i;
            string res;

            res = "";
            for(i = 0; i < cards.Length; i++)
            {
                res += GetCard(cards[i]) + "; ";
            }
            return res;
        }

        private string GetCard(Card card)
        {
            string res = "";

            switch(card.weight)
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
            switch(ptable.Round)
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

        protected void btnBet_Click(object sender, EventArgs e)
        {
            if (ptable.Bet((uint)id, uint.Parse(inpBet.Value)))
            {
                this.UpdateState();
            }
            else
            {
                inpBet.Value = "Не удалось поставить.";
            }
        }

        protected void btnFold_Click(object sender, EventArgs e)
        {
            ptable.Fold((uint)id);
            this.UpdateState();
        }

        protected void updTimer_Tick(object sender, EventArgs e)
        {
            this.UpdateState();
        }
    }
}