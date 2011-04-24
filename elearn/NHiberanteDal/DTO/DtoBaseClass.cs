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
        public static T Map(Y mappedModel)
        {
            return Mapper.Map<Y, T>(mappedModel);
        }

        public static List<T> Map(List<Y> mappedModels)
        {
            return Mapper.Map<List<Y>, List<T>>(mappedModels);
        }

        public static Y UnMap(T mappedModel)
        {
            return Mapper.Map<T, Y>(mappedModel);
        }

        public static List<Y> UnMap(List<T> mappedModels)
        {
            return Mapper.Map<List<T>, List<Y>>(mappedModels);
        }
    }
}
