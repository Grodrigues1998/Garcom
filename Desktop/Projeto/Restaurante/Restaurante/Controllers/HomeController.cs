using System;
using System.Web.Mvc;
using Restaurante.Models;
namespace Restaurante.Controllers
{
    public class HomeController : Controller
    {
        

        public ActionResult Index()
        {
            Mesas mesas = new Mesas();
            return View(mesas);
        }//pagina inicial para criar e escluir as mesas.
        [HttpPost]
        public ActionResult Index(string B)
        {
            Mesas mesas;
            if (B == "Create")
            {
                using (ConexaoBD bd = new ConexaoBD())
                {
                    bd.CUD("INSERT INTO Mesa (Nome_Cliente, Situacao, Numero) VALUES ('', '0', null)");
                }
            }
            if (B == "Dell")
            {
                mesas = new Mesas();
                for (int i = 0; i < mesas.M.Count; i++)
                {
                    mesas.M[i].Desoucupar();
                }
                using (ConexaoBD bd = new ConexaoBD())
                {
                    bd.CUD("Delete From Mesa");
                }
            }
            mesas = new Mesas();
            return View(mesas);
        }/*recebe o formulario dela mesma. 
        OBS: descobri que dava para fazer isso no ultimo dia*/

        public ActionResult Mesas()
        {
            ViewBag.message = "Mesas";
            Mesas mesas = new Mesas();
            return View(mesas);
        }//carrega as todas as mesas cadastradas
        [HttpPost]
        public ActionResult MesaInfo(string M)
        {
            Mesas mesas = new Mesas();
            int cod;
            ViewBag.Message = "Mesa " + M;
            if (M.Contains("."))
            {
                cod = int.Parse(M.Remove(0, 1));
            }
            else
            {
                mesas.M[int.Parse(M) - 1].Desoucupar();
                cod = int.Parse(M);
            }
            return View(mesas.M[cod - 1]);
        }//busca iformaçoes de uma determinada mesa
        [HttpPost]
        public ActionResult MesaPedido(int cod, int P1, int P2, int P3, int B1, int B2, int B3, string Nomecliente)
        {
            Mesas mesas = new Mesas();
          
            if (mesas.M[cod-1].Situacao != '1' && Nomecliente != "")
            {
                    mesas.M[cod-1].Oucupar(Nomecliente);
            }
            if (P1 + P2 + P3 != 0)
            {
                PedidoInfo p = new PedidoInfo("Cozinha", "Prato");
                p.Cadastrar(mesas.M[cod-1].Id, mesas.M[cod-1].Nomecliente, P1, P2, P3);
            }
            if (B1 + B2 + B3 != 0)
            {
                PedidoInfo p = new PedidoInfo("Copa", "Bebida");
                p.Cadastrar(mesas.M[cod-1].Id, mesas.M[cod-1].Nomecliente, B1, B2, B3);
            }
            ViewBag.Message = "Pedidos para a mesa " + cod;
            MesaPedidos mp = new MesaPedidos(mesas.M[cod-1].Id);
            return View(mp);
        }// Cria ou consulta pedidos relacionados a mesa
       
        public ActionResult Cozinha()
        {
            Pedidos pedido = new Pedidos();
            ViewBag.Message = "Cozinha";
            Cozinha c = new Cozinha();
            return View(c);
        }//Carrega todos os pratos
      
        public ActionResult Copa()//Carrega todas as bebidas
        {
            Pedidos pedido = new Pedidos();
            ViewBag.Message = "Copa";
            Copa c = new Copa();
            return View(c);
        }
        
        public ActionResult Pedidos()
        {
            Pedidos pedido = new Pedidos();
            return View(pedido);
        }//Carrega todos os pedidos
        [HttpPost]
        public ActionResult PedidosInfo(string Enviar)//Recebe e carrega as informaçoes de um pedido
        {
            PedidoInfo p;
            
            if (Enviar.Contains("p"))
            {
              p= new PedidoInfo("Cozinha", "Prato");
            }
            else
            {
              p= new PedidoInfo("Copa", "Bebida");
            }
            if (Enviar.Contains("."))
            {
                p.InformacoesP(int.Parse(Enviar.Remove(0, 2)));
                p.Atualizar();
            }
            else
            {
                p.InformacoesP(int.Parse(Enviar.Remove(0, 1)));
            }
            
            return View(p);
        }
    }
}