﻿using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.UseCases.Authoring;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
	[Route("api/competitionApply")]
	public class CompetitionApplyController : BaseApiController
	{
		private readonly ICompetitionApplyService _competitionApplyService;

		public CompetitionApplyController(ICompetitionApplyService competitionApplyService)
		{
			_competitionApplyService = competitionApplyService;
		}

		[HttpPost]
		public ActionResult<CompetitionApplyDto> Create([FromBody] CompetitionApplyDto competitionApply)
		{


			var result = _competitionApplyService.Create(competitionApply);

			return CreateResponse(result);
		}

        [HttpGet("getApplies/{comId}")]
        public ActionResult<List<CompetitionApplyDto>> GetAppliesByCompId(int comId)
        {
            var result = _competitionApplyService.GetAppliesByCompId(comId);
            return CreateResponse(result);
        }
    }
}
