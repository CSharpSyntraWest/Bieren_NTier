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

        public async Task<IActionResult> Index()
        {
            IList<Bier> model = await _repositoryManager.Bier.GetAllAsync();

            return View(model);
        }

        public async Task<IActionResult> Insert(Bier model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _repositoryManager.Bier.AddAsync(model);
                _repositoryManager.Save();
            }

            return RedirectToAction(actionName: nameof(Index));
        }
    }
}
