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

    public class TabQuadrante
    {
        public static SqlConnection SqlConnection = Connection.Metodo.GetConnection();

        public static string Id = "Qua_Id";
        public static string Descricao = "Qua_Descricao";
        public static string Peso = "Qua_Peso";

        public class Comprimento
        {
            public const int Id = 8;
            public const int Descricao = 300;
            public const int Peso = 10;
        }

        public class Procedimento
        {
            public const string Select = "Proc_SelectQuadrantesDynamic";
            public const string Insert = "Proc_InsertQuadrante";
            public const string Update = "Proc_UpdateQuadrante";
            public const string Delete = "Proc_DeleteQuadrantesByQua_Id";
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

            public static object Insert(string Qua_Descricao, int Qua_Peso)
            {
                object Qua_Id = null;

                SqlCommand SqlCommand = new SqlCommand(Procedimento.Insert, SqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] SQLParam = new SqlParameter[3];

                try
                {
                    if (SqlConnection.State != ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }

                    SQLParam[0] = new SqlParameter("@" + TabQuadrante.Descricao, SqlDbType.NVarChar, TabQuadrante.Comprimento.Descricao);
                    SQLParam[0].Value = Qua_Descricao;
                    SqlCommand.Parameters.Add(SQLParam[0]);

                    SQLParam[1] = new SqlParameter("@" + TabQuadrante.Peso, SqlDbType.Int, TabQuadrante.Comprimento.Peso);
                    SQLParam[1].Value = Qua_Peso;
                    SqlCommand.Parameters.Add(SQLParam[1]);

                    SQLParam[2] = new SqlParameter();
                    SQLParam[2].ParameterName = "@" + TabQuadrante.Id;
                    SQLParam[2].SqlDbType = SqlDbType.Int;
                    SQLParam[2].Direction = ParameterDirection.Output;
                    SqlCommand.Parameters.Add(SQLParam[2]);

                    SqlCommand.ExecuteNonQuery();

                    Qua_Id = SQLParam[2].Value;

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

                return Qua_Id;
            }

            public static void Update(string Qua_Descricao, int Qua_Peso, int Qua_Id)
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

                    SQLParam[0] = new SqlParameter("@" + TabQuadrante.Descricao, SqlDbType.NVarChar, TabQuadrante.Comprimento.Descricao);
                    SQLParam[0].Value = Qua_Descricao;
                    SqlCommand.Parameters.Add(SQLParam[0]);

                    SQLParam[1] = new SqlParameter("@" + TabQuadrante.Peso, SqlDbType.Int, TabQuadrante.Comprimento.Peso);
                    SQLParam[1].Value = Qua_Peso;
                    SqlCommand.Parameters.Add(SQLParam[1]);

                    SQLParam[2] = new SqlParameter("@" + TabQuadrante.Id, SqlDbType.Int, TabQuadrante.Comprimento.Id);
                    SQLParam[2].Value = Qua_Id;
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

            public static void Delete(int Qua_Id)
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

                    SQLParam[0] = new SqlParameter("@" + TabQuadrante.Id, SqlDbType.Int, TabQuadrante.Comprimento.Id);
                    SQLParam[0].Value = Qua_Id;
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

    public class TabClassificacao
    {
        public static SqlConnection SqlConnection = Connection.Metodo.GetConnection();

        public static string Id = "Cla_Id";
        public static string Qua_Id = "Cla_Qua_Id";
        public static string Descricao = "Cla_Descricao";
        public static string Cotacao = "Cla_Cotacao";

        public class Comprimento
        {
            public static int Id = 8;
            public static int Qua_Id = 8;
            public static int Descricao = 300;
            public static int Cotacao = 10;
        }

        public class Procedimento
        {
            public static string Select = "Proc_SelectClassificacaosDynamic";
            public static string Insert = "Proc_InsertClassificacao";
            public static string Update = "Proc_UpdateClassificacao";
            public static string Delete = "Proc_DeleteClassificacaosByCla_Id";
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

            public static object Insert(int Cla_Qua_Id, string Cla_Descricao, int Cla_Cotacao)
            {
                object Cla_Id = null;

                SqlCommand SqlCommand = new SqlCommand(Procedimento.Insert, SqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] SQLParam = new SqlParameter[4];

                try
                {
                    if (SqlConnection.State != ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }

                    SQLParam[0] = new SqlParameter("@" + TabClassificacao.Qua_Id, SqlDbType.Int, TabClassificacao.Comprimento.Qua_Id);
                    SQLParam[0].Value = Cla_Qua_Id;
                    SqlCommand.Parameters.Add(SQLParam[0]);

                    SQLParam[1] = new SqlParameter("@" + TabClassificacao.Descricao, SqlDbType.NVarChar, TabClassificacao.Comprimento.Descricao);
                    SQLParam[1].Value = Cla_Descricao;
                    SqlCommand.Parameters.Add(SQLParam[1]);

                    SQLParam[2] = new SqlParameter("@" + TabClassificacao.Cotacao, SqlDbType.Int, TabClassificacao.Comprimento.Cotacao);
                    SQLParam[2].Value = Cla_Cotacao;
                    SqlCommand.Parameters.Add(SQLParam[2]);

                    SQLParam[3] = new SqlParameter();
                    SQLParam[3].ParameterName = "@" + TabClassificacao.Id;
                    SQLParam[3].SqlDbType = SqlDbType.Int;
                    SQLParam[3].Direction = ParameterDirection.Output;
                    SqlCommand.Parameters.Add(SQLParam[3]);

                    SqlCommand.ExecuteNonQuery();

                    Cla_Id = SQLParam[3].Value;

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

                return Cla_Id;
            }

            public static void Update(int Cla_Qua_Id, string Cla_Descricao, int Cla_Cotacao, int Cla_Id)
            {
                SqlCommand SqlCommand = new SqlCommand(Procedimento.Update, SqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] SQLParam = new SqlParameter[4];

                try
                {
                    if (SqlConnection.State != ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }

                    SQLParam[0] = new SqlParameter("@" + TabClassificacao.Qua_Id, SqlDbType.Int, TabClassificacao.Comprimento.Qua_Id);
                    SQLParam[0].Value = Cla_Qua_Id;
                    SqlCommand.Parameters.Add(SQLParam[0]);

                    SQLParam[1] = new SqlParameter("@" + TabClassificacao.Descricao, SqlDbType.NVarChar, TabClassificacao.Comprimento.Descricao);
                    SQLParam[1].Value = Cla_Descricao;
                    SqlCommand.Parameters.Add(SQLParam[1]);

                    SQLParam[2] = new SqlParameter("@" + TabClassificacao.Cotacao, SqlDbType.Int, TabClassificacao.Comprimento.Cotacao);
                    SQLParam[2].Value = Cla_Cotacao;
                    SqlCommand.Parameters.Add(SQLParam[2]);

                    SQLParam[3] = new SqlParameter("@" + TabClassificacao.Id, SqlDbType.Int, TabClassificacao.Comprimento.Id);
                    SQLParam[3].Value = Cla_Id;
                    SqlCommand.Parameters.Add(SQLParam[3]);

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

            public static void Delete(int Cla_Id)
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

                    SQLParam[0] = new SqlParameter("@" + TabClassificacao.Id, SqlDbType.Int, TabClassificacao.Comprimento.Id);
                    SQLParam[0].Value = Cla_Id;
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

    public class TabQuestao
    {
        public static SqlConnection SqlConnection = Connection.Metodo.GetConnection();

        public static string Id = "Que_Id";
        public static string Qua_Id = "Que_Qua_Id";
        public static string Descricao = "Que_Descricao";
        public static string Peso = "Que_Peso";

        public class Comprimento
        {
            public static int Id = 8;
            public static int Qua_Id = 8;
            public static int Descricao = 300;
            public static int Peso = 10;
        }

        public class Procedimento
        {
            public static string Select = "Proc_SelectQuestaosDynamic";
            public static string Insert = "Proc_InsertQuestao";
            public static string Update = "Proc_UpdateQuestao";
            public static string Delete = "Proc_DeleteQuestaosByQue_Id";
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

            public static object Insert(int Que_Qua_Id, string Que_Descricao, int Que_Peso)
            {
                object Que_Id = null;

                SqlCommand SqlCommand = new SqlCommand(Procedimento.Insert, SqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] SQLParam = new SqlParameter[4];

                try
                {
                    if (SqlConnection.State != ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }

                    SQLParam[0] = new SqlParameter("@" + TabQuestao.Qua_Id, SqlDbType.Int, TabQuestao.Comprimento.Qua_Id);
                    SQLParam[0].Value = Que_Qua_Id;
                    SqlCommand.Parameters.Add(SQLParam[0]);

                    SQLParam[1] = new SqlParameter("@" + TabQuestao.Descricao, SqlDbType.NVarChar, TabQuestao.Comprimento.Descricao);
                    SQLParam[1].Value = Que_Descricao;
                    SqlCommand.Parameters.Add(SQLParam[1]);

                    SQLParam[2] = new SqlParameter("@" + TabQuestao.Peso, SqlDbType.Int, TabQuestao.Comprimento.Peso);
                    SQLParam[2].Value = Que_Peso;
                    SqlCommand.Parameters.Add(SQLParam[2]);

                    SQLParam[3] = new SqlParameter();
                    SQLParam[3].ParameterName = "@" + TabQuestao.Id;
                    SQLParam[3].SqlDbType = SqlDbType.Int;
                    SQLParam[3].Direction = ParameterDirection.Output;
                    SqlCommand.Parameters.Add(SQLParam[3]);

                    SqlCommand.ExecuteNonQuery();

                    Que_Id = SQLParam[3].Value;

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

                return Que_Id;
            }

            public static void Update(int Que_Qua_Id, string Que_Descricao, int Que_Peso, int Que_Id)
            {
                SqlCommand SqlCommand = new SqlCommand(Procedimento.Update, SqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] SQLParam = new SqlParameter[4];

                try
                {
                    if (SqlConnection.State != ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }

                    SQLParam[0] = new SqlParameter("@" + TabQuestao.Qua_Id, SqlDbType.Int, TabQuestao.Comprimento.Qua_Id);
                    SQLParam[0].Value = Que_Qua_Id;
                    SqlCommand.Parameters.Add(SQLParam[0]);

                    SQLParam[1] = new SqlParameter("@" + TabQuestao.Descricao, SqlDbType.NVarChar, TabQuestao.Comprimento.Descricao);
                    SQLParam[1].Value = Que_Descricao;
                    SqlCommand.Parameters.Add(SQLParam[1]);

                    SQLParam[2] = new SqlParameter("@" + TabQuestao.Peso, SqlDbType.Int, TabQuestao.Comprimento.Peso);
                    SQLParam[2].Value = Que_Peso;
                    SqlCommand.Parameters.Add(SQLParam[2]);

                    SQLParam[3] = new SqlParameter("@" + TabQuestao.Id, SqlDbType.Int, TabQuestao.Comprimento.Id);
                    SQLParam[3].Value = Que_Id;
                    SqlCommand.Parameters.Add(SQLParam[3]);

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

            public static void Delete(int Que_Id)
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

                    SQLParam[0] = new SqlParameter("@" + TabQuestao.Id, SqlDbType.Int, TabQuestao.Comprimento.Id);
                    SQLParam[0].Value = Que_Id;
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

    public class TabAvaliacao
    {
        public static SqlConnection SqlConnection = Connection.Metodo.GetConnection();

        public static string Id = "Ava_Id";
        public static string Uti_Id = "Ava_Uti_Id";
        public static string Peso_Quadrante_Um = "Ava_Peso_Quadrante_Um";
        public static string Peso_Quadrante_Dois = "Ava_Peso_Quadrante_Dois";
        public static string Peso_Quadrante_Tres = "Ava_Peso_Quadrante_Tres";
        public static string Peso_Quadrante_Quatro = "Ava_Peso_Quadrante_Quatro";
        public static string Classificacao_Um = "Ava_Classificacao_Um";
        public static string Classificacao_Dois = "Ava_Classificacao_Dois";
        public static string Classificacao_Tres = "Ava_Classificacao_Tres";
        public static string Classificacao_Quatro = "Ava_Classificacao_Quatro";
        public static string Classificacao_Final = "Ava_Classificacao_Final";

        public class Comprimento
        {
            public static int Id = 8;
            public static int Uti_Id = 8;
            public static int Peso_Quadrante_Um = 10;
            public static int Peso_Quadrante_Dois = 10;
            public static int Peso_Quadrante_Tres = 10;
            public static int Peso_Quadrante_Quatro = 10;
            public static int Classificacao_Um = 10;
            public static int Classificacao_Dois = 10;
            public static int Classificacao_Tres = 10;
            public static int Classificacao_Quatro = 10;
            public static int Classificacao_Final = 10;
        }

        public class Procedimento
        {
            public static string Select = "Proc_SelectAvaliacaosDynamic";
            public static string Insert = "Proc_InsertAvaliacao";
            public static string Update = "Proc_UpdateAvaliacao";
            public static string Delete = "Proc_DeleteAvaliacaosByAva_Id";
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

            public static object Insert(int Ava_Uti_Id, int Ava_Peso_Quadrante_Um, int Ava_Peso_Quadrante_Dois, int Ava_Peso_Quadrante_Tres, int Ava_Peso_Quadrante_Quatro, int Ava_Classificacao_Um, int Ava_Classificacao_Dois, int Ava_Classificacao_Tres, int Ava_Classificacao_Quatro, int Ava_Classificacao_Final)
            {
                object Ava_Id = null;

                SqlCommand SqlCommand = new SqlCommand(Procedimento.Insert, SqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] SQLParam = new SqlParameter[11];

                try
                {
                    if (SqlConnection.State != ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }

                    SQLParam[0] = new SqlParameter("@" + TabAvaliacao.Uti_Id, SqlDbType.Int, TabAvaliacao.Comprimento.Uti_Id);
                    SQLParam[0].Value = Ava_Uti_Id;
                    SqlCommand.Parameters.Add(SQLParam[0]);

                    SQLParam[1] = new SqlParameter("@" + TabAvaliacao.Peso_Quadrante_Um, SqlDbType.Int, TabAvaliacao.Comprimento.Peso_Quadrante_Um);
                    SQLParam[1].Value = Ava_Peso_Quadrante_Um;
                    SqlCommand.Parameters.Add(SQLParam[1]);

                    SQLParam[2] = new SqlParameter("@" + TabAvaliacao.Peso_Quadrante_Dois, SqlDbType.Int, TabAvaliacao.Comprimento.Peso_Quadrante_Dois);
                    SQLParam[2].Value = Ava_Peso_Quadrante_Dois;
                    SqlCommand.Parameters.Add(SQLParam[2]);

                    SQLParam[3] = new SqlParameter("@" + TabAvaliacao.Peso_Quadrante_Tres, SqlDbType.Int, TabAvaliacao.Comprimento.Peso_Quadrante_Tres);
                    SQLParam[3].Value = Ava_Peso_Quadrante_Tres;
                    SqlCommand.Parameters.Add(SQLParam[3]);

                    SQLParam[4] = new SqlParameter("@" + TabAvaliacao.Peso_Quadrante_Quatro, SqlDbType.Int, TabAvaliacao.Comprimento.Peso_Quadrante_Quatro);
                    SQLParam[4].Value = Ava_Peso_Quadrante_Quatro;
                    SqlCommand.Parameters.Add(SQLParam[4]);

                    SQLParam[5] = new SqlParameter("@" + TabAvaliacao.Classificacao_Um, SqlDbType.Int, TabAvaliacao.Comprimento.Classificacao_Um);
                    SQLParam[5].Value = Ava_Classificacao_Um;
                    SqlCommand.Parameters.Add(SQLParam[5]);

                    SQLParam[6] = new SqlParameter("@" + TabAvaliacao.Classificacao_Dois, SqlDbType.Int, TabAvaliacao.Comprimento.Classificacao_Dois);
                    SQLParam[6].Value = Ava_Classificacao_Dois;
                    SqlCommand.Parameters.Add(SQLParam[6]);

                    SQLParam[7] = new SqlParameter("@" + TabAvaliacao.Classificacao_Tres, SqlDbType.Int, TabAvaliacao.Comprimento.Classificacao_Tres);
                    SQLParam[7].Value = Ava_Classificacao_Tres;
                    SqlCommand.Parameters.Add(SQLParam[7]);

                    SQLParam[8] = new SqlParameter("@" + TabAvaliacao.Classificacao_Quatro, SqlDbType.Int, TabAvaliacao.Comprimento.Classificacao_Quatro);
                    SQLParam[8].Value = Ava_Classificacao_Quatro;
                    SqlCommand.Parameters.Add(SQLParam[8]);

                    SQLParam[9] = new SqlParameter("@" + TabAvaliacao.Classificacao_Final, SqlDbType.Int, TabAvaliacao.Comprimento.Classificacao_Final);
                    SQLParam[9].Value = Ava_Classificacao_Final;
                    SqlCommand.Parameters.Add(SQLParam[9]);

                    SQLParam[10] = new SqlParameter();
                    SQLParam[10].ParameterName = "@" + TabAvaliacao.Id;
                    SQLParam[10].SqlDbType = SqlDbType.Int;
                    SQLParam[10].Direction = ParameterDirection.Output;
                    SqlCommand.Parameters.Add(SQLParam[10]);

                    SqlCommand.ExecuteNonQuery();

                    Ava_Id = SQLParam[10].Value;

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

                return Ava_Id;
            }

            public static void Update(int Ava_Uti_Id, int Ava_Peso_Quadrante_Um, int Ava_Peso_Quadrante_Dois, int Ava_Peso_Quadrante_Tres, int Ava_Peso_Quadrante_Quatro, int Ava_Classificacao_Um, int Ava_Classificacao_Dois, int Ava_Classificacao_Tres, int Ava_Classificacao_Quatro, int Ava_Classificacao_Final, int Ava_Id)
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

                    SQLParam[0] = new SqlParameter("@" + TabAvaliacao.Uti_Id, SqlDbType.Int, TabAvaliacao.Comprimento.Uti_Id);
                    SQLParam[0].Value = Ava_Uti_Id;
                    SqlCommand.Parameters.Add(SQLParam[0]);

                    SQLParam[1] = new SqlParameter("@" + TabAvaliacao.Peso_Quadrante_Um, SqlDbType.Int, TabAvaliacao.Comprimento.Peso_Quadrante_Um);
                    SQLParam[1].Value = Ava_Peso_Quadrante_Um;
                    SqlCommand.Parameters.Add(SQLParam[1]);

                    SQLParam[2] = new SqlParameter("@" + TabAvaliacao.Peso_Quadrante_Dois, SqlDbType.Int, TabAvaliacao.Comprimento.Peso_Quadrante_Dois);
                    SQLParam[2].Value = Ava_Peso_Quadrante_Dois;
                    SqlCommand.Parameters.Add(SQLParam[2]);

                    SQLParam[3] = new SqlParameter("@" + TabAvaliacao.Peso_Quadrante_Tres, SqlDbType.Int, TabAvaliacao.Comprimento.Peso_Quadrante_Tres);
                    SQLParam[3].Value = Ava_Peso_Quadrante_Tres;
                    SqlCommand.Parameters.Add(SQLParam[3]);

                    SQLParam[4] = new SqlParameter("@" + TabAvaliacao.Peso_Quadrante_Quatro, SqlDbType.Int, TabAvaliacao.Comprimento.Peso_Quadrante_Quatro);
                    SQLParam[4].Value = Ava_Peso_Quadrante_Quatro;
                    SqlCommand.Parameters.Add(SQLParam[4]);

                    SQLParam[5] = new SqlParameter("@" + TabAvaliacao.Classificacao_Um, SqlDbType.Int, TabAvaliacao.Comprimento.Classificacao_Um);
                    SQLParam[5].Value = Ava_Classificacao_Um;
                    SqlCommand.Parameters.Add(SQLParam[5]);

                    SQLParam[6] = new SqlParameter("@" + TabAvaliacao.Classificacao_Dois, SqlDbType.Int, TabAvaliacao.Comprimento.Classificacao_Dois);
                    SQLParam[6].Value = Ava_Classificacao_Dois;
                    SqlCommand.Parameters.Add(SQLParam[6]);

                    SQLParam[7] = new SqlParameter("@" + TabAvaliacao.Classificacao_Tres, SqlDbType.Int, TabAvaliacao.Comprimento.Classificacao_Tres);
                    SQLParam[7].Value = Ava_Classificacao_Tres;
                    SqlCommand.Parameters.Add(SQLParam[7]);

                    SQLParam[8] = new SqlParameter("@" + TabAvaliacao.Classificacao_Quatro, SqlDbType.Int, TabAvaliacao.Comprimento.Classificacao_Quatro);
                    SQLParam[8].Value = Ava_Classificacao_Quatro;
                    SqlCommand.Parameters.Add(SQLParam[8]);

                    SQLParam[9] = new SqlParameter("@" + TabAvaliacao.Classificacao_Final, SqlDbType.Int, TabAvaliacao.Comprimento.Classificacao_Final);
                    SQLParam[9].Value = Ava_Classificacao_Final;
                    SqlCommand.Parameters.Add(SQLParam[9]);

                    SQLParam[10] = new SqlParameter("@" + TabAvaliacao.Id, SqlDbType.Int, TabAvaliacao.Comprimento.Id);
                    SQLParam[10].Value = Ava_Id;
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

            public static void Delete(int Ava_Id)
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

                    SQLParam[0] = new SqlParameter("@" + TabAvaliacao.Id, SqlDbType.Int, TabAvaliacao.Comprimento.Id);
                    SQLParam[0].Value = Ava_Id;
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

    public class TabLinhaAvaliacao
    {
        public static SqlConnection SqlConnection = Connection.Metodo.GetConnection();

        public static string Id = "LAva_Id";
        public static string Ava_Id = "LAva_Ava_Id";
        public static string Qua_Id = "LAva_Qua_Id";
        public static string PesoQuadrante = "LAva_PesoQuadrante";
        public static string Questao = "LAva_Questao";
        public static string PesoQuestao = "LAva_PesoQuestao";
        public static string Classificacao = "LAva_Classificacao";
        public static string Pontos = "LAva_Pontos";

        public class Comprimento
        {
            public static int Id = 8;
            public static int Ava_Id = 8;
            public static int Qua_Id = 8;
            public static int PesoQuadrante = 10;
            public static int Questao = 300;
            public static int PesoQuestao = 10;
            public static int Classificacao = 10;
            public static int Pontos = 10;
        }

        public class Procedimento
        {
            public static string Select = "Proc_SelectLinhaAvaliacaosDynamic";
            public static string Insert = "Proc_InsertLinhaAvaliacao";
            public static string Update = "Proc_UpdateLinhaAvaliacao";
            public static string Delete = "Proc_DeleteLinhaAvaliacaosByLAva_Id";
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

            public static object Insert(int LAva_Ava_Id, int LAva_Qua_Id, int LAva_PesoQuadrante, string LAva_Questao, int LAva_PesoQuestao, int LAva_Classificacao, int LAva_Pontos)
            {
                object LAva_Id = null;

                SqlCommand SqlCommand = new SqlCommand(Procedimento.Insert, SqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] SQLParam = new SqlParameter[8];

                try
                {
                    if (SqlConnection.State != ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }

                    SQLParam[0] = new SqlParameter("@" + TabLinhaAvaliacao.Ava_Id, SqlDbType.Int, TabLinhaAvaliacao.Comprimento.Ava_Id);
                    SQLParam[0].Value = LAva_Ava_Id;
                    SqlCommand.Parameters.Add(SQLParam[0]);

                    SQLParam[1] = new SqlParameter("@" + TabLinhaAvaliacao.Qua_Id, SqlDbType.Int, TabLinhaAvaliacao.Comprimento.Qua_Id);
                    SQLParam[1].Value = LAva_Qua_Id;
                    SqlCommand.Parameters.Add(SQLParam[1]);

                    SQLParam[2] = new SqlParameter("@" + TabLinhaAvaliacao.PesoQuadrante, SqlDbType.Int, TabLinhaAvaliacao.Comprimento.PesoQuadrante);
                    SQLParam[2].Value = LAva_PesoQuadrante;
                    SqlCommand.Parameters.Add(SQLParam[2]);

                    SQLParam[3] = new SqlParameter("@" + TabLinhaAvaliacao.Questao, SqlDbType.NVarChar, TabLinhaAvaliacao.Comprimento.Questao);
                    SQLParam[3].Value = LAva_Questao;
                    SqlCommand.Parameters.Add(SQLParam[3]);

                    SQLParam[4] = new SqlParameter("@" + TabLinhaAvaliacao.PesoQuestao, SqlDbType.Int, TabLinhaAvaliacao.Comprimento.PesoQuestao);
                    SQLParam[4].Value = LAva_PesoQuestao;
                    SqlCommand.Parameters.Add(SQLParam[4]);

                    SQLParam[5] = new SqlParameter("@" + TabLinhaAvaliacao.Classificacao, SqlDbType.Int, TabLinhaAvaliacao.Comprimento.Classificacao);
                    SQLParam[5].Value = LAva_Classificacao;
                    SqlCommand.Parameters.Add(SQLParam[5]);

                    SQLParam[6] = new SqlParameter("@" + TabLinhaAvaliacao.Pontos, SqlDbType.Int, TabLinhaAvaliacao.Comprimento.Pontos);
                    SQLParam[6].Value = LAva_Pontos;
                    SqlCommand.Parameters.Add(SQLParam[6]);

                    SQLParam[7] = new SqlParameter();
                    SQLParam[7].ParameterName = "@" + TabLinhaAvaliacao.Id;
                    SQLParam[7].SqlDbType = SqlDbType.Int;
                    SQLParam[7].Direction = ParameterDirection.Output;
                    SqlCommand.Parameters.Add(SQLParam[7]);

                    SqlCommand.ExecuteNonQuery();

                    LAva_Id = SQLParam[7].Value;

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

                return LAva_Id;
            }

            public static void Update(int LAva_Ava_Id, int LAva_Qua_Id, int LAva_PesoQuadrante, string LAva_Questao, int LAva_PesoQuestao, int LAva_Classificacao, int LAva_Pontos, int LAva_Id)
            {
                SqlCommand SqlCommand = new SqlCommand(Procedimento.Update, SqlConnection);
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] SQLParam = new SqlParameter[8];

                try
                {
                    if (SqlConnection.State != ConnectionState.Open)
                    {
                        SqlConnection.Open();
                    }

                    SQLParam[0] = new SqlParameter("@" + TabLinhaAvaliacao.Ava_Id, SqlDbType.Int, TabLinhaAvaliacao.Comprimento.Ava_Id);
                    SQLParam[0].Value = LAva_Ava_Id;
                    SqlCommand.Parameters.Add(SQLParam[0]);

                    SQLParam[1] = new SqlParameter("@" + TabLinhaAvaliacao.Qua_Id, SqlDbType.Int, TabLinhaAvaliacao.Comprimento.Qua_Id);
                    SQLParam[1].Value = LAva_Qua_Id;
                    SqlCommand.Parameters.Add(SQLParam[1]);

                    SQLParam[2] = new SqlParameter("@" + TabLinhaAvaliacao.PesoQuadrante, SqlDbType.Int, TabLinhaAvaliacao.Comprimento.PesoQuadrante);
                    SQLParam[2].Value = LAva_PesoQuadrante;
                    SqlCommand.Parameters.Add(SQLParam[2]);

                    SQLParam[3] = new SqlParameter("@" + TabLinhaAvaliacao.Questao, SqlDbType.NVarChar, TabLinhaAvaliacao.Comprimento.Questao);
                    SQLParam[3].Value = LAva_Questao;
                    SqlCommand.Parameters.Add(SQLParam[3]);

                    SQLParam[4] = new SqlParameter("@" + TabLinhaAvaliacao.PesoQuestao, SqlDbType.Int, TabLinhaAvaliacao.Comprimento.PesoQuestao);
                    SQLParam[4].Value = LAva_PesoQuestao;
                    SqlCommand.Parameters.Add(SQLParam[4]);

                    SQLParam[5] = new SqlParameter("@" + TabLinhaAvaliacao.Classificacao, SqlDbType.Int, TabLinhaAvaliacao.Comprimento.Classificacao);
                    SQLParam[5].Value = LAva_Classificacao;
                    SqlCommand.Parameters.Add(SQLParam[5]);

                    SQLParam[6] = new SqlParameter("@" + TabLinhaAvaliacao.Pontos, SqlDbType.Int, TabLinhaAvaliacao.Comprimento.Pontos);
                    SQLParam[6].Value = LAva_Pontos;
                    SqlCommand.Parameters.Add(SQLParam[6]);

                    SQLParam[7] = new SqlParameter("@" + TabLinhaAvaliacao.Id, SqlDbType.Int, TabLinhaAvaliacao.Comprimento.Id);
                    SQLParam[7].Value = LAva_Id;
                    SqlCommand.Parameters.Add(SQLParam[7]);

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

            public static void Delete(int LAva_Id)
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

                    SQLParam[0] = new SqlParameter("@" + TabLinhaAvaliacao.Id, SqlDbType.Int, TabLinhaAvaliacao.Comprimento.Id);
                    SQLParam[0].Value = LAva_Id;
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
