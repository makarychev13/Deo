using System;
using System.Text.Json;
using Confluent.Kafka;

namespace Common.Kafka
{
    internal sealed class KafkaDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (typeof(T) == typeof(Null))
            {
                if (data.Length > 0)
                {
                    throw new ArgumentException("Deserializer for Null may only be used to deserialize data that is null.");
                }

                return default;
            }

            if (typeof(T) == typeof(Ignore))
            {
                return default;
            }
            
            byte[] bytes = data.ToArray();
            return JsonSerializer.Deserialize<T>(bytes);
        }
    }
}