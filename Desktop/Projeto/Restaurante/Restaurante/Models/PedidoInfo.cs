using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Restaurante.Models
{
    public class PedidoInfo
    {
        private int id;
        private int mesa;
        private int p1;
        private int p2;
        private int p3;
        private string onde;
        private string oque;
        private char situacao;
        private string cliente;
        private int numero;


        ConexaoBD bd;

        public int Id { get => id; set => id = value; }
        public int Mesa { get => mesa; set => mesa = value; }
        public int P1 { get => p1; set => p1 = value; }
        public int P2 { get => p2; set => p2 = value; }
        public int P3 { get => p3; set => p3 = value; }
        public char Situacao { get => situacao; set => situacao = value; }
        public string Onde { get => onde; set => onde = value; }
        public string Cliente { get => cliente; set => cliente = value; }
        public string Oque { get => oque; set => oque = value; }
        public int Numero { get => numero; set => numero = value; }

        public PedidoInfo(string on,string oq)
        {
            Oque = oq;
            Onde = on;
        }
        public void Atualizar()
        {
            if (situacao == '0')
            {
                Situacao = '1';
                using (bd = new ConexaoBD())
                {
                    bd.CUD("Update Pedido_" + Onde + " SET Situacao = '1' Where Id=" + Id);
                }
            }
            else
            {
                Situacao = '2';
                using (bd = new ConexaoBD())
                {
                    bd.CUD("Update Pedido_" + Onde + " SET Situacao = '2' Where Id=" + Id);
                }
            }
        }//atualiza situação do pedido. ex: cozinhando, pronto e entregue.
        public void Excluir()
        {
            using (bd = new ConexaoBD())
            {
                bd.CUD("Delete From "+Onde+" Where Id=" + Id);
            }
        }//exclui pedido

        public void Cadastrar(int m,string c,int P1, int P2, int P3)
        {
            using (bd = new ConexaoBD())
            {
                string comando = string.Format("Insert into Pedido_" + Onde + " (Id_Mesa," + Oque + "_1, " + Oque + "_2, " + Oque + "_3, Situacao, Cliente) Values ({0},{1},{2},{3},'{4}','{5}')", m, P1, P2, P3, 0, c);
                bd.CUD(comando);
            }
        }//cadastra pedido.

        public void InformacoesP(int cod)
        {
            SqlDataReader dados;
            using (bd = new ConexaoBD())
            {
                dados = bd.pesquisa(string.Format("Select p.Id, p.Id_Mesa, p.Cliente, p.Situacao, p."+Oque+ "_1, p." + Oque + "_2, p." + Oque + "_3, m.Numero from Pedido_" + Onde+ " p , dbo.Mesa m Where p.Id = {0}", cod));
                while (dados.Read())
                {
                    Id = int.Parse(dados["Id"].ToString());
                    Mesa = int.Parse(dados["Id_Mesa"].ToString());
                    Cliente = dados["Cliente"].ToString();
                    Situacao = char.Parse(dados["Situacao"].ToString());
                    Numero = int.Parse(dados["Numero"].ToString());
                    if (int.Parse(dados[Oque + "_1"].ToString()) != 0)
                    {
                        P1 = int.Parse(dados[Oque +"_1"].ToString());
                    }
                    if (int.Parse(dados[Oque + "_2"].ToString()) != 0)
                    {
                        P2 = int.Parse(dados[Oque + "_2"].ToString());
                    }
                    if (int.Parse(dados[Oque + "_3"].ToString()) != 0)
                    {
                        P3 = int.Parse(dados[Oque + "_3"].ToString());
                    }
                }
            }
      
        }//busca as informações dos pedidos.

    }
}