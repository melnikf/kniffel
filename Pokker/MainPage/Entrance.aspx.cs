using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


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
        }


    }
}