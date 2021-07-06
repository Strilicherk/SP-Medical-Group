using senai_medical_group.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_medical_group.webApi.Interfaces
{
    interface IClinicaRepository
    {

        List<Clinica> Listar();

        void Cadastrar(Clinica novaClinica);

        void Deletar(int id);
    }
}
