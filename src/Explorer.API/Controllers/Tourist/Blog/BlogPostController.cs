using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.UseCases;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace Explorer.API.Controllers.Tourist.Blog
{
    //[Authorize(Policy = "touristPolicy")]
    [Route("api/blog/blogpost")]
    public class BlogPostController : BaseApiController
    {
        private readonly IBlogPostService _blogPostService;
        private static readonly HttpClient _client = new();
        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }
        //go
        [HttpGet]
        public async Task<ActionResult<PagedResult<BlogPostDto>>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = await GetAllBlogPosts(_client, page, pageSize);
            return result;
        }

        static async Task<ActionResult<PagedResult<BlogPostDto>>> GetAllBlogPosts(HttpClient httpClient, int page, int pageSize)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:8082/api/blog/blogpost?page={page}&pageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                var blogPosts = await response.Content.ReadFromJsonAsync<PagedResult<BlogPostDto>>();
                return blogPosts;
            }
            else
            {
                // Consider logging the response content here to see if the service is returning an error message
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
            }
        }

        //go
        [HttpGet("{blogPostId:int}")]
        public async Task<ActionResult<BlogPostDto>> GetById(int blogPostId)
        {
            var result = await GetBlogPostById(_client, blogPostId);
            return result;
        }

        static async Task<ActionResult<BlogPostDto>> GetBlogPostById(HttpClient httpClient, int blogPostId)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:8082/api/blog/blogpost/{blogPostId}");

            if (response.IsSuccessStatusCode)
            {
                var blogPost = await response.Content.ReadFromJsonAsync<BlogPostDto>();
                return blogPost;
            }
            else
            {
                // Consider logging the response content here to see if the service is returning an error message
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
            }
        }


        //go
        [HttpPost]
        public async Task<ActionResult<BlogPostDto>> Create([FromBody] BlogPostDto blog)
        {
            var result = await CreateBlogPost(_client, blog);
            return result;
        }

        static async Task<BlogPostDto> CreateBlogPost(HttpClient client, BlogPostDto blog)
        {
            var jsonContent = new StringContent
            (
                    JsonSerializer.Serialize(blog),
                    Encoding.UTF8,
                    "application/json"
                );

            HttpResponseMessage response = await _client.PostAsync(
                    "http://localhost:8082/blogpost", jsonContent
                 );

            var responseContent = await response.Content.ReadFromJsonAsync<BlogPostDto>();
            return responseContent;
        }
        //go
        [HttpPut("{id:int}")]
        public async Task<ActionResult<BlogPostDto>> Update(int id, [FromBody] BlogPostDto blogPost)
        {
            var result = await UpdateBlogPost(_client, id, blogPost);
            return result;
        }

        static async Task<ActionResult<BlogPostDto>> UpdateBlogPost(HttpClient httpClient, int id, BlogPostDto blogPost)
        {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync($"http://localhost:8082/api/blog/blogpost/{id}", blogPost);

            if (response.IsSuccessStatusCode)
            {
                var updatedBlogPost = await response.Content.ReadFromJsonAsync<BlogPostDto>();
                return updatedBlogPost;
            }
            else
            {
                // Consider logging the response content here to see if the service is returning an error message
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
            }
        }

        //go 
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await DeleteBlogPost(_client, id);
            return result;
        }

        static async Task<bool> DeleteBlogPost(HttpClient httpClient, int id)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync($"http://localhost:8082/blogpost/{id}");

            return response.IsSuccessStatusCode;
        }



        //KOMENTARI

        //go
        [HttpPost("{blogPostid:int}/comments")]
        public async Task<ActionResult<BlogPostDto>> AddComment(int blogPostid, [FromBody] BlogPostCommentDto blogPostComment)
        {
            var result = await AddCommentToBlogPost(_client, blogPostid, blogPostComment);
            return result;
        }

        static async Task<ActionResult<BlogPostDto>> AddCommentToBlogPost(HttpClient httpClient, int blogPostid, BlogPostCommentDto blogPostComment)
        {
            var json = JsonSerializer.Serialize(blogPostComment);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync($"http://localhost:8082/api/blog/blogpost/{blogPostid}/comments", content);

            if (response.IsSuccessStatusCode)
            {
                var blogPost = await response.Content.ReadFromJsonAsync<BlogPostDto>();
                return blogPost;
            }
            else
            {
                // Consider logging the response content here to see if the service is returning an error message
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
            }
        }

        //go
        [HttpPut("{blogPostId:int}/comments")]
        public async Task<ActionResult<BlogPostDto>> UpdateComment(int blogPostId, [FromBody] BlogPostCommentDto editedComment)
        {
            var result = await UpdateCommentForBlogPost(_client, blogPostId, editedComment);
            return result;
        }

        static async Task<ActionResult<BlogPostDto>> UpdateCommentForBlogPost(HttpClient httpClient, int blogPostId, BlogPostCommentDto editedComment)
        {
            var json = JsonSerializer.Serialize(editedComment);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync($"http://localhost:8082/api/blog/blogpost/{blogPostId}/comments", content);

            if (response.IsSuccessStatusCode)
            {
                var blogPost = await response.Content.ReadFromJsonAsync<BlogPostDto>();
                return blogPost;
            }
            else
            {
                // Consider logging the response content here to see if the service is returning an error message
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
            }
        }

        //go
        [HttpDelete("{blogPostId:int}/comments/{userId:int}/{creationTime:datetime}")]
        public async Task<ActionResult<BlogPostDto>> DeleteComment(int blogPostId, int userId, DateTime creationTime)
        {
            var result = await DeleteCommentForBlogPost(_client, blogPostId, userId, creationTime);
            return result;
        }

        static async Task<ActionResult<BlogPostDto>> DeleteCommentForBlogPost(HttpClient httpClient, int blogPostId, int userId, DateTime creationTime)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync($"http://localhost:8082/api/blog/blogpost/{blogPostId}/comments/{userId}/{creationTime}");

            if (response.IsSuccessStatusCode)
            {
                var blogPost = await response.Content.ReadFromJsonAsync<BlogPostDto>();
                return blogPost;
            }
            else
            {
                // Consider logging the response content here to see if the service is returning an error message
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
            }
        }




        //RATING

        //go
        [HttpPost("{blogPostid:int}/ratings")]
        public async Task<ActionResult<BlogPostDto>> AddRating(int blogPostid, [FromBody] BlogPostRatingDto blogPostRating)
        {
            var result = await AddRatingForBlogPost(_client, blogPostid, blogPostRating);
            return result;
        }

        static async Task<ActionResult<BlogPostDto>> AddRatingForBlogPost(HttpClient httpClient, int blogPostid, BlogPostRatingDto blogPostRating)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(blogPostRating), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync($"http://localhost:8082/api/blog/blogpost/{blogPostid}/ratings", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var blogPost = await response.Content.ReadFromJsonAsync<BlogPostDto>();
                return blogPost;
            }
            else
            {
                // Consider logging the response content here to see if the service is returning an error message
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
            }
        }

        //go
        [HttpDelete("{blogPostId:int}/ratings/{userId:int}")]
        public async Task<ActionResult<BlogPostDto>> DeleteRating(int blogPostId, int userId)
        {
            var result = await RemoveRatingForBlogPost(_client, blogPostId, userId);
            return result;
        }

        static async Task<ActionResult<BlogPostDto>> RemoveRatingForBlogPost(HttpClient httpClient, int blogPostId, int userId)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync($"http://localhost:8082/api/blog/blogpost/{blogPostId}/ratings/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var blogPost = await response.Content.ReadFromJsonAsync<BlogPostDto>();
                return blogPost;
            }
            else
            {
                // Consider logging the response content here to see if the service is returning an error message
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
            }
        }




    }
}
