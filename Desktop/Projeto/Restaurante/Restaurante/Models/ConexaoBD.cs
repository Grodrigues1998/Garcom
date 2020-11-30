using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Restaurante.Models
{
    public class ConexaoBD:IDisposable
    {
        private readonly SqlConnection conexao;
        SqlDataReader dados;
        public ConexaoBD()
        {
            conexao = new SqlConnection(@"Data source= CLELIA\MSSQLSERVER01 ; Integrated Security= SSPI ; Initial Catalog= SistemaGarconBD");
            if(conexao.State== ConnectionState.Closed)
            conexao.Open();
        }//abre conexao local.
        public void Dispose()
        {
            if (conexao.State == ConnectionState.Open)
                conexao.Close();
        }//fecha conexão ao terminar de usar.

        public SqlDataReader pesquisa(string comando)
        {
                SqlCommand cmdcomando = new SqlCommand(comando, conexao);
                dados = cmdcomando.ExecuteReader();
                return dados;
        }//recebe string de pesquisa dos models.
        public void CUD(string comando)
        {
                var CmdComando = new SqlCommand
                {
                    CommandText = comando,
                    CommandType = CommandType.Text,
                    Connection = conexao
                };
                CmdComando.ExecuteNonQuery();

        }//recebe string de Create, update ou Delete dos models.
    }
}