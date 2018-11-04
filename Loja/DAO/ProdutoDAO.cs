using Loja.Entidades;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.DAO
{
    public class ProdutoDAO
    {

        private ISession session;

        public ProdutoDAO(ISession session)
        {
            this.session = session;
        }

        public void Adiciona(Produto produto)
        {
            ITransaction transacao = session.BeginTransaction();
            session.Save(produto);
            transacao.Commit();
        }

        public Produto BuscaPorID(int id)
        {
            return session.Get<Produto>(id);
        }

        public IList<Produto> BuscaPorNomePrecoMinimoCategoria(string p_nome, decimal p_valMinimo, string p_categoria)
        {
            ICriteria criteria = session.CreateCriteria<Produto>();

            if(!string.IsNullOrEmpty(p_nome))
                criteria.Add(Restrictions.Eq("Nome",p_nome));

            if(p_valMinimo > 0)
                criteria.Add(Restrictions.Ge("Preco", p_valMinimo));

            if(!string.IsNullOrEmpty(p_categoria))
            {
                ICriteria crtCat = criteria.CreateCriteria("Categoria"); // categoria do Produto
                crtCat.Add(Restrictions.InsensitiveLike("Nome",p_categoria));
            }

            var result = criteria.List<Produto>();

            return result;
        }
    }
}
