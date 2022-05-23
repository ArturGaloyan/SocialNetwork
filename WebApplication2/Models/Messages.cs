using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Messages
    {
        [Key]
        public int id { get; set; }
        public int User1id { get; set; }
        public int User2id { get; set; }
        public string text { get; set; }
        public int status { get; set; }

        public User User1 { get; set; }
        public User User2 { get; set; }


    }
}
