namespace NHiberanteDal.Models
{
    public class FileModel : IModel
    {
        public virtual int ID { get; set; }
        public virtual string FileName { get; set; }
        public virtual string Address { get; set; }
    }
}
