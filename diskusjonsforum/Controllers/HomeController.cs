﻿using diskusjonsforum.DAL;
using Microsoft.AspNetCore.Mvc;
using diskusjonsforum.ViewModels;
using Thread = Diskusjonsforum.Models.Thread;

namespace diskusjonsforum.Controllers
{
    public class HomeController : Controller
    {
        //Initialise controllers and interfaces for constructor
        private readonly ILogger<HomeController> _logger;
        private readonly IThreadRepository _threadRepository;

        public HomeController(ILogger<HomeController> logger, IThreadRepository threadRepository)
        {
            _logger = logger;
            _threadRepository = threadRepository;
        }

        //Loads homepage/Index view with a list of threads
        public IActionResult Index()
        {
            var threads = GetThreads(); // Call your GetThreads method to fetch the list of threads.
            var threadListViewModel = new ThreadListViewModel(threads, "Table")
            {
                Threads = threads
            };
            ViewBag.CurrentView = "Index";
            return View(threadListViewModel);
        }
        
        // Fetch threads from repository
        public IEnumerable<Thread> GetThreads()
        { 
            try
            {
                var threads = _threadRepository.GetAll(); // Retrieve all threads
                return threads; // Return list
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[HomeController] An error occurred in the GetThreads method.");
                return new List<Thread>(); // Return empty list if an error occurs
            }
        }
        
        // Show error message on Error view
        public IActionResult Error(string errorMsg)
        {
            //Log error message using logger
            _logger.LogError("[HomeController] Error: {0}", errorMsg);
            //View error message in view
            ViewBag.ErrorMsg = errorMsg;
            return View();
        }
        
    }
}

