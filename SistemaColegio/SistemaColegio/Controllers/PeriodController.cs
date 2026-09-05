using Application.Periodo;
using Application.Periodo.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    public class PeriodController : Controller
    {
        private readonly IPeriodAppService _periodAppService;

        public PeriodController(IPeriodAppService periodAppService)
        {
            _periodAppService = periodAppService;
        }

        // GET: Period
        public async Task<IActionResult> Index()
        {
            var periods =
                await _periodAppService.GetAllPeriod();

            return View(periods);
        }

        // GET: Period/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var period =
                await _periodAppService.GetPeriodById(id);

            if (period == null)
            {
                return NotFound();
            }

            return View(period);
        }

        // GET: Period/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Period/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            PeriodDto period)
        {
            if (!ModelState.IsValid)
            {
                return View(period);
            }

            try
            {
                await _periodAppService.AddPeriod(period);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(period);
            }
        }

        // GET: Period/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var period =
                await _periodAppService.GetPeriodById(id);

            if (period == null)
            {
                return NotFound();
            }

            return View(period);
        }

        // POST: Period/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            PeriodDto period)
        {
            if (!ModelState.IsValid)
            {
                return View(period);
            }

            try
            {
                period.IdPeriodo = id;

                var updatedPeriod =
                    await _periodAppService.UpdatePeriod(period);

                if (updatedPeriod == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(period);
            }
        }

        // POST: Period/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var period =
                    await _periodAppService.GetPeriodById(id);

                if (period == null)
                {
                    return NotFound();
                }

                await _periodAppService.DeletePeriod(period);

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
