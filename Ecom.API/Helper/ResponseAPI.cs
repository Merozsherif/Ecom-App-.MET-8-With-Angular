namespace Ecom.API.Helper
{
    public class ResponseAPI
    {
        public ResponseAPI(int statusCode, string? message=null)
        {
            this.statusCode = statusCode;
            this.message = message?? GetMessageFormStatusCode( statusCode) ;
        }
        private string GetMessageFormStatusCode(int statuscode)
        {
            return statuscode switch
            {
                200 => "Done",
                400 => "Bad Request",
                401 => "Un Authorized",
                500 => "Server Erorr",
                _ => null,

            };
        }  
        public int statusCode   { get; set; }
        public string? message { get; set; } 
    }
}
