using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MonoWeb
{
    public partial class StartUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["continue"] = Label1.Text;
        }

        protected void Button2_Click(object sender, EventArgs e) //Continue Game
        {
            Label1.Text = "true";
            Session["continue"] = Label1.Text;
            Response.Redirect("default.aspx");
        }

        protected void Button1_Click1(object sender, EventArgs e) //New Game
        {
            Label1.Text = "false;";
            Session["continue"] = Label1.Text;
            Response.Redirect("default.aspx");
        }
    }
}