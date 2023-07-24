﻿using Microsoft.AspNetCore.Mvc;
using Tweetbook1.Contracts.V1;
using Tweetbook1.Contracts.V1.Requests;
using Tweetbook1.Contracts.V1.Responses;
using Tweetbook1.Domain;

namespace Tweetbook1.Controllers.V1;

public class PostsController: Controller
{
    private List<Post> _posts;

    public PostsController()
    {
        _posts = new List<Post>();
        for (int i = 0; i < 5; i++)
        {
            _posts.Add(new Post { Id = Guid.NewGuid().ToString()});
        }
    }
    
    [HttpGet(ApiRoutes.Posts.GetAll)]
    
    public IActionResult GetAll()
    {
        return Ok(_posts);
    }

    [HttpPost(ApiRoutes.Posts.Create)]
    public IActionResult Create([FromBody] CreatePostRequest postRequest)
    {
        var post = new Post { Id = postRequest.Id };
        
        if (string.IsNullOrEmpty(post.Id))
            post.Id = Guid.NewGuid().ToString();
        
        _posts.Add(post);

        var baseurl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUri = baseurl + "/" + ApiRoutes.Posts.Get.Replace("postId", post.Id);

        var response = new PostResponse { Id = post.Id };
        return Created(locationUri,response);
    }
    
}