using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JovemProgramadorMvc.Models
{
    public class ProfessorModel
    {
        public string nome { get; set; }
        public string idade { get; set; }
        public string contato { get; set; }
        public string email { get; set; }
        public string cep { get; set; }
        public ProfessorModel()
        {

        }
    }
}
