using System;
using System.Linq;
using AutoMapper;
using NHiberanteDal.Models;

namespace NHiberanteDal.DTO
{
    public static class DtoMappings
    {
        private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static bool _initialized;
        public static bool Initialize()
        {
            if (!_initialized)
            {
                Logger.Info("DTO Mappings Initialized");
                try
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
                        )
                        .ForMember(
                            dest => dest.IsPasswordProtected,
                            opt => opt.MapFrom(c => c.Password != null)
                        );
                    Mapper.CreateMap<CourseDto, CourseModel>()
                        .ForMember(dest => dest.Tests, opt => opt.Ignore())
                        .ForMember(dest => dest.Surveys, opt => opt.Ignore())
                        .ForMember(dest => dest.Contents, opt => opt.Ignore())
                        .ForMember(dest => dest.Password, opt => opt.Ignore());
                    Mapper.CreateMap<CourseModel, CourseSignatureDto>();
                    Mapper.CreateMap<CourseSignatureDto, CourseModel>()
                         .ForMember(dest => dest.Tests, opt => opt.Ignore())
                         .ForMember(dest => dest.Surveys, opt => opt.Ignore())
                         .ForMember(dest => dest.Contents, opt => opt.Ignore())
                         .ForMember(dest => dest.Group, opt => opt.Ignore())
                         .ForMember(dest => dest.Forum, opt => opt.Ignore())
                         .ForMember(dest => dest.ShoutBox, opt => opt.Ignore())
                         .ForMember(dest => dest.Password, opt => opt.Ignore());

                    Mapper.CreateMap<FinishedTestModel, FinishedTestModelDto>()
                        .ForMember(dest => dest.TestId, opt => opt.MapFrom(p=>p.Test.ID))
                        .ForMember(dest => dest.TestName, opt => opt.MapFrom(p => p.Test.Name));
                         
                    Mapper.CreateMap<FinishedTestModelDto,FinishedTestModel>();

                    Mapper.AssertConfigurationIsValid();
                    _initialized = true;
                    return true;
                }
                catch (AutoMapperConfigurationException ex)
                {
                    Logger.Error("Problem initializing DTO mappings! - {0}",ex.Message);
                }
            }
            return false;
        }
    }
}
