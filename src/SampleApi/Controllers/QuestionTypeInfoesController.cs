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
    public class QuestionTypeInfoesController : ControllerBase
    {
        private readonly SampleDbContext _context;

        public QuestionTypeInfoesController(SampleDbContext context)
        {
            _context = context;
        }

        // GET: api/QuestionTypeInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionTypeInfo>>> GetQuestionTypeInfos()
        {
            return await _context.QuestionTypeInfos.ToListAsync();
        }

        // GET: api/QuestionTypeInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionTypeInfo>> GetQuestionTypeInfo(int id)
        {
            var questionTypeInfo = await _context.QuestionTypeInfos.FindAsync(id);

            if (questionTypeInfo == null)
            {
                return NotFound();
            }

            return questionTypeInfo;
        }

        // PUT: api/QuestionTypeInfoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionTypeInfo(int id, QuestionTypeInfo questionTypeInfo)
        {
            if (id != questionTypeInfo.ID)
            {
                return BadRequest();
            }

            _context.Entry(questionTypeInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionTypeInfoExists(id))
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

        // POST: api/QuestionTypeInfoes
        [HttpPost]
        public async Task<ActionResult<QuestionTypeInfo>> PostQuestionTypeInfo(QuestionTypeInfo questionTypeInfo)
        {
            _context.QuestionTypeInfos.Add(questionTypeInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionTypeInfo", new { id = questionTypeInfo.ID }, questionTypeInfo);
        }

        // DELETE: api/QuestionTypeInfoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestionTypeInfo>> DeleteQuestionTypeInfo(int id)
        {
            var questionTypeInfo = await _context.QuestionTypeInfos.FindAsync(id);
            if (questionTypeInfo == null)
            {
                return NotFound();
            }

            _context.QuestionTypeInfos.Remove(questionTypeInfo);
            await _context.SaveChangesAsync();

            return questionTypeInfo;
        }

        private bool QuestionTypeInfoExists(int id)
        {
            return _context.QuestionTypeInfos.Any(e => e.ID == id);
        }
    }
}
