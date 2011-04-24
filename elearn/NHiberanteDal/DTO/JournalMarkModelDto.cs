﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHiberanteDal.Models;
using System.Runtime.Serialization;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class JournalMarkModelDto : DtoBaseClass<JournalMarkModelDto,JournalMarkModel>
    {
        [DataMember]
        public  int ID { get; private set; }
        [DataMember]
        public  string Name { get; set; }
        [DataMember]
        public  string Value { get; set; }
    }
}
