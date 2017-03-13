using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADFramework.Classes;

namespace AvaliacaoDesempenho.MasterPages
{
    public partial class PaginaBase : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable user = (DataTable)(Session["User"]);

                if (user != null)
                {
                    _VerificaSessao(user);
                }
                else
                {
                    Response.Redirect("/Login");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region "Menu Activo"

        public System.Web.UI.HtmlControls.HtmlGenericControl Menu_Dashboard
        {
            get { return menu_item_1; }
        }

        public System.Web.UI.HtmlControls.HtmlGenericControl Menu_Quadrantes
        {
            get { return menu_item_2; }
        }

        public System.Web.UI.HtmlControls.HtmlGenericControl Menu_Funcionarios
        {
            get { return menu_item_3; }
        }

        public System.Web.UI.HtmlControls.HtmlGenericControl Menu_Estatisticas
        {
            get { return menu_item_4; }
        }

        public System.Web.UI.HtmlControls.HtmlGenericControl Menu_Avaliacao
        {
            get { return menu_item_5; }
        }

        #endregion

        private void _VerificaSessao(DataTable user)
        {
            try
            {
                if (user.Rows.Count == 0)
                {
                    Response.Redirect("/Login");
                }
                else
                {
                    _CarregaDados(user);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void _CarregaDados(DataTable user)
        {
            string nome = "";

            try
            {
                if (user.Rows[0][TabUtilizador.PrimeiroNome] != DBNull.Value)
                {
                    nome = user.Rows[0][TabUtilizador.PrimeiroNome].ToString();

                    if (user.Rows[0][TabUtilizador.UltimoNome] != DBNull.Value)
                    {
                        nome += " " + user.Rows[0][TabUtilizador.UltimoNome].ToString();
                    }
                }
                else
                {
                    nome = user.Rows[0][TabUtilizador.Login].ToString();
                }

                NomeUtilizador.InnerText = nome + " ";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BTLogout_Click(object sender, EventArgs e)
        {
            DataTable user = (DataTable)(Session["User"]);

            try
            {
                user.Reset();
                Response.Redirect("/Login");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}