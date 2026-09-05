using Application.AsignacionDocente;
using Application.AsignacionDocente.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    public class TeachingAssignmentController : Controller
    {
        private readonly ITeachingAssignmentAppService
            _teachingAssignmentAppService;

        public TeachingAssignmentController(
            ITeachingAssignmentAppService teachingAssignmentAppService)
        {
            _teachingAssignmentAppService =
                teachingAssignmentAppService;
        }

        // GET: TeachingAssignment
        public async Task<IActionResult> Index()
        {
            var teachingAssignments =
                await _teachingAssignmentAppService
                    .GetAllTeachingAssignment();

            return View(teachingAssignments);
        }

        // GET: TeachingAssignment/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var teachingAssignment =
                await _teachingAssignmentAppService
                    .GetTeachingAssignmentById(id);

            if (teachingAssignment == null)
            {
                return NotFound();
            }

            return View(teachingAssignment);
        }

        // GET: TeachingAssignment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TeachingAssignment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            TeachingAssignmentDto teachingAssignment)
        {
            if (!ModelState.IsValid)
            {
                return View(teachingAssignment);
            }

            try
            {
                await _teachingAssignmentAppService
                    .AddTeachingAssignment(
                        teachingAssignment);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(teachingAssignment);
            }
        }

        // GET: TeachingAssignment/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var teachingAssignment =
                await _teachingAssignmentAppService
                    .GetTeachingAssignmentById(id);

            if (teachingAssignment == null)
            {
                return NotFound();
            }

            return View(teachingAssignment);
        }

        // POST: TeachingAssignment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            TeachingAssignmentDto teachingAssignment)
        {
            if (!ModelState.IsValid)
            {
                return View(teachingAssignment);
            }

            try
            {
                // El ID de la URL identifica
                // la asignación docente.
                teachingAssignment.IdAsignacionDocente = id;

                var updatedTeachingAssignment =
                    await _teachingAssignmentAppService
                        .UpdateTeachingAssignment(
                            teachingAssignment);

                if (updatedTeachingAssignment == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(teachingAssignment);
            }
        }

        // POST: TeachingAssignment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var teachingAssignment =
                    await _teachingAssignmentAppService
                        .GetTeachingAssignmentById(id);

                if (teachingAssignment == null)
                {
                    return NotFound();
                }

                await _teachingAssignmentAppService
                    .DeleteTeachingAssignment(
                        teachingAssignment);

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