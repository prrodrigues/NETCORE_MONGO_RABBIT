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

public class MotoTests : Factory
{
    public readonly IMotoService _motoService;
    public readonly IModel channel;

    public MotoTests()
    {
        _motoService = new MotoService();
    }

    [Fact]
    protected void SetMotoMensageriaSucesso()
    {
        IModel channel = Connection().CreateModel();

        channel.ConfirmSelect();

        string queueName = "createMoto";

        channel.QueueDeclare(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true; // Mensagem persistente

        channel.BasicNacks += (sender, ea) =>
        {
            Assert.Fail();
        };

        channel.BasicAcks += (sender, ea) =>
        {
            Assert.True(true);
        };


        Moto e = new Moto()
        {
            identificador = "moto123",
            ano = 2024,
            modelo = "Sports XL",
            placa = "SXL-2022",
        };
        e.Validate();
        _motoService.PlacaExists(e.placa);

        String message = JsonConvert.SerializeObject(e);

        var _me = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
                             routingKey: queueName,
                             mandatory: true,
                             basicProperties: properties,
                             body: _me);


    }

    [Fact]
    public void SetMotoSucesso()
    {
        MotoViewModel e = new MotoViewModel()
        {
            identificador = "moto123",
            ano = 2024,
            modelo = "Sports XL",
            placa = "SN1-2010",
        };

        var response = new MotoController(_motoService).MotosCadastro(e);
        Assert.NotNull(response);
    }

    [Fact]
    public void GetPlacaSucesso()
    {
        String id = "SN1-2010";

        var response = new MotoController(_motoService).MotoByPlate(id);
        Assert.NotNull(response);
    }

    [Fact]
    public void GetMotoByIdError()
    {
        String id = "SN1-2010";

        var response = new MotoController(_motoService).MotosById(id);
        var actionResult = Assert.IsAssignableFrom<IActionResult>(response);
        var returnValue = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);
    }

    [Fact]
    public void GetMotoByIdSucesso()
    {
        String id = "SN1-2010";

        var response = new MotoController(_motoService).MotosById(id);
        Assert.NotNull(response);
    }

    [Fact]
    public void GetAllMotoFilterSucess()
    {
        String id = "SN1-2010";

        var response = new MotoController(_motoService).MotoByPlate(id);
        var actionResult = Assert.IsAssignableFrom<IActionResult>(response);
    }



    [Fact]
    public void SetMotoPlacaSucesso()
    {
        MotoPlacaViewModel e = new MotoPlacaViewModel()
        {
            placa = "SN1-2010",
        };

        String id = "66ec18a8e25aafa9024db9f8";

        var response = new MotoController(_motoService).MotosUpdatePlate(id, e);
        Assert.NotNull(response);
    }

    [Fact]
    public void SetMotoPlacaErro()
    {
        MotoPlacaViewModel e = new MotoPlacaViewModel()
        {
            placa = "SN1-2010",
        };

        String id = "66ec18a8e25aafa9024db9f8111";

        var response = new MotoController(_motoService).MotosUpdatePlate(id, e);
        var actionResult = Assert.IsAssignableFrom<IActionResult>(response);
        var returnValue = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);
    }


    [Fact]
    public void SetMotoErro()
    {
        MotoViewModel e = new MotoViewModel()
        {
            identificador = "moto123",
            ano = 2024,
            modelo = "Sports XL",
            placa = "SXL-2020",
        };

        var response = new MotoController(_motoService).MotosCadastro(e);
        var actionResult = Assert.IsAssignableFrom<IActionResult>(response);
        var returnValue = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);


        e = new MotoViewModel()
        {
            identificador = "moto123",
            ano = 2024,
            modelo = "Sports XL",
            placa = "SXL-2020",
        };


        response = new MotoController(_motoService).MotosCadastro(e);
        actionResult = Assert.IsAssignableFrom<IActionResult>(response);
        Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);
    }


    
    [Fact]
    public void DeleteMotoErro()
    {
        String id = "Sbvcsfreomfodp656065";

        var response = new MotoController(_motoService).MotoDelete(id);
        var actionResult = Assert.IsAssignableFrom<IActionResult>(response);
        var returnValue = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);
    }
    
    [Fact]
    public void DeleteMotoLocacoesErro()
    {
        String id = "66ec18a8e25aafa9024db9f8";

        var response = new MotoController(_motoService).MotoDelete(id);
        var actionResult = Assert.IsAssignableFrom<IActionResult>(response);
        var returnValue = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult);
    }


    [Fact]
    public void DeleteMotoSucesso()
    {
        String id = "66ecc5eb6225ab66799d9349";

        var response = new MotoController(_motoService).MotoDelete(id);
        var actionResult = Assert.IsAssignableFrom<IActionResult>(response);
        Assert.NotNull(response);
    }

}
