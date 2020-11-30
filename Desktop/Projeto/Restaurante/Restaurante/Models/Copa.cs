using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Restaurante.Models
{
    public class Copa
    {
        private List<Pedido> bebidas;
        public List<Pedido> Bebidas { get => bebidas; set => bebidas = value; }
        ConexaoBD bd;
        public Copa ()
        {
            SqlDataReader dados;
            Bebidas = new List<Pedido>();
            using (bd = new ConexaoBD())
            {
                dados = bd.pesquisa(string.Format("Select * From Pedido_Copa where situacao = 0"));
                while (dados.Read())
                {
                    Pedido p = new Pedido(
                        int.Parse(dados["Id"].ToString()),
                        char.Parse(dados["Situacao"].ToString()),
                        int.Parse(dados["Id_Mesa"].ToString())
                        );
                    Bebidas.Add(p);
                }
            }
        }//carrega todos os pedidos que estão na fila de espera

    }
}