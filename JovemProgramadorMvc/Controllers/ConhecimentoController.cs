using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace JovemProgramadorMvc.Controllers
{
    public class ConhecimentoController : Controller
    {
        public ConhecimentoController()
        {
        }
        public IActionResult Index()
        {
            return View();
        }
         public IActionResult Informacao()
        {
            return View();
        }
    }
}