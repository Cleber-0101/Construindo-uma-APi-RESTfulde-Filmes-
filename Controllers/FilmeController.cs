using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmeController : ControllerBase
    {
        //private static List<Filme> filmes = new List<Filme>();
        //private static int id = 0;


        //fazer com que a api intenda a existencia do Context a conexão com banco de dados , instancia
        private FilmeContext _context;

        public FilmeController(FilmeContext context)
        {
            _context = context;
        }

        //postar filme , adiciona
        [HttpPost("adicionar")]
        public IActionResult AdicionarFilme([FromBody] Filme filme)
        {
            _context.Filme.Add(filme);
            _context.SaveChanges(); 
            return CreatedAtAction(nameof(RecuperarFilmePorId), new {id = filme.Id } , filme);
        }


        //lista de filmes 
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

    }
} 
