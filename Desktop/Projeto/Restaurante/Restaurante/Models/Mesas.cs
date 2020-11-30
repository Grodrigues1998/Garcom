using System.Collections.Generic;
using System.Data.SqlClient;
namespace Restaurante.Models
{
    public class Mesas
    {
        ConexaoBD BD;

        private List<Mesa> m = new List<Mesa>();
        public List<Mesa> M { get => m; set => m = value; }

        public Mesas()//adiciona as messas a uma lista para facilitar o preenchimento na View Mesas
        {
            using ( BD = new ConexaoBD())
            {
                SqlDataReader dados = BD.pesquisa("Select * From Mesa");
                int i = 1;
                while (dados.Read())
                {
                    Mesa mesa = new Mesa(int.Parse(dados["Id"].ToString()),i++);
                    M.Add(mesa);
                }
            }
        }

        
    }
}