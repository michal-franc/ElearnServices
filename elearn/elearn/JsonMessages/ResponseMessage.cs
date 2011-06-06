using System;

namespace elearn.JsonMessages
{
    public class ResponseMessage
    {
        public bool IsSuccess { get; private set; }

        public object Data { get; private set; }

        public ResponseMessage(bool isSuccess) : this(isSuccess,String.Empty)
        {

        }


        public ResponseMessage(bool isSuccess, object  data)
        {
            IsSuccess = isSuccess;
            Data = data;
        }

    }
}