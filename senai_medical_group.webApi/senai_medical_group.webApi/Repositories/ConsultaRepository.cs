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
    public class ConsultaRepository : IConsultaRepository
    {
        /// <summary>
        /// Objeto contexto por onde serão chamados os metódos do EF Core
        /// </summary>
        MedicalGroupContext ctx = new MedicalGroupContext();

        public void AlterarDescricao(int id, Consulta status)
        {
            Consulta consultaBuscada = ctx.Consulta.Find(id);

            if (status.Descricao != null)
            {
                consultaBuscada.Descricao = status.Descricao;
            }

            ctx.Update(consultaBuscada);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Atualiza uma consulta existente
        /// </summary>
        /// <param name="id">ID da consulta que será atualizado</param>
        /// <param name="consultaAtualizada">Objeto com as novas informações</param>
        public void Atualizar(int id, Consulta consultaAtualizada)
        {
            //busca uma consulta através do id
            Consulta consultaBuscada = ctx.Consulta.Find(id);

            // Verifica se o ID da consulta foi informado
            if (consultaAtualizada.IdMedico != null)
            {
                // Atribui os novos valores aos campos existentes
                consultaBuscada.IdMedico = consultaAtualizada.IdMedico;
            }
            if (consultaAtualizada.IdPaciente != null)
            {
                // Atribui os novos valores aos campos existentes
                consultaBuscada.IdPaciente = consultaAtualizada.IdPaciente;
            }
            if (consultaAtualizada.IdSituacao != null)
            {
                // Atribui os novos valores aos campos existentes
                consultaBuscada.IdSituacao = consultaAtualizada.IdSituacao;
            }
            if (consultaAtualizada.Descricao != null)
            {
                // Atribui os novos valores aos campos existentes
                consultaBuscada.Descricao = consultaAtualizada.Descricao;
            }

            //Atualiza a classe atualizada
            ctx.Consulta.Update(consultaBuscada);

            // Salva as informações para serem gravadas no banco
            ctx.SaveChanges();
        }

        public int BuscarIdMedico(int id)
        {
            var medico = ctx.Medicos.FirstOrDefault(c => c.IdUsuario == id);

            return medico.IdMedico;
        }

        public int BuscarIdPaciente(int id)
        {
            var paciente = ctx.Pacientes.FirstOrDefault(c => c.IdUsuario == id);

            return paciente.IdPaciente;
        }

        /// <summary>
        /// Busca uma consulta através do ID
        /// </summary>
        /// <param name="id">ID da consulta que será buscada</param>
        /// <returns>As informações da consulta buscada.</returns>
        public Consulta BuscarPorId(int id)
        {
            // Retorna o primeiro tipo de evento encontrado para o ID informado
            return ctx.Consulta.FirstOrDefault(c => c.IdConsulta == id);
        }

        /// <summary>
        /// Cadastra uma nova consulta
        /// </summary>
        /// <param name="novaConsulta">Objeto novaConsulta que será cadastrada</param>
        public void Cadastrar(Consulta novaConsulta)
        {
            // Adiciona este novoMedico
            ctx.Consulta.Add(novaConsulta);

            // Salva as informações para serem gravadas no banco de dados
            ctx.SaveChanges();
        }

        /// <summary>
        /// Deleta uma consulta existente
        /// </summary>
        /// <param name="id">ID da consulta que será deletada</param>
        public void Deletar(int id)
        {
            // Busca um tipo de evento através do id
            Consulta consultaBuscada = ctx.Consulta.Find(id);

            // Remove o tipo de evento que foi buscado
            ctx.Consulta.Remove(consultaBuscada);

            // Salva as alterações
            ctx.SaveChanges();
        }

        /// <summary>
        /// Lista todas as consultas
        /// </summary>
        /// <returns>Uma lista de consultas</returns>
        public List<Consulta> Listar()
        {
            return ctx.Consulta.Include(c => c.IdMedicoNavigation)
                         .Include(c => c.IdPacienteNavigation)
                         .Include(c => c.IdSituacaoNavigation)
                         .Select(c => new Consulta()
                         {
                             IdConsulta = c.IdConsulta,
                             Descricao = c.Descricao,
                             DataConsulta = c.DataConsulta,

                             IdMedicoNavigation = new Medico()
                             {
                                 IdMedico = c.IdMedicoNavigation.IdMedico,
                                 NomeMedico = c.IdMedicoNavigation.NomeMedico,
                                 IdEspecialidade = c.IdMedicoNavigation.IdEspecialidade,
                                 IdClinica = c.IdMedicoNavigation.IdClinica,


                                 IdEspecialidadeNavigation = new Especialidade()
                                 {
                                     IdEspecialidade = c.IdMedicoNavigation.IdEspecialidadeNavigation.IdEspecialidade,
                                     Especialidade1 = c.IdMedicoNavigation.IdEspecialidadeNavigation.Especialidade1
                                 }

                             },

                             IdPacienteNavigation = new Paciente()
                             {
                                 IdPaciente = c.IdPacienteNavigation.IdPaciente,
                                 NomePaciente = c.IdPacienteNavigation.NomePaciente,
                                 DataNascimento = c.IdPacienteNavigation.DataNascimento,
                                 Rg = c.IdPacienteNavigation.Rg,
                                 Cpf = c.IdPacienteNavigation.Cpf,
                                 Endereco = c.IdPacienteNavigation.Endereco
                             },

                             IdSituacaoNavigation = new Situacao()
                             {
                                 IdSituacao = c.IdSituacaoNavigation.IdSituacao,
                                 Situacao1 = c.IdSituacaoNavigation.Situacao1
                             }

                         }).ToList();
        }

        /// <summary>
        /// Lista todas as consultas do médico
        /// </summary>
        /// <param name="id">parametro id que identifica o id do médico</param>
        /// <returns>Uma lista de consultas</returns>
        public List<Consulta> ListarMinhasMedico(int id)
        {
            return ctx.Consulta.Include(c => c.IdMedicoNavigation)
                      .Include(c => c.IdPacienteNavigation)
                      .Include(c => c.IdSituacaoNavigation)
                      .Where(c => c.IdMedico == id)

                      .Select(c => new Consulta()
                      {
                          IdConsulta = c.IdConsulta,
                          Descricao = c.Descricao,
                          DataConsulta = c.DataConsulta,

                          IdMedicoNavigation = new Medico()
                          {
                              IdMedico = c.IdMedicoNavigation.IdMedico,
                              NomeMedico = c.IdMedicoNavigation.NomeMedico,
                              IdEspecialidade = c.IdMedicoNavigation.IdEspecialidade,
                              IdClinica = c.IdMedicoNavigation.IdClinica,


                              IdEspecialidadeNavigation = new Especialidade()
                              {
                                  IdEspecialidade = c.IdMedicoNavigation.IdEspecialidadeNavigation.IdEspecialidade,
                                  Especialidade1 = c.IdMedicoNavigation.IdEspecialidadeNavigation.Especialidade1
                              }

                          },

                          IdPacienteNavigation = new Paciente()
                          {
                              IdPaciente = c.IdPacienteNavigation.IdPaciente,
                              NomePaciente = c.IdPacienteNavigation.NomePaciente,
                              DataNascimento = c.IdPacienteNavigation.DataNascimento,
                              Rg = c.IdPacienteNavigation.Rg,
                              Cpf = c.IdPacienteNavigation.Cpf,
                              Endereco = c.IdPacienteNavigation.Endereco,
                              Telefone = c.IdPacienteNavigation.Telefone
                          },

                          IdSituacaoNavigation = new Situacao()
                          {
                              IdSituacao = c.IdSituacaoNavigation.IdSituacao,
                              Situacao1 = c.IdSituacaoNavigation.Situacao1
                          }

                      }).ToList();
        }

        /// <summary>
        /// Lista todas as consultas do paciente
        /// </summary>
        /// <param name="id">parametro id que identifica o id do paciente</param>
        /// <returns>Uma lista de consultas</returns>
        public List<Consulta> ListarMinhasPaciente(int id)
        {
            return ctx.Consulta.Include(c => c.IdMedicoNavigation)
                       .Include(c => c.IdPacienteNavigation)
                       .Include(c => c.IdSituacaoNavigation)
                       .Where(c => c.IdPaciente == id)

                       .Select(c => new Consulta()
                       {
                           IdConsulta = c.IdConsulta,
                           Descricao = c.Descricao,
                           DataConsulta = c.DataConsulta,

                           IdMedicoNavigation = new Medico()
                           {
                               IdMedico = c.IdMedicoNavigation.IdMedico,
                               NomeMedico = c.IdMedicoNavigation.NomeMedico,
                               IdEspecialidade = c.IdMedicoNavigation.IdEspecialidade,
                               IdClinica = c.IdMedicoNavigation.IdClinica,
                               Crm = c.IdMedicoNavigation.Crm,


                               IdEspecialidadeNavigation = new Especialidade()
                               {
                                   IdEspecialidade = c.IdMedicoNavigation.IdEspecialidadeNavigation.IdEspecialidade,
                                   Especialidade1 = c.IdMedicoNavigation.IdEspecialidadeNavigation.Especialidade1
                               }

                           },

                           IdPacienteNavigation = new Paciente()
                           {
                               IdPaciente = c.IdPacienteNavigation.IdPaciente,
                               NomePaciente = c.IdPacienteNavigation.NomePaciente,
                               DataNascimento = c.IdPacienteNavigation.DataNascimento,
                               Rg = c.IdPacienteNavigation.Rg,
                               Cpf = c.IdPacienteNavigation.Cpf,
                               Endereco = c.IdPacienteNavigation.Endereco
                           },

                           IdSituacaoNavigation = new Situacao()
                           {
                               IdSituacao = c.IdSituacaoNavigation.IdSituacao,
                               Situacao1 = c.IdSituacaoNavigation.Situacao1
                           }

                       }).ToList();
        }

        public void Situacao(int id, string status)
        {
            Consulta consultaBuscada = ctx.Consulta
                                          .FirstOrDefault(p => p.IdConsulta == id);

            switch (status)
            {
                case "1":
                    consultaBuscada.IdSituacao = 1;
                    break;

                case "2":
                    consultaBuscada.IdSituacao = 2;
                    break;

                case "3":
                    consultaBuscada.IdSituacao = 3;
                    break;

                default:
                    consultaBuscada.IdSituacao = consultaBuscada.IdSituacao;
                    break;
            }

            ctx.Consulta.Update(consultaBuscada);

            ctx.SaveChanges();
        }
    }
}
