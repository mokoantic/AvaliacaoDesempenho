using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace ADFramework.Classes
{
    public class Connection
    {
        public static SqlConnection SqlConnection = new SqlConnection();

        public class Metodo
        {
            public static void SetConnection(string ligacao)
            {
                try
                {
                    SqlConnection.ConnectionString = ligacao;
                }
                catch (Exception)
                {

                    throw;
                }
            }

            public static SqlConnection GetConnection()
            {
                return SqlConnection;
            }
        }
    }

    public class TabGrupo
    {
        public static SqlConnection SqlConnection = Connection.Metodo.GetConnection();

        public static string Id = "Gru_Id";
        public static string Descricao = "Gru_Descricao";
        public static string Activo = "Gru_Ativo";

        public class Comprimento
        {
            public const int Id = 8;
            public const int Descricao = 300;
            public const int Activo = 1;
        }

        public class Procedimento
        {
            public const string Select = "Proc_SelectGruposDynamic";
            public const string Insert = "Proc_InsertGrupo";
            public const string Update = "Proc_UpdateGrupo";
            public const string Delete = "Proc_DeleteGruposByGru_Id";
        }

        public class Metodo
        {
            public static DataTable Select(string Where, string OrderBy = null)
            {
                DataTable Tabela = new DataTable();

                try
                {
                    if (SqlConnection.State != ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }

                    SqlCommand SqlCommand = new SqlCommand(Procedimento.Select, SqlConnection);
                    SqlCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(SqlCommand);


                    da.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@WhereCondition",
                        Value = Where,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 500
                    });

                    da.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@OrderByExpression",
                        Value = OrderBy,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 250
                    });

                    da.Fill(Tabela);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (SqlConnection.State == ConnectionState.Open)
                    {
                        SqlConnection.Close();
                    }
                }

                return Tabela;
            }

            public static object Insert(string Gru_Descricao)
            {
                object Gru_Id = null;

                SqlCommand SqlCommand = new SqlCommand(Procedimento.Insert, SqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] SQLParam = new SqlParameter[3];

                try
                {
                    if (SqlConnection.State != ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }

                    SQLParam[0] = new SqlParameter("@" + TabGrupo.Descricao, SqlDbType.NVarChar, TabGrupo.Comprimento.Descricao);
                    SQLParam[0].Value = Gru_Descricao;
                    SqlCommand.Parameters.Add(SQLParam[0]);

                    SQLParam[1] = new SqlParameter("@" + TabGrupo.Activo, SqlDbType.Bit, TabGrupo.Comprimento.Activo);
                    SQLParam[1].Value = 1;
                    SqlCommand.Parameters.Add(SQLParam[1]);

                    SQLParam[2] = new SqlParameter();
                    SQLParam[2].ParameterName = "@" + TabGrupo.Id;
                    SQLParam[2].SqlDbType = SqlDbType.Int;
                    SQLParam[2].Direction = ParameterDirection.Output;

                    SqlCommand.Parameters.Add(SQLParam[2]);

                    SqlCommand.ExecuteNonQuery();

                    Gru_Id = SQLParam[2].Value;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (SqlConnection.State == ConnectionState.Open)
                    {
                        SqlConnection.Close();
                    }
                }

                return Gru_Id;
            }

            public static void Update(string Gru_Descricao, bool Gru_Activo, int Gru_Id)
            {

                SqlCommand SqlCommand = new SqlCommand(Procedimento.Update, SqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] SQLParam = new SqlParameter[3];

                try
                {
                    if (SqlConnection.State != ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }

                    SQLParam[0] = new SqlParameter("@" + TabGrupo.Descricao, SqlDbType.NVarChar, TabGrupo.Comprimento.Descricao);
                    SQLParam[0].Value = Gru_Descricao;
                    SqlCommand.Parameters.Add(SQLParam[0]);

                    SQLParam[1] = new SqlParameter("@" + TabGrupo.Activo, SqlDbType.Bit, TabGrupo.Comprimento.Activo);
                    SQLParam[1].Value = Gru_Activo;
                    SqlCommand.Parameters.Add(SQLParam[1]);

                    SQLParam[2] = new SqlParameter("@" + TabGrupo.Id, SqlDbType.Int, TabGrupo.Comprimento.Id);
                    SQLParam[2].Value = Gru_Id;
                    SqlCommand.Parameters.Add(SQLParam[2]);

                    SqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (SqlConnection.State == ConnectionState.Open)
                    {
                        SqlConnection.Close();
                    }
                }
            }

            public static void Delete(int Gru_Id)
            {
                SqlCommand SqlCommand = new SqlCommand(Procedimento.Delete, SqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] SQLParam = new SqlParameter[1];

                try
                {
                    if (SqlConnection.State != ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }

                    SQLParam[0] = new SqlParameter("@" + TabGrupo.Id, SqlDbType.Int, TabGrupo.Comprimento.Id);
                    SQLParam[0].Value = Gru_Id;
                    SqlCommand.Parameters.Add(SQLParam[0]);

                    SqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (SqlConnection.State == ConnectionState.Open)
                    {
                        SqlConnection.Close();
                    }
                }
            }
        }
    }

    public class TabUtilizador
    {
        public static SqlConnection SqlConnection = Connection.Metodo.GetConnection();

        public static string Id = "Uti_ID";
        public static string Gru_Id = "Uti_Gru_Id";
        public static string Login = "Uti_Login";
        public static string Password = "Uti_Password";
        public static string PrimeiroNome = "Uti_PrimeiroNome";
        public static string UltimoNome = "Uti_UltimoNome";
        public static string PastaFicheiro = "Uti_PastaFicheiro";
        public static string NomeFicheiro = "Uti_NomeFicheiro";
        public static string Email = "Uti_Email";
        public static string EmailConfirmado = "Uti_EmailConfirmado";
        public static string Activo = "Uti_Activo";
        public class Comprimento
        {
            public static int Id = 8;
            public static int Gru_Id = 8;
            public static int Login = 50;
            public static int Password = 300;
            public static int PrimeiroNome = 50;
            public static int UltimoNome = 50;
            public static int PastaFicheiro = 250;
            public static int NomeFicheiro = 250;
            public static int Email = 250;
            public static int EmailConfirmado = 1;
            public static int Activo = 1;
        }

        public class Procedimento
        {
            public static string Select = "Proc_SelectUtilizadorsDynamic";
            public static string Insert = "Proc_InsertUtilizador";
            public static string Update = "Proc_UpdateUtilizador";
            public static string Delete = "Proc_DeleteUtilizadorsByUti_ID";
        }

        public class Metodo
        {
            public static object EncriptaPassword(string Uti_Password)
            {
                object PassworEncriptada = null;

                try
                {
                    HashAlgorithm hashAlg = new SHA1Managed();
                    byte[] pwordData = Encoding.Unicode.GetBytes(Uti_Password.ToString());
                    byte[] hash = hashAlg.ComputeHash(pwordData);
                    PassworEncriptada = BitConverter.ToString(hash);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return PassworEncriptada;
            }

            public static DataTable Select(string Where, string OrderBy = null)
            {
                DataTable Tabela = new DataTable();

                try
                {
                    if (SqlConnection.State != ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }

                    SqlCommand SqlCommand = new SqlCommand(Procedimento.Select, SqlConnection);
                    SqlCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(SqlCommand);


                    da.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@WhereCondition",
                        Value = Where,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 500
                    });

                    da.SelectCommand.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@OrderByExpression",
                        Value = OrderBy,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 250
                    });

                    da.Fill(Tabela);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (SqlConnection.State == ConnectionState.Open)
                    {
                        SqlConnection.Close();
                    }
                }

                return Tabela;
            }

            public static object Insert(int Uti_Gru_Id, string Uti_Login, string Uti_Password, object Uti_PrimeiroNome, object Uti_UltimoNome, object Uti_PastaFicheiro, object Uti_NomeFicheiro, object Uti_Email, bool Uti_EmailConfirmado)
            {
                object Uti_Id = null;

                SqlCommand SqlCommand = new SqlCommand(Procedimento.Insert, SqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] SQLParam = new SqlParameter[11];

                try
                {
                    if (SqlConnection.State != ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }

                    SQLParam[0] = new SqlParameter("@" + TabUtilizador.Gru_Id, SqlDbType.Int, TabUtilizador.Comprimento.Gru_Id);
                    SQLParam[0].Value = Uti_Gru_Id;
                    SqlCommand.Parameters.Add(SQLParam[0]);

                    SQLParam[1] = new SqlParameter("@" + TabUtilizador.Login, SqlDbType.NVarChar, TabUtilizador.Comprimento.Login);
                    SQLParam[1].Value = Uti_Login;
                    SqlCommand.Parameters.Add(SQLParam[1]);

                    SQLParam[2] = new SqlParameter("@" + TabUtilizador.Password, SqlDbType.NVarChar, TabUtilizador.Comprimento.Password);
                    SQLParam[2].Value = TabUtilizador.Metodo.EncriptaPassword(Uti_Password);
                    SqlCommand.Parameters.Add(SQLParam[2]);

                    SQLParam[3] = new SqlParameter("@" + TabUtilizador.PrimeiroNome, SqlDbType.NVarChar, TabUtilizador.Comprimento.PrimeiroNome);
                    SQLParam[3].Value = Uti_PrimeiroNome;
                    SqlCommand.Parameters.Add(SQLParam[3]);

                    SQLParam[4] = new SqlParameter("@" + TabUtilizador.UltimoNome, SqlDbType.NVarChar, TabUtilizador.Comprimento.UltimoNome);
                    SQLParam[4].Value = Uti_UltimoNome;
                    SqlCommand.Parameters.Add(SQLParam[4]);

                    SQLParam[5] = new SqlParameter("@" + TabUtilizador.PastaFicheiro, SqlDbType.NVarChar, TabUtilizador.Comprimento.PastaFicheiro);
                    SQLParam[5].Value = Uti_PastaFicheiro;
                    SqlCommand.Parameters.Add(SQLParam[5]);

                    SQLParam[6] = new SqlParameter("@" + TabUtilizador.NomeFicheiro, SqlDbType.NVarChar, TabUtilizador.Comprimento.NomeFicheiro);
                    SQLParam[6].Value = Uti_NomeFicheiro;
                    SqlCommand.Parameters.Add(SQLParam[6]);

                    SQLParam[7] = new SqlParameter("@" + TabUtilizador.Email, SqlDbType.NVarChar, TabUtilizador.Comprimento.Email);
                    SQLParam[7].Value = Uti_Email;
                    SqlCommand.Parameters.Add(SQLParam[7]);

                    SQLParam[8] = new SqlParameter("@" + TabUtilizador.EmailConfirmado, SqlDbType.Bit, TabUtilizador.Comprimento.EmailConfirmado);
                    SQLParam[8].Value = Uti_EmailConfirmado;
                    SqlCommand.Parameters.Add(SQLParam[8]);

                    SQLParam[9] = new SqlParameter("@" + TabUtilizador.Activo, SqlDbType.Bit, TabUtilizador.Comprimento.Activo);
                    SQLParam[9].Value = 1;
                    SqlCommand.Parameters.Add(SQLParam[9]);

                    SQLParam[10] = new SqlParameter();
                    SQLParam[10].ParameterName = "@" + TabUtilizador.Id;
                    SQLParam[10].SqlDbType = SqlDbType.Int;
                    SQLParam[10].Direction = ParameterDirection.Output;
                    SqlCommand.Parameters.Add(SQLParam[10]);

                    SqlCommand.ExecuteNonQuery();

                    Uti_Id = SQLParam[10].Value;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (SqlConnection.State == ConnectionState.Open)
                    {
                        SqlConnection.Close();
                    }
                }

                return Uti_Id;
            }

            public static void Update(int Uti_Gru_Id, string Uti_Login, string Uti_Password, object Uti_PrimeiroNome, object Uti_UltimoNome, object Uti_PastaFicheiro, object Uti_NomeFicheiro, object Uti_Email, bool Uti_EmailConfirmado, bool Uti_Activo, int Uti_Id)
            {
                SqlCommand SqlCommand = new SqlCommand(Procedimento.Update, SqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] SQLParam = new SqlParameter[11];

                try
                {
                    if (SqlConnection.State != ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }

                    SQLParam[0] = new SqlParameter("@" + TabUtilizador.Gru_Id, SqlDbType.Int, TabUtilizador.Comprimento.Gru_Id);
                    SQLParam[0].Value = Uti_Gru_Id;
                    SqlCommand.Parameters.Add(SQLParam[0]);

                    SQLParam[1] = new SqlParameter("@" + TabUtilizador.Login, SqlDbType.NVarChar, TabUtilizador.Comprimento.Login);
                    SQLParam[1].Value = Uti_Login;
                    SqlCommand.Parameters.Add(SQLParam[1]);

                    SQLParam[2] = new SqlParameter("@" + TabUtilizador.Password, SqlDbType.NVarChar, TabUtilizador.Comprimento.Password);
                    SQLParam[2].Value = Uti_Password;
                    SqlCommand.Parameters.Add(SQLParam[2]);

                    SQLParam[3] = new SqlParameter("@" + TabUtilizador.PrimeiroNome, SqlDbType.NVarChar, TabUtilizador.Comprimento.PrimeiroNome);
                    SQLParam[3].Value = Uti_PrimeiroNome;
                    SqlCommand.Parameters.Add(SQLParam[3]);

                    SQLParam[4] = new SqlParameter("@" + TabUtilizador.UltimoNome, SqlDbType.NVarChar, TabUtilizador.Comprimento.UltimoNome);
                    SQLParam[4].Value = Uti_UltimoNome;
                    SqlCommand.Parameters.Add(SQLParam[4]);

                    SQLParam[5] = new SqlParameter("@" + TabUtilizador.PastaFicheiro, SqlDbType.NVarChar, TabUtilizador.Comprimento.PastaFicheiro);
                    SQLParam[5].Value = Uti_PastaFicheiro;
                    SqlCommand.Parameters.Add(SQLParam[5]);

                    SQLParam[6] = new SqlParameter("@" + TabUtilizador.NomeFicheiro, SqlDbType.NVarChar, TabUtilizador.Comprimento.NomeFicheiro);
                    SQLParam[6].Value = Uti_NomeFicheiro;
                    SqlCommand.Parameters.Add(SQLParam[6]);

                    SQLParam[7] = new SqlParameter("@" + TabUtilizador.Email, SqlDbType.NVarChar, TabUtilizador.Comprimento.Email);
                    SQLParam[7].Value = Uti_Email;
                    SqlCommand.Parameters.Add(SQLParam[7]);

                    SQLParam[8] = new SqlParameter("@" + TabUtilizador.EmailConfirmado, SqlDbType.Bit, TabUtilizador.Comprimento.EmailConfirmado);
                    SQLParam[8].Value = Uti_EmailConfirmado;
                    SqlCommand.Parameters.Add(SQLParam[8]);

                    SQLParam[9] = new SqlParameter("@" + TabUtilizador.Activo, SqlDbType.Bit, TabUtilizador.Comprimento.Activo);
                    SQLParam[9].Value = Uti_Activo;
                    SqlCommand.Parameters.Add(SQLParam[9]);

                    SQLParam[10] = new SqlParameter("@" + TabUtilizador.Id, SqlDbType.Int, TabUtilizador.Comprimento.Id);
                    SQLParam[10].Value = Uti_Id;
                    SqlCommand.Parameters.Add(SQLParam[10]);

                    SqlCommand.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (SqlConnection.State == ConnectionState.Open)
                    {
                        SqlConnection.Close();
                    }
                }
            }

            public static void Delete(int Uti_Id)
            {
                SqlCommand SqlCommand = new SqlCommand(Procedimento.Delete, SqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] SQLParam = new SqlParameter[1];

                try
                {
                    if (SqlConnection.State != ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }

                    SQLParam[0] = new SqlParameter("@" + TabUtilizador.Id, SqlDbType.Int, TabUtilizador.Comprimento.Id);
                    SQLParam[0].Value = Uti_Id;
                    SqlCommand.Parameters.Add(SQLParam[0]);

                    SqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (SqlConnection.State == ConnectionState.Open)
                    {
                        SqlConnection.Close();
                    }
                }
            }
        }
    }

}
