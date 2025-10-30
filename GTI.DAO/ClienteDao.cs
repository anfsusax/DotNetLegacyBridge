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
        public int Inserir(Cliente cliente)
        {
            int id;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conAlex"].ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("sp_InsertCliente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@Cpf", cliente.Cpf);
                    cmd.Parameters.AddWithValue("@Rg", cliente.Rg);
                    cmd.Parameters.AddWithValue("@OrgaoExpedicao", cliente.OrgaoExpedicao);
                    cmd.Parameters.AddWithValue("@UfExpedicao", cliente.UfExpedicao);
                    cmd.Parameters.AddWithValue("@Sexo", cliente.Sexo);
                    cmd.Parameters.AddWithValue("@EstadoCivil", cliente.EstadoCivil);
                    cmd.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);
                    cmd.Parameters.AddWithValue("@DataExpedicao", cliente.DataExpedicao);

                    cmd.Parameters.AddWithValue("@Endereco", cliente.Logradouro);
                    cmd.Parameters.AddWithValue("@Complemento", cliente.Complemento);
                    cmd.Parameters.AddWithValue("@Numero", cliente.Numero);
                    cmd.Parameters.AddWithValue("@Bairro", cliente.Bairro);
                    cmd.Parameters.AddWithValue("@Cep", cliente.Cep);
                    cmd.Parameters.AddWithValue("@Cidade", cliente.Cidade);
                    cmd.Parameters.AddWithValue("@Uf", cliente.Uf);

                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return id;
        }
        #endregion

        #region Atualizar
        public void Atualizar(Cliente cliente)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conAlex"].ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("sp_UpdateCliente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ClienteID", cliente.Id);
                    cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@Cpf", cliente.Cpf);
                    cmd.Parameters.AddWithValue("@Rg", cliente.Rg);
                    cmd.Parameters.AddWithValue("@OrgaoExpedicao", cliente.OrgaoExpedicao);
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
            }
        }
        #endregion

        #region Excluir
        public void Excluir(int id)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conAlex"].ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("sp_DeleteCliente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClienteID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region Listar
        public List<Cliente> Listar()
        {
            List<Cliente> lstCliente = new List<Cliente>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conAlex"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetClientes", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente
                        {
                            Id = reader["ClienteID"] != DBNull.Value ? Convert.ToInt32(reader["ClienteID"]) : 0,
                            Nome = reader["Nome"] as string ?? string.Empty,
                            Cpf = reader["CPF"] as string ?? string.Empty,
                            Rg = reader["RG"] as string ?? string.Empty,
                            OrgaoExpedicao = reader["OrgaoExpedicao"] as string ?? string.Empty,
                            UfExpedicao = reader["UfCliente"] as string ?? string.Empty,
                            Sexo = reader["Sexo"] as string ?? string.Empty,
                            EstadoCivil = reader["EstadoCivil"] as string ?? string.Empty,
                            DataNascimento = reader["DataNascimento"] != DBNull.Value ? Convert.ToDateTime(reader["DataNascimento"]) : DateTime.MinValue,
                            DataExpedicao = reader["DataExpedicao"] != DBNull.Value ? Convert.ToDateTime(reader["DataExpedicao"]) : DateTime.MinValue,

                            Logradouro = reader["Logradouro"] as string ?? string.Empty,
                            Complemento = reader["Complemento"] as string ?? string.Empty,
                            Numero = reader["Numero"] as string ?? string.Empty,
                            Bairro = reader["Bairro"] as string ?? string.Empty,
                            Cidade = reader["Cidade"] as string ?? string.Empty,
                            Cep = reader["CEP"] as string ?? string.Empty,
                            Uf = reader["UfEndereco"] as string ?? string.Empty,
                        };
                        lstCliente.Add(cliente);
                    }
                }
            }
            return lstCliente;
        }
        #endregion

        #region Obter
        public Cliente Obter(int id)
        {
            Cliente cliente = null;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conAlex"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetClienteId", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClienteID", id);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cliente = new Cliente
                        {
                            Id = reader["ClienteID"] != DBNull.Value ? Convert.ToInt32(reader["ClienteID"]) : 0,
                            Nome = reader["Nome"] as string ?? string.Empty,
                            Cpf = reader["CPF"] as string ?? string.Empty,
                            Rg = reader["RG"] as string ?? string.Empty,
                            OrgaoExpedicao = reader["OrgaoExpedicao"] as string ?? string.Empty,
                            UfExpedicao = reader["UfCliente"] as string ?? string.Empty,
                            Sexo = reader["Sexo"] as string ?? string.Empty,
                            EstadoCivil = reader["EstadoCivil"] as string ?? string.Empty,
                            DataNascimento = reader["DataNascimento"] != DBNull.Value ? Convert.ToDateTime(reader["DataNascimento"]) : DateTime.MinValue,
                            DataExpedicao = reader["DataExpedicao"] != DBNull.Value ? Convert.ToDateTime(reader["DataExpedicao"]) : DateTime.MinValue,

                            Logradouro = reader["Logradouro"] as string ?? string.Empty,
                            Complemento = reader["Complemento"] as string ?? string.Empty,
                            Numero = reader["Numero"] as string ?? string.Empty,
                            Bairro = reader["Bairro"] as string ?? string.Empty,
                            Cidade = reader["Cidade"] as string ?? string.Empty,
                            Cep = reader["CEP"] as string ?? string.Empty,
                            Uf = reader["UfEndereco"] as string ?? string.Empty,
                        };
                    }
                }
            }
            return cliente;
        }
        #endregion
    }

}