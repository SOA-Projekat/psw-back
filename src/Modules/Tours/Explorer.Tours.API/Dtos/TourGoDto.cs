using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourGoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; } 
        public int Status { get; set; } 
        public int UserId { get; set; }
        public List<TourPointDto> TourPoints { get; set; }
        public List<EquipmentDto> Equipments {  get; set; }
    }
}
