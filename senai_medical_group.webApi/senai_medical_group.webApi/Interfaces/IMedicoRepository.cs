using senai_medical_group.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_medical_group.webApi.Interfaces
{
    interface IMedicoRepository
    {
        List<Medico> Listar();
        Medico BuscarPorId(int id);
        void Cadastrar(Medico novoMedico);
        void Atualizar(int id, Medico medicoAtualizado);
        void Deletar(int id);
    }
}
