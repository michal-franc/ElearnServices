namespace elearn.Models
{
    public enum StatusType
    {
        Red,
        Green
    }


    public class StatusModel
    {
        public StatusType Type { get; set; }
        public string Message { get; set; }

        public StatusModel()
        {
            
        }

        public StatusModel(string message , StatusType type)
        {
            Type = type;
            Message = message;
        }
    }
}