using Application.ProfesorMateria;
using Application.ProfesorMateria.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    public class ProfessorSubjectController : Controller
    {
        private readonly IProfessorSubjectAppService
            _professorSubjectAppService;

        public ProfessorSubjectController(IProfessorSubjectAppService professorSubjectAppService)
        {
            _professorSubjectAppService = professorSubjectAppService;
        }

        // GET: ProfessorSubject
        public async Task<IActionResult> Index()
        {
            var professorSubjects =
                await _professorSubjectAppService
                    .GetAllProfessorSubject();

            return View(professorSubjects);
        }

        // GET: ProfessorSubject/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var professorSubject =
                await _professorSubjectAppService
                    .GetProfessorSubjectById(id);

            if (professorSubject == null)
            {
                return NotFound();
            }

            return View(professorSubject);
        }

        // GET: ProfessorSubject/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProfessorSubject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            ProfessorSubjectDto professorSubject)
        {
            if (!ModelState.IsValid)
            {
                return View(professorSubject);
            }

            try
            {
                await _professorSubjectAppService
                    .AddProfessorSubject(professorSubject);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(professorSubject);
            }
        }

        // GET: ProfessorSubject/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var professorSubject =
                await _professorSubjectAppService
                    .GetProfessorSubjectById(id);

            if (professorSubject == null)
            {
                return NotFound();
            }

            return View(professorSubject);
        }

        // POST: ProfessorSubject/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            ProfessorSubjectDto professorSubject)
        {
            if (!ModelState.IsValid)
            {
                return View(professorSubject);
            }

            try
            {
                professorSubject.IdProfesorMateria = id;

                var updatedProfessorSubject =
                    await _professorSubjectAppService
                        .UpdateProfessorSubject(
                            professorSubject);

                if (updatedProfessorSubject == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(professorSubject);
            }
        }

        // POST: ProfessorSubject/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var professorSubject =
                    await _professorSubjectAppService
                        .GetProfessorSubjectById(id);

                if (professorSubject == null)
                {
                    return NotFound();
                }

                await _professorSubjectAppService
                    .DeleteProfessorSubject(
                        professorSubject);

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