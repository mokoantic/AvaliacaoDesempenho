using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADFramework.Classes;
using System.Data;
using AvaliacaoDesempenho.MasterPages;

namespace AvaliacaoDesempenho
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TabUtilizador.Metodo.Insert(2, "admin", "admin", DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, "admin@email.com", true);
        }

        protected void BTLogin_Click(object sender, EventArgs e)
        {
            DataTable Tabela = null;

            string ErroVazio = "<div class=\"alert alert-danger\" role=\"alert\"><strong>Erro:</strong> Preencha os campos de Nome de Utilizador e Password. </div>";
            string ErroLogin = "<div class=\"alert alert-danger\" role=\"alert\"><strong>Erro:</strong> O nome de utilizador e/ou password estão errados.</div>";

            try
            {
                if (TBUser.Text == "" || TBPassword.Text == "")
                {
                    DivErro.InnerHtml = ErroVazio;
                }
                else
                {
                    Tabela = TabUtilizador.Metodo.Select(TabUtilizador.Login + "='" + TBUser.Text + "'");

                    if (Tabela.Rows.Count == 0)
                    {
                        DivErro.InnerHtml = ErroLogin;
                    }
                    else
                    {
                        if (TabUtilizador.Metodo.EncriptaPassword(TBPassword.Text).ToString() != Tabela.Rows[0][TabUtilizador.Password].ToString())
                        {
                            DivErro.InnerHtml = ErroLogin;
                        }
                        else
                        {
                            DataTable dt = new DataTable();

                            dt.Columns.Add(TabUtilizador.Id, Type.GetType("System.Int32"));
                            dt.Columns.Add(TabUtilizador.Gru_Id, Type.GetType("System.Int32"));
                            dt.Columns.Add(TabUtilizador.Login, Type.GetType("System.String"));
                            dt.Columns.Add(TabUtilizador.PrimeiroNome, Type.GetType("System.String"));
                            dt.Columns.Add(TabUtilizador.UltimoNome, Type.GetType("System.String"));
                            dt.Columns.Add(TabUtilizador.PastaFicheiro, Type.GetType("System.String"));
                            dt.Columns.Add(TabUtilizador.NomeFicheiro, Type.GetType("System.String"));
                            dt.Columns.Add(TabUtilizador.Email, Type.GetType("System.String"));

                            dt.Rows.Add(Tabela.Rows[0][TabUtilizador.Id],
                                        Tabela.Rows[0][TabUtilizador.Gru_Id],
                                        Tabela.Rows[0][TabUtilizador.Login],
                                        Tabela.Rows[0][TabUtilizador.PrimeiroNome],
                                        Tabela.Rows[0][TabUtilizador.UltimoNome],
                                        Tabela.Rows[0][TabUtilizador.PastaFicheiro],
                                        Tabela.Rows[0][TabUtilizador.NomeFicheiro],
                                        Tabela.Rows[0][TabUtilizador.Email]);

                            Session["User"] = dt;

                            Response.Redirect("/Dashboard");
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}