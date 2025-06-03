using System;
using System.Collections.Generic;
using System.Linq;
using MessengerServer.Domain.Repositories;
using MessengerServer.Infrastructure.Persistence;

#pragma warning disable CS0162 // Обнаружен недостижимый код

namespace MessengerServer.Infrastructure.Repositories
{
	public class BaseRepository<TEntity, TDomain> : IRepository<TDomain>
		where TEntity : class
		where TDomain : class
	{
		private AppDbContextBase _appDbContextBase;

		public BaseRepository(AppDbContextBase appDbContextBase)
		{
			this._appDbContextBase = appDbContextBase;
		}

		public bool Add(TDomain entity)
		{
			try
			{
				this._appDbContextBase.Set<TEntity>().Add(entity);
				return true;
			}
			catch (Exception ex)
			{
				throw;
			}

			return false;
		}

		public TDomain GetById(int id)
		{
			try
			{
				TEntity? entity = this._appDbContextBase.Set<TEntity>().Find(id);

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

		public IEnumerable<TDomain> GetAll()
		{
			try
			{
				return this._appDbContextBase.Set<TEntity>();
			}
			catch (Exception ex)
			{
				throw;
			}

			return Enumerable.Empty<T>();
		}

		public bool UpdateById(TDomain entity)
		{
			try
			{
				this._appDbContextBase.Set<TEntity>().Update(entity);
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
				TEntity? entity = this._appDbContextBase.Set<TEntity>().Find(id);

				if (entity != null)
				{
					this._appDbContextBase.Set<TEntity>().Remove(entity);
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