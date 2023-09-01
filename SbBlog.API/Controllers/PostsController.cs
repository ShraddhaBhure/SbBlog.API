using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SbBlog.API.Data;
using SbBlog.API.Models.Entities;
using SbBlog.API.Models.Entities.DTO;

namespace SbBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly BlogDbContext _context;

        public PostsController(BlogDbContext context)
        {
            _context = context;
        }

        // GET: api/posts
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _context.Posts.ToListAsync();
            return Ok(posts);
        }

        // GET: api/posts/{id}
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetPostById")]  
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id ==id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // POST: api/posts
        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostRequest addPostRequest)
        {
            //convert DTO to entity
            var post = new Post()
            {
                Title = addPostRequest.Title,
                Content = addPostRequest.Content,
                Summary = addPostRequest.Summary,
                UrlHandel = addPostRequest.UrlHandel,
                FeaturedImageUrl = addPostRequest.FeaturedImageUrl,
                Visible = addPostRequest.Visible,
                Author = addPostRequest.Author,
                PublishDate = addPostRequest.PublishDate,
            };

            post.Id = Guid.NewGuid();
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }


        ////PUT: api/posts/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost([FromRoute] Guid id, UpdatePostRequest updatePostRequest)
        {
           
            var existingPost = await _context.Posts.FindAsync(id);

            if(existingPost != null)
            {
                existingPost.Title = updatePostRequest.Title;
                existingPost.Content = updatePostRequest.Content;
                existingPost.Summary = updatePostRequest.Summary;
                existingPost.UrlHandel = updatePostRequest.UrlHandel;
                existingPost.FeaturedImageUrl = updatePostRequest.FeaturedImageUrl;
                existingPost.Visible = updatePostRequest.Visible;
                existingPost.Author = updatePostRequest.Author;
                existingPost.PublishDate = updatePostRequest.PublishDate;

                await _context.SaveChangesAsync();
                return Ok(existingPost);
            }

            return NotFound();
        }



        // DELETE: api/posts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var existingPost = await _context.Posts.FindAsync(id);

            if (existingPost != null)
            {
                _context.Posts.Remove(existingPost);
                await _context.SaveChangesAsync();
                return Ok(existingPost);

            }
            return NotFound();
       
        }
    }


}

