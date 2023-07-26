using Microsoft.AspNetCore.Mvc;
using Tweetbook1.Contracts.V1;
using Tweetbook1.Contracts.V1.Requests;
using Tweetbook1.Contracts.V1.Responses;
using Tweetbook1.Domain;
using Tweetbook1.Services;

namespace Tweetbook1.Controllers.V1;

public class PostsController: Controller
{
    private readonly IPostService _postService;
   // private readonly List<Post> _posts; 
   //added that field to the service

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }
    
    [HttpGet(ApiRoutes.Posts.GetAll)]
    public IActionResult GetAll()
    {
        return Ok(_postService.GetPosts());
    }
    [HttpGet(ApiRoutes.Posts.Get)]
    public IActionResult Get([FromRoute]Guid postId)
    {
        var post = _postService.GetPostById(postId);

        if (post == null)
            return NotFound();
        
        return Ok(post);//single post
    }
    
    [HttpPost(ApiRoutes.Posts.Create)]
    public IActionResult Create([FromBody] CreatePostRequest postRequest)
    {
        var post = new Post { Id = postRequest.Id };
        
        if (post.Id != Guid.Empty)
            post.Id = Guid.NewGuid();
        
        _postService.GetPosts().Add(post);

        var baseurl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUri = baseurl + "/" + ApiRoutes.Posts.Get.Replace("postId", post.Id.ToString());

        var response = new PostResponse { Id = post.Id };
        return Created(locationUri,response);
    }
    [HttpPut(ApiRoutes.Posts.Update)]
    public IActionResult Update([FromRoute]Guid postId,[FromBody] UpdatePostRequest request)

    {
        var post = new Post()
        {
            Id = postId,
            Name = request.Name
        };
        var updated = _postService.UpdatePost(post);

        if (updated)
            return Ok(post);
        
        return NotFound();
        
        return Ok(post);//single post
    }

    [HttpDelete(ApiRoutes.Posts.Delete)]
    public IActionResult Delete([FromRoute] Guid postId)
    {
        var deleted = _postService.DeletePost(postId);

        if (deleted)
            return NoContent();
        return NotFound();
    }
    
}