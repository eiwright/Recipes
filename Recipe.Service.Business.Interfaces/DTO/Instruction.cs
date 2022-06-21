using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Service.Business.Interfaces.DTO
{
    public class Instruction
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
    }
}
