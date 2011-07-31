using System;
using System.Collections.Generic;

namespace NHiberanteDal.Models
{
    public class LearningMaterialModel : IModel
    {
        public virtual int ID { get; set; }
        public virtual string Description { get; set; }
        public virtual string Title { get; set; }
        public virtual string IconName { get; set; }
        public virtual string Goals { get; set; }
        public virtual int Level { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual DateTime UpdateDate { get; set; }
        public virtual string VersionNumber { get; set; }
        public virtual string Summary { get; set; }
        public virtual string Links { get; set; }

        //References Many

        public virtual IList<FileModel> Files { get; set; }
        public virtual IList<SectionModel> Sections { get; set; }
        public virtual IList<TestModel> Tests { get; set; }
    }
}
