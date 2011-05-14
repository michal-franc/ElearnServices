namespace elearn.JsonMessages
{
    public class ResponseMessage
    {
        public bool IsSuccess { get; private set; }

        public object Data { get; private set; }

        public ResponseMessage(bool isSuccess, object  data)
        {
            IsSuccess = isSuccess;
            Data = data;
        }

    }
}