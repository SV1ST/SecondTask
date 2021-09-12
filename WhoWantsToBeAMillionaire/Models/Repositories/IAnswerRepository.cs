using System.Linq;

namespace WhoWantsToBeAMillionaire.Models.Repositories
{
    public interface IAnswerRepository
    {
        public IQueryable<Answer> Answers { get; }
    }
}
