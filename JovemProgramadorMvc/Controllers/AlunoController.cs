using JovemProgramadorMvc.Data.Repositorio.interfaces;
using JovemProgramadorMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace JovemProgramadorMvc.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IAlunoRepositorio _alunoRepositorio;
        public AlunoController(IConfiguration configuration, IAlunoRepositorio alunoRepositorio)
        {
            _configuration = configuration;
            _alunoRepositorio = alunoRepositorio;
        }
        public IActionResult Index(AlunoModel filtroAluno )
        {
            List<AlunoModel> aluno = new();
            if(filtroAluno.Idade > 0 )
            {
                aluno = _alunoRepositorio.FiltroIdade(filtroAluno.Idade);
            }
            else
            {
                aluno = _alunoRepositorio.BuscarAlunos();
            }

             
            return View(aluno);
        }
        public IActionResult Adicionar()
        {
            return View();
        }
        public IActionResult Mensagem()
        {
            return View();
        }
        public async Task<IActionResult> BuscarCep(string cep)
        {
            EnderecoModel enderecoModel = new();
            try
            {
                cep = cep.Replace("-", "");
                using var client = new HttpClient();
                var result = await client.GetAsync(_configuration.GetSection("ApiCep")["BaseUrl"] + cep + "/json");
                if (result.IsSuccessStatusCode)
                {
                    enderecoModel = JsonSerializer.Deserialize<EnderecoModel>(
                        await result.Content.ReadAsStringAsync(), new JsonSerializerOptions() { });
                    if (enderecoModel.complemento == "")
                    {
                        enderecoModel.complemento = "Sem complemento";
                    }
                    if (Regex.IsMatch(cep, (@"000")) == true)
                    {
                        enderecoModel.logradouro = "CEP geral de " + enderecoModel.localidade;
                        enderecoModel.bairro = "Não especificado";
                    }
                }
                else
                {
                    ViewData["Mensagem"] = "Erro na busca do endereço!";
                    return View("Index");
                }
            }
            catch (Exception e)
            {
            }
            return View("Buscarcep", enderecoModel);
        }

        [HttpPost]
        public IActionResult Inserir(AlunoModel aluno)
        {
            var retorno = _alunoRepositorio.Inserir(aluno);
            if (retorno != null)
            {
                TempData["Mensagem2"] = "Dados gravados com sucesso!";
                
            }
            return RedirectToAction("index");
        }
        public IActionResult Editar (int id )
        {
            var aluno = _alunoRepositorio.BuscarId(id);
            return View("Editar", aluno);
        }

        public IActionResult Atualizar(AlunoModel aluno)
        {
            var retorno = _alunoRepositorio.Atualizar(aluno);

            return RedirectToAction("Index");
        }

        public IActionResult Excluir(int id)
        {

            var retorno = _alunoRepositorio.Excluir(id);
            if (retorno != false)
            {
                TempData["Mensagem3"] = "Aluno excluido com sucesso!";
            }
            else
            {

                TempData["Mensagem3"] = "Não foi possivel excluir o aluno";
            }
                
            return RedirectToAction("Index");

        }

        public IActionResult FiltroIdade()
        {
            return View();
        }

       
    }


}