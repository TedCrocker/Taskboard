using System;
using System.Collections.Generic;

namespace Taskboard.Data
{
	public interface IDataRepository<T> : IDisposable
	{
		void Add(T entity);
		void Delete(T entity);
		T Get(string id);
		void Update(T entity);
		IList<T> GetWhere(Func<T, bool> whereCondition);
		void Clear();
	}
}