using senai_medical_group.webApi.Contexts;
using senai_medical_group.webApi.Domains;
using senai_medical_group.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_medical_group.webApi.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        /// <summary>
        /// Objeto contexto por onde serão chamados os métodos do EF Core
        /// </summary>
        MedicalGroupContext ctx = new MedicalGroupContext();

        /// <summary>
        /// Cadastra um novo tipo de usuário
        /// </summary>
        /// <param name="novoTipoUsuario">Objeto novoTipoUsuario que será cadastrado</param>
        public void Cadastrar(TipoUsuario novoTipoUsuario)
        {
            // Adiciona este novoTipoUsuario
            ctx.TipoUsuarios.Add(novoTipoUsuario);

            // Salva as informações para serem gravadas no banco de dados
            ctx.SaveChanges();
        }

        /// <summary>
        /// Deleta um tipo de usuário existente
        /// </summary>
        /// <param name="id">ID do tipo de usuario que será deletado</param>
        public void Deletar(int id)
        {
            // Busca um tipo de usuario através do id
            TipoUsuario tipoBuscado = ctx.TipoUsuarios.Find(id);

            // Remove o tipo de usuario que foi buscado
            ctx.TipoUsuarios.Remove(tipoBuscado);

            // Salva as alterações
            ctx.SaveChanges();
        }

        public List<TipoUsuario> Listar()
        {
            return ctx.TipoUsuarios.ToList();
        }
    }
}
