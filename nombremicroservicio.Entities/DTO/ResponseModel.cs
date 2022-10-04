using System;
using System.Collections.Generic;
using System.Text;

namespace nombremicroservicio.Entities.DTO
{
    public class ResponseModel
    {
        public int StatusCode { get; set; } = 200;
        public bool IsSuccess { get; set; } = true;
        public string Mensaje { get; set; } = string.Empty;
        public object  Result { get; set; }
    }
}
