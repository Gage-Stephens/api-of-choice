using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIofChoiceGage.Models;

namespace APIofChoiceGage.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class CharactersController : ControllerBase
        {
            private readonly CharacterContext _context;

            public CharactersController(CharacterContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Character>>> GetCharacter()
            {
                if (_context.Characters == null)
                {
                    return NotFound();
                }
                return await _context.Characters.ToListAsync();
            }

          
            [HttpGet("{id}")]
            public async Task<ActionResult<Character>> GetCharacter(int id)
            {
                if (_context.Characters == null)
                {
                    return NotFound();
                }
                var character = await _context.Characters.FindAsync(id);

                if (character == null)
                {
                    return NotFound();
                }

                return character;
            }

            
            [HttpPut("{id}")]
            public async Task<IActionResult> PutCharacter(int id, Character character)
            {
                if (id != character.Id)
                {
                    return BadRequest();
                }

                _context.Entry(character).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterExists(id))
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

            
            [HttpPost]
            public async Task<ActionResult<Character>> PostCharacter(Character character)
            {
                if (_context.Characters == null)
                {
                    return Problem("Entity set 'CharacterContext.Character'  is null.");
                }
                _context.Characters.Add(character);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCharacter", new { id = character.Id }, character);
            }

            
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteCharacter(int id)
            {
                if (_context.Characters == null)
                {
                    return NotFound();
                }
                var character = await _context.Characters.FindAsync(id);
                if (character == null)
                {
                    return NotFound();
                }

                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool CharacterExists(int id)
            {
                return (_context.Characters?.Any(e => e.Id == id)).GetValueOrDefault();
            }

        }
    }
