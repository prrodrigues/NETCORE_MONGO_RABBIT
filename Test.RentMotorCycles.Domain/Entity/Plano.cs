using System;
using MongoDB.Bson;

namespace Test.RentMotorCycles.Domain.Entity;

public class Plano
{
        public ObjectId _id { get; set; }

        public int plano { get; set; }
        public int quantidade_dias { get; set; }
        public decimal diaria { get; set; }

        public bool Validate()
        {
                if (this.quantidade_dias == null) throw new ArgumentNullException(nameof(this.quantidade_dias));
                if (this.diaria == null) throw new ArgumentNullException(nameof(this.diaria));
                return true;
        }
}
