using System;
using NHiberanteDal.Models;

namespace NHiberanteDal.DTO
{
    public class ShoutBoxMessageModelDto : DtoBaseClass<ShoutBoxMessageModelDto,ShoutBoxMessageModel>
    {
        public int ID { get; set; }
        public int ShoutBoxId { get; set; }
        public string Author { get; set; }
        public DateTime TimePosted { get; set; }
        public string Message { get; set; }
    }
}
