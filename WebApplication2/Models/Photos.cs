using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Photos
    {
        [Key]
        public int id { get; set; }
        public int userId { get; set; }
        public string photo { get; set; }
        public string desc { get; set; }

        public User user { get; set; }
    }
}
