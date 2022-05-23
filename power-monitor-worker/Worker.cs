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
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://power-monitor-ui:80/");


                //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                //using (Stream stream = response.GetResponseStream())
                //using (StreamReader reader = new StreamReader(stream))
                //{
                //    content = reader.ReadToEnd();
                //}

                using (var handler = new HttpClientHandler())
                {
                    // allow the bad certificate
                    handler.ServerCertificateCustomValidationCallback = (request, cert, chain, errors) => true;
                    using (var httpClient = new HttpClient(handler))
                    {
                        content = await httpClient.GetAsync("http://power-monitor-ui/").Result.Content.ReadAsStringAsync();
                    }
                }

                _logger.LogInformation(content);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}