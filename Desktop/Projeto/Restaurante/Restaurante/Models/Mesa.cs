using System.Data.SqlClient;

namespace Restaurante.Models
{
    public class Mesa
    {
        private int id;
        private string nomecliente;
        private char situacao;
        private int numero;

        public int Id { get => id; set => id = value; }
        public string Nomecliente { get => nomecliente; set => nomecliente = value; }
        public char Situacao { get => situacao; set => situacao = value; }
        public int Numero { get => numero; set => numero = value; }

        ConexaoBD bd;

        public Mesa(int num,int i)
        {
            Id = num;
            using (bd = new ConexaoBD())
            {
                bd.CUD("UPDATE Mesa SET Numero = " + i + " Where Id =" + num);
            }
            using (bd = new ConexaoBD())
            {
                SqlDataReader dados = bd.pesquisa("Select * From Mesa Where Id =" + num);
                while (dados.Read())
                {
                    nomecliente = dados["Nome_Cliente"].ToString();
                    situacao = char.Parse(dados["Situacao"].ToString());
                    numero = int.Parse(dados["Numero"].ToString());
                }
            }
        }//carrega informações dessa mesa

        public void Oucupar(string nome)
        {
            using (bd = new ConexaoBD())
            {
                bd.CUD(string.Format("UPDATE Mesa SET Nome_Cliente = '{0}' , Situacao = 1 WHERE Id = {1}", nome, id));
                nomecliente = nome;
                situacao = char.Parse("1");
            }
        }//faz update do status para 1(oucupado) e adiciona o nome do cliente
        public char Desoucupar()
        {
            using (bd = new ConexaoBD())
            {
                bd.CUD(string.Format("UPDATE Mesa SET Nome_Cliente ='' ,Situacao = 0 WHERE Id = {0}", id));
                nomecliente = "";
            }
            using (bd = new ConexaoBD())
            {
                bd.CUD(string.Format("Delete From Pedido_Copa where Id_Mesa ="+id));
            }
            using(bd = new ConexaoBD())
                {
                bd.CUD(string.Format("Delete From Pedido_Cozinha where Id_Mesa =" + id));
            }
            return situacao = char.Parse("0");
        }//Limpa todos os pedidos relacionados a mesa (independente do status) e faz updade do status e nome do cliente
        public char Disponivel()
        {
            return situacao;
        }//verifica se está oucupada ou n;


    }
}