using Loja.DAO;
using Loja.Entidades;
using Loja.Infraestrutura;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Transform;
using System;
using System.Collections.Generic;

namespace Loja
{
    class Program
    {
        static void Main(string[] args)
        {
            //NHibernateHelper.GeraEsquema();

            //Configuration cfg = NHibernateHelper.RecuperaConfiguracao();
            //ISessionFactory sessionFactory = cfg.BuildSessionFactory();
            //ISession session = sessionFactory.OpenSession();

            //Usuario u = new Usuario();
            //u.Nome = "Jessica";

            //ITransaction transacao = session.BeginTransaction();
            //session.Save(u);
            //transacao.Commit();

            //session.Close();


            //ISession session = NHibernateHelper.AbreSession();

            //var uRobson = new Usuario();
            //uRobson.Nome = "Robson";

            //var uDAO = new UsuarioDAO(session);
            //uDAO.Adiciona(uRobson);

            //session.Close();



            //var cat = new Categoria();
            //cat.Nome = "Roupas";

            //var p = new Produto();
            //p.Nome = "Camiseta";
            //p.Preco = 10;
            //p.Categoria = cat;

            //ISession session = NHibernateHelper.AbreSession();
            //ITransaction transacao = session.BeginTransaction();

            //session.Save(cat);
            //session.Save(p);

            //transacao.Commit();
            //session.Close();





            //ISession session = NHibernateHelper.AbreSession();

            //var cat = session.Load<Categoria>(1);
            //var prods = cat.Produtos;

            //session.Close();

            //ListarProdutos();

            //BuscarProdutoCOmCriteria();

            //TesteComCache();

            //TesteVenda();

            TestePessoa();

            Console.ReadKey();
            
        }

        private static void TestePessoa()
        {
            var p = new PessoaFisica();
            p.Nome = "Gabriel";
            p.Cpf = "123.456.789-00";

            var j = new PessoaJuridica();
            j.Nome = "Empresa Teste";
            j.Cnpj = "00.000.000/0000-00";

            ISession s = NHibernateHelper.AbreSession();
            //ITransaction trans = s.BeginTransaction();

            var uDAO = new UsuarioDAO(s);
            uDAO.Adiciona(p);
            uDAO.Adiciona(j);



            //trans.Commit();
            s.Close();
            Console.Read();

        }

        private static void TesteVenda()
        {
            ISession s = NHibernateHelper.AbreSession();
            ITransaction trans = s.BeginTransaction();

            var cli = s.Get<Usuario>(1);
            var venda = new Venda();
            venda.Cliente = cli;

            var p1 = s.Get<Produto>(1);
            var p2 = s.Get<Produto>(2);

            venda.Produtos.Add(p1);
            venda.Produtos.Add(p2);

            s.Save(venda);

            trans.Commit();
            s.Close();
            Console.Read();
        }

        private static void TesteComCache()
        {
            ISession s1 = NHibernateHelper.AbreSession();
            ISession s2 = NHibernateHelper.AbreSession();

            s1.CreateQuery("from Usuario").SetCacheable(true).List<Usuario>();
            s2.CreateQuery("from Usuario").SetCacheable(true).List<Usuario>();

            s1.Close();
            Console.Read();
        }

        private static void BuscarProdutoCOmCriteria()
        {
            ISession session = NHibernateHelper.AbreSession();
            ProdutoDAO dao = new ProdutoDAO(session);

            var prods = dao.BuscaPorNomePrecoMinimoCategoria("Camiseta",1,"");

            foreach(var p in prods)
                Console.WriteLine($"{p.Nome} - {p.Preco} - {p.Categoria.Nome}");
        }


        private static void ListarProdutos()
        {
            ISession session = NHibernateHelper.AbreSession();
            ITransaction transacao = session.BeginTransaction();

            //case sensitive, pois é o nome da classe
            //string hql = "from Produto p where p.Preco > :minimo and p.Categoria.Nome = :catNome order by p.Nome";
            //IQuery query = session.CreateQuery(hql);
            //query.SetParameter("minimo",44.68);
            //query.SetParameter("catNome","Roupas"); 




            //string hql = "select p.Categoria, count(p) from Produto p group by p.Categoria";
            //IQuery query = session.CreateQuery(hql);
            //IList<Object[]> res = query.List<Object[]>();

            //IList<ProdutosPorCategoria> rel = new List<ProdutosPorCategoria>();
            //foreach(Object[] r in res)
            //{
            //    var pc = new ProdutosPorCategoria();
            //    pc.Categoria = (Categoria)r[0];
            //    pc.NumeroDePedidos = (long)r[1];

            //    rel.Add(pc);
            //}

            string hql = "select p.Categoria as Categoria, count(p) as NumeroDePedidos from Produto p group by p.Categoria";
            IQuery query = session.CreateQuery(hql);
            query.SetResultTransformer(Transformers.AliasToBean<ProdutosPorCategoria>());

            IList<ProdutosPorCategoria> rel = query.List<ProdutosPorCategoria>();


            // desta maneira, já é feito um mapeamento




            //IList<Produto> produtos = query.List<Produto>();

            //foreach(Produto p in produtos)
            //{ 
            //    Console.WriteLine(p.Nome);
            //}

            transacao.Commit(); 
            session.Close();
        }
    }

    public class ProdutosPorCategoria
    {
        public Categoria Categoria { get; set; }
        public long NumeroDePedidos { get; set; }
    }

}
