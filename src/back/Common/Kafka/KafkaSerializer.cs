using System;
using System.Text;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace Common.Kafka
{
    internal sealed class KafkaSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            if (typeof(T) == typeof(Null)) return null;

            if (typeof(T) == typeof(Ignore))
                throw new NotSupportedException("Невозможно сериализовать тип Ignore для получения значения выражения");

            string json = JsonConvert.SerializeObject(data);
            
            return Encoding.UTF8.GetBytes(json);
        }
    }
}