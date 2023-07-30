using Tweetbook1.Contracts.V1;
using Tweetbook1.Domain;

namespace Tweetbook1.Services;

public interface IPostService
{
    Task<List<Post>> GetPostsAsync();
    Task<bool> CreatePostAsync(Post post);
    Task<Post> GetPostByIdAsync(Guid postId);

    Task<bool> UpdatePostAsync(Post postToUpdate);
    Task<bool> DeletePostAsync(Guid postId);
}