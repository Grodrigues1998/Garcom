using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Restaurante.Models
{
    public class Cozinha
    {
        private List<Pedido> pratos;
        public List<Pedido> Pratos { get => pratos; set => pratos = value; }
        ConexaoBD bd;
        public Cozinha()
        {
            SqlDataReader dados;
            Pratos = new List<Pedido>();
            using (bd = new ConexaoBD())
            {
                dados = bd.pesquisa(string.Format("Select * From Pedido_Cozinha where situacao = 0"));
                while (dados.Read())
                {
                    Pedido p = new Pedido(
                        int.Parse(dados["Id"].ToString()),
                        char.Parse(dados["Situacao"].ToString()),
                        int.Parse(dados["Id_Mesa"].ToString())
                        );
                    Pratos.Add(p);
                }
            }
        }//carrega todos os pedidos que estão na fila de espera

    }
}