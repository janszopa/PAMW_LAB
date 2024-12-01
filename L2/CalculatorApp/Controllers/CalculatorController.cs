using Microsoft.AspNetCore.Mvc;
using CalculatorApp.Models;

namespace CalculatorApp.Controllers
{
    public class CalculatorController : Controller
    {
        // GET: /Calculator/
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Calculator/
        [HttpPost]
        public IActionResult Index(CalculatorModel model)
        {
            if (ModelState.IsValid)
            {
                switch (model.Operation) {
                    case "+": 
                        model.Result = model.Number1 + model.Number2; 
                        break;
                    case "-":
                        model.Result = model.Number1 - model.Number2;
                        break;
                    case "*":
                        model.Result = model.Number1 * model.Number2;
                        break;
                    case "/":
                        model.Result = model.Number1 / model.Number2;
                        break;
                }
            }

            return View(model);
        }
    }
}