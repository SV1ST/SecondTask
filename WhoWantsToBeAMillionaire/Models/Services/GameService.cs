using System;
using System.Linq;
using System.Collections.Generic;
using WhoWantsToBeAMillionaire.Models.Repositories;

namespace WhoWantsToBeAMillionaire.Models.Services
{
    public class GameService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        private Dictionary<Question, Answer[]> _gameQuestions;

        public int NumberOfQuestions { get; set; }
        public int CurrentQuestionNumber { get; set; } = 1;
        public Dictionary<Question, Answer[]> GameQuestions
        {
            get
            {
                if (_gameQuestions == null)
                    _gameQuestions = GetGameQuestions();

                return _gameQuestions;
            }
            private set => _gameQuestions = value;
        }
        public Question CurrentQuestion { get => GameQuestions.Keys.FirstOrDefault(); }
        public Answer[] CurrentAnswers { get => GameQuestions[CurrentQuestion]; }
        public int MaxPrize { get; set; } = 1_000_000;
        public int CurrentPrize { get; set; }
        public int PrizeStep
        {
            get
            {
                return MaxPrize / NumberOfQuestions;
            }
        }
        public bool FiftyFiftyUsed { get; set; }
        public bool GameLost { get; set; }

        public GameService(IQuestionRepository questionRepository, IAnswerRepository answerRepository)
        {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
        }

        // TODO optimize
        public void ManageFiftyFifty()
        {
            FiftyFiftyUsed = true;

            var rnd = new Random();
            var wrongAnswer1 = CurrentAnswers.Where(a => a.IsCorrect == false).ToArray()[rnd.Next(3)];
            var wrongAnswer2 = CurrentAnswers.Where(a => a.IsCorrect == false && a != wrongAnswer1).ToArray()[rnd.Next(2)];

            int wrongIndex1 = Array.IndexOf(CurrentAnswers, wrongAnswer1);
            int wrongIndex2 = Array.IndexOf(CurrentAnswers, wrongAnswer2);

            CurrentAnswers[wrongIndex1] = null;
            CurrentAnswers[wrongIndex2] = null;
        }

        // this method will be called if the player's answer is correct to proceed to the next question
        public void RemoveFirstQuestion() => GameQuestions.Remove(GameQuestions.Keys.FirstOrDefault());

        public void ResetGame()
        {
            CurrentQuestionNumber = 1;
            GameQuestions = null;
            CurrentPrize = 0;
            GameLost = false;
        }

        // getting random Questions
        private Question[] GetQuestions()
        {
            var questions = new Question[NumberOfQuestions];
            Random rnd = new();

            for (int i = 0; i < NumberOfQuestions; i++)
            {
                int randomId = rnd.Next(1, _questionRepository.Questions.Count() + 1);

                if (!questions.Any(q => q?.Id == randomId) || questions.Length == 0)
                    questions[i] = _questionRepository.Questions.ToArray()[randomId - 1];
                else
                    i--;
            }

            return questions;
        }

        // getting answers for questions that were randomly selected by GetQuestions method
        private Dictionary<Question, Answer[]> GetGameQuestions()
        {
            var gameQuestions = new Dictionary<Question, Answer[]>();
            var questions = GetQuestions();

            for (int i = 0; i < questions.Length; i++)
            {
                var answers = _answerRepository.Answers.Where(a => a.QuestionId == questions[i].Id).ToArray();
                gameQuestions[questions[i]] = answers;
            }

            return gameQuestions;
        }
    }
}
