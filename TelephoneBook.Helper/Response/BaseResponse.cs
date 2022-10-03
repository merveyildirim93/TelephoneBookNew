using System;

namespace TelephoneBook.Helper.Response
{
    [Serializable]
    public abstract class BaseResponse
    {
        public ResponseCodes ResponseCode { get; set; }

        public string ResponseMessage { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", ResponseCode, ResponseMessage);
        }

        public string ResponseValue { get; set; }

        public string EntityResponseId { get; set; }
    }
}
