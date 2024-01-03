using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CKK.Logic.Models;


namespace CKK.Logic.tests
{
    public class UnitTest1
    {
    }
}
        
        /*public class ShoppingCartTests
        {
            private ShoppingCart cart;
            private Customer customer;

            [Fact]
            public void TestInitialize()
            {
                customer = new Customer();
               // ShoppingCart cart = new ShoppingCart(customer);
            }

            [Fact]
            public void AddProduct_ShouldAddProductToCart()
            {
                // Arrange
                var product = new Product();
                var expectedQuantity = 1;
                Customer cust = new Customer();
                ShoppingCart cart = new ShoppingCart(cust);
                // Act
                var result = cart.AddProduct(product);

                // Assert
                Assert.Equal(expectedQuantity, result.GetQuantity());
            }

            [Fact]
            public void RemoveProduct_ShouldRemoveProductFromCart()
            {
                // Arrange
                var product = new Product();
                //var expectedQuantity = null;
                Customer cust = new Customer();
                Product product2 = new Product();
                ShoppingCart cart = new ShoppingCart(cust);

                // Act
                cart.AddProduct(product2);
                var result = cart.RemoveProduct(product,1);

                // Assert
                Assert.Equal(null, result);
            }

            [Fact]
            public void GetTotal_ShouldReturnTotalCostOfItemsInCart()
            {
                // Arrange1
                var product1 = new Product();
                product1.SetPrice(10);
                var product2 = new Product();
                product2.SetPrice(20);
                var expectedTotal = 20;
                Customer cust = new Customer();
                ShoppingCart cart = new ShoppingCart(cust); 

                // Act
                cart.AddProduct(product1);
                cart.AddProduct(product2);
                var result = cart.GetTotal();

                // Assert
                Assert.Equal(expectedTotal, result);
            }
        }
    }
}

        /* public void Test1()
        {
            try
            {
               
                ShoppingCart shopcart = new ShoppingCart();
                int expected = 

            }
    }*/

    