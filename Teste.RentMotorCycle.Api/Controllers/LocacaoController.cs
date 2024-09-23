using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Test.RentMotorCycle.Api.ViewModel;
using Test.RentMotorCycles.Domain.Entity;
using Test.RentMotorCycles.Domain.Repository;
using Microsoft.AspNetCore.Http;

namespace Test.RentMotorCycle.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocacaoController : ControllerBase
    {
        private readonly ILocacaoService _locacaoService;
        public LocacaoController(ILocacaoService locacaoService)
        {
            _locacaoService = locacaoService;
        }


        // POST /Locacao
        /// <summary>
        /// Cadastrar Locacao
        /// </summary>
        /// <response code="400">Dados inválidos</response>   
        [HttpPost("Locacao")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Locacao(LocacaoViewModel locacao)
        {
            try
            {
                Locacao e = new Locacao()
                {
                    entregador_id = locacao.entregador_id,
                    moto_id = locacao.moto_id,
                    data_inicio = locacao.data_inicio,
                    data_termino = locacao.data_termino,
                    data_previsao_termino = locacao.data_previsao_termino,
                    plano = locacao.plano,
                };
                
                e.ValidateSearch();
                _locacaoService.SetLocacao(e);

                var locationUri = Url.Action("Locacao", e);
                return Created(locationUri, e);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET /locacao
        /// <summary>
        /// Lista locacao existentes
        /// </summary>
        /// <response code="200">Listagem de locacao</response>   
        [HttpGet("locacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetLocacao(String id)
        {
            try
            {
                Locacao e = new Locacao
                {
                    _id = ObjectId.Parse(id),
                };

                List<Locacao> l = _locacaoService.GetLocacaoById(e);

                if(l.Count == 0)
                    return NotFound(new { mensagem = "Locação não encontrada."});

                return Ok(l);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Dados inválidos" });
            }
        }

        // PUT /locacao
        /// <summary>
        /// Modificar a placa de uma locacao
        /// </summary>
        /// <response code="200">Data de devolucao informada com sucesso</response>   
        /// <response code="400">Dados inválidos</response>   
        [HttpPut("locacao/{id}/devolucao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DevolucaoLocacao(string id, LocacaoViewModelDevolucao locacao)
        {
            try
            {
                Locacao e = new Locacao()
                {
                    _id = ObjectId.Parse(id),
                    data_devolucao = locacao.data_devolucao
                };

                List<Locacao> l = _locacaoService.GetLocacaoById(e);

                if(l.Count == 0)
                    return NotFound("Locação não encontrada.");

                l[0].data_devolucao = e.data_devolucao;
                _locacaoService.UpdateLocacao(l.First());

                return Ok(new { mensagem = "Data de devolucao informada com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
