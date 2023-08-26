using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SzotanuloAPI.Entities;

namespace SzotanuloAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WordsController : ControllerBase
    {
        public readonly DBContext _context;
        public WordsController(DBContext context) 
        {
            _context = context;
        }

        [HttpGet("getAll/{treshold}")]
        public async Task<object> GetAll(int treshold)
        {
            try
            {
                var words = await _context.Words.Where(x => x.RememberanceLevel < treshold).ToListAsync();
                return words;
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //quantity = the quantity of return values
        //treshold = return the lower number values
        [HttpGet("lowRememberance/{quantity}/{treshold}")]
        public async Task<IEnumerable<Words>> GetFirstXElement(uint quantity, uint treshold)
        {
            try
            {
                var elements = await _context.Words.
                    OrderBy(x => x.RememberanceLevel).
                    Where(x => x.RememberanceLevel <= treshold).Take((int)quantity).ToListAsync();
                return elements;
            } catch (Exception)
            {
                return new List<Words>();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Words>> GetWord(int id)
        {
            try
            {
                var result = await _context.Words.FirstAsync(x => x.wordId == id);
                return result;
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong. Error: " + e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Words>> PostWord(Words word)
        {
            try
            {
                await _context.Words.AddAsync(word);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetWord", new { id = word.wordId }, word);
            } catch (Exception ex)
            {
                return NotFound("Error while trying to save: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Words>> PutWord(Words word, int id)
        {
            try
            {
                if (word.wordId != id)
                {
                    return BadRequest("Id is not the same");
                } else
                {
                    _context.Entry(word).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                NotFound("Error while parsing: " + ex.Message);
            }
            return BadRequest("Something went wrong. Try again later");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Words>> DeleteWord(int id)
        {
            var word = await _context.Words.FirstOrDefaultAsync(x => x.wordId == id);
            try
            {
                if (word != null)
                {
                    var result = _context.Words.Remove(word);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound("word not found.");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong. Error: " + e.Message);
            }
        }
    }
}
