using Test.RentMotorCycles.Domain.Entity;

namespace Test.RentMotorCycles.Domain.Repository;

public interface IMotoService
{
    public void SetMoto(Moto p);

    public void SetMotoMessage(Moto p);

    public void SetMotoNovo(MotoEvento p);

    public void UpdateMoto(Moto p);

    public void UpdatePlate(Moto p);

    public void DeleteMoto(Moto p);

    public List<Moto> GetAllMoto();

    public List<Moto> GetAllMotoFilter(String placa);

    public List<Moto> GetMoto(Moto p);

    public void PlacaExists(string placa);

    public void GetMotoByIdExists(Moto p);

    public List<Moto> GetMotoById(Moto p);

}
