using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();
        private static int id = 0;

        //postar filme , adiciona
        [HttpPost("adicionar")]
        public IActionResult AdicionarFilme([FromBody] Filme filme)
        {
            filme.Id = id++;
            filmes.Add(filme);
            return CreatedAtAction(nameof(RecuperarFilmePorId), new {id = filme.Id } , filme);
        }


        //lista de filmes 
        [HttpGet("busca")]
        public IEnumerable<Filme> RecuperarFilme([FromQuery]int skip = 0, [FromQuery]int take = 50 )
        {
            return filmes.Skip(skip).Take(take);
        }


        [HttpGet("busca/{id}")]
        public IActionResult RecuperarFilmePorId(int id)
        {         
            var filme =  filmes.FirstOrDefault(filme => filme.Id == id);  
            if (filme == null) return NotFound();
            return Ok(filme);
            
        }

    }
} 
