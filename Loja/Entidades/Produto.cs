namespace Loja.Entidades
{
    public class Produto
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual decimal Preco { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
