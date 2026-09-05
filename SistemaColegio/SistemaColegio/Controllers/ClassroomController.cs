using Application.Aula;
using Application.Aula.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    public class ClassroomController : Controller
    {
        private readonly IClassroomAppService _classroomAppService;

        public ClassroomController(
            IClassroomAppService classroomAppService)
        {
            _classroomAppService = classroomAppService;
        }

        // GET: Classroom
        public async Task<IActionResult> Index()
        {
            var classrooms =
                await _classroomAppService.GetAllClassroom();

            return View(classrooms);
        }

        // GET: Classroom/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var classroom =
                await _classroomAppService.GetClassroomById(id);

            if (classroom == null)
            {
                return NotFound();
            }

            return View(classroom);
        }

        // GET: Classroom/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Classroom/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            ClassroomDto classroom)
        {
            if (!ModelState.IsValid)
            {
                return View(classroom);
            }

            try
            {
                await _classroomAppService.AddClassroom(classroom);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(classroom);
            }
        }

        // GET: Classroom/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var classroom =
                await _classroomAppService.GetClassroomById(id);

            if (classroom == null)
            {
                return NotFound();
            }

            return View(classroom);
        }

        // POST: Classroom/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            ClassroomDto classroom)
        {
            if (!ModelState.IsValid)
            {
                return View(classroom);
            }

            try
            {
                classroom.IdAula = id;

                var updatedClassroom =
                    await _classroomAppService
                        .UpdateClassroom(classroom);

                if (updatedClassroom == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(classroom);
            }
        }

        // POST: Classroom/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var classroom =
                    await _classroomAppService
                        .GetClassroomById(id);

                if (classroom == null)
                {
                    return NotFound();
                }

                await _classroomAppService
                    .DeleteClassroom(classroom);

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
