using Application.PeriodoSesion;
using Application.PeriodoSesion.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    public class SessionPeriodController : Controller
    {
        private readonly ISessionPeriodAppService
            _sessionPeriodAppService;

        public SessionPeriodController(
            ISessionPeriodAppService sessionPeriodAppService)
        {
            _sessionPeriodAppService =
                sessionPeriodAppService;
        }

        // GET: SessionPeriod
        public async Task<IActionResult> Index()
        {
            var sessionPeriods =
                await _sessionPeriodAppService
                    .GetAllSessionPeriod();

            return View(sessionPeriods);
        }

        // GET: SessionPeriod/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var sessionPeriod =
                await _sessionPeriodAppService
                    .GetSessionPeriodById(id);

            if (sessionPeriod == null)
            {
                return NotFound();
            }

            return View(sessionPeriod);
        }

        // GET: SessionPeriod/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SessionPeriod/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            SessionPeriodDto sessionPeriod)
        {
            if (!ModelState.IsValid)
            {
                return View(sessionPeriod);
            }

            try
            {
                await _sessionPeriodAppService
                    .AddSessionPeriod(sessionPeriod);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(sessionPeriod);
            }
        }

        // GET: SessionPeriod/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var sessionPeriod =
                await _sessionPeriodAppService
                    .GetSessionPeriodById(id);

            if (sessionPeriod == null)
            {
                return NotFound();
            }

            return View(sessionPeriod);
        }

        // POST: SessionPeriod/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            SessionPeriodDto sessionPeriod)
        {
            if (!ModelState.IsValid)
            {
                return View(sessionPeriod);
            }

            try
            {
                // El ID de la URL identifica
                // la relación sección-período.
                sessionPeriod.IdSessionPeriod = id;

                var updatedSessionPeriod =
                    await _sessionPeriodAppService
                        .UpdateSessionPeriod(sessionPeriod);

                if (updatedSessionPeriod == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(sessionPeriod);
            }
        }

        // POST: SessionPeriod/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var sessionPeriod =
                    await _sessionPeriodAppService
                        .GetSessionPeriodById(id);

                if (sessionPeriod == null)
                {
                    return NotFound();
                }

                await _sessionPeriodAppService
                    .DeleteSessionPeriod(sessionPeriod);

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
