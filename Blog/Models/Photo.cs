using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{

    [Table("Photos")]
    public class Photo
    {

        public int  Id  { get; set; }

        [Required]
        public string FileName { get; set; }





    }
}
