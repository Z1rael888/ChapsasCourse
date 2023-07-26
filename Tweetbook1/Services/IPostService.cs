using Tweetbook1.Contracts.V1;
using Tweetbook1.Domain;

namespace Tweetbook1.Services;

public interface IPostService
{
    List<Post> GetPosts();

    Post GetPostById(Guid postId);

    bool UpdatePost(Post postToUpdate);
    bool DeletePost(Guid postId);
}