using System;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Test.RentMotorCycle.Api.Controllers;
using Test.RentMotorCycle.Api.ViewModel;
using Test.RentMotorCycles.Domain.Entity;
using Test.RentMotorCycles.Domain.Repository;
using Test.RentMotorCycles.Infrastructure;
using Test.RentMotorCycles.Service;

namespace Test.RentMotorCycle.Tests;

public class LocacaoTests : Factory
{
    public ILocacaoService _LocacaoService;
    public readonly IModel channel;

    public LocacaoTests()
    {
        _LocacaoService = new LocacaoService();
    }

    [Fact]
    public void SetLocacaoSucesso()
    {
        LocacaoViewModel e = new LocacaoViewModel()
        {
            entregador_id = "fdsfsdf",
            moto_id = "fsdfsd",
            data_inicio = "",
            data_termino = "",
            data_previsao_termino = "",
            plano = 7,
        };

        var response = new LocacaoController(_LocacaoService).Locacao(e);
        Assert.NotNull(response);
    }

    [Fact]
    public void GetLocacaoSucesso()
    {
        String id = "SN1-2010";

        var response = new LocacaoController(_LocacaoService).GetLocacao(id);
        Assert.NotNull(response);
    }


    [Fact]
    public void GetLocacaoNotFound()
    {
        String id = "SN1-2010";

        var response = new LocacaoController(_LocacaoService).GetLocacao(id);
        var actionResult = Assert.IsAssignableFrom<IActionResult>(response);
        var returnValue = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);
    }


    [Fact]
    public void GetLocacaoError()
    {
        String id = "SN1-2010";

        var response = new LocacaoController(_LocacaoService).GetLocacao(id);
        var actionResult = Assert.IsAssignableFrom<IActionResult>(response);
        var returnValue = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);
    }


    [Fact]
    public void DevolucaoLocacaoErro()
    {
        LocacaoViewModel e = new LocacaoViewModel()
        {
            entregador_id = "fdsfsdf",
            moto_id = "fsdfsd",
            data_inicio = "",
            data_termino = "",
            data_previsao_termino = "",
            plano = 7,
        };

        String id = "Sbvcsfreomfodp656065";

        var response = new LocacaoController(_LocacaoService).DevolucaoLocacao(id, e);
        var actionResult = Assert.IsAssignableFrom<IActionResult>(response);
        var returnValue = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);
    }


    [Fact]
    public void DevolucaoLocacaoSucesso()
    {
        LocacaoViewModel e = new LocacaoViewModel()
        {
            entregador_id = "fdsfsdf",
            moto_id = "fsdfsd",
            data_inicio = "",
            data_termino = "",
            data_previsao_termino = "",
            plano = 7,
        };

        String id = "66ecc5eb6225ab66799d9349";

        var response = new LocacaoController(_LocacaoService).DevolucaoLocacao(id, e);
        var actionResult = Assert.IsAssignableFrom<IActionResult>(response);
        Assert.NotNull(response);
    }

}
