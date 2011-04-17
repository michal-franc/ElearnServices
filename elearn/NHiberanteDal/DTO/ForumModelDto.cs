using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHiberanteDal.Models;
using System.Runtime.Serialization;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class ForumModelDto : DtoBaseClass<ForumModelDto,ForumModel>
    {
        [DataMember]
        public  int ID { get; private set; }
        [DataMember]
        public  string Name { get; set; }
        [DataMember]
        public  string Author { get; set; }

        public  IList<TopicModel> Topics { get; set; }
    }
}
