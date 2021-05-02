using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_backend.Models
{
    public class notes
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime createddate { get; set; }
        public int createdby { get; set; }
        public DateTime updateddate { get; set; }
        public int updatedby { get; set; }
    }
}
