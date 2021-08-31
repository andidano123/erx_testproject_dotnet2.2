using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleApi.Domain;

namespace SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionCategoryInfoesController : ControllerBase
    {
        private readonly SampleDbContext _context;

        public QuestionCategoryInfoesController(SampleDbContext context)
        {
            _context = context;
        }

        // GET: api/QuestionCategoryInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionCategoryInfo>>> GetQuestionCategoryInfos()
        {
            return await _context.QuestionCategoryInfos.ToListAsync();
        }

        // GET: api/QuestionCategoryInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionCategoryInfo>> GetQuestionCategoryInfo(int id)
        {
            var questionCategoryInfo = await _context.QuestionCategoryInfos.FindAsync(id);

            if (questionCategoryInfo == null)
            {
                return NotFound();
            }

            return questionCategoryInfo;
        }

        // PUT: api/QuestionCategoryInfoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionCategoryInfo(int id, QuestionCategoryInfo questionCategoryInfo)
        {
            if (id != questionCategoryInfo.ID)
            {
                return BadRequest();
            }

            _context.Entry(questionCategoryInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionCategoryInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/QuestionCategoryInfoes
        [HttpPost]
        public async Task<ActionResult<QuestionCategoryInfo>> PostQuestionCategoryInfo(QuestionCategoryInfo questionCategoryInfo)
        {
            _context.QuestionCategoryInfos.Add(questionCategoryInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionCategoryInfo", new { id = questionCategoryInfo.ID }, questionCategoryInfo);
        }

        // DELETE: api/QuestionCategoryInfoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestionCategoryInfo>> DeleteQuestionCategoryInfo(int id)
        {
            var questionCategoryInfo = await _context.QuestionCategoryInfos.FindAsync(id);
            if (questionCategoryInfo == null)
            {
                return NotFound();
            }

            _context.QuestionCategoryInfos.Remove(questionCategoryInfo);
            await _context.SaveChangesAsync();

            return questionCategoryInfo;
        }

        private bool QuestionCategoryInfoExists(int id)
        {
            return _context.QuestionCategoryInfos.Any(e => e.ID == id);
        }
    }
}
