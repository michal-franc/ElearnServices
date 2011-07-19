namespace NHiberanteDal.Models
{
    public class TestQuestionAnswer : IModel
    {
        public virtual int ID { get; private set; }
        public virtual string Text { get; set; }
        public virtual bool Correct { get; set; }
        public virtual int NumberSelected { get; set; }
    }
}
