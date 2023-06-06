using POC_Project.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_Project.BL.Interface
{
    public interface IDepartment
    {
        Task<IEnumerable<Department>> GetAllAsync();

        Task<Department> GetByIdAsync(int id);

        Task CreateAsync(Department obj);

        Task UpdateAsync(Department obj);

        Task DeleteAsync(int id);

    }
}
 