using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwayDayz.Application.Common
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        
        public OperationResult(bool isSuccess, string? message, T? data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public static OperationResult<T> Success(T data, string? message = null)
            => new(true, message, data);

        public static OperationResult<T> Failure(string? message)
            => new(false, message, default);
    }
}
