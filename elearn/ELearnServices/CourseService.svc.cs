using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using NHiberanteDal.DTO;
using NHiberanteDal.DataAccess;
using NHiberanteDal.Models;

namespace ELearnServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CourseService" in code, svc and config file together.
    public class CourseService : ICourseService
    {
        public CourseService()
        {
            DTOMappings.Initialize();
        }

        public IList<CourseDto> GetAll()
        {
            return CourseDto.Map(new Repository<CourseModel>().GetAll().ToList());
        }

        public CourseDto GetById(int id)
        {
            return CourseDto.Map(new Repository<CourseModel>().GetById(id));
        }
    }
}
