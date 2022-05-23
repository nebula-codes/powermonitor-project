using System.Text.Json;

namespace power_monitor_ui.Data
{
    public class DataLoader
    {






        public DataLoader()
        {
            UsageData data = new UsageData(DateTime.Now, 10000, 100);
            string jsonString = JsonSerializer.Serialize(data);



            Console.WriteLine(jsonString);
        }

    }
}
