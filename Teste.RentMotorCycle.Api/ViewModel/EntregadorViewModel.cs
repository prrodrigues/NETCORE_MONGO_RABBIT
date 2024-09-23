using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Test.RentMotorCycle.Api.ViewModel
{
    public class EntregadorViewModel
    {
        public string identificador { get; set; }

        public string nome { get; set; }

        public string cnpj { get; set; }

        public string data_nascimento { get; set; }

        public string numero_cnh { get; set; }

        public string tipo_cnh { get; set; }

        public string imagem_cnh { get; set; }

    }
}
