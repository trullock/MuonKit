using System;

namespace MuonLab.NHibernate
{
	public abstract class Entity : IEntity
	{
		public virtual Guid Id { get; protected set; }

		protected Entity()
		{
			this.Id = Guid.NewGuid();
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;

			var entity = obj as Entity;
			if (ReferenceEquals(null, entity)) return false;

			return entity.Id.Equals(this.Id);
		}

		public override string ToString()
		{
			return base.ToString() + ": " + this.Id;
		}
	}
}