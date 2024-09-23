using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using Test.RentMotorCycle.Api.Controllers;
using Test.RentMotorCycle.Api.ViewModel;
using Test.RentMotorCycles.Domain.Entity;
using Test.RentMotorCycles.Domain.Repository;
using Test.RentMotorCycles.Service;

namespace Test.RentMotorCycle.Tests;

public class EntregadorTests
{
    public readonly IEntregadorService _entregadorService;

    public EntregadorTests()
    {
        _entregadorService = new EntregadorService();
        
    }

    [Fact]
    public void SetEntregadorSucesso()
    {
        EntregadorViewModel entregador = new EntregadorViewModel
        {
            cnpj = "012345678901234",
            identificador = "user0123456789",
            nome = "Teste da Silva",
            data_nascimento = "1966-10-10",
            numero_cnh = "0123456789",
            tipo_cnh = "AB",
            imagem_cnh = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAFUlEQVR42mNkYPhfz0AEYBxVSF+FAP5FDvcfRYWgAAAAAElFTkSuQmCC",
        };

        var response = new EntregadorController(_entregadorService).entregadores(entregador);
        Assert.NotNull(response);

    }


    [Fact]
    public void SetEntregadorErro()
    {
        EntregadorViewModel entregador = new EntregadorViewModel
        {
            cnpj = null,
            identificador = "user0123456789",
            nome = "Teste da Silva",
            data_nascimento = "1966-10-10",
            numero_cnh = "0123456789",
            tipo_cnh = "AB",
            imagem_cnh = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAFUlEQVR42mNkYPhfz0AEYBxVSF+FAP5FDvcfRYWgAAAAAElFTkSuQmCC",
        };

        var response = new EntregadorController(_entregadorService).entregadores(entregador);
        var actionResult = Assert.IsAssignableFrom<IActionResult>(response);
        var returnValue = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);



        entregador = new EntregadorViewModel
        {
            cnpj = "0265945687920233",
            identificador = "user0123456789",
            nome = "Teste da Silva",
            data_nascimento = "1966-10-10",
            numero_cnh = "0123456789",
            tipo_cnh = "AB",
            imagem_cnh = "data:image/png;base64,sdasd",
        };

        
        response = new EntregadorController(_entregadorService).entregadores(entregador);
        actionResult = Assert.IsAssignableFrom<IActionResult>(response);
        Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);


        entregador = new EntregadorViewModel
        {
            cnpj = "dsadasdsa",
            identificador = "user0123456789",
            nome = "Teste da Silva",
            data_nascimento = "1966-10-10",
            numero_cnh = "0123456789",
            tipo_cnh = "AB",
            imagem_cnh = "data:image/png;base64,sdasd",
        };

        response = new EntregadorController(_entregadorService).entregadores(entregador);
        actionResult = Assert.IsAssignableFrom<IActionResult>(response);
        Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);

        entregador = new EntregadorViewModel
        {
            cnpj = "051515105105150",
            identificador = "user0123456789",
            nome = "Teste da Silva",
            data_nascimento = "1966-10-10",
            numero_cnh = "0123456789",
            tipo_cnh = "DE",
            imagem_cnh = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAYAAACNMs+9AAAAFUlEQVR42mNkYPhfz0AEYBxVSF+FAP5FDvcfRYWgAAAAAElFTkSuQmCC",
        };

        response = new EntregadorController(_entregadorService).entregadores(entregador);
        actionResult = Assert.IsAssignableFrom<IActionResult>(response);
        Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);

    }




}