namespace IttFelTeheted.API.Models
{
    public class PostFollow
    {
        public int FollowerId { get; set; }
        public int FollowedId { get; set; }
        public User Follower { get; set; }
        public Post Followed { get; set; }
    }
}