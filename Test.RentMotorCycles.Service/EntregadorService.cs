using System.Text.RegularExpressions;
using MongoDB.Driver;
using Test.RentMotorCycles.Domain.Entity;
using Test.RentMotorCycles.Domain.Repository;
using Test.RentMotorCycles.Infrastructure;


namespace Test.RentMotorCycles.Service;

public class EntregadorService : Factory, IEntregadorService
{
    public void SetEntregador(Entregador dm)
    {
        InsertOne<Entregador>(dm);
    }

    public void UpdateCNHImageEntregador(Entregador usr)
    {
        var upd = Builders<Entregador>.Update
        .Set(x => x.imagem_cnh, usr.imagem_cnh);

        UpdateFields<Entregador>(x => x._id == usr._id, upd, usr);
    }

    public Entregador? GetEntregadorById(Entregador usr)
    {
        return Find<Entregador>(x => x._id == usr._id).FirstOrDefault();
    }

    public void CNPJExists(string cnpj)
    {
        string cleanedCNPJ = Regex.Replace(cnpj, @"[^0-9]", "");
        if (Find<Entregador>(x => x.cnpj == cleanedCNPJ).Count > 0)
            throw new Exception("CNPJ já cadastrado");
    }

    public void CNHExists(string numero_cnh)
    {
        if (Find<Entregador>(x => x.numero_cnh == numero_cnh).Count > 0)
            throw new Exception("CNH já cadastrada");
    }

}
