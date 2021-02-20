using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bieren.MVC.Controllers
{
    public class BierenController : Controller
    {
        private IRepositoryManager _repositoryManager;
        public BierenController(IRepositoryManager repositoryManager)//, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
        }

        public IActionResult Index()
        {
            IList<Bier> model = _repositoryManager.Bier.GetAll();

            return View(model);
        }
       

    }
}
