using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NHiberanteDal.Models;

namespace NHiberanteDal.DTO
{
    public static class DTOMappings
    {
        public static bool Initialize()
        {
            try
            {
                Mapper.CreateMap<SurveyModel, SurveyModelDto>();
                Mapper.CreateMap<TestModel, TestDto>();
                Mapper.CreateMap<ShoutboxModel, ShoutboxModelDto>();
                Mapper.CreateMap<ForumModel, ForumModelDto>();
                Mapper.CreateMap<CourseTypeModel, CourseTypeModelDto>();
                Mapper.CreateMap<GroupTypeModel, GroupTypeModelDto>();
                Mapper.CreateMap<GroupModel, GroupModelDto>();
                Mapper.CreateMap<CourseModel, CourseDto>()
                    .ForMember(
                    dest =>dest.LatestSurvey,
                    opt => opt.MapFrom(c => 
                        c.Surveys.OrderByDescending(s=>s.DateCreated).FirstOrDefault()
                        )
                    );
                Mapper.CreateMap<CourseModel,CourseSignatureDto>();
                Mapper.AssertConfigurationIsValid();
                return true;
            }
            catch (AutoMapperConfigurationException)
            {
                return false;
            }
        }
    }
}
