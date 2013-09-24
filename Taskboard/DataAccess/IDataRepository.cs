using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Taskboard.DataAccess
{
	public interface IDataRepository<T, K>
	{
		void Add(T entity);
		void Delete(T entity);
		T Get(K id);
		void Update(T entity);
		IList<T> GetWhere(Expression<Func<T, bool>> whereCondition);
	}
}