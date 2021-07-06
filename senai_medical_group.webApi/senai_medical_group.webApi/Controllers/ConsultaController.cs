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
    public class ConsultaController : ControllerBase
    {
        private IConsultaRepository _consultaRepository { get; set; }

        public ConsultaController()
        {
            _consultaRepository = new ConsultaRepository();
        }

        /// <summary>
        /// Lista todas as consultas
        /// </summary>
        /// <returns>Uma lista de consultas</returns>
        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_consultaRepository.Listar());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Lista uma consulta através do seu id
        /// </summary>
        /// <param name="id">Parâmetro id a ser buscado</param>
        /// <returns>Uma consulta</returns>
        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
                return Ok(_consultaRepository.BuscarPorId(id));
        }

        /// <summary>
        /// Lista todas as consultas de um determinado médico
        /// </summary>
        /// <param name="id">parâmetro id a ser buscado</param>
        /// <returns>Uma lista das consultas daquele determinado médico</returns>
        [Authorize(Roles = "2")]
        [HttpGet("medico")]
        public IActionResult GetMedico(int id)
        {
            int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            var idMedico = _consultaRepository.BuscarIdMedico(idUsuario);

            return Ok(_consultaRepository.ListarMinhasMedico(idMedico));
        }

        /// <summary>
        /// Lista todas as consultas de um determinado paciente
        /// </summary>
        /// <param name="id">parâmetro id a ser buscado</param>
        /// <returns>Uma lista das consultas daquele determinado paciente</returns>
        [Authorize(Roles = "3")]
        [HttpGet("paciente")]
        public IActionResult GetPaciente(int id)
        {
            int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            var idPaciente = _consultaRepository.BuscarIdPaciente(idUsuario);

            return Ok(_consultaRepository.ListarMinhasPaciente(idPaciente));
        }

        /// <summary>
        /// Cadastra uma nova consulta
        /// </summary>
        /// <param name="novaConsulta">O objeto novaConsulta recebe as informações a serem cadastradas</param>
        /// <returns>Retorna um status code 201- Created </returns>
        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(Consulta novaConsulta)
        {
            // Chama o método
            _consultaRepository.Cadastrar(novaConsulta);
            // Retorna um status code
            return StatusCode(201);
        }

        /// <summary>
        /// Atualiza uma consulta através do ID
        /// </summary>
        /// <param name="id">Parâmetro ID que será buscado</param>
        /// <param name="consultaAtualizada">Objeto onde serão armazenadas as novas informações</param>
        /// <returns>Retorna um status code 204</returns>
        [Authorize(Roles = "1,2")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, Consulta consultaAtualizada)
        {
            // Chama o método
            _consultaRepository.Atualizar(id, consultaAtualizada);
            // Retorna um status code
            return StatusCode(204);
        }

        /// <summary>
        /// Deleta uma consulta existente
        /// </summary>
        /// <param name="id">Id da consulta que será deletada</param>
        /// <returns>Status Code 204 - No Content</returns>
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Chama o métodos
            _consultaRepository.Deletar(id);
            // Retorna um Status Code
            return StatusCode(204);
        }

        /// <summary>
        /// Altera a descrição da consulta
        /// </summary>
        /// <param name="id">Parâmetro ID da descrição que será mudada</param>
        /// <param name="Descricao">objeto Descricao que recebera a nova descrição</param>
        /// <returns>Status Code 204 - No Content</returns>
        [Authorize(Roles = "2")]
        [HttpPatch("medico/{id}")]
        public IActionResult PatchDescricao(int id, Consulta Descricao)
        {
                _consultaRepository.AlterarDescricao(id, Descricao);
                return StatusCode(204);
        }

        /// <summary>
        /// Altera uma situação
        /// </summary>
        /// <param name="id">Id da consulta que será alterada</param>
        /// <param name="status">Objeto status que recebe as novas informações</param>
        /// <returns>Status Code 204 - No Content</returns>
        [Authorize(Roles = "1")]
        [HttpPatch("{id}")]
        public IActionResult AtualizarSituacao(int id, Situacao status)
        {
            try
            {
                _consultaRepository.Situacao(id, status.Situacao1);

                return StatusCode(204);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
