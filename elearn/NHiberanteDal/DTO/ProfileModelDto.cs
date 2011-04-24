﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class ProfileModelDto
    {
        [DataMember]
        public int ID { get; private set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public  IList<JournalModelDto> Journals { get; set; }
    }
}
