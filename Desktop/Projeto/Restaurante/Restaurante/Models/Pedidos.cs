using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Restaurante.Models
{
    public class Pedidos
    {
        private List<Pedido> pratos, bebidas;
        public List<Pedido> Pratos { get => pratos; set => pratos = value; }
        public List<Pedido> Bebidas { get => bebidas; set => bebidas = value; }
        ConexaoBD bd;

        public Pedidos()
        {
            Pratos = CarregarPedidos("Cozinha");
            Bebidas = CarregarPedidos("Copa");
        }//chama metodo Carregar pedidos
        public List<Pedido> CarregarPedidos(string onde)//carrega pedidos para serem entregues pelo garçom
        {
            SqlDataReader dados;
            List<Pedido> listaP = new List<Pedido>();
            using (bd = new ConexaoBD())
            {
                dados = bd.pesquisa(string.Format("Select * From Pedido_"+onde+" where situacao = 1"));
                while (dados.Read())
                {
                    Pedido p = new Pedido(
                        int.Parse(dados["Id"].ToString()),
                        char.Parse(dados["Situacao"].ToString()),
                        int.Parse(dados["Id_Mesa"].ToString())
                        );
                    listaP.Add(p);
                }
                return listaP;
            }
        }

    }
}