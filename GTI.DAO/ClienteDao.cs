using GTI.API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GTI.DAO
{
    public class ClienteDao
    {
        #region Inserir
        //------------------------------------------------------------------------------------------
        public int Inserir(Cliente cliente)
        {
            int id;
            SqlConnection con = null;
             
            try
            {
                string strConexao = ConfigurationManager.ConnectionStrings["conAlex"].ConnectionString;
                con = new SqlConnection(strConexao);
                con.Open();


                SqlCommand cmd = new SqlCommand("sp_InsertCliente", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@Cpf", cliente.Cpf);
                cmd.Parameters.AddWithValue("@Rg", cliente.Rg);
                cmd.Parameters.AddWithValue("@OrgaoExpedicao", cliente.OrgaoExpedicao);
                cmd.Parameters.AddWithValue("@UfExpedicao", cliente.UfExpedicao);
                cmd.Parameters.AddWithValue("@Sexo", cliente.Sexo);
                cmd.Parameters.AddWithValue("@EstadoCivil", cliente.EstadoCivil);
                cmd.Parameters.AddWithValue("@DataNascimento", Convert.ToDateTime(cliente.DataNascimento));
                cmd.Parameters.AddWithValue("@DataExpedicao", Convert.ToDateTime(cliente.DataExpedicao));

                cmd.Parameters.AddWithValue("@Endereco", cliente.Logradouro);
                cmd.Parameters.AddWithValue("@Complemento", cliente.Complemento);
                cmd.Parameters.AddWithValue("@Numero", cliente.Numero);
                cmd.Parameters.AddWithValue("@Bairro", cliente.Bairro);
                cmd.Parameters.AddWithValue("@Cep", cliente.Cep);
                cmd.Parameters.AddWithValue("@Cidade", cliente.Cidade);
                cmd.Parameters.AddWithValue("@Uf", cliente.Uf);

                id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return id;
        }
        #endregion
        //---------------------------------------------------------------------------------------
        #region Atualizar
        public void Atualizar(Cliente cliente)
        {
            SqlConnection con = null;

            try
            {
                string strConexao = ConfigurationManager.ConnectionStrings["conAlex"].ConnectionString;
                con = new SqlConnection(strConexao);
                con.Open();

                SqlCommand cmd = new SqlCommand("sp_UpdateCliente", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", cliente.Id);
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@Cpf", cliente.Cpf);
                cmd.Parameters.AddWithValue("@Rg", cliente.Rg);
           
                cmd.Parameters.AddWithValue("@UfExpedicao", cliente.UfExpedicao);
                cmd.Parameters.AddWithValue("@Sexo", cliente.Sexo);
                cmd.Parameters.AddWithValue("@EstadoCivil", cliente.EstadoCivil);
                cmd.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);
                cmd.Parameters.AddWithValue("@DataExpedicao", cliente.DataExpedicao);


                cmd.Parameters.AddWithValue("@Endereco", cliente.Logradouro);
                cmd.Parameters.AddWithValue("@Complemento", cliente.Complemento);
                cmd.Parameters.AddWithValue("@Numero", cliente.Numero);
                cmd.Parameters.AddWithValue("@Bairro", cliente.Bairro);
                cmd.Parameters.AddWithValue("@Cidade", cliente.Cidade);
                cmd.Parameters.AddWithValue("@Cep", cliente.Cep);
                cmd.Parameters.AddWithValue("@Uf", cliente.Uf);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        #endregion
        //---------------------------------------------------------------------------------------
        #region Excluir
        public void Excluir(int id)
        {
            SqlConnection con = null;

            try
            {
                string strConexao = ConfigurationManager.ConnectionStrings["conAlex"].ConnectionString;
                con = new SqlConnection(strConexao);
                con.Open();

                SqlCommand cmd = new SqlCommand("sp_DeleteCliente", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        #endregion
        //---------------------------------------------------------------------------------------
        #region Listar
        public List<Cliente> Listar()
        {
            List<Cliente> lstCliente = new List<Cliente>();

            string strConexao = ConfigurationManager.ConnectionStrings["conAlex"].ConnectionString;

            using (SqlConnection con = new SqlConnection(strConexao))
            using (SqlCommand cmd = new SqlCommand("sp_GetClientes", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                using (SqlDataReader radClientes = cmd.ExecuteReader())
                {
                    while (radClientes.Read())
                    {
                        Cliente cliente = new Cliente
                        {
                            Id = radClientes["ClienteID"] != DBNull.Value ? Convert.ToInt32(radClientes["ClienteID"]) : 0,
                            Nome = radClientes["Nome"] as string ?? string.Empty,
                            Cpf = radClientes["CPF"] as string ?? string.Empty,
                            Rg = radClientes["RG"] as string ?? string.Empty,
                            OrgaoExpedicao = radClientes["OrgaoExpedicao"] as string ?? string.Empty,
                            UfExpedicao = radClientes["UfCliente"] as string ?? string.Empty,
                            Sexo = radClientes["Sexo"] as string ?? string.Empty,
                            EstadoCivil = radClientes["EstadoCivil"] as string ?? string.Empty,
                            DataNascimento = radClientes["DataNascimento"] != DBNull.Value
                                                ? Convert.ToDateTime(radClientes["DataNascimento"])
                                                : DateTime.MinValue,
                            DataExpedicao = radClientes["DataExpedicao"] != DBNull.Value
                                                ? Convert.ToDateTime(radClientes["DataExpedicao"])
                                                : DateTime.MinValue,

                            Logradouro = radClientes["Logradouro"] as string ?? string.Empty,
                            Complemento = radClientes["Complemento"] as string ?? string.Empty,
                            Numero = radClientes["Numero"] as string ?? string.Empty,
                            Bairro = radClientes["Bairro"] as string ?? string.Empty,
                            Cidade = radClientes["Cidade"] as string ?? string.Empty,
                            Cep = radClientes["CEP"] as string ?? string.Empty,
                            Uf = radClientes["UfEndereco"] as string ?? string.Empty,
                        };

                        lstCliente.Add(cliente);
                    }
                }
            }

            return lstCliente;
        }

        #endregion
        //---------------------------------------------------------------------------------------
        #region Obter
        public Cliente Obter(int id)
        {
            SqlConnection con = null;
            Cliente cliente = null;

            try
            {
                string strConexao = ConfigurationManager.ConnectionStrings["conAlex"].ConnectionString;
                con = new SqlConnection(strConexao);
                con.Open();

                SqlCommand cmd = new SqlCommand("sp_GetClienteId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);

                SqlDataReader radClientes = cmd.ExecuteReader();


                if (radClientes.Read())
                {
                    cliente = new Cliente();

                    cliente.Id = Convert.ToInt32(radClientes["Id"]);
                    cliente.Nome = (string)radClientes["Nome"];
                    cliente.Cpf = (string)radClientes["Cpf"];
                    cliente.Rg = (string)(radClientes["Rg"]);
                    cliente.OrgaoExpedicao = (string)(radClientes["UfExpedicao"]);
                    cliente.UfExpedicao = (string)radClientes["Uf"];
                    cliente.Sexo = (string)radClientes["Sexo"];
                    cliente.EstadoCivil = (string)radClientes["EstadoCivil"];
                    cliente.DataNascimento = Convert.ToDateTime(radClientes["DataNascimento"]);
                    cliente.DataExpedicao = Convert.ToDateTime(radClientes["DataExpedicao"]);

                    cliente.Logradouro = (string)radClientes["Endereco"];
                    cliente.Complemento = (string)radClientes["Complemento"];
                    cliente.Numero = (string)radClientes["Numero"];
                    cliente.Bairro = (string)radClientes["Bairro"];
                    cliente.Cidade = (string)radClientes["Cidade"];
                    cliente.Cep = (string)radClientes["Cep"];
                    cliente.Uf = (string)radClientes["Uf"];
                }

            }
            finally
            {
                if (con != null)
                    con.Close();
            }

            return cliente;
        }
        #endregion
        //---------------------------------------------------------------------------------------
    }
}