using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{

    public class OrderControllerTests
    {

        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            Order order = new Order();
            OrderController target = new OrderController(mock.Object, cart);

            ViewResult result = target.Checkout(order) as ViewResult;

            //заказ не был сохранен
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            //метод возвращает стандартное представление
            Assert.True(string.IsNullOrEmpty(result.ViewName));

            //представлению передана недопустимая модель
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            OrderController target = new OrderController(mock.Object, cart);

            //принудительно добавляем ошибку
            target.ModelState.AddModelError("error", "error");

            ViewResult result = target.Checkout(new Order()) as ViewResult;

            // заказ не был сохранен
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            // метод возвращает стандартное представление
            Assert.True(string.IsNullOrEmpty(result.ViewName));

            // представлению передается недопустимая модель
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            OrderController target = new OrderController(mock.Object, cart);

            RedirectToActionResult result =
                 target.Checkout(new Order()) as RedirectToActionResult;

            // проверка сохранения
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);

            // проверка, что редирект сработал (на действие Completed в том же
            // классе-контроллере)
            Assert.Equal("Completed", result.ActionName);
        }

    }
}