using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Text.RegularExpressions;

namespace MontyPythonApi.Tests.PageObjects
{
	public class HomePage
	{
		[FindsBy(How = How.Id, Using = "prodId")]
		public IWebElement ProductIdInput { get; private set; }

		[FindsBy(How = How.Id, Using = "searchButton")]
		public IWebElement SearchButton { get; private set; }

		[FindsBy(How = How.Id, Using = "product")]
		public IWebElement ProductDisplayOutput { get; private set; }

		public HomePage(IWebDriver browser)
		{
			PageFactory.InitElements(browser, this);
		}

		public decimal GetPriceFromDisplayText(string displayText)
		{
			decimal result;
			Regex r = new Regex("\\$(.*)");
			Match m = r.Match(displayText);
			decimal.TryParse(m.Groups[1].Value, out result);
			return result;
		}
	}
}
