using Test.RentMotorCycles.Domain.Entity;

namespace Test.RentMotorCycles.Domain.Repository;

public interface IPlanoService
{
        public void SetPlano(Plano p);

        public void UpdatePlano(Plano p);

        public void DeletePlano(Plano p);

        public List<Plano> GetAllPlano();
}
