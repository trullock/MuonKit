using NHibernate;

namespace NHibernate
{
	public static class Extensions
	{
		/// <summary>
		/// Evict a collection of entities from the session
		/// </summary>
		/// <param name="session"></param>
		/// <param name="entities">The entities to evict</param>
		public static void EvictMany(this ISession session, params object[] entities)
		{
			foreach (var obj in entities)
				session.Evict(obj);
		}

		/// <summary>
		/// Save a collection of entities from the session
		/// </summary>
		/// <param name="session"></param>
		/// <param name="entities">The entities to save</param>
		public static void SaveMany(this ISession session, params object[] entities)
		{
			foreach (var obj in entities)
				session.Save(obj);
		}
	}
}