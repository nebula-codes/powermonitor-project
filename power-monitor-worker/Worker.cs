using System.Net;

namespace power_monitor_worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string content;
                _logger.LogInformation("Worker running at: {time} \n", DateTimeOffset.Now);

                var request = new HttpRequestMessage(HttpMethod.Get, "https://power-monitor-ui/");
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
                {
                    return true;
                };

                var client = new HttpClient(httpClientHandler) { BaseAddress = new Uri("https://power-monitor-ui/") };

                try
                {
                    var res = await client.SendAsync(request);

                    content = await res.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex.Message);
                    content = null;
                }




                _logger.LogInformation(content);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}