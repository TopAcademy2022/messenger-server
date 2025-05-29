using System;
using System.Collections.Generic;
using System.Linq;
using MessengerServer.Domain.Repositories;
using MessengerServer.Infrastructure.Persistence;

#pragma warning disable CS0162 // Обнаружен недостижимый код

namespace MessengerServer.Infrastructure.Repositories
{
	public class BaseRepository<T> : IRepository<T> where T : class
	{
		private AppDbContextBase _appDbContextBase;

		public BaseRepository(AppDbContextBase appDbContextBase)
		{
			this._appDbContextBase = appDbContextBase;
		}

		public bool Add(T entity)
		{
			try
			{
				this._appDbContextBase.Set<T>().Add(entity);
				return true;
			}
			catch (Exception ex)
			{
				throw;
			}

			return false;
		}

		public T GetById(int id)
		{
			try
			{
				T? entity = this._appDbContextBase.Set<T>().Find(id);

				if (entity != null)
				{
					return entity;
				}
			}
			catch (Exception ex)
			{
				throw;
			}

			#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
			return default;
			#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
		}

		public IEnumerable<T> GetAll()
		{
			try
			{
				return this._appDbContextBase.Set<T>();
			}
			catch (Exception ex)
			{
				throw;
			}

			return Enumerable.Empty<T>();
		}

		public bool UpdateById(T entity)
		{
			try
			{
				this._appDbContextBase.Set<T>().Update(entity);
				return true;
			}
			catch (Exception ex)
			{
				throw;
			}

			return false;
		}

		public bool DeleteById(string id)
		{
			try
			{
				T? entity = this._appDbContextBase.Set<T>().Find(id);

				if (entity != null)
				{
					this._appDbContextBase.Set<T>().Remove(entity);
					return true;
				}
			}
			catch (Exception ex)
			{
				throw;
			}

			return false;
		}
	}
}

#pragma warning restore CS0162 // Обнаружен недостижимый код