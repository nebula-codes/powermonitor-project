namespace power_monitor_ui.Data
{
    public class UsageData
    {
        public DateTime DateTime { get; set; }
        public float Usage { get; set; }
        public float Offset { get; set; }

        public UsageData(DateTime time, float usage, float offset)
        {
            DateTime = time;
            Usage = usage;
            Offset = offset;
        }
    }
}
