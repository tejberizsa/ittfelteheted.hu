namespace IttFelTeheted.API.Models
{
    public class Vote
    {
        public int VoterId { get; set; }
        public int VotedId { get; set; }
        public User Voter { get; set; }
        public Answer Voted { get; set; }
        public bool IsCorrect { get; set; }
    }
}