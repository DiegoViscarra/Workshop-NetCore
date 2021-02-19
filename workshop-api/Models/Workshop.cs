using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace workshop_api.Models
{
    public class Workshop
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
