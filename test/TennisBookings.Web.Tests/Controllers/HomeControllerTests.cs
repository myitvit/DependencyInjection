using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.Controllers;
using TennisBookings.Web.Services;
using TennisBookings.Web.ViewModels;
using Xunit;

namespace TennisBookings.Web.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void ReturnsExpectedViewModel_WhenWeatherIsSun()
        {
            var mockWeatherForecaster = new Mock<IWeatherForecaster>();

            mockWeatherForecaster.Setup(w => w.GetCurrentWeather()).Returns(new WeatherResult
            {
                WeatherCondition = WeatherCondition.Sun
            });

            var options = new Mock<IOptions<FeaturesConfiguration>>();

            options.Setup(t => t.Value).Returns(new FeaturesConfiguration()
            {
                EnableWeatherForecast = true
            });

            var sut = new HomeController(mockWeatherForecaster.Object, options.Object);

            var result = sut.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Contains("It's sunny right now.", model.WeatherDescription);
        }

        [Fact]
        public void ReturnsExpectedViewModel_WhenWeatherIsRain()
        {
            var mockWeatherForecaster = new Mock<IWeatherForecaster>();

            mockWeatherForecaster.Setup(w => w.GetCurrentWeather()).Returns(new WeatherResult
            {
                WeatherCondition = WeatherCondition.Rain
            });

            var options = new Mock<IOptions<FeaturesConfiguration>>();

            options.Setup(t => t.Value).Returns(new FeaturesConfiguration()
            {
                EnableWeatherForecast = true
            });

            var sut = new HomeController(mockWeatherForecaster.Object, options.Object);

            var result = sut.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Contains("We're sorry but it's raining here.", model.WeatherDescription);
        }

        [Fact]
        public void ReturnsExpectedViewModel_EnableWeatherForecastFalse()
        {
            var mockWeatherForecaster = new Mock<IWeatherForecaster>();

            mockWeatherForecaster.Setup(w => w.GetCurrentWeather()).Returns(new WeatherResult
            {
                WeatherCondition = WeatherCondition.Rain
            });

            var options = new Mock<IOptions<FeaturesConfiguration>>();

            options.Setup(t => t.Value).Returns(new FeaturesConfiguration()
            {
                EnableWeatherForecast = false
            });

            var sut = new HomeController(mockWeatherForecaster.Object, options.Object);

            var result = sut.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Null(model.WeatherDescription);
        }
    }
}
