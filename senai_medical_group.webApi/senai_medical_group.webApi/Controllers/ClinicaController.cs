using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_medical_group.webApi.Domains;
using senai_medical_group.webApi.Interfaces;
using senai_medical_group.webApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_medical_group.webApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicaController : ControllerBase
    {
        private IClinicaRepository _clinicaRepository { get; set; }

        public ClinicaController()
        {
            _clinicaRepository = new ClinicaRepository();
        }
        
        /// <summary>
        /// Cadastra uma nova clínica
        /// </summary>
        /// <param name="novaClinica">Objeto novaClinica que receberá as informações a serem cadastradas</param>
        /// <returns>Um Status Code 201 - Created</returns>
        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(Clinica novaClinica)
        {
            _clinicaRepository.Cadastrar(novaClinica);
            return StatusCode(201);
        }

        /// <summary>
        /// Lista todas as clinicas
        /// </summary>
        /// <returns>Um status code 200 - ok e uma lista de clinicas</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_clinicaRepository.Listar());
        }

        /// <summary>
        /// Deleta uma clinica existente
        /// </summary>
        /// <param name="id">Id da clinica que será deletado</param>
        /// <returns>Status Code 204 - No Content</returns>
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _clinicaRepository.Deletar(id);
            return StatusCode(204);
        }
    }
}
