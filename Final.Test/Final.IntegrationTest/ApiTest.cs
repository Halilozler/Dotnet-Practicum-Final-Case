using System;
using System.Text;
using Final.Dto.Dtos.Create;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace Final.IntegrationTest
{
	[TestClass]
	public class ApiTest
	{
		private readonly HttpClient _httpClient;
		public ApiTest()
		{
			var webAppFactory = new WebApplicationFactory<Program>();
			_httpClient = webAppFactory.CreateDefaultClient();
		}

        /*Create test cannot be performed due to the SQL connection
        //[TestMethod]
		public async Task CreateUser()
		{
            // Arrange
            var request = new CreateUserDto
            {
                Name = "Halil23",
				Surname = "Ozler23",
				Password = "1234",
				RoleId = 1
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/user/create", content);

            var actualStatusCode = response.StatusCode;
            var actualResult = await response.Content.ReadAsStringAsync();
            // Assert
            Assert.AreEqual(201, actualStatusCode);
		}
        */

        [TestMethod]
        public async Task TestUser()
        {
            // Act
            var response = await _httpClient.GetAsync("/user/test/");

            var actualStatusCode = response.StatusCode;
            var stringResult = await response.Content.ReadAsStringAsync();

            // Assert
            //Assert.AreEqual(200, actualStatusCode);
            Assert.AreEqual("Test success", stringResult);
        }
    }
}

