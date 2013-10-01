using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Taskboard.DataAccess
{
	public interface IDataRepository<T>
	{
		void Add(T entity);
		void Delete(T entity);
		T Get(string id);
		void Update(T entity);
		IList<T> GetWhere(Func<T, bool> whereCondition);
	}
}