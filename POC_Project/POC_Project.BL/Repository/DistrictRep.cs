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
    public class DistrictRep : IDistrict    
    {
        #region Prop
        private readonly ApplicationContext db;

        #endregion
        
        #region Ctor

        public DistrictRep(ApplicationContext db)
        {
            this.db = db;
        }
        #endregion

        #region Action
        public async Task<IEnumerable<District>> GetDistricts(Expression<Func<District, bool>> filter =null)
        {
            if (filter != null)
            {
                return await db.District.Where(filter).Include("City").ToListAsync();
            }
            else
            {
                return await db.District.Include("City").ToListAsync();
            }
        }


        #endregion
    }
}
