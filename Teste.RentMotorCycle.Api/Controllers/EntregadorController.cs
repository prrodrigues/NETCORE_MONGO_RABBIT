using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Test.RentMotorCycle.Api.ViewModel;
using Test.RentMotorCycles.Domain.Entity;
using Test.RentMotorCycles.Domain.Repository;

namespace Test.RentMotorCycle.Api.Controllers
{
    [Route("/", Name = "Entregadores")]
    [ApiController]
    public class EntregadorController : ControllerBase
    {
        public readonly IEntregadorService _entregadorService;

        public EntregadorController(IEntregadorService EntregadorService)
        {
            _entregadorService = EntregadorService;
        }

        // POST /entregadores
        /// <summary>
        /// Cadastrar Entregador
        /// </summary>
        /// <response code="400">Dados inválidos</response>   
        [HttpPost("entregadores")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult entregadores(EntregadorViewModel entregador)
        {
            try
            {
                Entregador e = new Entregador()
                {
                    identificador = entregador.identificador,
                    nome = entregador.nome,
                    cnpj = entregador.cnpj,
                    data_nascimento = entregador.data_nascimento,
                    numero_cnh = entregador.numero_cnh,
                    tipo_cnh = entregador.tipo_cnh,
                    imagem_cnh = entregador.imagem_cnh
                };
                
                e.Validate();
                _entregadorService.CNPJExists(e.cnpj);
                _entregadorService.CNHExists(e.numero_cnh);

                SaveImage(e);

                e.imagem_cnh = GetFileName(e);

                _entregadorService.SetEntregador(e);

                var locationUri = Url.Action("Entregador", e);
                return Created(locationUri, e);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST /entregadores
        /// <summary>
        /// Enviar foto da CNH
        /// </summary>
        /// <response code="400">Dados inválidos</response>   
        [HttpPost("entregadores/{id}/cnh")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult entregadoresCnhImage(string id, EntregadorCNHViewModel entregador)
        {
            try
            {

                Entregador e = new Entregador()
                {
                    _id = ObjectId.Parse(id),
                    imagem_cnh = entregador.imagem_cnh
                };

                e = _entregadorService.GetEntregadorById(e);
                e.imagem_cnh = entregador.imagem_cnh;

                entregador.Validate();


                SaveImage(e);
                e.imagem_cnh = GetFileName(e);

                _entregadorService.UpdateCNHImageEntregador(e);

                var locationUri = Url.Action("GetEmployee", e);
                return Created(locationUri, e);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        string GetFileName(Entregador entregador){
            return  $"{entregador.numero_cnh}img.{GetImageExtension(entregador.imagem_cnh)}";
        }


        void SaveImage(Entregador entregador)
        {
            string filePath = @$"./IMGSTORAGE/{GetFileName(entregador)}";
            byte[] imageAsBytes = Convert.FromBase64String(getPureBase64String(entregador.imagem_cnh));
            System.IO.File.WriteAllBytes(filePath, imageAsBytes);
        }

        string getPureBase64String(string base64String){
            int pos = base64String.IndexOf("base64,");
            pos = pos != -1 ? pos + 7 : 0;
            var data = base64String.Substring(pos, base64String.Length - pos);
            return data;
        }

        string GetImageExtension(string base64String)
        {
            string str = getPureBase64String(base64String);
            var dataextension = str.Substring(0, 1);
            Dictionary<String, String> extensions = new Dictionary<string, string>();
            extensions.Add("I", "png");
            extensions.Add("Q", "bmp");
            return extensions[dataextension.ToUpper()];
        }

    }
}
