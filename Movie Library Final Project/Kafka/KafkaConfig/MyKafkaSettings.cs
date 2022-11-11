namespace Kafka.KafkaConfig
{
    public class MyKafkaSettings
    {
        public string BootstrapServers { get; set; }
        public int AutoOffsetReset { get; set; }
        public string GroupId { get; set; }
        public string Topic { get; set; }
        public string objectType { get; set; }
    }
}
