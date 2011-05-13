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
        private static bool initialized = false;
        public static bool Initialize()
        {
            if (!initialized)
            {
                //try
                {
                    Mapper.CreateMap<JournalModelDto, JournalModel>();
                    Mapper.CreateMap<JournalModel, JournalModelDto>();
                    Mapper.CreateMap<JournalMarkModelDto, JournalMarkModel>();
                    Mapper.CreateMap<JournalMarkModel, JournalMarkModelDto>();
                    Mapper.CreateMap<ProfileModel, ProfileModelDto>();
                    Mapper.CreateMap<ProfileModelDto, ProfileModel>();
                    Mapper.CreateMap<SurveyModel, SurveyModelDto>();
                    Mapper.CreateMap<SurveyModelDto, SurveyModel>();
                    Mapper.CreateMap<SurveyQuestionModel, SurveyQuestionModelDto>();
                    Mapper.CreateMap<SurveyQuestionModelDto, SurveyQuestionModel>();
                    Mapper.CreateMap<TestQuestionModel, TestQuestionModelDto>();
                    Mapper.CreateMap<TestQuestionModelDto, TestQuestionModel>();
                    Mapper.CreateMap<TestQuestionAnswer, TestQuestionAnswerDto>();
                    Mapper.CreateMap<TestQuestionAnswerDto, TestQuestionAnswer>();
                    Mapper.CreateMap<TestModel, TestDto>();
                    Mapper.CreateMap<TestDto, TestModel>();
                    Mapper.CreateMap<TestModel, TestSignatureDto>();
                    Mapper.CreateMap<TestSignatureDto, TestModel>()
                        .ForMember(dest => dest.Questions,opt=>opt.Ignore());
                    Mapper.CreateMap<TestTypeModel, TestTypeModelDto>();
                    Mapper.CreateMap<TestTypeModelDto, TestTypeModel>();
                    Mapper.CreateMap<ShoutboxModel, ShoutboxModelDto>();
                    Mapper.CreateMap<ShoutboxModelDto, ShoutboxModel>();
                    Mapper.CreateMap<ShoutBoxMessageModel,ShoutBoxMessageModelDto>();
                    Mapper.CreateMap<ShoutBoxMessageModelDto,ShoutBoxMessageModel>();
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
                        dest => dest.LatestSurvey,
                        opt => opt.MapFrom(c =>
                            c.Surveys.OrderByDescending(s => s.DateCreated).FirstOrDefault())
                        );
                    Mapper.CreateMap<CourseDto, CourseModel>()
                         .ForMember(dest => dest.Tests, opt => opt.Ignore())
                         .ForMember(dest => dest.Surveys, opt => opt.Ignore())
                         .ForMember(dest => dest.Contents, opt => opt.Ignore());
                    Mapper.CreateMap<CourseModel, CourseSignatureDto>();
                    Mapper.CreateMap<CourseSignatureDto, CourseModel>()
                         .ForMember(dest => dest.Tests, opt => opt.Ignore())
                         .ForMember(dest => dest.Surveys, opt => opt.Ignore())
                         .ForMember(dest => dest.Contents, opt => opt.Ignore())
                         .ForMember(dest => dest.Group, opt => opt.Ignore())
                         .ForMember(dest => dest.Forum, opt => opt.Ignore())
                         .ForMember(dest => dest.ShoutBox, opt => opt.Ignore());
                    Mapper.AssertConfigurationIsValid();
                    initialized = true;
                    return true;
                }
                //catch (AutoMapperConfigurationException)
                //{
                   //throw new Exception("Problem initializing DTO mappings!");
                //}
            }
            return false;
        }
    }
}
