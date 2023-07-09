using System;
using eCart.API.Models;
using eCart.API.Services;

namespace eCart.API.Data.Services.UoW
{
	public interface IUnitOfWork: IDisposable
	{
		IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

		Task<int> Complete();
    }
}

