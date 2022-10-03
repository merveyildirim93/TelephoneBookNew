using System;

namespace TelephoneBook.Helper.Response
{
    [Serializable]
    public class PrimitiveResponse: BaseResponse
    {
        public bool IsSuccess
        {
            get
            {
                if (ResponseCode.Equals(ResponseCodes.Successful))
                {
                    return true;
                }
                return false;
            }
        }
    }
}
