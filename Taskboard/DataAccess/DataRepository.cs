using System.Collections.Generic;

namespace Taskboard.DataAccess
{
	public class DataRepository
	{
		private IDataSource _source;

		public DataRepository(IDataSource source)
		{
			_source = source;
		}

		public IEntity Add(IEntity entity)
		{
			return _source.Add(entity.Id, entity);
		}

		public IEntity Update(IEntity entity)
		{
			return _source.Update(entity.Id, entity);
		}

		public IEntity Fetch(int ID)
		{
			return _source.Fetch(ID);
		}

		//public IList<IEntity> FetchWhere<IEntity>()
		//{
		//	return null;
		//}

		public void Delete(IEntity entity)
		{
			_source.Delete(entity.Id);
		}
	}
}