using Application.Materia;
using Application.Materia.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace SistemaColegio.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectAppService _subjectAppService;

        public SubjectController(ISubjectAppService subjectAppService)
        {
            _subjectAppService = subjectAppService;
        }

        // GET: Subject
        public async Task<IActionResult> Index()
        {
            var subjects = await _subjectAppService.GetAllSubject();

            return View(subjects);
        }

        // GET: Subject/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var subject = await _subjectAppService.GetSubjectById(id);

            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Subject/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubjectDto subject)
        {
            if (!ModelState.IsValid)
            {
                return View(subject);
            }

            try
            {
                await _subjectAppService.AddSubject(subject);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(subject);
            }
        }

        // GET: Subject/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var subject = await _subjectAppService.GetSubjectById(id);

            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subject/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            SubjectDto subject)
        {
            if (!ModelState.IsValid)
            {
                return View(subject);
            }

            try
            {
                subject.IdMateria = id;

                var updatedSubject =
                    await _subjectAppService.UpdateSubject(subject);

                if (updatedSubject == null)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(subject);
            }
        }

        // POST: Subject/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var subject =
                    await _subjectAppService.GetSubjectById(id);

                if (subject == null)
                {
                    return NotFound();
                }

                await _subjectAppService.DeleteSubject(subject);

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