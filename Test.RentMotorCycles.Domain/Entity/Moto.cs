using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Test.RentMotorCycles.Domain.Entity;

public class Moto
{
        [BsonIgnoreIfNull]
        public ObjectId _id { get; set; }

        //
        public string identificador { get; set; }

        //
        public int ano { get; set; }

        //
        public string modelo { get; set; }

        //
        public string placa { get; set; }


        public bool Validate()
        {
                if (this.identificador == null) throw new ArgumentNullException(nameof(this.identificador));
                if (this.ano == null) throw new ArgumentNullException(nameof(this.ano));
                if (this.modelo == null) throw new ArgumentNullException(nameof(this.modelo));
                if (this.placa == null) throw new ArgumentNullException(nameof(this.placa));
                return true;
        }

        public bool ValidatePlaca()
        {
                if (this._id == null) throw new ArgumentNullException(nameof(this._id));
                if (this.placa == null) throw new ArgumentNullException(nameof(this.placa));
                return true;
        }


}
