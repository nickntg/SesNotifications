using NHibernate;
using NHibernate.Criterion;

namespace SesNotifications.DataAccess.Repositories
{
	public class Repository
	{
		public ISession Session { get; set; }

		public Repository()
		{
			Session = SessionManager.Manager.GetSession();
		}

		public Repository(ISession session)
		{
			Session = session;
		}
    }
}