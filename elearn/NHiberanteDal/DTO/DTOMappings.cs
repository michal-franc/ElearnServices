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
                Mapper.CreateMap<ProfileModel, ProfileModelDto>();
                Mapper.CreateMap<ProfileModelDto, ProfileModel>();
                Mapper.CreateMap<SurveyModel, SurveyModelDto>();
                Mapper.CreateMap<SurveyModelDto, SurveyModel>();
                Mapper.CreateMap<TestQuestionModel, TestQuestionModelDto>();
                Mapper.CreateMap<TestQuestionModelDto, TestQuestionModel>();
                Mapper.CreateMap<TestModel, TestDto>();
                Mapper.CreateMap<TestDto, TestModel>();
                Mapper.CreateMap<TestModel, TestSignatureDto>();
                Mapper.CreateMap<TestSignatureDto, TestModel>();
                Mapper.CreateMap<TestTypeModel, TestTypeModelDto>();
                Mapper.CreateMap<TestTypeModelDto, TestTypeModel>();
                Mapper.CreateMap<ShoutboxModel, ShoutboxModelDto>();
                Mapper.CreateMap<ShoutboxModelDto, ShoutboxModel>();
                Mapper.CreateMap<ForumModel, ForumModelDto>();
                Mapper.CreateMap<ForumModelDto, ForumModel>();
                Mapper.CreateMap<CourseTypeModel, CourseTypeModelDto>();
                Mapper.CreateMap<CourseTypeModelDto, CourseTypeModel>();
                Mapper.CreateMap<GroupTypeModel, GroupTypeModelDto>();
                Mapper.CreateMap<GroupTypeModelDto, GroupTypeModel>();
                Mapper.CreateMap<GroupModel, GroupModelDto>();
                Mapper.CreateMap<GroupModelDto, GroupModel>();
                Mapper.CreateMap<CourseModel, CourseDto>()
                    .ForMember(
                    dest =>dest.LatestSurvey,
                    opt => opt.MapFrom(c => 
                        c.Surveys.OrderByDescending(s=>s.DateCreated).FirstOrDefault())
                    );
                Mapper.CreateMap<CourseDto, CourseModel>();
                Mapper.CreateMap<CourseModel,CourseSignatureDto>();
                Mapper.CreateMap<CourseSignatureDto, CourseModel>();
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
