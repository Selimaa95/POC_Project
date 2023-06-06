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
    public class CityRep : ICity
    {
        #region Prop
        private readonly ApplicationContext db;

        #endregion

        #region Ctor

        public CityRep(ApplicationContext db)
        {
            this.db = db;
        }
        #endregion

        #region Action
        public async Task<IEnumerable<City>> GetCities(Expression<Func<City, bool>> filter = null)
        {
            if (filter != null)
            {
                return await db.City.Where(filter).Include("Country").ToListAsync();
            }
            else
            {
                return await db.City.Include("Country").ToListAsync();
            }
        }
        #endregion
    }
}
