using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author.Administration;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Administration;
	/*
[Collection("Sequential")]
public class TourPointCommandTests : BaseToursIntegrationTest

{
	public TourPointCommandTests(ToursTestFactory factory) : base(factory) { }

	[Fact]
	public void Creates()
	{
		// Arrange
		using var scope = Factory.Services.CreateScope();
		var controller = CreateController(scope);
		var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
		var newEntity = new TourPointDto
		{
			Name = "Strand",
			Description = "Kej na obali Dunava.",
			ImageUrl = "kej.png",
			Latitude = 332.3,
			Longitude = 7832.32

		};

		// Act
		var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourPointDto;

		// Assert - Response
		result.ShouldNotBeNull();
		result.Id.ShouldNotBe(0);
		result.Name.ShouldBe(newEntity.Name);

		// Assert - Database
		var storedEntity = dbContext.TourPoint.FirstOrDefault(i => i.Name == newEntity.Name);
		storedEntity.ShouldNotBeNull();
		storedEntity.Id.ShouldBe(result.Id);
	}

	

	[Fact]
	public void Updates()
	{
		// Arrange
		using var scope = Factory.Services.CreateScope();
		var controller = CreateController(scope);
		var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
		var updatedEntity = new TourPointDto
		{
			Id = -1,
			//IdTour = -1,
			Name = "Petrovaradin",
			Description = "Prelep dan Petrovaradinom.",
			ImageUrl = "petrovaradin.png",
			Latitude = 12.2,
			Longitude = 3223.3323

		};

		// Act
		var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourPointDto;

		// Assert - Response
		result.ShouldNotBeNull();
		result.Id.ShouldBe(-1);
		result.Name.ShouldBe(updatedEntity.Name);
		result.Description.ShouldBe(updatedEntity.Description);
		result.ImageUrl.ShouldBe(updatedEntity.ImageUrl);
		result.Latitude.ShouldBe(updatedEntity.Latitude);
		result.Longitude.ShouldBe(updatedEntity.Longitude);

		// Assert - Database
		var storedEntity = dbContext.TourPoint.FirstOrDefault(i => i.Name == "Petrovaradin");
		storedEntity.ShouldNotBeNull();
		storedEntity.Description.ShouldBe(updatedEntity.Description);
		storedEntity.ImageUrl.ShouldBe(updatedEntity.ImageUrl);
		storedEntity.Longitude.ShouldBe(updatedEntity.Longitude);
		storedEntity.Latitude.ShouldBe(updatedEntity.Latitude);
		var oldEntity = dbContext.TourPoint.FirstOrDefault(i => i.Name == "Kej");
		oldEntity.ShouldBeNull();
	}

	[Fact]
	public void Update_fails_invalid_id()
	{
		// Arrange
		using var scope = Factory.Services.CreateScope();
		var controller = CreateController(scope);
		var updatedEntity = new TourPointDto
		{
			Id = -1000,
			//IdTour = -1,
			Name = "Test",
			Description = "Test",
			Longitude = 0,
			Latitude = 0,
			ImageUrl = "Test"
		};

		// Act
		var result = (ObjectResult)controller.Update(updatedEntity).Result;

		// Assert
		result.ShouldNotBeNull();
		result.StatusCode.ShouldBe(404);
	}

	[Fact]
	public void Deletes()
	{
		// Arrange
		using var scope = Factory.Services.CreateScope();
		var controller = CreateController(scope);
		var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

		// Act
		var result = (OkResult)controller.Delete(-2);

		// Assert - Response
		result.ShouldNotBeNull();
		result.StatusCode.ShouldBe(200);

		// Assert - Database
		var storedCourse = dbContext.TourPoint.FirstOrDefault(i => i.Id == -3);
		storedCourse.ShouldBeNull();
	}

	[Fact]
	public void Delete_fails_invalid_id()
	{
		// Arrange
		using var scope = Factory.Services.CreateScope();
		var controller = CreateController(scope);

		// Act
		var result = (ObjectResult)controller.Delete(-1000);

		// Assert
		result.ShouldNotBeNull();
		result.StatusCode.ShouldBe(404);
	}

	private static TourPointController CreateController(IServiceScope scope)
	{
		return new TourPointController(scope.ServiceProvider.GetRequiredService<ITourPointService>())
		{
			ControllerContext = BuildContext("-1")
		};
	}
}
	*/