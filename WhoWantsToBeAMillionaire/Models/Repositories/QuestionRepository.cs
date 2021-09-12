using System.Linq;

namespace WhoWantsToBeAMillionaire.Models.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly MillionaireDbContext _context;

        public QuestionRepository(MillionaireDbContext context) => _context = context;

        public IQueryable<Question> Questions => _context.Questions;
    }
}
