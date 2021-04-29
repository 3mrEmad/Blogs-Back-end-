using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class BlogPosts
    {

        public int Id { get; set; }
     
        public string  Creatotr { get; set; }
 
        public string Title { get; set; }
       
        public string Body { get; set; }

      
        public DateTime Dt { get; set; }
        public Photo photo { get; set; }

    }
}
