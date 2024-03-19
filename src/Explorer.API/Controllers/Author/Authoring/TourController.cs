using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace Explorer.API.Controllers.Author.Authoring
{
    [Route("api/administration/tour")]
    public class TourController : BaseApiController
    {
        private readonly ITourService _tourService;
        private static readonly HttpClient _client = new();

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }
        [HttpPost("go")]
        public async Task<ActionResult<TourGoDto>> CreateGo([FromBody] TourGoDto tour)
        {
            var result = await CreateTourGo(_client, tour);
            return result;
        }

        static async Task<TourGoDto> CreateTourGo(HttpClient client, TourGoDto tour)
        {
            var jsonContent = new StringContent
            (
                    JsonSerializer.Serialize(tour),
                    Encoding.UTF8,
                    "application/json"
                );

            HttpResponseMessage response = await _client.PostAsync(
                    "http://localhost:8082/tours", jsonContent
                 );

            var responseContent = await response.Content.ReadFromJsonAsync<TourGoDto>();
            return responseContent;
        }

        [HttpGet("go/{userId:int}")]
        public async Task<ActionResult<List<TourGoDto>>> GetToursForAuthorGo(int userId)
        {
            var result = await FindToursGo(_client, userId);
            return result;
        }

        static async Task<List<TourGoDto>> FindToursGo(HttpClient httpClient, int userId)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:8082/tours/{userId}");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var tours = await response.Content.ReadFromJsonAsync<List<TourGoDto>>();
                    return tours ?? new List<TourGoDto>(); // Return an empty list instead of null
                }
                catch (Exception ex)
                {
                    // Log the exception details
                    throw new Exception($"An error occurred while deserializing the response: {ex.Message}", ex);
                }
            }
            else
            {
                // Consider logging the response content here to see if the service is returning an error message
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
            }
        }

        [HttpGet("go/single/{id:int}")]
        public async Task<ActionResult<TourGoDto>> GetTour(int id)
        {
            var result = await FindTourGo(_client, id);
            return result;
        }

        static async Task<ActionResult<TourGoDto>> FindTourGo(HttpClient httpClient, int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:8082/tour/{id}");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var tour = await response.Content.ReadFromJsonAsync<TourGoDto>();
                    return tour;
                }
                catch (Exception ex)
                {
                    // Log the exception details
                    throw new Exception($"An error occurred while deserializing the response: {ex.Message}", ex);
                }
            }
            else
            {
                // Consider logging the response content here to see if the service is returning an error message
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
            }
        }
        [HttpPost]
        public ActionResult<TourDTO> Create([FromBody] TourDTO tour)
        {
            tour.Status = "Draft";
            tour.Price = 0;

            var result = _tourService.Create(tour);

            return CreateResponse(result);
        }

        [HttpGet("search/{lat:double}/{lon:double}/{ran:int}/{type:int}")]
        //[AllowAnonymous]
        public ActionResult<PagedResult<TourDTO>> GetByRange(double lat, double lon, int ran, int type, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetByRange(lat, lon, ran, type, page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("filter/{level}/{price}")]
        public ActionResult<PagedResult<TourDTO>> GetByLevelAndPrice(string level, int price, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetByLevelAndPrice(level, price, page, pageSize);
            return CreateResponse(result);
        }




        [HttpGet("{userId:int}")]
        public ActionResult<PagedResult<TourDTO>> GetByUserId(int userId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetByUserId(userId, page, pageSize);
            return CreateResponse(result);
        }
        
        [Authorize(Policy = "touristAuthorPolicy")]
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourService.Delete(id);
            return CreateResponse(result);
        }
       
        //[Authorize(Policy = "authorPolicy")] ostaviti ovako, jer i administrator updatuje turu
        [HttpPut("{id:int}")]
        public ActionResult<TourDTO> Update([FromBody] TourDTO tourDto)
        {
            var result = _tourService.Update(tourDto);
            return CreateResponse(result);
        }


        [HttpPut("caracteristics/{id:int}")]
        public ActionResult AddCaracteristics(int id, [FromBody] TourCharacteristicDTO tourCharacteristic)
        {
            var result = _tourService.SetTourCharacteristic(id, tourCharacteristic.Distance, tourCharacteristic.Duration, tourCharacteristic.TransportType);
            return CreateResponse(result);
        }

        [HttpPut("publish/{tourId:int}")]
        public ActionResult Publish(int tourId)
        {
            var result = _tourService.Publish(tourId);
            return CreateResponse(result);
        }

       
        [Authorize(Policy = "touristAuthorPolicy")]
        [HttpPut("archive/{id:int}")]
        public ActionResult ArchiveTour(int id)
        {
            var result = _tourService.ArchiveTour(id);
            return CreateResponse(result);
        }
       
        [Authorize(Policy = "touristAuthorPolicy")]
        [HttpDelete("deleteAggregate/{id:int}")]
        public ActionResult DeleteAggregate(int id)
        {
            var result = _tourService.DeleteAggregate(id);
            return CreateResponse(result);
        }

        [HttpGet("onetour/{id:int}")]

        public ActionResult<TourDTO> getTourByTourId(int id)
        {
            var result = _tourService.GetTourByTourId(id);
            return CreateResponse(result);
        }



       //[Authorize(Policy = "touristPolicy")]

        [HttpGet("allTours")]
        public ActionResult<PagedResult<TourReviewDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize) {
            var result = _tourService.GetAll(page, pageSize);
            return CreateResponse(result);
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpGet("sales/{id:int}")]
        public ActionResult<PagedResult<TourDTO>> GetAllPublishedByAuthor(int id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourService.GetAllPublishedByAuthor(id, page, pageSize);
            return CreateResponse(result);
        }
        
        //[Authorize(Policy = "touristPolicy")]
        /*
        [HttpGet("filteredTours")]
        public ActionResult<PagedResult<TourDTO>> FilterToursByPublicTourPoints(
        [FromQuery] string publicTourPoints,
        [FromQuery] int page,
        [FromQuery] int pageSize)
        {
            try
            {
                
                var publicTourPointsArray = JsonConvert.DeserializeObject<PublicTourPointDto[]>(publicTourPoints);

                
              

                var result = _tourService.FilterToursByPublicTourPoints(publicTourPointsArray, page, pageSize);
                return CreateResponse(result);
            }
            catch (JsonException ex)
            {
          
                Console.WriteLine($"Error deserializing publicTourPoints: {ex.Message}");

                
                return BadRequest("Invalid publicTourPoints format");
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Unexpected error: {ex.Message}");

                return StatusCode(500, "Internal server error");
            }
		}
        */
		[Authorize(Policy = "touristPolicy")]
		[HttpGet("lastId")]
    public long GetLastTourId([FromQuery] int page,[FromQuery] int pageSize)
    {
        return _tourService.GetLastTourId(page, pageSize);
    }
	}
}