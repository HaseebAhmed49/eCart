using System;
using eCart.API.Models;

namespace eCart.API.Services
{
	public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> ListAllAsync();
	}
}

