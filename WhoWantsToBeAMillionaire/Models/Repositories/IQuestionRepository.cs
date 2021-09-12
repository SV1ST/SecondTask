using System.Linq;

namespace WhoWantsToBeAMillionaire.Models.Repositories
{
    public interface IQuestionRepository
    {
        IQueryable<Question> Questions { get; }
    }
}
