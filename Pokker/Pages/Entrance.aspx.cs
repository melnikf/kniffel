using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pokker.Models;
using System.Web.Security;

namespace Pokker.Pages
{
    public partial class Entrance : System.Web.UI.Page
    {
        string name;

        private string GetUserName()
        {
            HttpCookie authCookie = Request.Cookies[".ASPXAUTH"];
            return FormsAuthentication.Decrypt(authCookie.Value).Name;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Game[] games;

            name = this.GetUserName();

            lblName.Text = name;
            lblEmail.Text = EntranceUtils.TakeEmail(name);
            lblCash.Text = EntranceUtils.GetCash(name).ToString();

            games = EntranceUtils.TakeGames(name);
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
        }

        protected void btnJoin_Click(object sender, EventArgs e)
        {

        }

        protected void btnCash_Click(object sender, EventArgs e)
        {
            this.AddCash(100);
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
    }
}