﻿using AvaliacaoDesempenho.MasterPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AvaliacaoDesempenho.WebForms
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((PaginaBase)this.Master).Menu_Dashboard.Attributes.Add("class", "active");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}