using Microsoft.EntityFrameworkCore;

namespace WhoWantsToBeAMillionaire.Models
{
    [Keyless]
    public class Answer
    {
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
    }
}
