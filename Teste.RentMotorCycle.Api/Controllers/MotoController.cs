using Microsoft.AspNetCore.Mvc;
using Test.RentMotorCycle.Api.ViewModel;
using Test.RentMotorCycles.Domain.Repository;
using Test.RentMotorCycles.Domain.Entity;
using MongoDB.Bson;
using Test.RentMotorCycles.Service;

namespace Test.RentMotorCycle.Api.Controllers
{
    [Route("/", Name = "Motos")]
    [ApiController]
    public class MotoController : ControllerBase
    {
        public readonly IMotoService _motoService;
        public ILocacaoService _locacaoService;

        public MotoController(IMotoService motoService)
        {
            _motoService = motoService;
        }


        // PUT /motos
        /// <summary>
        /// Modificar a placa de uma moto
        /// </summary>
        /// <response code="200">Placa modificada com sucesso</response>   
        /// <response code="400">Dados inválidos</response>   
        [HttpPut("motos/{id}/placa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult MotosUpdatePlate(string id, MotoPlacaViewModel moto)
        {
            try
            {
                Moto e = new Moto()
                {
                    _id = ObjectId.Parse(id),
                    placa = moto.placa
                };

                _motoService.GetMotoById(e);
                _motoService.UpdatePlate(e);

                return Ok(new { mensagem = "Placa modificada com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE /motos
        /// <summary>
        /// Remover uma moto
        /// </summary>
        /// <response code="200"></response>   
        /// <response code="400">Dados inválidos</response>   
        [HttpDelete("motos/{id}/placa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult MotoDelete(string id)
        {
            try
            {
                _locacaoService =  new LocacaoService();

                Moto e = new Moto()
                {
                    _id = ObjectId.Parse(id),
                };


                if(_locacaoService.GetLocacaoByMoto(e).Count > 0)
                    throw new Exception("Existe em locações ligadas a esse veículo.");


                _motoService.DeleteMoto(e);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
 


        // GET /Motos
        /// <summary>
        /// Lista motos existentes
        /// </summary>
        /// <response code="200">Listagem de motos</response>   
        [HttpGet("motos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult MotoByPlate(String? placa)
        {
            try
            {
                List<Moto> l = _motoService.GetAllMotoFilter(placa);

                return Ok(l);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Dados inválidos" });
            }
        }


        // GET /Motos
        /// <summary>
        /// Consultar motos existentes por id
        /// </summary>
        /// <response code="200">Listagem de motos</response>   
        [HttpGet("motos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult MotosById(String id)
        {
            try
            {
                Moto e = new Moto()
                {
                    _id = ObjectId.Parse(id),
                };
                List<Moto> l = _motoService.GetMotoById(e);

                return Ok(l);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        // POST /Motos
        /// <summary>
        /// Cadastrar Moto
        /// </summary>
        /// <response code="400">Dados inválidos</response>   
        [HttpPost("motos")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult MotosCadastro(MotoViewModel moto)
        {
            try
            {
                Moto e = new Moto()
                {
                    identificador = moto.identificador,
                    ano = moto.ano,
                    modelo = moto.modelo,
                    placa = moto.placa,
                };
                e.Validate();
                _motoService.PlacaExists(e.placa);

                _motoService.SetMotoMessage(e);

                var locationUri = Url.Action("Moto", e);
                return Created(locationUri, e);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
