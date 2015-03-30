using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pokker.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if(!UserUtils.StringValid(uname.Value, 4))
            {
                lblError.Visible = true;
                lblError.InnerText = "Имена короче 4-х знаков запрещены";
                Page.ClientScript.RegisterStartupScript(this.GetType(),
                "alert", "NoReg();", true);
                return;
            }
            if (!UserUtils.StringValid(upass.Value, 4))
            {
                lblError.Visible = true;
                lblError.InnerText = "Минимальное количество символов в пароле - 4";
                Page.ClientScript.RegisterStartupScript(this.GetType(),
                "alert", "NoReg();", true);
                return;
            }

            if (!UserUtils.LoginSuccess(uname.Value, upass.Value))
            {
                lblError.Visible = true;
                lblError.InnerText = "Неправильное имя или пароль";
                Page.ClientScript.RegisterStartupScript(this.GetType(),
                "alert", "NoReg();", true);
                return;
            }

            lblError.Visible = false;

            Response.Cookies.Add(this.GetAuthCookie(uname.Value));
            Response.Redirect("../Pages/Entrance.aspx");
        }

        private HttpCookie GetAuthCookie(string name)
        {
            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(name, true);
            authCookie.Expires = DateTime.Now.AddDays(10);

            return authCookie;
        }
    }
}