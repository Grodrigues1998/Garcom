using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Restaurante.Models
{
    public class Pedido
    {
        private int id;
        private int mesa;
        private char situacao;
        private int numero;


        public int Id { get => id; set => id = value; }

        public char Situacao { get => situacao; set => situacao = value; }
        public int Mesa { get => mesa; set => mesa = value; }
        public int Numero { get => numero; set => numero = value; }
        ConexaoBD bd;
        public Pedido(int i, char s,int m)
        {
            Id = i; Situacao = s; Mesa = m;
            using(bd =new ConexaoBD())
            {
                SqlDataReader dados;
                dados = bd.pesquisa("select Numero from Mesa where Id=" + Mesa);
                while (dados.Read())
                {
                    Numero = int.Parse(dados["Numero"].ToString());
                };
                
            }
        }//carrega informaçoes de um determinado pedido.
    }
}