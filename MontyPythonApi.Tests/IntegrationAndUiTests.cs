using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MontyPythonApi.Tests
{
	[TestFixture]
	public class IntegrationAndUiTests
	{
		private IWebDriver browser;
		private static string webUrl;
		private static string apiUrl;
		private static string baseUrl;

		[SetUp]
		public void Setup()
		{
			browser = new ChromeDriver();
			baseUrl = "http://localhost:20461";
			webUrl = baseUrl + "/index.html";
			apiUrl = baseUrl + "/api/products";
		}

		[TearDown]
		public void TearDown()
		{
			browser.Quit();
		}

		[Test]
		public void ProductsDiscountPriceDisplaysOnWebPage()
		{
			//API call
			var uri = apiUrl + "/1"; //get the product with ID = 1
			var apiResponse = Utilities.SendHttpWebRequest(uri, "GET");
			Assert.That(apiResponse.IsSuccessStatusCode, 
				"Did not get success status code; got " + 
				apiResponse.StatusCode.ToString());
			Models.Product product = JsonConvert.DeserializeObject<Models.Product>(Utilities.ReadWebResponse(apiResponse));

			//WebUI
			browser.Navigate().GoToUrl(webUrl);
			PageObjects.HomePage page = new PageObjects.HomePage(browser);
			page.ProductIdInput.SendKeys("1");
			page.SearchButton.Click();
			var displayedPrice = page.GetPriceFromDisplayText(page.ProductDisplayOutput.Text);

			Assert.AreEqual(product.DiscountPrice, displayedPrice, "The prices don't match!");
			
		}
	}
}
