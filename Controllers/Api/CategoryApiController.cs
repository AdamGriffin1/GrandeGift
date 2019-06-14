using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GrandeGift.Models;
using GrandeGift.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeGift.Controllers.Api
{
    public class CategoryApiController : Controller
    {
        private IDataService<Category> _categoryDataService;
        private IDataService<Hamper> _hamperDataService;

        public CategoryApiController(IDataService<Category> catServcie, IDataService<Hamper> hamperService)
        {
            _categoryDataService = catServcie;
            _hamperDataService = hamperService;
        }

        //web method - get all categories
        //we method URI
        [HttpGet("api/categories")]
        public JsonResult GetAll()
        {
            try
            {
                IEnumerable<Category> list = _categoryDataService.GetAll();
                return Json(list);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = ex.Message });
            }
        }

        //web method - get all products by category name
        [HttpGet("api/hampers")] //follow with url query ?name="VALUE"
        public JsonResult GetHampersByCategory(string name)
        {
            try
            {
                Category cat = _categoryDataService.GetSingle(c => c.Name.ToUpper() == name.ToUpper());
                if (cat != null)
                {
                    IEnumerable<Hamper> list = _hamperDataService.Query(p => p.CategoryId == cat.CategoryId);
                    return Json(list);
                }
                else
                {
                    return Json(new { message = "cannot find this category" });
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = ex.Message });
            }
        }
    }
}
