using Blog.Models;
using Blog.persestant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{


    [Route("api/Blogs")]
    [Produces("application/json")]
    public class Blogs : Controller
    {

        private readonly BlogDbContext DB;
        public Blogs(BlogDbContext DB)
        {
            this.DB = DB;
        }





        [HttpGet]
       
        public async Task<ActionResult<IEnumerable<BlogPosts>>> getBlogs()
        {

            return await this.DB.Blogs.ToListAsync();

        }






        [HttpGet("{id}")]
        public  async Task< ActionResult<BlogPosts> > getOneBlog(int id)
        {

            var blog = await this.DB.Blogs.FindAsync(id);
            if(blog == null)
            {
                return NotFound();
            }
           
            return blog;
        
        }




        [HttpPost]
        public async Task<ActionResult> PostBlog([FromBody]BlogPosts blog)
        {

             this.DB.Blogs.Add(blog);
             await DB.SaveChangesAsync();
            return CreatedAtAction("getBlogs", new { id = blog.Id }, blog);



        }




        [HttpPut("{id}")]
        public async Task<ActionResult<BlogPosts>> PutBlogPost(int id, [FromBody]BlogPosts UpdatedBlog)
        {

            if (id != UpdatedBlog.Id)
            {
                return BadRequest();
            }

            BlogPosts currentBlog = new BlogPosts();
            currentBlog = await this.DB.Blogs.FindAsync(id);
            if(currentBlog == null)
            {
                return NotFound("Sorry this blog not found");
            }
            currentBlog.Creatotr = UpdatedBlog.Creatotr;
            currentBlog.Body = UpdatedBlog.Body;
            currentBlog.Title = UpdatedBlog.Title;
            currentBlog.Dt = UpdatedBlog.Dt;
            await DB.SaveChangesAsync();
            return currentBlog;
           
        }





        private bool BlogPostExists(int id)
        {
            return DB.Blogs.Any(e => e.Id == id);
        }



        [HttpDelete("{blogId}")]
        public async Task<ActionResult<BlogPosts>> DeleteBlog(int blogId)
        {
            var blog = await this.DB.Blogs.FindAsync(blogId);
            if(blog == null)
            {
                return NotFound();
            }

             DB.Blogs.Remove(blog);
            await DB.SaveChangesAsync();
            return blog;

        }






    }
}
