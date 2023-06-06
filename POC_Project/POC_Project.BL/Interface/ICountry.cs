using POC_Project.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace POC_Project.BL.Interface
{
    public interface ICountry
    {
        Task<IEnumerable<Country>> GetCountries(Expression<Func<Country,bool>> filter = null);
    }
}
