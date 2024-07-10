// Controllers/EFQMController.cs
using GestionApiTareas;
using GestionApiTareas.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFQMController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EFQMController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EFQM>> GetEFQMs()
        {
            return _context.EFQMs.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<EFQM> GetEFQM(int id)
        {
            var efqm = _context.EFQMs.Find(id);
            if (efqm == null)
            {
                return NotFound();
            }

            return efqm;
        }

        [HttpPost]
        public ActionResult<EFQM> PostEFQM(EFQM efqm)
        {
            _context.EFQMs.Add(efqm);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEFQM), new { id = efqm.idEFQM }, efqm);
        }

        [HttpPut("{id}")]
        public IActionResult PutEFQM(int id, EFQM efqm)
        {
            if (id != efqm.idEFQM)
            {
                return BadRequest();
            }

            _context.Entry(efqm).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEFQM(int id)
        {
            var efqm = _context.EFQMs.Find(id);
            if (efqm == null)
            {
                return NotFound();
            }

            _context.EFQMs.Remove(efqm);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("evaluate/{id}")]
        public ActionResult<string> EvaluateEFQM(int id)
        {
            var efqm = _context.EFQMs.Find(id);
            if (efqm == null)
            {
                return NotFound();
            }

            return efqm.Evaluate();
        }
    }
}
