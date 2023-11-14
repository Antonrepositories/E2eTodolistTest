using NUnit.Framework;
using OpenQA.Selenium;
using System.ComponentModel;

namespace E2eTodolistTest
{
	public class Tests
	{
		private IWebDriver _driver;
		private readonly By inputTask = By.XPath("/html/body/div/div/div/input");
		private readonly By addButton = By.XPath("/html/body/div/div/div/button");
		private readonly By toDelete = By.XPath("/html/body/div/div/ul/li[2]");
		private readonly By toMark = By.XPath("/html/body/div/div/ul/li[1]");
		//private readonly By marked = By.XPath("//*[@id=\"task-list\"]/li[1]");
		private readonly By deleteButton = By.XPath("/html/body/div/div/ul/li[2]/span");
		[SetUp]
		public void Setup()
		{
			_driver = new OpenQA.Selenium.Edge.EdgeDriver();
			_driver.Navigate().GoToUrl("http://127.0.0.1:5500/index.html");
		}

		[Test]
		public void TestAddDeleteMark()
		{
			var input = _driver.FindElement(inputTask);
			input.SendKeys("testMarked");
			var add = _driver.FindElement(addButton);
			Thread.Sleep(1000);
			add.Click();
			input.SendKeys("testDeleted");
			Thread.Sleep(1000);
			add.Click();
			var tomark = _driver.FindElement(toMark);
			tomark.Click();

			var deletebuttton = _driver.FindElement(deleteButton);
			deletebuttton.Click();
			bool deletedworking = false;
			Thread.Sleep(1000);
			var markedElement = _driver.FindElement(toMark).GetAttribute("class");
			if (markedElement != "checked")
			{
				throw new Exception("Marking task is not working properly!");
			}
			Thread.Sleep(1000);
			try
			{
				var deletedElement = _driver.FindElement(toDelete);
			}
			catch
			{
				deletedworking = true;
			}
			Assert.AreEqual(deletedworking, true, "Deleting is not working properly!");
			
		}
		[Test]
		public void TestEmptyAdd()
		{
			var input = _driver.FindElement(inputTask);
			input.SendKeys("");
			var add = _driver.FindElement(addButton);
			bool alertFound = true;

			Thread.Sleep(1000);
			add.Click();
			try
			{
				_driver.SwitchTo().Alert();
			}   
			catch (NoAlertPresentException Ex)
			{
				alertFound = false;
			}
			Assert.AreEqual(alertFound, true, "Alert is not working properly!");

		}
		[TearDown]
		public void TearDown()
		{
			_driver.Quit();
		}
	}
}