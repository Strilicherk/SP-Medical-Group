using Microsoft.EntityFrameworkCore;
using senai_medical_group.webApi.Contexts;
using senai_medical_group.webApi.Domains;
using senai_medical_group.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_medical_group.webApi.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        /// <summary>
        /// Objeto contexto por onde serão chamados os métodos do EF Core
        /// </summary>
        MedicalGroupContext ctx = new MedicalGroupContext();

        /// <summary>
        /// Atualiza um paciente existente
        /// </summary>
        /// <param name="id">ID do paciente que será atualizado</param>
        /// <param name="pacienteAtualizado">Objeto com as novas informações</param>
        public void Atualizar(int id, Paciente pacienteAtualizado)
        {
            //busca uma habilidade através do id
            Paciente pacienteBuscado = ctx.Pacientes.Find(id);

            // Verifica se o nome do paciente foi informado
            if (pacienteAtualizado.NomePaciente != null)
            {
                // Atribui os novos valores aos campos existentes
                pacienteBuscado.NomePaciente = pacienteAtualizado.NomePaciente;
            }

            // Verifica se o telefone do paciente foi informado
            if (pacienteAtualizado.Telefone != null)
            {
                // Atribui os novos valores aos campos existentes
                pacienteBuscado.Telefone = pacienteAtualizado.Telefone;
            }

            // Verifica se o endereço do paciente foi informado
            if (pacienteAtualizado.Endereco != null)
            {
                // Atribui os novos valores aos campos existentes
                pacienteBuscado.Endereco = pacienteAtualizado.Endereco;
            }

            //Atualizo a classe atualizada
            ctx.Pacientes.Update(pacienteBuscado);

            // Salva as informações para serem gravadas no banco
            ctx.SaveChanges();
        }

        /// <summary>
        /// Busca um paciente através do ID
        /// </summary>
        /// <param name="id">ID do paciente que será buscado</param>
        /// <returns>Um paciente buscado</returns>
        public Paciente BuscarPorId(int id)
        {
            // Retorna o primeiro tipo de evento encontrado para o ID informado
            return ctx.Pacientes.FirstOrDefault(m => m.IdPaciente == id);
        }

        /// <summary>
        /// Cadastra um novo paciente
        /// </summary>
        /// <param name="novoPaciente">Objeto novoPaciente que será cadastrado</param>
        public void Cadastrar(Paciente novoPaciente)
        {
            // Adiciona este novoMedico
            ctx.Pacientes.Add(novoPaciente);

            // Salva as informações para serem gravadas no banco de dados
            ctx.SaveChanges();
        }

        /// <summary>
        /// Deleta um paciente existente
        /// </summary>
        /// <param name="id">ID do paciente que será deletado</param>
        public void Deletar(int id)
        {
            // Busca um tipo de evento através do id
            Paciente pacienteBuscado = ctx.Pacientes.Find(id);

            // Remove o tipo de evento que foi buscado
            ctx.Pacientes.Remove(pacienteBuscado);

            // Salva as alterações
            ctx.SaveChanges();
        }

        /// <summary>
        /// Lista todos os pacientes
        /// </summary>
        /// <returns>Uma lista de pacientes</returns>
        public List<Paciente> Listar()
        {
            // Retorna uma lista com todas as informações dos médicos
            return ctx.Pacientes.ToList();
        }
    }
}
