using Microsoft.AspNetCore.Mvc;
using AutoService.Web.Data;
using AutoService.Web.Models;
using System.Linq;

namespace AutoService.Web.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequestsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        // 📌 1. Показать список заявок
        public IActionResult Index()
        {
            var requests = _context.Requests.ToList();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView(requests); // 🔹 Возвращаем `PartialView` для AJAX
            }

            return View(requests);
        }


        // 📌 2. Показать детали заявки
        public IActionResult Details(int id)
        {
            var request = _context.Requests.Find(id);
            if (request == null) return NotFound();
            return View(request);
        }

        // 📌 3. Открыть страницу редактирования заявки
        public IActionResult Edit(int id)
        {
            var request = _context.Requests.Find(id);
            if (request == null) return NotFound();
            return View(request);
        }

        // 📌 4. Сохранить отредактированную заявку
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public IActionResult Create(Request request)
        {
            Console.WriteLine($"Полученные данные: {request.ClientName}, {request.ServiceType}, {request.Status}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("❌ ModelState невалиден! Ошибки:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return View(request);
            }

            _context.Requests.Add(request);
            _context.SaveChanges();
            Console.WriteLine("✅ Заявка сохранена в БД!");

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Request request)
        {
            if (ModelState.IsValid)
            {
                _context.Requests.Update(request);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(request);
        }

        // 📌 5. Открыть страницу удаления заявки
        public IActionResult Delete(int id)
        {
            var request = _context.Requests.Find(id);
            if (request == null) return NotFound();
            return View(request);
        }

        // 📌 6. Подтвердить удаление заявки
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var request = _context.Requests.Find(id);
            if (request != null)
            {
                _context.Requests.Remove(request);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
