using System;

namespace Taskboard.DataAccess
{
	public interface IDataSource
	{
		IEntity Add(int id, IEntity entity);
		IEntity Update(int id, IEntity entity);
		IEntity Fetch(int id);
		void Delete(int id);
	}
}