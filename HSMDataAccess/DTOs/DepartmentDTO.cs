using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSMDataAccess.DTOs
{
    public class DepartmentDTO
    {
        public string ID { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? HeadOf { get; set; }
        public DepartmentDTO()
        {

        }
        public DepartmentDTO(
            string id,
            string name,
            string? headOf)
                {
                    ID = id;
                    Name = name;
                    HeadOf = headOf;
                }
    }

}
