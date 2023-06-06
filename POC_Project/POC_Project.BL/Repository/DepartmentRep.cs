using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using POC_Project.BL.Interface;
using POC_Project.BL.VModels;
using POC_Project.DAL.DataBase;
using POC_Project.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace POC_Project.BL.Repository
{
    public class DepartmentRep : IDepartment
    {
        #region Prop
        private readonly ApplicationContext db;

        #endregion

        #region Ctor

        public DepartmentRep(ApplicationContext db)
        {
            this.db = db;
        }
        #endregion

        #region Methods

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            var data =  await db.Department.ToListAsync();
            return data;
        }
        public async Task<Department> GetByIdAsync(int id)
        {
           var data = await db.Department.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;
        }
        public async Task CreateAsync(Department obj)
        {
            
            await db.Department.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department obj)
        {
           db.Entry(obj).State = EntityState.Modified;

            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var oldData = db.Department.Find(id);

            db.Department.Remove(oldData);
            await db.SaveChangesAsync();
        }
        #endregion
    }
}
