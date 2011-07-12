using System.Collections.Generic;


namespace NHiberanteDal.Models
{
    public class ProfileModel : IModel
    {
        public virtual int ID { get; private set; }
        public virtual string LoginName { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Role { get; set; }
        public virtual string Email { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual IList<JournalModel> Journals { get; set; }
        public virtual IList<FinishedTestModel> FinishedTests { get; set; }
    }
}