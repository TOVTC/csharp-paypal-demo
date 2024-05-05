using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PayPalTest.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PayPalTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly HttpService _client;

        public PaymentController(HttpService service)
        {
            _client = service;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            return await _client.GetAccessToken();
        }
    }
}
