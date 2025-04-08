// See https://aka.ms/new-console-template for more information
using System;
using Confluent.Kafka;

var config = new ProducerConfig
{
    BootstrapServers = "localhost:9092"
};

using var producer = new ProducerBuilder<Null, string>(config).Build();

while (true)
{
    Console.Write("Enter order details (or 'exit' to quit): ");
    var order = Console.ReadLine();
    if (order == "exit") break;

    var result = await producer.ProduceAsync("orders-topic", new Message<Null, string> { Value = order });
    Console.WriteLine($"✅ Sent: {order} to partition {result.Partition}, offset {result.Offset}");
}
