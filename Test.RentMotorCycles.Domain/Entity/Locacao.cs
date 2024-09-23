using MongoDB.Bson;

namespace Test.RentMotorCycles.Domain.Entity;

public class Locacao
{
        public ObjectId _id { get; set; }

        public string moto_id { get; set; }

        public string entregador_id { get; set; }

        public string data_inicio { get; set; }

        public string data_termino { get; set; }

        public string data_previsao_termino { get; set; }

        public string data_devolucao { get; set; }

        public int plano { get; set; }


        public bool Validate()
        {
             if (this.entregador_id == null) throw new ArgumentNullException(nameof(this.entregador_id));
             if (this.plano == null) throw new ArgumentNullException(nameof(this.plano));
             if (this.data_inicio == null) throw new ArgumentNullException(nameof(this.data_inicio));
             if (this.data_termino == null) throw new ArgumentNullException(nameof(this.data_termino));
             if (this.data_previsao_termino == null) throw new ArgumentNullException(nameof(this.data_previsao_termino));
             return true;
        }

        public bool ValidateSearch()
        {
             if (this._id == null) throw new ArgumentNullException(nameof(this._id));
             return true;
        }



}
