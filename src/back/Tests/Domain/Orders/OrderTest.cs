using System;
using Infrastructure.Orders.Rss;
using Xunit;

namespace Tests.Domain.Orders
{
    public class OrderTest
    {
        [Fact]
        public void EqualsTest()
        {
            Assert.Equal(
                new Order("title1", "description1", new Uri("https://yandex.ru/"), DateTime.Now),
                new Order("title2", "description2", new Uri("https://yandex.ru/"), DateTime.Now.AddDays(1)));
            Assert.NotEqual(
                new Order("title1", "description1", new Uri("https://yandex.ru/"), DateTime.Now), 
                new Order("title1", "description1", new Uri("https://yande.ru/"), DateTime.Now.AddDays(1)));
        }
    }
}