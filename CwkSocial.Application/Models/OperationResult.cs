using CwkSocial.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkSocial.Application.Models
{
    public class OperationResult<T>
    {
        public T Payload { get; set; }
        public bool isError { get; private set; }
        public List<Error> Errors { get; set; }=new List<Error>();

        /// <summary>
        /// Adds Errors to the list and sets isError flag to true
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public void AddError(ErrorCode code, string message)
        {
            HandleError(code, message);
        }
        public void AddUnknownError(string message)
        {
            HandleError(ErrorCode.UnknownError, message);
        }
        public void ResetIsError()
        {
            isError = false;
        }

        private void HandleError(ErrorCode code, string message)
        {
            Errors.Add(new Error()
            {
                Code = code,
                Message = message
            });
            isError = true;
        }
    }
}
