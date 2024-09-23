using Test.RentMotorCycles.Domain.Entity;

namespace Test.RentMotorCycles.Domain.Repository;

public interface IEntregadorService
{
    void SetEntregador(Entregador dm);

    // void UpdateEntregador(Entregador usr);

    void UpdateCNHImageEntregador(Entregador usr);

    // void DeleteEntregador(Entregador usr);

    // List<Entregador> GetAllEntregador();

    // Entregador? GetEntregador(Entregador usr);
    
    Entregador? GetEntregadorById(Entregador usr);

    void CNPJExists(string cnpj);

    void CNHExists(string numero_cnh);
 
}
