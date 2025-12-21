using System;
using System.Collections.Generic;
using System.Text;

namespace Structo.Communication.Responses
{
    public class ResponseErrorJson //essa classe detalha a estrutura de dados para respostas de erro retornadas pela API
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
