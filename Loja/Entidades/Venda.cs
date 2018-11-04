using System.Collections.Generic;

namespace Loja.Entidades
{
    public class Venda
    {
        public virtual int Id { get; set; }
        public virtual Usuario Cliente { get; set; }
        public virtual IList<Produto> Produtos { get; set; }

        public Venda()
        {
            this.Produtos = new List<Produto>();
        }
    }
}
