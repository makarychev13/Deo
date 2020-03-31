using System;
using Confluent.Kafka;
using Utf8Json;

namespace Infrastructure.Common.Kafka
{
    public sealed class KafkaSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            if (typeof(T) == typeof(Null)) return null;

            if (typeof(T) == typeof(Ignore))
                throw new NotSupportedException("Невозможно сериализовать тип Ignore для получения значения выражения");

            return JsonSerializer.Serialize(data);
        }
    }
}