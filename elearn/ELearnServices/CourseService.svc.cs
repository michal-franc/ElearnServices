﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using NHiberanteDal.DTO;
using NHiberanteDal.DataAccess;
using NHiberanteDal.Models;
using NHiberanteDal.DataAccess.QueryObjects;

namespace ELearnServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CourseService" in code, svc and config file together.
    public class CourseService : ICourseService
    {

        private static bool _initialized;

        public CourseService()
        {
            if (!_initialized)
            {
                _initialized = DTOMappings.Initialize();
            }
        }

        public IList<CourseDto> GetAll()
        {
            return CourseDto.Map(new Repository<CourseModel>().GetAll().ToList());
        }

        public IList<CourseSignatureDto> GetAllSignatures()
        {
            return CourseSignatureDto.Map(new Repository<CourseModel>().GetAll().ToList());
        }

        public CourseDto GetById(int id)
        {
            CourseDto course = null;
            using (var session = DataAccess.OpenSession())
            {
               course=  CourseDto.Map(session.Get<CourseModel>(id));
            }
            return course;
        }

        public TestDto GetLatestTest(int id)
        {
            TestDto test = null;
            using(var session = DataAccess.OpenSession())
            {
                test = TestDto.Map(
                    session.Get<CourseModel>(id)
                    .Tests.OrderByDescending(c => c.CreationDate).FirstOrDefault()
                    );
            }
            return test;
        }

        public IList<TestDto> GetAllTests(int id)
        {
            IList<TestDto> tests = null;
            using (var session = DataAccess.OpenSession())
            {
                tests = TestDto.Map(
                    session.Get<CourseModel>(id).Tests.ToList()
                    );
            }
            return tests;
        }

        public List<CourseDto> GetByName(string value)
        {
            List<CourseDto> returnedList = null;
            using (var session = DataAccess.OpenSession())
            {
                returnedList =CourseDto.Map((List<CourseModel>)session.CreateQuery(new QueryCourseByName("test").Query).List<CourseModel>());
            }
            return returnedList;
        }

        public List<CourseDto> GetByCourseType(CourseTypeModelDto _testCourseType)
        {
            List<CourseDto> returnedList = null;
            using (var session = DataAccess.OpenSession())
            {
                returnedList = CourseDto.Map((List<CourseModel>)session.CreateQuery(new QueryCourseByCourseType(CourseTypeModelDto.UnMap(_testCourseType)).Query).List<CourseModel>());
            }
            return returnedList;
        }

        public bool Update(CourseDto updatedCourse)
        {
            var course = CourseDto.UnMap(updatedCourse);
            return new Repository<CourseModel>().Update(course);
        }

        public int AddCourse(CourseDto newCourse)
        {
            var course = CourseDto.UnMap(newCourse);
            return new Repository<CourseModel>().Add(course);
        }
    }
}
