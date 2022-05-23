using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Requests
    {
        [Key]
        public int id { get; set; }
        public int User1id { get; set; }
        public int User2id { get; set; }
        public User User1 { get; set; }
        public User User2 { get; set; }



    }
}
