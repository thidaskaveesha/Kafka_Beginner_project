// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;
using System;

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "order-consumers",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
consumer.Subscribe("orders-topic");

Console.WriteLine("🟢 Listening for new orders...");
while (true)
{
    var cr = consumer.Consume();
    Console.WriteLine($"📦 New Order Received: {cr.Message.Value}");
}
