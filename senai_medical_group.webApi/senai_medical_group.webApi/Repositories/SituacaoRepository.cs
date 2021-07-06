using senai_medical_group.webApi.Contexts;
using senai_medical_group.webApi.Domains;
using senai_medical_group.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_medical_group.webApi.Repositories
{
    public class SituacaoRepository : ISituacaoRepository
    {
        MedicalGroupContext ctx = new MedicalGroupContext();

        public List<Situacao> Listar()
        {
            return ctx.Situacaos.ToList();
        }
    }
}
