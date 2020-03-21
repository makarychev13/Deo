using System;
using Domain.Orders.ValueObjects;
using Xunit;

namespace Tests.Domain.Orders.ValueObjects
{
    public class OrderTest
    {
        [Fact]
        public void EqualsTest()
        {
            Assert.Equal(
                new OrderBody("title1", "description1", new Uri("https://yandex.ru/"), DateTime.Now),
                new OrderBody("title2", "description2", new Uri("https://yandex.ru/"), DateTime.Now.AddDays(1)));
            Assert.NotEqual(
                new OrderBody("title1", "description1", new Uri("https://yandex.ru/"), DateTime.Now),
                new OrderBody("title1", "description1", new Uri("https://yande.ru/"), DateTime.Now.AddDays(1)));
        }
    }
}