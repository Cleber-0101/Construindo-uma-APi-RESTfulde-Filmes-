using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmeController : ControllerBase
    {
       
        private FilmeContext _context;

        public FilmeController(FilmeContext context)
        {
            _context = context;
        }
        


        [HttpPost("adicionar")]
        public IActionResult AdicionarFilme([FromBody] Filme filme)
        {
           
            _context.Filme.Add(filme);
            _context.SaveChanges(); 
            return CreatedAtAction(nameof(RecuperarFilmePorId), new {id = filme.Id } , filme);
        }


        [HttpGet("busca")]
        public IEnumerable<Filme> RecuperarFilme([FromQuery]int skip = 0, [FromQuery]int take = 50 )
        {
            return _context.Filme.Skip(skip).Take(take);
        }


        [HttpGet("busca/{id}")]
        public IActionResult RecuperarFilmePorId(int id)
        {         
            var filme =  _context.Filme.FirstOrDefault(filme => filme.Id == id);  
            if (filme == null) return NotFound();
            return Ok(filme);
            
        }

        [HttpPut("atualiza/{id}")]
        public async Task<IActionResult> AtualizaFilme(int id, [FromBody] Filme filme)
        {
            // Verificar se o filme existe no banco de dados
            var filmeExistente = await _context.Filme.FirstOrDefaultAsync(f => f.Id == id);
            if (filmeExistente == null) return NotFound();

            // Atualizar as propriedades do filme existente
            filmeExistente.Titulo = filme.Titulo;          
            filmeExistente.Genero = filme.Genero;
            filmeExistente.Duracao = filme.Duracao;
            // Adicione outras propriedades conforme necessário

            // Salvar as alterações no banco de dados
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 
