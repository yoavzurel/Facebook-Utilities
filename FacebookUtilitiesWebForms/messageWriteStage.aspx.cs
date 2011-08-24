using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FacebookUtilitiesWebForms
{
    public partial class messageWriteStage : System.Web.UI.Page
    {
        private string m_Friends;

        protected void Page_Load(object sender, EventArgs e)
        {


            m_Friends = Request.QueryString["friendsList"];
        }
    }
}