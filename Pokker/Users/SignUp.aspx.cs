using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pokker.Users
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            if (!UserUtils.StringValid(uname.Value, 4))
            {
                lblError.Visible = true;
                lblError.InnerText = "Имена короче 4-х знаков запрещены";
                return;
            }
            if (!UserUtils.MailValid(umail.Value))
            {
                lblError.Visible = true;
                lblError.InnerText = "Неверный емайл";
                return;
            }
            if (!UserUtils.StringValid(upass.Value, 4))
            {
                lblError.Visible = true;
                lblError.InnerText = "Минимальное количество символов в пароле - 4";
                return;
            }
            if (upass.Value != upassr.Value)
            {
                lblError.Visible = true;
                lblError.InnerText = "Пароль не подтвержден";
                return;
            }

            lblError.Visible = false;

            try
            {
                UserUtils.SignUpUser(uname.Value, umail.Value, upass.Value);
                Page.ClientScript.RegisterStartupScript(this.GetType(),
                "alert", "Reg();", true);
                // TODO: Перенаправление на персональную страницу.
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(),
                "alert", "NoReg();", true);
            }
            
        }
    }
}