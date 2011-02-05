using System;
using NHibernate;

namespace Godot.Infrastructure.Persistence
{
	public interface INHibernateSessionFactory : IDisposable
	{
		ISession CreateSession();
	}
}