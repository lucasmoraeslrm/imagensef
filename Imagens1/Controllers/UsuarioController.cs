using Imagens1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace Imagens1.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly Imagens1.Data.AppContext _appContext;


        public UsuarioController(Imagens1.Data.AppContext appContext)
        {
            _appContext = appContext;
        }


        public async Task<ActionResult> Index()
        {
            var puxa_usuario = await _appContext.Usuarios.ToListAsync();
            return View(puxa_usuario);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario, IList<IFormFile> Img)
        {
            // Verificar tamanho da imagem
            IFormFile uploadedImage = Img.FirstOrDefault();
            MemoryStream ms = new MemoryStream();

            if (Img.Count > 0)
            {
                uploadedImage.OpenReadStream().CopyTo(ms);
                usuario.Imagem = ms.ToArray();
            }
            _appContext.Usuarios.Add(usuario);
            _appContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _appContext.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return BadRequest();
            }
            return View(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _appContext.Usuarios.FindAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, Usuario usuario, IList<IFormFile> Img)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dadosAntigos = _appContext.Usuarios.AsNoTracking().FirstOrDefault(p => p.Id == id);

            IFormFile uploadedImage = Img.FirstOrDefault();
            MemoryStream ms = new MemoryStream();
            if (Img.Count > 0)
            {
                uploadedImage.OpenReadStream().CopyTo(ms);
                usuario.Imagem = ms.ToArray();
            }
            else
            {
                usuario.Imagem = dadosAntigos.Imagem;
            }
            if (ModelState.IsValid)
            {
                Console.WriteLine("teste");
                _appContext.Update(usuario);
                await _appContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _appContext.Usuarios.FindAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            var produto = await _appContext.Usuarios.FindAsync(id);
            _appContext.Usuarios.Remove(produto);
            await _appContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}
