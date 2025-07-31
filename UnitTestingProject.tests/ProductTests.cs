using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using UnitTestingProject.Entities;

namespace UnitTestingProject.tests
{
    [TestFixture]
    public class ProductTests
    {
        //private WebApplicationFactory<Program> _factory;
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {

                });
            });

            //_factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task GetProducts_ReturnsOkResponse()
        {
            //Arrange
            var response = await _client.GetAsync("/api/product?top=10");
            response.EnsureSuccessStatusCode();

            //Assert
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            Assert.That(products != null);
        }

        [Test]
        public async Task GetProduct_ReturnsCorrectProduct()
        {
            //Arrange
            var response = await _client.GetAsync("/api/product?top=3");
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();

            var product = products?.FirstOrDefault();
            if(product != null)
            {
                var responseProduct = await _client.GetAsync($"/api/product/{product.Id}");
                Assert.That(responseProduct != null);
                var p = await responseProduct.Content.ReadFromJsonAsync<Product>();
                Assert.That(p != null);
                Assert.That(p.Id, Is.EqualTo(product.Id));
            }
        }

        [Test]
        public async Task PostProduct_AddsNewProduct()
        {
            //Arrange
            var newProduct = new Product() { 
                Name="New product",
                Price=500
            };
            //Actions
            var response = await _client.PostAsJsonAsync("/api/product",newProduct);

            //Assert
            response.EnsureSuccessStatusCode();
            var createdProduct = await response.Content.ReadFromJsonAsync<Product>();
            Assert.That(createdProduct != null);
            Assert.That(newProduct.Name,Is.EqualTo(createdProduct?.Name));

        }
    }
}
