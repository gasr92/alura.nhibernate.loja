namespace Loja.Entidades
{
    public class PessoaJuridica : Usuario
    {
        public virtual string Cnpj { get; set; }
    }
}
