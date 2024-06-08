using Lab10.Data;
using Lab10.Models;
using Lab10.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lab10.Controllers
{
    public class UserLogsController : Controller
    {
        private readonly ApplicationDbContext context;

        public UserLogsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index(int page = 1, bool showFromUser = false, string filterType = "NONE", string filterSeverity = "NONE")
        {
			const int pageSize = 4;
			if (page < 1) page = 1;

			int? userId = HttpContext.Session.GetInt32("UserId");

			if (userId == null)
			{
				return RedirectToAction("Login", "Users");
			}

			var logs = context.UserLogs.Include(c => c.User).ToList();

			if (showFromUser)
				logs = logs.FindAll(log => log.UserId == (int)userId);
			
			if(filterType != "NONE")
				logs = logs.FindAll(log => log.LogType == filterType);

			if (filterSeverity != "NONE")
				logs = logs.FindAll(log => log.LogSeverity == filterSeverity);

			int recordCount = logs.Count();

			var pager = new Pager(recordCount, page, pageSize);

			int recordSkip = (page - 1) * pageSize;

			var data = logs.Skip(recordSkip).Take(pager.PageSize).ToList();

			ViewBag.Pager = pager;
			ViewBag.filterByUser = showFromUser;
			ViewBag.filterType = filterType;
			ViewBag.filterSeverity = filterSeverity;
			ViewBag.LogTypes =
				new List<SelectListItem>
				{
					new SelectListItem {Text = "NONE", Value = "NONE"},
					new SelectListItem {Text = "EVENT", Value = "EVENT"},
					new SelectListItem {Text = "SERVER", Value = "SERVER"},
					new SelectListItem {Text = "SYSTEM", Value = "SYSTEM"},
				};
			ViewBag.LogSeverities =
				new List<SelectListItem>
				{
					new SelectListItem {Text = "NONE", Value = "NONE"},
					new SelectListItem {Text = "NOTICE", Value = "NOTICE"},
					new SelectListItem {Text = "DEBUG", Value = "DEBUG"},
					new SelectListItem {Text = "WARNING", Value = "WARNING"},
					new SelectListItem {Text = "ERROR", Value = "ERROR"},
					new SelectListItem {Text = "CRITICAL", Value = "CRITICAL"},
				};

			return View(data);
        }

        [HttpGet]
        public IActionResult Add() {
			int? userId = HttpContext.Session.GetInt32("UserId");

			if (userId == null)
			{
				return RedirectToAction("Login", "Users");
			}

			ViewBag.LogTypes = 
				new List<SelectListItem>
				{
					new SelectListItem {Text = "EVENT", Value = "EVENT"},
					new SelectListItem {Text = "SERVER", Value = "SERVER"},
					new SelectListItem {Text = "SYSTEM", Value = "SYSTEM"},
				};
			ViewBag.LogSeverities =
				new List<SelectListItem>
				{
					new SelectListItem {Text = "NOTICE", Value = "NOTICE"},
					new SelectListItem {Text = "DEBUG", Value = "DEBUG"},
					new SelectListItem {Text = "WARNING", Value = "WARNING"},
					new SelectListItem {Text = "ERROR", Value = "ERROR"},
					new SelectListItem {Text = "CRITICAL", Value = "CRITICAL"},
				}; 

			return View(); 
        }

		[HttpPost]
		public async Task<IActionResult> Add(AddLogViewModel viewModel)
		{
            int? userId = HttpContext.Session.GetInt32("UserId");

			if (userId == null)
			{
				return RedirectToAction("Login", "Users");
			}
			
			if (userId != null)
            {
				UserLog log = new UserLog
				{
					LogContent = viewModel.LogContent,
					LogDate = viewModel.LogDate,
					LogSeverity = viewModel.LogSeverity,
					LogType = viewModel.LogType,
					UserId = (int)userId
				};

				await context.UserLogs.AddAsync(log);
                await context.SaveChangesAsync();
			}

			return RedirectToAction("Index", "UserLogs");
		}

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
			int? userId = HttpContext.Session.GetInt32("UserId");

			if (userId == null)
			{
				return RedirectToAction("Login", "Users");
			}

			ViewBag.LogTypes =
				new List<SelectListItem>
				{
					new SelectListItem {Text = "EVENT", Value = "EVENT"},
					new SelectListItem {Text = "SERVER", Value = "SERVER"},
					new SelectListItem {Text = "SYSTEM", Value = "SYSTEM"},
				};
			ViewBag.LogSeverities =
				new List<SelectListItem>
				{
					new SelectListItem {Text = "NOTICE", Value = "NOTICE"},
					new SelectListItem {Text = "DEBUG", Value = "DEBUG"},
					new SelectListItem {Text = "WARNING", Value = "WARNING"},
					new SelectListItem {Text = "ERROR", Value = "ERROR"},
					new SelectListItem {Text = "CRITICAL", Value = "CRITICAL"},
				};

			UserLog? log = await context.UserLogs.FindAsync(id);

			if(log == null || log.UserId != (int)userId)
			{
				return RedirectToAction("Login", "Users");
			}

            return View(log);
        }

		[HttpPost]
		public async Task<IActionResult> Edit(UserLog log)
		{
			int? userId = HttpContext.Session.GetInt32("UserId");

			if (userId == null)
			{
				return RedirectToAction("Login", "Users");
			}

			UserLog? dbLog = await context.UserLogs.FindAsync(log.Id);

            if(dbLog is not null && dbLog.UserId == (int)userId)
            {
                dbLog.LogDate = log.LogDate;
                dbLog.LogSeverity = log.LogSeverity;
                dbLog.LogContent = log.LogContent;
                dbLog.LogType = log.LogType;

                await context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "UserLogs");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(UserLog log)
		{
			int? userId = HttpContext.Session.GetInt32("UserId");

			if (userId == null)
			{
				return RedirectToAction("Login", "Users");
			}

			UserLog? dbLog = await context.UserLogs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == log.Id && x.UserId == (int)userId);

			if (dbLog is not null)
			{
				context.UserLogs.Remove(dbLog);
				await context.SaveChangesAsync();
			}

			return RedirectToAction("Index", "UserLogs");
		}
	}
}
