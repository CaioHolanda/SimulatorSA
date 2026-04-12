using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorSA.Bacnet.Models
{
    public class BacnetOperationResult
    {
        public bool Success { get; init; }
        public object? Value { get; init; }
        public string? ErrorCode { get; init; }
        public string? ErrorMessage { get; init; }
        public static BacnetOperationResult Ok(object? value = null) 
            => new() { Success= true, Value = value };
        public static BacnetOperationResult Fail(string errorCode,string errorMessage)
            => new() { Success=false, ErrorCode=errorCode, ErrorMessage = errorMessage };
    }
}
