using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHiberanteDal.Models;

namespace NHiberanteDal.DTO
{
    public class CourseSignatureDto : DtoBaseClass<CourseSignatureDto,CourseModel>
    {
        public  string Name { get; set; }
        public  DateTime CreationDate { get; set; }
        public  string Logo { get; set; }
        public  string Description { get; set; }

        //One
        public  CourseTypeModelDto CourseType { get; set; }
    }
}
