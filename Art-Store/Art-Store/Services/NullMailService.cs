using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtStore.Services
{
    public class NullMailService : INullMailService
    {
        private readonly ILogger<NullMailService> _logger;

        public NullMailService(ILogger<NullMailService> logger)
        {
            _logger = logger;
        }
        public void SendMessage(string to,string email,string subject,string message)
        {
            _logger.LogInformation($"Sent:{to},From:{email},Subject:{subject},Message:{message}");
        }
    }
}
