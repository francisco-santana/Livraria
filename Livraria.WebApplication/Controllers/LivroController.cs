using Livraria.WebApplication.Helper;
using Livraria.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.WebApplication.Controllers
{
    public class LivroController : Controller
    {
        private const string ACTION_INDEX = "Index";
        private readonly LivrariaApi<LivroViewModel> _api;

        public LivroController()
        {
            _api = new LivrariaApi<LivroViewModel>("api/livraria/livro");
        }

        public async Task<IActionResult> Index()
        {
            var livros = await _api.GetAsync();
            return View(livros.OrderBy(prop => prop.Nome));
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var livro = await _api.GetAsync(id);
            return View(livro ?? new LivroViewModel());
        }

        public ActionResult NovoLivro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NovoLivro(LivroViewModel livro)
        {
            var res = _api.PostAsJsonAsync(livro);

            if (res.IsSuccessStatusCode)
                return RedirectToAction(ACTION_INDEX);

            return View();
        }

        [HttpGet]
        public async Task<ViewResult> Editar(int id)
        {
            var livro = await _api.GetAsync(id);
            return View(livro);
        }

        [HttpPost]
        public IActionResult Editar(int id, LivroViewModel livro)
        {
            var res = _api.PutAsJsonAsync(id, livro);
            
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction(ACTION_INDEX);
            }

            return View();
        }

        public async Task<IActionResult> Excluir(int id)
        {
            await _api.DeleteAsync(id);
            return RedirectToAction(ACTION_INDEX);
        }

        public IActionResult Sobre()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
