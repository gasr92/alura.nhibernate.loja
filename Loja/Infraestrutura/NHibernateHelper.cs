using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.Reflection;

namespace Loja.Infraestrutura
{
    public class NHibernateHelper
    {
        private static ISessionFactory _fabrica = CriaSectionFactory();

        private static ISessionFactory CriaSectionFactory()
        {
            Configuration cfg = RecuperaConfiguracao();
            return cfg.BuildSessionFactory();
        }

        public static Configuration RecuperaConfiguracao()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(Assembly.GetExecutingAssembly());

            return cfg;
        }

        public static void GeraEsquema()
        {
            var cfg = RecuperaConfiguracao();
            new SchemaExport(cfg).Create(true,true);
        }

        public static ISession AbreSession()
        {
            return _fabrica.OpenSession();
        }
    }
}
