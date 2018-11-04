namespace Loja.Entidades
{
    // a classe será abstrata, pois SEMPRE será pai de PessoFisica ou Juridica
    public abstract class Usuario
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
    }
}
