using Application.CursoMateria;
using Application.CursoMateria.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    public class CourseSubjectController : Controller
    {
        private readonly ICourseSubjectAppService _courseSubjectAppService;

        public CourseSubjectController(
            ICourseSubjectAppService courseSubjectAppService)
        {
            _courseSubjectAppService = courseSubjectAppService;
        }

        // GET: CourseSubject
        public async Task<IActionResult> Index()
        {
            var courseSubjects =
                await _courseSubjectAppService
                    .GetAllCourseSubject();

            return View(courseSubjects);
        }

        // GET: CourseSubject/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var courseSubject =
                await _courseSubjectAppService
                    .GetCourseSubjectById(id);

            if (courseSubject == null)
            {
                return NotFound();
            }

            return View(courseSubject);
        }

        // GET: CourseSubject/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseSubject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            CourseSubjectDto courseSubject)
        {
            if (!ModelState.IsValid)
            {
                return View(courseSubject);
            }

            try
            {
                await _courseSubjectAppService
                    .AddCourseSubject(courseSubject);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(courseSubject);
            }
        }

        // GET: CourseSubject/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var courseSubject =
                await _courseSubjectAppService
                    .GetCourseSubjectById(id);

            if (courseSubject == null)
            {
                return NotFound();
            }

            return View(courseSubject);
        }

        // POST: CourseSubject/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            CourseSubjectDto courseSubject)
        {
            if (!ModelState.IsValid)
            {
                return View(courseSubject);
            }

            try
            {
                // El ID de la URL identifica
                // directamente la relación.
                courseSubject.IdCursoMateria = id;

                var updatedCourseSubject =
                    await _courseSubjectAppService
                        .UpdateCourseSubject(courseSubject);

                if (updatedCourseSubject == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(courseSubject);
            }
        }

        // POST: CourseSubject/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var courseSubject =
                    await _courseSubjectAppService
                        .GetCourseSubjectById(id);

                if (courseSubject == null)
                {
                    return NotFound();
                }

                await _courseSubjectAppService
                    .DeleteCourseSubject(courseSubject);

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