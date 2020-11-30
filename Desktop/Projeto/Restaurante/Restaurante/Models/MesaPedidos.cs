using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Restaurante.Models
{
    public class MesaPedidos
    {
        private int mesa;
        private string cliente;
        private int numero;
        List<PedidoInfo> pedidos = new List<PedidoInfo>();
        
        ConexaoBD bd;

        public int Mesa { get => mesa; set => mesa = value; }
        public string Cliente { get => cliente; set => cliente = value; }
        public List<PedidoInfo> Pedidos { get => pedidos; set => pedidos = value; }
        public int Numero { get => numero; set => numero = value; }

        public MesaPedidos(int cod)
        {
            mesa = cod;
            ExibirPedidos();
        }
        public void ExibirPedidos()
        {
            SqlDataReader dados;
            PedidoInfo p;
            using (bd = new ConexaoBD())
            {
                dados = bd.pesquisa(string.Format("Select * from Pedido_Copa Where Id_Mesa = " + mesa));
                while (dados.Read())
                {
                    p = new PedidoInfo("Copa", "Bebida");
                    p.Id = int.Parse(dados["Id"].ToString());
                    p.Mesa = int.Parse(dados["Id_Mesa"].ToString());
                    p.Cliente = dados["Cliente"].ToString();
                    p.Situacao = char.Parse(dados["Situacao"].ToString());
                    if (int.Parse(dados[p.Oque + "_1"].ToString()) != 0)
                    {
                        p.P1 = int.Parse(dados[p.Oque + "_1"].ToString());
                    }
                    if (int.Parse(dados[p.Oque + "_2"].ToString()) != 0)
                    {
                        p.P2 = int.Parse(dados[p.Oque + "_2"].ToString());
                    }
                    if (int.Parse(dados[p.Oque + "_3"].ToString()) != 0)
                    {
                        p.P3 = int.Parse(dados[p.Oque + "_3"].ToString());
                    }
                    Pedidos.Add(p);
                }
            }
                using (bd = new ConexaoBD())
                {
                    dados = bd.pesquisa(string.Format("Select * from Pedido_Cozinha Where Id_Mesa = " + mesa));
                while (dados.Read())
                {
                    p = new PedidoInfo("Cozinha", "Prato");
                    p.Id = int.Parse(dados["Id"].ToString());
                    p.Mesa = int.Parse(dados["Id_Mesa"].ToString());
                    p.Cliente = dados["Cliente"].ToString();
                    p.Situacao = char.Parse(dados["Situacao"].ToString());
                    if (int.Parse(dados[p.Oque + "_1"].ToString()) != 0)
                    {
                        p.P1 = int.Parse(dados[p.Oque + "_1"].ToString());
                    }
                    if (int.Parse(dados[p.Oque + "_2"].ToString()) != 0)
                    {
                        p.P2 = int.Parse(dados[p.Oque + "_2"].ToString());
                    }
                    if (int.Parse(dados[p.Oque + "_3"].ToString()) != 0)
                    {
                        p.P3 = int.Parse(dados[p.Oque + "_3"].ToString());
                    }
                    Pedidos.Add(p);
                }
            }
        }//seleciona todos os pedidos relacionados a uma mesa.

    }
}