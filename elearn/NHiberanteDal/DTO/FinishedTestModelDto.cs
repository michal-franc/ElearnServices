using System;
using NHiberanteDal.Models;
namespace NHiberanteDal.DTO
{
    public class FinishedTestModelDto : DtoBaseClass<FinishedTestModelDto,FinishedTestModel>
    {
        public int ID { get; private set; }
        public int TestId { get; set; }
        public DateTime DateFinished { get; set; }
        public double Mark { get; set; }
        public string TestName { get; set; }
    }
}
