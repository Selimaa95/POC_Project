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
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace POC_Project.BL.Repository
{
    public class EmployeeRep : IEmployee
    {
        #region Prop
        private readonly ApplicationContext db;

        #endregion

        #region Ctor

        public EmployeeRep(ApplicationContext db)
        {
            this.db = db;
        }
        #endregion

        #region Methods

        public async Task<IEnumerable<Employee>> GetAllAsync(Expression<Func<Employee,bool>> filter)
        {
            if(filter != null)
            {
                return await db.Employee.Where(filter)
                    .Include("Department")  
                    .Include("District").ToListAsync();
            }
            else
            {
                //Without Filter.
                return await db.Employee
                    .Include("Department")
                    .Include("District").ToListAsync();
            }

        }
        public async Task<Employee> GetByIdAsync(Expression<Func<Employee, bool>> filter)
        {
           var data = await db.Employee.Where(filter)
                      .Include("Department")
                      .Include("District").FirstOrDefaultAsync();

            return data;
        }
        public async Task CreateAsync(Employee obj)
        {
            obj.CreationDate = DateTime.Now;
            await db.Employee.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee obj)
        {
            obj.UpdateData = DateTime.Now;
            obj.IsUpdated = true;
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee obj)
        {
            var oldData = db.Employee.Find(obj.Id);
            
            //Soft Delete.
            oldData.IsDeleted = true;
            oldData.DeleteData = DateTime.Now;
            db.Employee.Remove(oldData);
            await db.SaveChangesAsync();
        }

        /*public async Task<IEnumerable<Employee>> SearchAsync(Expression<Func<Employee, bool>> filter)
        {
            var data = await db.Employee.Where(filter).Include("Department").ToListAsync();
            return data;
        }*/
        #endregion
    }
}
