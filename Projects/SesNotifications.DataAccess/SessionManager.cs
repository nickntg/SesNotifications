using System.Data;
using NHibernate;

namespace SesNotifications.DataAccess
{
	/// Borrowed paradigm from http://lostechies.com/nelsonmontalvo/2007/03/30/simple-nhibernate-example-part-4-session-management/
	/// <summary>
	///     This is the main class used for data access. It manages and provides access to NHibernate sessions. When handling
	///     web a web request
	///     sessions are stored in the HttpContext to be reused. Otherwise they are kept in the CallContext
	/// </summary>
	public sealed class SessionManager
	{
		private const string SessionKey = "SesNotifications.UoW.Session";
		private static ISessionFactory _sessionFactory;

        public static SessionManager Manager { get; private set; }

        public static void Build(ISessionFactory sessionFactory)
        {
            Manager = new SessionManager(sessionFactory);
        }

		private static ISession ThreadSession
		{
			get => (ISession)CallContext.GetData(SessionKey);
			set => CallContext.SetData(SessionKey, value);
		}

        private static ITransaction Transaction => ((ISession) CallContext.GetData(SessionKey))?.Transaction;

        private SessionManager(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

		public void BeginTransaction(IsolationLevel isolationLevel)
		{
			if (Transaction == null || !Transaction.IsActive)
			{
				GetSession().BeginTransaction(isolationLevel);
            }
		}

		public void CloseSession()
		{
			var session = ThreadSession;
			ThreadSession = null;

			if (session != null && session.IsOpen)
			{
				session.Close();
			}
		}

		public void CommitTransaction()
		{
            if (Transaction != null && !Transaction.WasCommitted && !Transaction.WasRolledBack)
            {
                Transaction.Commit();
            }
		}

		public ISession GetSession()
		{
			var session = ThreadSession;

			if (session == null)
			{
				session = _sessionFactory.OpenSession();
				ThreadSession = session;
			}

			return ThreadSession;
		}

		public void RollbackTransaction()
		{
			try
			{
				if (Transaction != null && !Transaction.WasCommitted && !Transaction.WasRolledBack)
				{
					Transaction.Rollback();
				}
			}
			catch (HibernateException)
			{
			}
			finally
			{
				CloseSession();
			}
		}
    }
}