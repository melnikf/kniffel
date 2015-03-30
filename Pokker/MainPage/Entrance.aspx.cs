using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pokker.Models;

namespace Pokker.MainPage
{
    public partial class Entrance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(),
                "alert", "Reg();", true);
            lbLogin.Text=lbLogin.Text + " " + Request.QueryString["Login"];
            lbEmail.Text = lbEmail.Text + " " + MainPageUtils.TakeEmail(Request.QueryString["Login"]);
            lbChips.Text = lbChips.Text + " " + MainPageUtils.TakeChips(Request.QueryString["Login"]);
            Game[] games=MainPageUtils.TakeGames(Request.QueryString["Login"]);

            for (int i = 0; i < games.Length+1;i++)
            {
                TableRow tmp = new TableRow();
                for(int j=0;j<3;j++)
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

            for (int i = 0; i < tblGames.Rows.Count;i++)
            {
                if(i==0)
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


    }
}