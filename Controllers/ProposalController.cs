using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using proposal_items.Models;
using proposal_items.Services;
using proposal_items.ViewModels;

namespace proposal_items.Controllers
{
    public class ProposalController : Controller
    {
        private readonly Repository _repository;

        public ProposalController(Repository repository)
        {
            _repository = repository;
        }

        // GET: ToDo
        public ActionResult Index()
        {
            return View(_repository.Proposals);
        }

        public ActionResult SearchImages(int proposalId, string searchString) {
            // create an ImageSearch ViewModel that holds everything the view needs,
            // including the proposalId, search text, and image search results
            var vm = new ImageSearch {
                ProposalId = proposalId,
                SearchString = searchString,
                // search for images if there is search text
                Images = String.IsNullOrEmpty(searchString) 
                    ? _repository.Images
                    : _repository.Images.Where(i => i.Filename.Contains(searchString))
            };
            // pass the viewmodel to the view
            return View(vm);
        }

        public ActionResult AddProposalItem(int imageId, int proposalId) {
            // create an AddProposalItem view model to hold info for the view
            var vm = new AddProposalItem {
                ProposalId = proposalId,
                ImageId = imageId,
                // get the image object by id
                Image = _repository.Images.FirstOrDefault(i => i.Id == imageId),
                Caption = ""
            };
            // pass the view model to the view
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProposalItem(int id, AddProposalItem vm) {
            // create a new proposalItem model and fill in the image id, caption, etc
            var newProposalItem = new ProposalItem {
                ImageId = vm.ImageId,
                ProposalId = vm.ProposalId,
                Caption = vm.Caption
            };
            // add it to the proposal
            _repository.AddProposalItem(vm.ProposalId, newProposalItem);

            // redirect to the Proposal Edit page
            return RedirectToAction("Edit", new { id = vm.ProposalId });
        }

        // GET: ToDo/Details/5
        // public ActionResult Details(int id)
        // {
        //     return View(_repository.GetToDo(id));
        // }

        // GET: ToDo/Create
        // public ActionResult Create()
        // {
        //     return View();
        // }

        // POST: ToDo/Create
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public ActionResult Create(ToDo toDo)
        // {
        //     try
        //     {
        //         _repository.Add(toDo);
        //         return RedirectToAction(nameof(Index));
        //     }
        //     catch (Exception ex)
        //     {
        //         return ErrorView(ex);
        //     }
        // }

        // GET: ToDo/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_repository.Proposals.FirstOrDefault(p => p.Id == id));
        }

        // POST: ToDo/Edit/5
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public ActionResult Edit(int id, ToDo toDo)
        // {
        //     try
        //     {
        //         _repository.Update(id, toDo);
        //         return RedirectToAction(nameof(Index));
        //     }
        //     catch (Exception ex)
        //     {
        //         return ErrorView(ex);
        //     }
        // }

        private ActionResult ErrorView(Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Unknown Error");
            return View();
        }
    }
}