using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Ecom.Core.interfaces
{
    public interface IGenericRepositry<T> where T : class 
 
    {
        Task<IReadOnlyList<T>> GetAllAsync();
            Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T , Object>>[] includes);
        Task<T> GetByIdAsync(int id);
        // التعديل هنا: أضفنا بارامتر الـ params للاستماع للـ Includes ديناميكياً
        //Task<T> GetByIdAsync(int id, params Expression<Func<T, Object>>[] includes);

        Task<T> GetByAsync(int id);
        Task<T> GetByAsync(int id, params Expression<Func<T, Object>>[] includes);
        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> DeleteAsync(int id);

    }
}
