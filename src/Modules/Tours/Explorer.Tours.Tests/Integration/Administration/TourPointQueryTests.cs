using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration;
[Collection("Sequential")]
public class TourPointQueryTests : BaseToursIntegrationTest
{
	public TourPointQueryTests(ToursTestFactory factory) : base(factory) { }

	[Fact]
	public void Retrieves_all()
	{
		// Arrange
		using var scope = Factory.Services.CreateScope();
		var controller = CreateController(scope);

		// Act
		//var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<TourPointDto>;

		// Assert
		//result.ShouldNotBeNull();
		//result.Results.Count.ShouldBe(2);
		//result.TotalCount.ShouldBe(2);
	}

	private static TourPointController CreateController(IServiceScope scope)
	{
		return new TourPointController(scope.ServiceProvider.GetRequiredService<ITourPointService>())
		{
			//ControllerContext = BuildContext("-1")
		};
	}
}
