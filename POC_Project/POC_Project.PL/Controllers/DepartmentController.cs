using AutoMapper;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using POC_Project.BL.Interface;
using POC_Project.BL.Repository;
using POC_Project.BL.VModels;
using POC_Project.DAL.Entity;
using System.Net.WebSockets;

namespace POC_Project.PL.Controllers
{
    public class DepartmentController : Controller
    {
        #region Prop
        private readonly IDepartment department;
        private readonly IMapper mapper;


        #endregion

        #region Ctor
        public DepartmentController(IDepartment department, IMapper mapper)
        {
            this.department = department;
            this.mapper = mapper;
        }

        #endregion

        #region Actions
   
        public async Task<IActionResult> Index()
        {
            var data = await department.GetAllAsync();

            var result = mapper.Map<IEnumerable<DepartmentVM>>(data);

            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentVM model)
        {
            try
            {
                //Validation
                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Department>(model);
                    await department.CreateAsync(result);

                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message; 
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await department.GetByIdAsync(id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }

        public async Task<IActionResult> Update(int id)
        {
            var data = await department.GetByIdAsync(id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DepartmentVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Department>(model);
                    await department.UpdateAsync(result);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return View(model);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var data = await department.GetByIdAsync(id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await department.DeleteAsync(id);
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
    }
}
