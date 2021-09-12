using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WhoWantsToBeAMillionaire.Models.Services;

namespace WhoWantsToBeAMillionaire.Controllers
{
    public class HomeController : Controller
    {
        private readonly GameService _gameService;

        public HomeController(GameService gameService) => _gameService = gameService;

        [HttpGet]
        public IActionResult Index()
        {
            _gameService.ResetGame();
            return View();
        }

        [HttpPost]
        public IActionResult Game(int numberOfQuestions, string fiftyFiftyUsed)
        {
            if (fiftyFiftyUsed == "true")
                _gameService.ManageFiftyFifty();
            if (_gameService.NumberOfQuestions == 0)
                _gameService.NumberOfQuestions = numberOfQuestions;

            return View(_gameService);
        }

        [HttpPost]
        public IActionResult AnswerResult(int answerIndex)
        {
            if (_gameService.CurrentAnswers[answerIndex].IsCorrect)
            {
                _gameService.CurrentQuestionNumber++;
                _gameService.RemoveFirstQuestion();
                _gameService.CurrentPrize += _gameService.PrizeStep;

                if (!_gameService.GameQuestions.Any())
                    return RedirectToAction("GameOver");
            }
            else
            {
                _gameService.GameLost = true;
                return RedirectToAction("GameOver");
            }

            return View(_gameService);
        }

        [HttpGet]
        public IActionResult GameOver()
        {
            return View(_gameService);
        }
    }
}
