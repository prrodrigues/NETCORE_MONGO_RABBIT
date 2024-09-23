using MongoDB.Driver;
using Newtonsoft.Json;
using Test.RentMotorCycles.Domain.Entity;
using Test.RentMotorCycles.Domain.Repository;
using Test.RentMotorCycles.Infrastructure;

namespace Test.RentMotorCycles.Service;

public class MotoService : Factory, IMotoService
{
    public void SetMoto(Moto p)
    {
        try
        {
            InsertOne<Moto>(p);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw ex;
        }
    }

    public void SetMotoMessage(Moto p)
    {
        SetPublisher("createMoto", JsonConvert.SerializeObject(p));
    }

    public void SetMotoNovo(MotoEvento p)
    {
        InsertOne<MotoEvento>(p);
    }

    public void UpdateMoto(Moto p)
    {
        UpdateOne<Moto>(x => x._id == p._id, p);
    }

    public void UpdatePlate(Moto p)
    {
        var upd = Builders<Moto>.Update
        .Set(x => x.placa, p.placa);

        UpdateFields<Moto>(x => x._id == p._id, upd , p);
    }


    public void DeleteMoto(Moto p)
    {
        DeleteOne<Moto>(x => x._id == p._id);
    }

    public List<Moto> GetAllMoto()
    {
        return FindAll<Moto>();
    }

    public List<Moto> GetAllMotoFilter(String placa)
    {
        if(placa != null){
            return Find<Moto>(x => x.placa == placa);
        }
        else{
            return FindAll<Moto>();
        }
    }

    public List<Moto> GetMoto(Moto p)
    {
        return Find<Moto>(x => x.placa == p.placa);
    }

    public List<Moto> GetMotoById(Moto p)
    {
        return Find<Moto>(x => x._id == p._id);
    }


    public void PlacaExists(string placa)
    {
        if (Find<Moto>(x => x.placa == placa).Count > 0)
            throw new Exception("Placa já cadastrada");
    }

    public void GetMotoByIdExists(Moto e)
    {
        if (Find<Moto>(x => x._id == e._id).Count == 0)
            throw new Exception("Dados inválidos");
    }
}
