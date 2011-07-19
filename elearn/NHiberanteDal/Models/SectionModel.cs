namespace NHiberanteDal.Models
{
    public class SectionModel : IModel
    {
        public virtual int ID { get; set; }
        public virtual string IconName { get; set; }
        public virtual string Title { get; set; }
        public virtual string Text { get; set; }
    }
}
