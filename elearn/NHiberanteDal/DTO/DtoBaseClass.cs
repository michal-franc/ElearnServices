using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using System.Runtime.Serialization;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class DtoBaseClass <T,Y>
    {
        public static T Map(Y coursemodel)
        {
            return Mapper.Map<Y, T>(coursemodel);
        }

        public static List<T> Map(List<Y> coursemodels)
        {
            return Mapper.Map<List<Y>, List<T>>(coursemodels);
        }

        public static Y UnMap(T coursemodel)
        {
            return Mapper.Map<T, Y>(coursemodel);
        }

        public static List<Y> UnMap(List<T> coursemodels)
        {
            return Mapper.Map<List<T>, List<Y>>(coursemodels);
        }
    }
}
