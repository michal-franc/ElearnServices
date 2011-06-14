using System;

namespace NHiberanteDal.Models
{
    public class FinishedTestModel : IModel
    {
        public virtual int ID { get; private set; }
        public virtual TestModel Test { get; set; }
        public virtual DateTime DateFinished { get; set; }
        public virtual double Mark { get; set; }
    }
}