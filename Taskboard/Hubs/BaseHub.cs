using System.Linq;
using Microsoft.AspNet.SignalR;
using Taskboard.DataAccess;

namespace Taskboard.Hubs
{
	public abstract class BaseHub<T> : Hub
	{
		protected IDataRepository<T> _repository;


		public abstract void Add();

		public void Remove(T entity)
		{
			_repository.Delete(entity);
			Clients.All.remove(entity);
		}

		public void GetAll()
		{
			var entities = _repository.GetWhere(t => true).ToArray();
			Clients.Caller.getAll(entities);
		}

		public virtual void Update(T entity)
		{
			_repository.Update(entity);
			Clients.AllExcept(Context.ConnectionId).update(entity);
		}
	}
}