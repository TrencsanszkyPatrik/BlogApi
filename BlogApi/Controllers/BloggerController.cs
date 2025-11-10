using BlogApi.Models;
using BlogApi.Models.DtoS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloggerController : ControllerBase
    {
        [HttpPost]
        public ActionResult AddNewBlogger(AddBloggerDto blogger)
        {
            try
            {
                var NewBlogger = new Blogger
                {
                    Name = blogger.Name,
                    Password = blogger.Password,
                    Email = blogger.Email
                };

                using (var context = new BlogDbContext())
                {
                    if(NewBlogger != null)
                    {
                        context.bloggers.Add(NewBlogger);
                        context.SaveChanges();
                        return StatusCode(201, new {message = "Sikeres hozzáadás", result = NewBlogger});

                    }

                    return NotFound(new {message = "Nincs blogger.", result = ""});
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message, result = ""});
            }
        }

        [HttpGet]
        public ActionResult GetAllBlogger()
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    return Ok(new { message = "Sikeres lekérdezés", result = context.bloggers.ToList() });   
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, result = "" });
            }
        }

        [HttpGet("byid")]
        public ActionResult GetBloggerById(int id)
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var blogger = context.bloggers.FirstOrDefault(b => b.Id == id);
                    if (blogger != null)
                    {
                        return Ok(new {message = "Sikeres lekérdézés", result = blogger});
                    }
                    return NotFound(new { message = "Nincs ilyen blogger.", result = "" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, result = "" });
            }
        }

        [HttpDelete]
        public ActionResult DeleteBlogger(int id)
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var blogger = context.bloggers.FirstOrDefault(b => b.Id == id);

                    if(blogger != null)
                    {
                        context.bloggers.Remove(blogger);
                        context.SaveChanges();
                        return Ok(new { message = "Sikeres törlés.", result = blogger });
                    }
                    return NotFound(new { message = "Nincs ilyen blogger.", result = "" });

                }

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, result = "" });
            }
        }

        [HttpPut]
        public ActionResult UpdateBlogger(int id, UpdateBloggerDto updatedBlogger)
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var blogger = context.bloggers.FirstOrDefault(b => b.Id == id);
                    if (blogger != null)
                    {

                        blogger.Name = updatedBlogger.Name;
                        blogger.Password = updatedBlogger.Password;
                        blogger.Email = updatedBlogger.Email;
                        context.bloggers.Update(blogger);
                        context.SaveChanges();
                        return Ok(new { message = "Sikeres frissítés.", result = blogger });

                    }
                    return NotFound(new { message = "Nincs ilyen blogger.", result = "" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, result = "" });
            }
        }
    }
}
