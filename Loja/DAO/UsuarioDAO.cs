using Loja.Entidades;
using NHibernate;

namespace Loja.DAO
{
    public class UsuarioDAO
    {
        private ISession _session;

        public UsuarioDAO(ISession session)
        {
            this._session = session;
        }

        public void Adiciona(Usuario usuario)
        {
            ITransaction transacao = _session.BeginTransaction();
            _session.Save(usuario);
            transacao.Commit();
        }

        public Usuario Busca(int id)
        {
            return _session.Get<Usuario>(id);
        }
    }
}