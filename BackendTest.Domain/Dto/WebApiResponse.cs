using System;
using System.Collections.Generic;
using BackendTest.Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace BackendTest.Domain.Dto
{
    public class WebApiResponse
    {
        public bool ErrorFlag { get; set; }
        public string Message { get; set; }
    }

    public class WebApiPagedCollectionResponse<T> : WebApiResponse
    {
        public List<T> Result { get; set; }
        public long TotalRecords { get; set; }
    }

    public class WebApiSimpleResponse<T> : WebApiResponse
    {
        public T Result { get; set; }

        public static explicit operator WebApiSimpleResponse<T>(JsonResult v)
        {
            throw new NotImplementedException();
        }
    }
}