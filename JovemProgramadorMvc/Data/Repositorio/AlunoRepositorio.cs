using JovemProgramadorMvc.Data.Repositorio.interfaces;
using JovemProgramadorMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JovemProgramadorMvc.Data.Repositorio
{
    public class AlunoRepositorio : IAlunoRepositorio
    {
        private readonly BancoContexto _bancoContexto;

        public AlunoRepositorio(BancoContexto bancoContexto)
        {

            _bancoContexto = bancoContexto;

        }

        public AlunoModel Inserir(AlunoModel aluno)
        {

            _bancoContexto.Aluno.Add(aluno);
            _bancoContexto.SaveChanges();
            return aluno;

        }
        public List<AlunoModel> BuscarAlunos()
        {
            return _bancoContexto.Aluno.ToList();
        }

        public AlunoModel BuscarId(int id)
        {
            return _bancoContexto.Aluno.FirstOrDefault(x => x.id == id);
        }
            

        public bool Atualizar (AlunoModel aluno)
        {
            AlunoModel alunoDb = BuscarId(aluno.id);

            if (alunoDb == null)
                return false;

            alunoDb.Nome = aluno.Nome;
            alunoDb.Idade = aluno.Idade;
            alunoDb.Contato = aluno.Contato;
            alunoDb.Email = aluno.Email;
            alunoDb.Cep = aluno.Cep;

            _bancoContexto.Aluno.Update(alunoDb);
            _bancoContexto.SaveChanges();

            return true;

        }

    }
}
