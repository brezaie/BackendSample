using BackendTest.Common;
using Microsoft.AspNetCore.Mvc;

namespace BackendTest.Api.Controllers
{
    public class CustomController : ControllerBase
    {
        public CustomLogger Logger = new CustomLogger();
    }
}