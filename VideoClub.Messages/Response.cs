using System.Collections.Generic;

namespace VideoClub.Messages
{
    public interface Response<T>
    {
        T Data { get; }
        bool Success { get; }
        IList<string> Errors { get; }
    }

    public class ResponseMessage<T> : Response<T>
    {
        public ResponseMessage()
        {
            Errors = new List<string>();
        }

        public T Data { get; set; }

        public IList<string> Errors { get; private set; }

        public bool Success { get; set; }
    }
}
