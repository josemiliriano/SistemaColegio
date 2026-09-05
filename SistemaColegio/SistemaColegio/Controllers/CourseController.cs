using Application.Curso;
using Application.Curso.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseAppService _courseAppService;

        public CourseController(ICourseAppService courseAppService)
        {
            _courseAppService = courseAppService;
        }

        // GET: Course
        public async Task<IActionResult> Index()
        {
            var courses = await _courseAppService.GetAllCourse();

            return View(courses);
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseAppService.GetCourseById(id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Course/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseDto course)
        {
            if (!ModelState.IsValid)
            {
                return View(course);
            }

            try
            {
                await _courseAppService.AddCourse(course);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(course);
            }
        }

        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseAppService.GetCourseById(id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Course/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            CourseDto course)
        {
            if (!ModelState.IsValid)
            {
                return View(course);
            }

            try
            {
                course.IdCurso = id;

                var updatedCourse =
                    await _courseAppService.UpdateCourse(course);

                if (updatedCourse == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(course);
            }
        }

        // POST: Course/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var course =
                    await _courseAppService.GetCourseById(id);

                if (course == null)
                {
                    return NotFound();
                }

                await _courseAppService.DeleteCourse(course);

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