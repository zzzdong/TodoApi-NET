namespace Base.Exceptions
{
    public class PlatformException: Exception {
        public int code { get; set; }
        public string message { get; set; }


        public PlatformException(int code, string message) 
        {
            this.code = code;
            this.message = message;
        }

        public PlatformException(string message) 
        {
            this.code = -1;
            this.message = message;
        }
    }    
}