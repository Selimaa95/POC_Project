using POC_Project.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace POC_Project.BL.Interface
{
    public interface IEmployee
    {
        Task<IEnumerable<Employee>> GetAllAsync(Expression<Func<Employee,bool>> filter = null);

        Task<Employee> GetByIdAsync(Expression<Func<Employee, bool>> filter);

       // Task<IEnumerable<Employee>> SearchAsync(Expression<Func<Employee, bool>> filter);

        Task CreateAsync(Employee obj);

        Task UpdateAsync(Employee obj);

        Task DeleteAsync(Employee obj);

    }
}
 