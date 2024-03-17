using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;

namespace Explorer.API.Controllers.Administrator.Administration
{


    [Route("api/administration/equipment")]

    public class EquipmentController
    {
        private readonly IEquipmentService _equipmentService;
        private static readonly HttpClient _client = new();

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentDto>> CreateEquipment([FromBody] EquipmentDto equipment)
        {
            var result = await CreateEquipmentGo(_client, equipment);
            return result;
        }

        static async Task<EquipmentDto> CreateEquipmentGo(HttpClient client, EquipmentDto equipment)
        {
            var jsonContent = new StringContent
                (
                    JsonSerializer.Serialize(equipment),
                    Encoding.UTF8,
                    "application/json"
                );

            HttpResponseMessage response = await _client.PostAsync(
                    "http://localhost:8082/equipments", jsonContent
                 );

            var responseContent = await response.Content.ReadFromJsonAsync<EquipmentDto>();
            return responseContent;
        }

        [HttpPut]
        public async Task<ActionResult<EquipmentDto>> Update([FromBody] EquipmentDto equipment)
        {
            var result = await UpdateTourPointGo(_client, equipment);
            return result;
        }

        static async Task<EquipmentDto> UpdateTourPointGo(HttpClient httpClient, EquipmentDto equipment)
        {
            var jsonContent = new StringContent
               (
                   JsonSerializer.Serialize(equipment),
                   Encoding.UTF8,
                   "application/json"
               );

            HttpResponseMessage response = await _client.PutAsync(
                    "http://localhost:8082/equipments", jsonContent
                 );

            var responseContent = await response.Content.ReadFromJsonAsync<EquipmentDto>();
            return responseContent;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await DeleteEquipmentGo(_client, id);
            return result;
        }

        static async Task<bool> DeleteEquipmentGo(HttpClient httpClient, int id)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync($"http://localhost:8082/equipment/{id}");
            return response.IsSuccessStatusCode;
        }
        /*
        [HttpGet]
        public ActionResult<PagedResult<EquipmentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _equipmentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("getTouristEquipment")]
        public ActionResult<ObservableCollection<EquipmentDto>> GetTouristEquipment([FromQuery]List<int> ids)
        {
            var result = _equipmentService.GetTouristEquipment(ids);
            return CreateResponse(result);
        }

        [HttpGet("getOtherEquipment")]
        public ActionResult<ObservableCollection<EquipmentDto>> GetOtherEquipment([FromQuery] List<int> ids)
        {
            var result = _equipmentService.GetOtherEquipment(ids);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<EquipmentDto> Create([FromBody] EquipmentDto equipment)
        {
            var result = _equipmentService.Create(equipment);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<EquipmentDto> Update([FromBody] EquipmentDto equipment)
        {
            var result = _equipmentService.Update(equipment);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _equipmentService.Delete(id);
            return CreateResponse(result);
        }
        */
    }
}
