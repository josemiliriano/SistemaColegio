using Application.CursoPeriodo;
using Application.CursoPeriodo.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    public class CoursePeriodController : Controller
    {
        private readonly ICoursePeriodAppService _coursePeriodAppService;

        public CoursePeriodController(
            ICoursePeriodAppService coursePeriodAppService)
        {
            _coursePeriodAppService = coursePeriodAppService;
        }

        // GET: CoursePeriod
        public async Task<IActionResult> Index()
        {
            var coursePeriods =
                await _coursePeriodAppService.GetAllCoursePeriod();

            return View(coursePeriods);
        }

        // GET: CoursePeriod/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var coursePeriod =
                await _coursePeriodAppService
                    .GetCoursePeriodById(id);

            if (coursePeriod == null)
            {
                return NotFound();
            }

            return View(coursePeriod);
        }

        // GET: CoursePeriod/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CoursePeriod/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            CursoPeriodoDto coursePeriod)
        {
            if (!ModelState.IsValid)
            {
                return View(coursePeriod);
            }

            try
            {
                await _coursePeriodAppService
                    .AddCoursePeriod(coursePeriod);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(coursePeriod);
            }
        }

        // GET: CoursePeriod/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var coursePeriod =
                await _coursePeriodAppService
                    .GetCoursePeriodById(id);

            if (coursePeriod == null)
            {
                return NotFound();
            }

            return View(coursePeriod);
        }

        // POST: CoursePeriod/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            CursoPeriodoDto coursePeriod)
        {
            if (!ModelState.IsValid)
            {
                return View(coursePeriod);
            }

            try
            {
                // El ID de la URL identifica
                // directamente el registro.
                coursePeriod.IdCursoPeriodo = id;

                var updatedCoursePeriod =
                    await _coursePeriodAppService
                        .UpdateCoursePeriod(coursePeriod);

                if (updatedCoursePeriod == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(coursePeriod);
            }
        }

        // POST: CoursePeriod/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var coursePeriod = await _coursePeriodAppService.GetCoursePeriodById(id);

                if (coursePeriod == null)
                {
                    return NotFound();
                }

                await _coursePeriodAppService
                    .DeleteCoursePeriod(coursePeriod);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return RedirectToAction(nameof(Index));
            }
        }
    }
}