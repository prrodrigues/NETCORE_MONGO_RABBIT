namespace Test.RentMotorCycles.Domain.Entity;

public interface ILocacaoService
{
        public void SetLocacao(Locacao p);

        public void UpdateLocacao(Locacao p);

        public void DeleteLocacao(Locacao p);

        public List<Locacao> GetAllLocacao();

        public List<Locacao> GetLocacaoByDeliveryman(Locacao p);
        public List<Locacao> GetLocacaoByMoto(Moto p);

        public List<Locacao> GetLocacaoById(Locacao p);

        public List<Locacao> GetLocacaoByStartDate(Locacao p);
}
