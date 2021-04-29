using Abp.Domain.Uow;
using Blog.Models;
using Blog.persestant;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Blog.Controllers
{

    [Route("api/Blogs/{blogId}/Photos")]
  
    public class PhotosController:Controller
    {


        private readonly IHostingEnvironment host;

        private readonly BlogDbContext DB;
       

        public PhotosController(IHostingEnvironment host ,BlogDbContext DB  )
        {
            this.host = host;
            this.DB = DB;
            
            
        }

        [HttpPost]
        [Route("api/upload")]
        public  async Task<string> Upload(int blogid , IFormFile file)
        {

            var blog = this.DB.Blogs.Find(blogid);
           

            var uploadsfolderPath   =  Path.Combine(host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsfolderPath))
            {
                Directory.CreateDirectory(uploadsfolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
             var filePath  = Path.Combine(uploadsfolderPath, fileName);
             using(var stream = new FileStream(filePath , FileMode.Create))
            { 

                await file.CopyToAsync(stream);
            }


            var photo = new Photo() { FileName = fileName };
            blog.photo = photo;
            this.DB.SaveChangesAsync();
            return file.FileName;
        }



    }
}
