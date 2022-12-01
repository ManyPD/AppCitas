using AppCitas.UnitTests.Helpers;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace AppCitas.UnitTests.Pruebas
{
    public class BuggyControllerTests
    {
        private string apiRoute = "api/buggy";
        private readonly HttpClient _client;
        private HttpResponseMessage httpResponse;
        private string requestUri;
        private string registeredObject;
        private HttpContent httpContent;

        public BuggyControllerTests()
        {
            _client = TestHelper.Instance.Client;
        }

        [Theory]
        [InlineData("OK", "rosa", "Pa$$w0rd")]
        public async Task GetSecret_OK(string statusCode, string username, string password)
        {
            // Arrange
            var user = await LoginHelper.LoginUser(username, password);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
            requestUri = $"{apiRoute}/auth";

            // Act
            httpResponse = await _client.GetAsync(requestUri);
            _client.DefaultRequestHeaders.Authorization = null;
            // Assert
            Assert.Equal(Enum.Parse<HttpStatusCode>(statusCode, true), httpResponse.StatusCode);
            Assert.Equal(statusCode, httpResponse.StatusCode.ToString());
        }

        [Theory]
        [InlineData("NotFound")]
        public async Task GetNotFound_NotFound(string statusCode)
        {
            // Arrange
            
            requestUri = $"{apiRoute}/not-fund";

            // Act
            httpResponse = await _client.GetAsync(requestUri);

            // Assert
            Assert.Equal(Enum.Parse<HttpStatusCode>(statusCode, true), httpResponse.StatusCode);
            Assert.Equal(statusCode, httpResponse.StatusCode.ToString());
        }

        [Theory]
        [InlineData("InternalServerError")]
        public async Task GetServerError_ServerError(string statusCode)
        {
            // Arrange

            requestUri = $"{apiRoute}/server-error";

            // Act
            httpResponse = await _client.GetAsync(requestUri);

            // Assert
            Assert.Equal(Enum.Parse<HttpStatusCode>(statusCode, true), httpResponse.StatusCode);
            Assert.Equal(statusCode, httpResponse.StatusCode.ToString());
        }

        [Theory]
        [InlineData("BadRequest")]
        public async Task GetBadRequest_BadRequest(string statusCode)
        {
            // Arrange

            requestUri = $"{apiRoute}/bad-request";

            // Act
            httpResponse = await _client.GetAsync(requestUri);

            // Assert
            Assert.Equal(Enum.Parse<HttpStatusCode>(statusCode, true), httpResponse.StatusCode);
            Assert.Equal(statusCode, httpResponse.StatusCode.ToString());
        }

        #region Privated methods
        private static string GetRegisterObject(string roles)
        {
            var entityObject = new JObject()
            {
                { "roles", roles }
            };
            return entityObject.ToString();
        }
        private StringContent GetHttpContent(string objectToEncode)
        {
            return new StringContent(objectToEncode, Encoding.UTF8, "application/json");
        }

        #endregion
    }
}
