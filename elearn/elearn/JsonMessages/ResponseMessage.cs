namespace elearn.JsonMessages
{
    public class ResponseMessage
    {
        public bool IsSuccess { get; private set; }

        public string Data { get; private set; }

        public ResponseMessage(bool isSuccess, string data)
        {
            IsSuccess = isSuccess;
            Data = data;
        }

    }
}