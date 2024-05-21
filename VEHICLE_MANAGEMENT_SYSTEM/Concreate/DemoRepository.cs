using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VEHICLE_MANAGEMENT_SYSTEM.Abstract;
using VEHICLE_MANAGEMENT_SYSTEM.Data;
using VEHICLE_MANAGEMENT_SYSTEM.Entity;
using VEHICLE_MANAGEMENT_SYSTEM.Models;

namespace VEHICLE_MANAGEMENT_SYSTEM.Concreate
{
    public class DemoRepository:Interface
    {
        public vehicledb_context vehicledb_;
        public DemoRepository(vehicledb_context vehicledb)
        {
            vehicledb_ = vehicledb;
        }

        public int saveVehicle(VEHICLEDATAMODEL VEHICLEDATAMODEL)
        {
            int return_value = 0;
            if(VEHICLEDATAMODEL.ID != 0)
            {
                //EDIT
                var existing_vehicle = vehicledb_.VEHICLE.Where(v => v.ID == VEHICLEDATAMODEL.ID).FirstOrDefault();
                if (existing_vehicle != null)
                {
                    existing_vehicle.NAME = VEHICLEDATAMODEL.NAME;
                    existing_vehicle.D_ID = VEHICLEDATAMODEL.D_ID;
                    existing_vehicle.B_ID = VEHICLEDATAMODEL.B_ID;
                    existing_vehicle.TOFVC = VEHICLEDATAMODEL.TOFVC;
                    existing_vehicle.VCNAME = VEHICLEDATAMODEL.VCNAME;
                    existing_vehicle.NOOFVC = VEHICLEDATAMODEL.NOOFVC;
                    if(VEHICLEDATAMODEL.FILE_ != null)
                    {
                        existing_vehicle.FILE_ = VEHICLEDATAMODEL.FILE_;
                    }
                    else
                    {
                        existing_vehicle.FILE_ = existing_vehicle.FILE_;
                    }
                    vehicledb_.SaveChanges();
                }
                return_value = 2;
            }
            else
            {
                //ADD
                VEHICLE vh = new VEHICLE();
                vh.NAME = VEHICLEDATAMODEL.NAME;
                vh.D_ID = VEHICLEDATAMODEL.D_ID;
                vh.B_ID = VEHICLEDATAMODEL.B_ID;
                vh.TOFVC = VEHICLEDATAMODEL.TOFVC;
                vh.VCNAME = VEHICLEDATAMODEL.VCNAME;
                vh.NOOFVC = VEHICLEDATAMODEL.NOOFVC;
                vh.FILE_ = VEHICLEDATAMODEL.FILE_;
                vehicledb_.VEHICLE.Add(vh);
                vehicledb_.SaveChanges();
                return_value = 1;
            }
            return return_value;
        }

        public VEHICLE getVehicle(int Id)
        {
            var existing_vehicle = vehicledb_.VEHICLE.Where(v => v.ID == Id).FirstOrDefault();
            return existing_vehicle;
        }
        public int deleteVehicle(int Id)
        {
            int return_value = 0;
            var existing_vehicle = vehicledb_.VEHICLE.Where(v => v.ID == Id).FirstOrDefault();
            if(existing_vehicle != null)
            {
                vehicledb_.Remove(existing_vehicle);
                vehicledb_.SaveChanges();
                return_value = 1;
            }
            return return_value;
        }
        public List<VEHICLEINFO> getAllVehicles()
        {
            return vehicledb_.VEHICLEINFO.FromSqlRaw("SELECT * FROM VEHICLEINFO;").ToList();
        }

        public List<SelectListItem> SelectDistrict()
        {
            var districts = vehicledb_.DISTRICT.ToList();
            List<SelectListItem> select_district = new List<SelectListItem>();
            foreach(var district in districts)
            {
                select_district.Add(new SelectListItem()
                {
                    Value = district.ID.ToString(),
                    Text = district.NAME
                });
            }
            return select_district;
        }

        public List<SelectListItem> SelectBlock(string Id)
        {
            int id = int.Parse(Id);
            var blocks = vehicledb_.BLOCK.Where(BLOCK => BLOCK.D_ID == id).ToList();
            List<SelectListItem> select_block = new List<SelectListItem>();
            foreach(var block in blocks)
            {
                select_block.Add(new SelectListItem()
                {
                    Value= block.ID.ToString(),
                    Text = block.NAME
                });
            }
            return select_block;
        }
        
        public List<SelectListItem> SelectTofvc(string Id)
        {
            int id = int.Parse(Id);
            var tofvc = vehicledb_.TOFVC.Where(TOFVC => TOFVC.B_ID == id).ToList();
            List<SelectListItem> select_tofvc = new List<SelectListItem>();
            foreach(var tvc in tofvc)
            {
                select_tofvc.Add(new SelectListItem()
                {
                    Value = tvc.ID.ToString(),
                    Text = tvc.NAME
                });
            }
            return select_tofvc;
        }

        public List<SelectListItem> SelectVcname(string Id)
        {
            int id = int.Parse(Id);
            var vcname = vehicledb_.VCNAME.Where(VCNAME => VCNAME.T_ID == id).ToList();
            List<SelectListItem> select_VCNAME = new List<SelectListItem>();
            foreach(var vcn  in vcname)
            {
                select_VCNAME.Add(new SelectListItem()
                {
                    Value = vcn.ID.ToString(),
                    Text = vcn.NAME
                });
            }
            return select_VCNAME;
        }
        public int SelectNoofvc(string Id)
        {
            int id = int.Parse(Id);
            var data = vehicledb_.NOOFVC.Where(NOOFVC => NOOFVC.VCNAME_ID == id).Select(NOFVC => NOFVC.NOFVC).FirstOrDefault();
            return data;
        }

        //This method i will write for only call stored procedure
        public List<DISTRICT> GetDistrictUsingId(int Id)
        {
            var data = new List<DISTRICT>();
            if (Id != 0)
            {
                 data = vehicledb_.DISTRICT.FromSqlRaw("EXECUTE SEE_ALL_DIST_USING_ID @ID", new SqlParameter("@ID", Id)).ToList();
            }
            else
            {
                data = vehicledb_.DISTRICT.FromSqlRaw("EXECUTE SEE_ALL_DIST_USING_ID").ToList();
            }
            return data;
        }
    }
}
