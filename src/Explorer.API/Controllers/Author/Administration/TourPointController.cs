using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using System.Text.Json;

namespace Explorer.API.Controllers.Author.Administration
{

   // [Authorize(Policy = "authorAndAdminPolicy")]

    [Route("api/administration/tourPoint")] 
    public class TourPointController : BaseApiController
    {
        private readonly ITourPointService _tourPointService;
        private static readonly HttpClient _client = new();

        public TourPointController(ITourPointService tourPointService)
        {
            _tourPointService = tourPointService;
        }
        [Authorize(Policy = "authorPolicy")]
        [HttpGet]
        public ActionResult<PagedResult<TourPointDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourPointService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<TourPointDto>> CreateTourPoint([FromBody] TourPointDto tourPoint)
        {
            var result = await CreateTourPointGo(_client,tourPoint);
            return result;
        }

        static async Task<TourPointDto> CreateTourPointGo(HttpClient httpClient, TourPointDto tourPoint)
        {
            var jsonContent = new StringContent
                (
                    JsonSerializer.Serialize(tourPoint),
                    Encoding.UTF8,
                    "application/json"
                );

            HttpResponseMessage response = await _client.PostAsync(
                    "http://localhost:8082/tourPoints", jsonContent
                 );

            var responseContent = await response.Content.ReadFromJsonAsync<TourPointDto>();
            return responseContent;
        }


        [HttpGet]
        public async Task<ActionResult<TourPointDto>> GetTourPointbyTourId([FromBody] TourPointDto tourPoint)
        {
            var result = await CreateTourPointGo(_client, tourPoint);
            return result;
        }

        /*
        public (ActionResult<TourPointDto>, int) Create([FromBody] TourPointDto tourPoint)
        {
            var result = _tourPointService.Create(tourPoint);
            Console.WriteLine("rezultat id:" + result.Value.Id);

            return (CreateResponse(result), result.Value.Id);
        }*/
        /*
        public ActionResult<PagedResult<TourPointDto>> Create([FromBody] TourPointDto tourPoint)
        {
            var result = _tourPointService.Create(tourPoint);
            Console.WriteLine("rezultat id:" + result.Value.Id);
            
            return CreateResponse(result);
        }
        */
        
        [Authorize(Policy = "authorPolicy")]
        [HttpPut("{id:int}")]
       

        public ActionResult<TourPointDto> Update([FromBody] TourPointDto tourPoint)
        {
            var result = _tourPointService.Update(tourPoint);
            return CreateResponse(result);
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourPointService.Delete(id);
            return CreateResponse(result);
        }

        [Authorize(Policy = "touristAuthorPolicy")]
        [HttpGet("{tourId:int}")]

		public ActionResult<List<TourPointDto>> GetTourPointsByTourId(int tourId)
		{
			var result = _tourPointService.GetTourPointsByTourId(tourId);
			return CreateResponse(result);
		}

        [HttpGet("getById/{id:int}")]
        public ActionResult<TourPointDto> GetTourPointById(int id)
        {
            var result = _tourPointService.Get(id);
            return CreateResponse(Result.Ok(result));
        }

        
    }
}
