﻿using static WebVilla.Utility.SD;

namespace WebHouse.Web.Models
{
    public class ApiRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; } 
    }
}
