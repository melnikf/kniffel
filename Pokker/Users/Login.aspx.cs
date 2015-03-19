using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pokker.Users
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
                return;
            }
            if (!UserUtils.StringValid(upass.Value, 4))
            {
                lblError.Visible = true;
                lblError.InnerText = "Минимальное количество символов в пароле - 4";
                return;
            }

            if (!UserUtils.LoginSuccess(uname.Value, upass.Value))
            {
                lblError.Visible = true;
                lblError.InnerText = "Неправильное имя или пароль";
                return;
            }

            lblError.Visible = false;
            // TODO: Перенаправление на персональную страницу.
        }
    }
}