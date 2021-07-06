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
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private ITipoUsuarioRepository _tipoRepository { get; set; }
        public TipoUsuarioController()
        {
            _tipoRepository = new TipoUsuarioRepository();
        }

        /// <summary>
        /// Cadastra um novo tipo de usuário
        /// </summary>
        /// <param name="novoTipo">O objeto novoTipo recebe as informações a serem cadastradas</param>
        /// <returns>Um Status Code 201 - Created</returns>
        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(TipoUsuario novoTipo)
        {
            // Chama método
            _tipoRepository.Cadastrar(novoTipo);
            // Retorna status code
            return StatusCode(201);
        }

        /// <summary>
        /// Lista todos os tipos de usuarios
        /// </summary>
        /// <returns>Uma lista de tipos</returns>
        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Get()
        {
            // Retorna a resposta da requisição fazendo a chamada para o método
            return Ok(_tipoRepository.Listar());
        }
        /// <summary>
        /// Deleta um tipo de usuário
        /// </summary>
        /// <param name="id">Parâmetro id a ser deletado</param>
        /// <returns>Um status code 204 - No Content</returns>
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Chama método
            _tipoRepository.Deletar(id);
            // Retorna status code
            return StatusCode(204);
        }
    }
}
