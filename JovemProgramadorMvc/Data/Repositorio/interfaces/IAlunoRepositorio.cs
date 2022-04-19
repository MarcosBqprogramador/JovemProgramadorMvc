using JovemProgramadorMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JovemProgramadorMvc.Data.Repositorio.interfaces
{
    public interface IAlunoRepositorio
    {
        AlunoModel Inserir(AlunoModel aluno);

        List<AlunoModel> BuscarAlunos();

        AlunoModel BuscarId(int id);

        bool Atualizar(AlunoModel aluno);
    }
}
