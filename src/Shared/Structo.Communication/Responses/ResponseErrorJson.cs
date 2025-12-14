using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Communication.Responses
{
    public class ResponseErrorJson
    {
        
        public bool TokenIsExpired { get; set; }
        public IList<string> Errors { get; set; }

        public ResponseErrorJson(IList<string> errors)
        {
            Errors = errors;
        }

        

        public ResponseErrorJson(string error)
        {
            Errors = new List<string>
            {
                error
            };
        }

    }
}
