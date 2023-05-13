using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Common
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; } 
        public object Data {get; set;}

        //public  Response(bool success = true, string message = null, object data = null)
        //{
        //    _success = success;
        //    _message = message;
        //    _data = data;
        //}



        //public ResponseResult GetResponse()
        //{
        //    return new ResponseResult()
        //    {
        //        Success = _success,
        //        Message  = _message,                
        //        Data = _data,
        //    };
          
        //}

      


    }
}