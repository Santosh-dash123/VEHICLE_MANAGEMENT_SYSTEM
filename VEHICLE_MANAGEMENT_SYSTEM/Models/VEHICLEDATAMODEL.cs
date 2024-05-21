using Microsoft.AspNetCore.Mvc.Rendering;

namespace VEHICLE_MANAGEMENT_SYSTEM.Models
{
    public class VEHICLEDATAMODEL
    {
        public int ID { get; set; }
        public string? NAME { get; set; }
        public int D_ID { get; set; }
        public int B_ID { get; set; }
        public int TOFVC { get; set; }
        public int VCNAME { get; set; }
        public int NOOFVC { get; set; }
        public string? FILE_ { get; set; }
        public List<SelectListItem> SelectDistrict { get; set; }
        public List<SelectListItem> SelectBlock { get; set; }
        public List<SelectListItem> SelectTofvc { get; set; }
        public List<SelectListItem> SelectVcname { get; set; }
        //public int SelectNoofvc { get; set; }

    }
}
