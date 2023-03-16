using Microsoft.AspNetCore.Mvc;
using MvcNetCoreZapatillas.Models;
using MvcNetCoreZapatillas.Repositories;

namespace MvcNetCoreZapatillas.Controllers
{
    public class ZapatillasController : Controller
    {

        public RepositoryZapatillas repo;


        public ZapatillasController(RepositoryZapatillas repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Zapatilla> zapatillas =  this.repo.GetZapatillas();
            return View(zapatillas);
        }

        //public IActionResult _ZapatillaPartial(int idProducto)
        //{
        //   Zapatilla zapatilla = this.repo.FindZapatilla(idProducto);
        //    return PartialView("_ZapatillaPartial", zapatilla);
        //}

        public IActionResult DetailsZapatilla(int idproducto)
        {
            Zapatilla zapatilla = this.repo.FindZapatilla(idproducto);
            return View(zapatilla);
        }

        //public IActionResult ImagenesPaginadas(int idProducto)
        //{
        //    ViewData["TOTAL"] = this.repo.TotalZapatillas(idProducto);
        //    return View();
        //}



        //public IActionResult ImagenesPaginadas(int idProducto, int registrospagina, int? posicion)
        //{
        //    if (posicion == null)
        //    {
        //        posicion = 1;
        //    }

        //    ViewData["TOTAL"] = this.repo.TotalZapatillas(idProducto);
        //    ViewData["REGISTROPAGINA"] = registrospagina;

        //    ImagenZapatilla imagen = this.repo.ImagenZapatilla(registrospagina, posicion.Value, idProducto);
        //    return View(imagen);

        //}

        [HttpGet]
        public IActionResult ImagenesPaginadas(int idProducto)
        {

            
            ViewData["TOTAL"] = this.repo.TotalZapatillas(idProducto);
            ViewData["REGISTROPAGINA"] = 10;

            ImagenZapatilla imagen = this.repo.ImagenZapatilla(10, 1, idProducto);
            return View(imagen);
        }

        [HttpPost]
        public IActionResult ImagenesPaginadas(int idProducto, int? registrospagina, int? posicion )
        {

            if (registrospagina == null)
            {

                registrospagina = 10;
            }
            if (posicion == null)
            {
                posicion = 1;
            }
            ViewData["TOTAL"] = this.repo.TotalZapatillas(idProducto);
            ViewData["REGISTROPAGINA"] = registrospagina;

            ImagenZapatilla imagen = this.repo.ImagenZapatilla(registrospagina.Value, posicion.Value, idProducto);
            return View(imagen);
        }




    }
}
