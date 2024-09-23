using System;
using Test.RentMotorCycles.Domain.Entity;
using Test.RentMotorCycles.Domain.Repository;
using Test.RentMotorCycles.Infrastructure;

namespace Test.RentMotorCycles.Service;

public class PlanoService : Factory, IPlanoService
{
    public void SetPlano(Plano p)
    {
        InsertOne<Plano>(p);
    }

    public void UpdatePlano(Plano p)
    {
        UpdateOne<Plano>(x => x._id == p._id, p);
    }

    public void DeletePlano(Plano p)
    {
        DeleteOne<Plano>(x => x._id == p._id);
    }

    public List<Plano> GetAllPlano()
    {
        return FindAll<Plano>();
    }

}
