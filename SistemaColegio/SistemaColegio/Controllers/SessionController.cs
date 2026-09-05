using Application.Seccion;
using Application.Seccion.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionAppService _sessionAppService;

        public SessionController(ISessionAppService sessionAppService)
        {
            _sessionAppService = sessionAppService;
        }

        // GET: Session
        public async Task<IActionResult> Index()
        {
            var sessions = await _sessionAppService.GetAllSession();

            return View(sessions);
        }

        // GET: Session/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var session = await _sessionAppService.GetSessionById(id);

            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // GET: Session/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Session/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SessionDto session)
        {
            if (!ModelState.IsValid)
            {
                return View(session);
            }

            try
            {
                await _sessionAppService.AddSession(session);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(session);
            }
        }

        // GET: Session/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var session = await _sessionAppService.GetSessionById(id);

            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // POST: Session/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            SessionDto session)
        {
            if (!ModelState.IsValid)
            {
                return View(session);
            }

            try
            {
                session.IdSeccion = id;

                var updatedSession =
                    await _sessionAppService.UpdateSession(session);

                if (updatedSession == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(session);
            }
        }

        // POST: Session/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var session =
                    await _sessionAppService.GetSessionById(id);

                if (session == null)
                {
                    return NotFound();
                }

                await _sessionAppService.DeleteSession(session);

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
