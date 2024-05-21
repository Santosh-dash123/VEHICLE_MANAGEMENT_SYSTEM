using Microsoft.AspNetCore.Mvc.Rendering;
using VEHICLE_MANAGEMENT_SYSTEM.Entity;
using VEHICLE_MANAGEMENT_SYSTEM.Models;

namespace VEHICLE_MANAGEMENT_SYSTEM.Abstract
{
    public interface Interface
    {
        //This method i will write for add a vehicle
        int saveVehicle(VEHICLEDATAMODEL VEHICLEDATAMODEL);

        //This method i will write for get a particular vehicle using a id
        VEHICLE getVehicle(int Id);

        //This method i will write for delete a particular vehicle using a id
        int deleteVehicle(int Id);

        //This method i will write for get all vehicle details
        List<VEHICLEINFO> getAllVehicles();

        //This all method i will write for geting value in a dropdownlist which is return List<SelectListItem>
        List<SelectListItem> SelectDistrict();
        List<SelectListItem> SelectBlock(string Id);
        List<SelectListItem> SelectTofvc(string Id);
        List<SelectListItem> SelectVcname(string Id);
        int SelectNoofvc(string Id);

        //Get the list of district using stored procedure
        List<DISTRICT> GetDistrictUsingId(int Id);
    }
}
