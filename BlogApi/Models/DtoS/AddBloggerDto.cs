using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Models.DtoS
{
   
    public class AddBloggerDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
