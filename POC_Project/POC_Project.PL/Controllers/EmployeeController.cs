using AutoMapper;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POC_Project.BL.Helper;
using POC_Project.BL.Interface;
using POC_Project.BL.Repository;
using POC_Project.BL.VModels;
using POC_Project.DAL.Entity;
using System;
using System.Linq.Expressions;
using System.Net.WebSockets;
using System.Runtime.Serialization;

namespace POC_Project.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Prop
        private readonly IEmployee employee;
        private readonly IMapper mapper;
        private readonly IDepartment department;
        private readonly ICity city;
        private readonly IDistrict district;

        #endregion

        #region Ctor
        public EmployeeController(IEmployee employee, IMapper mapper, IDepartment department, ICity city, IDistrict district)
        {
            this.employee = employee;
            this.mapper = mapper;
            this.department = department;
            this.city = city;
            this.district = district;
        }

        #endregion

        #region Actions
   
        public async Task<IActionResult> Index()
        {           
            var data = await employee.GetAllAsync(x => x.IsActive == true && x.IsDeleted == false);
            var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
            return View(result);
        }

        public async Task<IActionResult> Archive()
        {
            var data = await employee.GetAllAsync(x => x.IsActive == false || x.IsDeleted == true);
            var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
            return View(result);
        }

        public  IActionResult Create()
        {
            /*var data = await department.GetAllAsync();
            ViewBag.DepartmentList = new SelectList(data, "Id", "Name");*/
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM model)
        {
            try
            {
                //Validation
                if (ModelState.IsValid)
                {

                    //UploadFile
                    model.CvName = FileUploader.UploadFile("Docs", model.CV);
                    model.ImageName = FileUploader.UploadFile("Imgs", model.Image);

                    var result = mapper.Map<Employee>(model);
                    await employee.CreateAsync(result);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message; 
            }
           /* var data = await department.GetAllAsync();
            ViewBag.DepartmentList = new SelectList(data, "Id", "Name",model.Name);*/
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await employee.GetByIdAsync(x => x.IsActive == true && x.IsDeleted == false && x.Id == id);
            var result = mapper.Map<EmployeeVM>(data);
            return View(result);
        }

        public async Task<IActionResult> Update(int id)
        {
            var data = await employee.GetByIdAsync(x => x.IsActive == true && x.IsDeleted == false && x.Id == id);
            var result = mapper.Map<EmployeeVM>(data);
            /*var departments = await department.GetAllAsync();
            ViewBag.DepartmentList = new SelectList(departments, "Id", "Name",data.DepartmentId);*/
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Employee>(model);
                    await employee.UpdateAsync(result);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            /*var departments = await department.GetAllAsync();
            ViewBag.DepartmentList = new SelectList(departments, "Id", "Name",model.DepartmentId);*/
            return View(model);
        }
        
        public async Task<IActionResult> Delete(EmployeeVM obj)
        {
            var data = await employee.GetByIdAsync(x => x.IsActive == true && x.IsDeleted == false && x.Id == obj.Id);
            var result = mapper.Map<EmployeeVM>(data);
            return View(result);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(EmployeeVM obj)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    /*//RemoveFile.
                    FileUploader.RemoveFile("Docs", obj.CvName);
                    FileUploader.RemoveFile("Imgs", obj.ImageName);*/
                    var data = mapper.Map<Employee>(obj);
                    await employee.DeleteAsync(data);
                    return RedirectToAction("Index");             
                }
            }
            catch(Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return View();
        }


        #endregion

        #region Ajax

        [HttpPost]
        public async Task<IActionResult> GetCityByCountryId(int CntryId)
        {
            var Data = await city.GetCities(x => x.CountryId == CntryId);
            var Result = mapper.Map<IEnumerable<CityVM>>(Data);
            return Json(Result);
        }

        [HttpPost]
        public async Task<IActionResult> GetDistrictByCityId(int CtyId)
        {
            var Data = await district.GetDistricts(x => x.CityId == CtyId);
            var Result = mapper.Map<IEnumerable<DistrictVM>>(Data);
            return Json(Result);
        }

        #endregion
    }
}
