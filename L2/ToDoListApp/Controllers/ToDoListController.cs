using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using ToDoListApp.Models;
using ToDoListApp.Helpers;

namespace ToDoListApp.Controllers
{
    public class TodoListController : Controller
    {
        // Klucz sesji dla listy todo
        private const string SessionKey = "TodoList";

        // Pobierz listę zadań z sesji lub utwórz nową listę, jeśli nie istnieje
        private List<TodoModel> GetTodoList()
        {
            var todoList = HttpContext.Session.GetObjectFromJson<List<TodoModel>>(SessionKey);
            if (todoList == null)
            {
                todoList = new List<TodoModel>();
                HttpContext.Session.SetObjectAsJson(SessionKey, todoList);
            }
            return todoList;
        }

        // GET: /Todo/
        public IActionResult Index()
        {
            var todoList = GetTodoList();
            return View(todoList);
        }

        // POST: /Todo/Add
        [HttpPost]
        public IActionResult Add(string task)
        {
            if (!string.IsNullOrEmpty(task))
            {
                var todoList = GetTodoList();
                todoList.Add(new TodoModel { Task = task, IsCompleted = false });
                HttpContext.Session.SetObjectAsJson(SessionKey, todoList);
            }
            return RedirectToAction("Index");
        }

        // POST: /Todo/Complete
        [HttpPost]
        public IActionResult Complete(int index)
        {
            var todoList = GetTodoList();
            if (index >= 0 && index < todoList.Count)
            {
                todoList[index].IsCompleted = true;
                HttpContext.Session.SetObjectAsJson(SessionKey, todoList);
            }
            return RedirectToAction("Index");
        }

        // POST: /Todo/Delete
        [HttpPost]
        public IActionResult Delete(int index)
        {
            var todoList = GetTodoList();
            if (index >= 0 && index < todoList.Count)
            {
                todoList.RemoveAt(index);
                HttpContext.Session.SetObjectAsJson(SessionKey, todoList);
            }
            return RedirectToAction("Index");
        }
    }
}
