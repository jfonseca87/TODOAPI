﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class APIResponse
    {
        public string HttpResponse { get; set; }
        public object SuccessfullResponse { get; set; }
        public string ErrorResponse { get; set; }
    }
}
