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
    public class QuestionInfoesController : ControllerBase
    {
        private readonly SampleDbContext _context;

        public QuestionInfoesController(SampleDbContext context)
        {
            _context = context;
        }

        // GET: api/QuestionInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionInfo>>> GetQuestionInfos()
        {
            return await _context.QuestionInfos.ToListAsync();
        }

        // GET: api/QuestionInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionInfo>> GetQuestionInfo(int id)
        {
            var questionInfo = await _context.QuestionInfos.FindAsync(id);

            if (questionInfo == null)
            {
                return NotFound();
            }

            return questionInfo;
        }

        // PUT: api/QuestionInfoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionInfo(int id, QuestionInfo questionInfo)
        {
            if (id != questionInfo.ID)
            {
                return BadRequest();
            }

            _context.Entry(questionInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionInfoExists(id))
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

        // POST: api/QuestionInfoes
        [HttpPost]
        public async Task<ActionResult<QuestionInfo>> PostQuestionInfo(QuestionInfo questionInfo)
        {
            _context.QuestionInfos.Add(questionInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionInfo", new { id = questionInfo.ID }, questionInfo);
        }

        // DELETE: api/QuestionInfoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestionInfo>> DeleteQuestionInfo(int id)
        {
            var questionInfo = await _context.QuestionInfos.FindAsync(id);
            if (questionInfo == null)
            {
                return NotFound();
            }

            _context.QuestionInfos.Remove(questionInfo);
            await _context.SaveChangesAsync();

            return questionInfo;
        }

        private bool QuestionInfoExists(int id)
        {
            return _context.QuestionInfos.Any(e => e.ID == id);
        }
    }
}
