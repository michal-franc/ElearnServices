namespace elearn.Models
{
    public class MarkItUpDataModel
    {
        public string Name { get; set; }
        public string Data { get; set; }


        public MarkItUpDataModel()
        {
            
        }

        public MarkItUpDataModel(string data ,string name)
        {
            Data = data;
            Name = name;
        }
    }
}