using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_medical_group.webApi.Domains;
using senai_medical_group.webApi.Interfaces;
using senai_medical_group.webApi.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace senai_medical_group.webApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private IPacienteRepository _pacienteRepository { get; set; }
        public PacienteController()
        {
            _pacienteRepository = new PacienteRepository();
        }

        /// <summary>
        /// Cadastra um novo paciente
        /// </summary>
        /// <param name="novoPaciente">O objeto novoPaciente recebe as informações a serem cadastradas</param>
        /// <returns>Um Status Code 201 - Created</returns>
        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(Paciente novoPaciente)
        {
            // Chama método
            _pacienteRepository.Cadastrar(novoPaciente);
            // Retorna status code
            return StatusCode(201);
        }

        /// <summary>
        /// Lista todos os pacientes
        /// </summary>
        /// <returns>Uma lista de pacientes</returns>
        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Get()
        {
            // Retorna a resposta da requisição fazendo a chamada para o método
            return Ok(_pacienteRepository.Listar());
        }

        /// <summary>
        /// Busca um paciente através do id
        /// </summary>
        /// <param name="id">Parâmetro id a ser buscado</param>
        /// <returns>Um paciente especifico</returns>
        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_pacienteRepository.BuscarPorId(id));
        }

        /// <summary>
        /// Atualiza as informaçoes de um paciente
        /// </summary>
        /// <param name="id">Parâmetro id do paciente a ser atualizado</param>
        /// <param name="pacienteAtualizado">Objeto pacienteAtualizado que vai receber as novas informaçoes</param>
        /// <returns>Um status code 204 - No Content</returns>
        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, Paciente pacienteAtualizado)
        {
            _pacienteRepository.Atualizar(id, pacienteAtualizado);
            return StatusCode(204);
        }

        /// <summary>
        /// Deleta um paciente
        /// </summary>
        /// <param name="id">Parâmetro id a ser deletado</param>
        /// <returns>Um status code 204 - No Content</returns>
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Chama método
            _pacienteRepository.Deletar(id);
            // Retorna status code
            return StatusCode(204);
        }
    }
}
