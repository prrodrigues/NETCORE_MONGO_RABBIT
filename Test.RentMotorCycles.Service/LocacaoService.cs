using MongoDB.Bson;
using Test.RentMotorCycles.Domain.Entity;
using Test.RentMotorCycles.Infrastructure;

namespace Test.RentMotorCycles.Service;

public class LocacaoService : Factory, ILocacaoService
{
    public void SetLocacao(Locacao p)
    {
        InsertOne<Locacao>(p);
    }

    public void UpdateLocacao(Locacao p)
    {
        UpdateOne<Locacao>(x => x._id == p._id, p);
    }

    public void DeleteLocacao(Locacao p)
    {
        DeleteOne<Locacao>(x => x._id == p._id);
    }

    public List<Locacao> GetAllLocacao()
    {
        return FindAll<Locacao>();
    }

    public List<Locacao> GetLocacaoByDeliveryman(Locacao p)
    {
        return Find<Locacao>(x => x.entregador_id == p.entregador_id);
    }

    public List<Locacao> GetLocacaoByMoto(Moto p)
    {
        return Find<Locacao>(x => x.moto_id == p._id.ToString());
    }

    public List<Locacao> GetLocacaoById(Locacao p)
    {
        return Find<Locacao>(x => x._id == p._id);
    }

    public List<Locacao> GetLocacaoByStartDate(Locacao p)
    {
        return Find<Locacao>(x => x.entregador_id == p.entregador_id);
    }


}
