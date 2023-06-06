using Microsoft.EntityFrameworkCore;
using POC_Project.BL.Interface;
using POC_Project.DAL.DataBase;
using POC_Project.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace POC_Project.BL.Repository
{
    public class CountryRep : ICountry
    {
        #region Prop

        private readonly ApplicationContext db;
        #endregion

        #region Ctor

        public CountryRep(ApplicationContext db)
        {
            this.db = db;
        }

        #endregion

        #region Action

        public async Task<IEnumerable<Country>> GetCountries(Expression<Func<Country, bool>> filter = null)
        {
            if (filter != null) 
            {
                return await db.Country.Where(filter).ToListAsync();
            }
            else
            {
                return await db.Country.ToListAsync();
            }
        }
        #endregion
    }
}
