using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using VEHICLE_MANAGEMENT_SYSTEM.Abstract;
using VEHICLE_MANAGEMENT_SYSTEM.Data;
using VEHICLE_MANAGEMENT_SYSTEM.Models;

namespace VEHICLE_MANAGEMENT_SYSTEM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Interface inter;
        vehicledb_context context;
        IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger,Interface inter,vehicledb_context _context,IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            this.inter = inter;
            context = _context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Vehicle(int id)
        {
            if(id != 0)
            {
                //DISPLAY FOR EDIT OPERATION LIKE ALL FIELD VALUE IS PRESENT
                VEHICLEDATAMODEL model = new VEHICLEDATAMODEL();
                var existing_vehicle = inter.getVehicle(id);
                model.ID = existing_vehicle.ID;
                model.NAME = existing_vehicle.NAME;
                model.D_ID = existing_vehicle.D_ID;
                model.B_ID = existing_vehicle.B_ID;
                model.TOFVC = existing_vehicle.TOFVC;
                model.VCNAME = existing_vehicle.VCNAME;
                model.NOOFVC = existing_vehicle.NOOFVC;
                model.FILE_ = existing_vehicle.FILE_;
                model.SelectDistrict = inter.SelectDistrict();
                model.SelectBlock = inter.SelectBlock(model.D_ID.ToString());
                model.SelectTofvc = inter.SelectTofvc(model.B_ID.ToString());
                model.SelectVcname = inter.SelectVcname(model.TOFVC.ToString());
                return View(model);
            }
            else
            {
                //DISPLAY FOR ADD OPERATION LIKE NO VALUE IS PRESENT
                VEHICLEDATAMODEL model = new VEHICLEDATAMODEL();
                model.SelectDistrict = inter.SelectDistrict();
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult Vehicle([FromForm] VEHICLEDATAMODEL vEHICLEDATAMODEL,IFormFile allinputpath)
        {
            if (vEHICLEDATAMODEL != null)
            {
                if (allinputpath != null && allinputpath.Length > 0)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(allinputpath.FileName);
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");


                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    string imagePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        allinputpath.CopyToAsync(stream);
                    }

                    vEHICLEDATAMODEL.FILE_ = "/images/" + uniqueFileName;
                }
                int count = inter.saveVehicle(vEHICLEDATAMODEL);
                if (count == 1)
                {
                    return Json(new { result = "Vehicle created successfully!" });
                }
                else
                {
                    return Json(new { result = "Vehicle updated successfully!" });
                }
            }
            return RedirectToAction("StudentData");
        }
        [HttpGet]
        public List<SelectListItem> GetBlock(string Id)
        {
            var block = inter.SelectBlock(Id);
            return block;
        }
        [HttpGet]
        public List<SelectListItem> GetTypeOfVehicle(string Id)
        {
            var tofvc = inter.SelectTofvc(Id);
            return tofvc;
        }
        [HttpGet]
        public List<SelectListItem> GetVehicleName(string Id)
        {
            var gvena = inter.SelectVcname(Id);
            return gvena;
        }
        [HttpGet]
        public int GetNoOfVehicle(string Id)
        {
            int no_of_vehicle = inter.SelectNoofvc(Id);
            return no_of_vehicle;
        }

        [HttpGet]
        public IActionResult ShowAllVehicles()
        {
            var list_of_vehicle = inter.getAllVehicles();
            return View(list_of_vehicle);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            int value = inter.deleteVehicle(id);
            if(value == 1)
            {
                return Json(new { result = "Vehicle deleted successfully!" });
            }
            return Json(new { result = "There is some error present please try again!" });
        }

        [HttpGet]
        public IActionResult GetVehiclePhotoUsingId(int id)
        {
            var image_url = inter.getVehicle(id);
            ViewBag.image_url = image_url.FILE_;
            ViewBag.extension = Path.GetExtension(ViewBag.image_url);
            return View();
        }

        //This action method for get data using stored procedure and pass parameter also

        [HttpGet]
        public IActionResult GetDistrictUsingId(int Id)
        {
            var data = inter.GetDistrictUsingId(Id);
            return View(data);
        }
    }
}
