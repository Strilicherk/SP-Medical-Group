using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_medical_group.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using senai_medical_group.webApi.Repositories;
using senai_medical_group.webApi.Domains;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace senai_medical_group.webApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private IMedicoRepository _medicoRepository { get; set; }
        public MedicoController()
        {
            _medicoRepository = new MedicoRepository();
        }

        /// <summary>
        /// Cadastra um novo médico
        /// </summary>
        /// <param name="novoMedico">O objeto novoMedico recebe as informações a serem cadastradas</param>
        /// <returns>Um Status Code 201 - Created</returns>
        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(Medico novoMedico)
        {
            // Chama método
            _medicoRepository.Cadastrar(novoMedico);
            // Retorna status code
            return StatusCode(201);
        }

        /// <summary>
        /// Lista todos os médicos
        /// </summary>
        /// <returns>Uma lista de médicos</returns>
        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Get()
        {
            // Retorna a resposta da requisição fazendo a chamada para o método
            return Ok(_medicoRepository.Listar());
        }

        /// <summary>
        /// Busca um médico através do id
        /// </summary>
        /// <param name="id">Parâmetro id a ser buscado</param>
        /// <returns>As informações do médico buscado.</returns>
        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_medicoRepository.BuscarPorId(id));
        }

        /// <summary>
        /// Atualiza as informaçoes de um médico
        /// </summary>
        /// <param name="id">Parâmetro id do médico a ser atualizado</param>
        /// <param name="medicoAtualizado">Objeto medicoAtualizado que vai receber as novas informaçoes</param>
        /// <returns>Um status code 204 - No Content</returns>
        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, Medico medicoAtualizado)
        {
            _medicoRepository.Atualizar(id, medicoAtualizado);
            return StatusCode(204);
        }

        /// <summary>
        /// Deleta um médico
        /// </summary>
        /// <param name="id">Parâmetro id a ser deletado</param>
        /// <returns>Um status code 204 - No Content</returns>
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Chama método
            _medicoRepository.Deletar(id);
            // Retorna status code
            return StatusCode(204);
        }
    }
}
