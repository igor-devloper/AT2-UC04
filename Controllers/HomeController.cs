using AT2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AT2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastro()
        {
            return View();
        }
        public IActionResult Cadastrar()
        {
            if (HttpContext.Session.GetInt32("id") == 0 || HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login");

            }
            else { return View(); }
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar(Packege p)
        {   
            PackageRepository pkg = new PackageRepository();
            p.usuario = (int)HttpContext.Session.GetInt32("id");
            pkg.InsertPackage(p);
            ViewBag.Mensagem = "Usuario Cadastrado com sucesso";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Cadastro(Usuarios u)
        {
            UsuarioRepository ur = new UsuarioRepository();
            ur.Insert(u);
            ViewBag.Mensagem = "Usuario Cadastrado com sucesso";
            return RedirectToAction("Index");
        }

        public IActionResult Listar()
        {
            PackageRepository pacTur = new PackageRepository();
            return View(pacTur.Listar());
        }

        public IActionResult Editar(int id)
        {
            if (HttpContext.Session.GetInt32("id") == 0 || HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login");
            }
            PackageRepository pacTur = new PackageRepository();
            return View(pacTur.BuscarPorId(id));
        }

        [HttpPost]
        public IActionResult Editar(Packege pt)
        {
            PackageRepository pacTur = new PackageRepository();
            pt.usuario = (int)HttpContext.Session.GetInt32("id");
            pacTur.Editar(pt);
            ViewBag.Mensagem = "Pacote Editado com sucesso";
            return RedirectToAction("Listar");
        }

        public IActionResult Excluir(int id)
        {
            if (HttpContext.Session.GetInt32("id") == 0 || HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login");
            }
            PackageRepository pacTur = new PackageRepository();
            pacTur.Deletar(id);
            ViewBag.Mensagem = "Pacote Editado com sucesso";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Login(Usuarios p)
        {
            UsuarioRepository user = new UsuarioRepository();
            Usuarios usuario = user.ValidarLogin(p);
            if (usuario.id != 0)
            {
                HttpContext.Session.SetInt32("id", usuario.id);
                HttpContext.Session.SetString("login", usuario.login);

                int idUsuario = (int)HttpContext.Session.GetInt32("id");

                ViewBag.Mensagem = "Você está logado";
                return Redirect("Index");
            }
            else
            {
                ViewBag.Mensagem = "Login e/ou senha estão incorretos";
                return View();
            }
        }

    }
}

