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
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Objeto _usuarioRepository que irá receber todos os métodos definidos na interface IUsuarioRepository
        /// </summary>
        private IUsuarioRepository _usuarioRepository { get; set; }

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        /// <summary>
        /// Lista todos os usuários
        /// </summary>
        /// <returns>Uma lista dos usuários e suas informações</returns>
        [Authorize (Roles = "1")]
        [HttpGet]
        public IActionResult Get()
        {
                return Ok(_usuarioRepository.Listar()); 
        }

        /// <summary>
        /// Busca um usuário através do seu ID
        /// </summary>
        /// <param name="id">ID do usuário que será buscado</param>
        /// <returns>Um usuário que será buscada</returns>
        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
                //Retorna a resposta da requisição fazenda a chamada para o método
                return Ok(_usuarioRepository.BuscarPorId(id));
        }

        /// <summary>
        /// Atualiza um usuário existente
        /// </summary>
        /// <param name="id">ID do usuário que será atualizado</param>
        /// <param name="usuarioAtualizado">Objeto usuarioAtualizado com as novas informações</param>
        /// <returns>Retorna um status code 204 - No Content</returns>
        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, Usuario usuarioAtualizado)
        {
                //Faz a chamada para o método
                _usuarioRepository.Atualizar(id, usuarioAtualizado);
                //Retorna um status code
                return StatusCode(204);
            
        }

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="novoUsuario">Objeto chamado novoUsuario</param>
        /// <returns>Um status code - 201</returns>
        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(Usuario novoUsuario)
        {
                //Faz a chamada para o método
                _usuarioRepository.Cadastrar(novoUsuario);

                //Retorna um status code
                return StatusCode(201);
        }

        /// <summary>
        /// Deleta um usuário existente
        /// </summary>
        /// <param name="id">Id do usuário que será deletado</param>
        /// <returns>Retorna um status code 204 - No Content</returns>
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //Faz a chamada para o método
            _usuarioRepository.Deletar(id);
            //Retorna um status code
            return StatusCode(204);
        }
    }
}
