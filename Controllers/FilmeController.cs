using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly FilmeContext _context;

        public FilmeController(FilmeContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona um novo filme.
        /// </summary>
        /// <param name="filme">Objeto Filme que será adicionado.</param>
        /// <returns>Retorna o filme criado com o status 201.</returns>
        [HttpPost("adicionar")]
        public IActionResult AdicionarFilme([FromBody] Filme filme)
        {
            _context.Filme.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarFilmePorId), new { id = filme.Id }, filme);
        }

        /// <summary>
        /// Recupera uma lista de filmes com paginação.
        /// </summary>
        /// <param name="skip">Número de registros a serem pulados.</param>
        /// <param name="take">Número de registros a serem retornados.</param>
        /// <returns>Retorna uma lista de filmes.</returns>
        [HttpGet("busca")]
        public IEnumerable<Filme> RecuperarFilme([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _context.Filme.Skip(skip).Take(take);
        }

        /// <summary>
        /// Recupera um filme pelo ID.
        /// </summary>
        /// <param name="id">ID do filme a ser recuperado.</param>
        /// <returns>Retorna o filme correspondente ao ID fornecido.</returns>
        [HttpGet("busca/{id}")]
        public IActionResult RecuperarFilmePorId(int id)
        {
            var filme = _context.Filme.FirstOrDefault(f => f.Id == id);
            if (filme == null) return NotFound();
            return Ok(filme);
        }

        /// <summary>
        /// Atualiza um filme existente.
        /// </summary>
        /// <param name="id">ID do filme a ser atualizado.</param>
        /// <param name="filme">Objeto Filme com os dados atualizados.</param>
        /// <returns>Retorna status 204 se a atualização for bem-sucedida.</returns>
        [HttpPut("atualiza/{id}")]
        public async Task<IActionResult> AtualizaFilme(int id, [FromBody] Filme filme)
        {
            var filmeExistente = await _context.Filme.FirstOrDefaultAsync(f => f.Id == id);
            if (filmeExistente == null) return NotFound();

            filmeExistente.Titulo = filme.Titulo;
            filmeExistente.Genero = filme.Genero;
            filmeExistente.Duracao = filme.Duracao;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Deleta um filme pelo ID.
        /// </summary>
        /// <param name="id">ID do filme a ser deletado.</param>
        /// <returns>Retorna status 204 se a deleção for bem-sucedida.</returns>
        [HttpDelete("deletar/{id}")]
        public async Task<IActionResult> DeletaFilme(int id)
        {
            var filmeExistente = await _context.Filme.FirstOrDefaultAsync(f => f.Id == id);
            if (filmeExistente == null) return NotFound();

            _context.Filme.Remove(filmeExistente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
