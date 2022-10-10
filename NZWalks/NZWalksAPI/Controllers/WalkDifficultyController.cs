using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTOs;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDiff()
        {
            var walkDiff = await walkDifficultyRepository.GetAllWalksDifficulties();
            var walkDiffDto = mapper.Map<List<Models.Domain.WalkDifficulty>>(walkDiff);
            return Ok(walkDiffDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDiffById")]
        public async Task<IActionResult> GetWalkDiffById([FromRoute] Guid id)
        {
            var walkDiff = await walkDifficultyRepository.GetDifficultyById(id);
            var walkDiffDto=mapper.Map<Models.Domain.WalkDifficulty>(walkDiff);
            return Ok(walkDiffDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddDifficulty([FromBody] AlterWalkDifficultyDto newDifficulty)
        {
            if (!ValidateDifficultyData(newDifficulty))
            {
                return BadRequest(ModelState);
            }

            var difficulty = new Models.Domain.WalkDifficulty()
            {
                Code = newDifficulty.Code
            };

            difficulty=await walkDifficultyRepository.AddDifficulty(difficulty);
            var addedDifficulty = mapper.Map<WalkDifficultyDto>(difficulty);
            return CreatedAtAction(nameof(GetWalkDiffById), new { id = addedDifficulty.Id }, addedDifficulty);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateDifficulty([FromRoute] Guid id, [FromBody] AlterWalkDifficultyDto diffData)
        {
            if (!ValidateDifficultyData(diffData))
            {
                return BadRequest(ModelState);
            }

            var difficulty = new Models.Domain.WalkDifficulty()
            {
                Id = id,
                Code = diffData.Code
            };

            difficulty = await walkDifficultyRepository.UpdateDifficulty(difficulty);
            if (difficulty == null)
            {
                return BadRequest();
            }

            var updatedWalk = mapper.Map<WalkDifficultyDto>(difficulty);
            return CreatedAtAction(nameof(GetWalkDiffById), new { id = updatedWalk.Id }, updatedWalk);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteDifficulty([FromRoute] Guid id)
        {
            var difficulty = await walkDifficultyRepository.DeleteDifficulty(id);
            return CreatedAtAction(nameof(GetWalkDiffById), new { id = difficulty.Id }, difficulty);
        }

        #region
        public bool ValidateDifficultyData(AlterWalkDifficultyDto diffData)
        {
            if (diffData == null)
            {
                ModelState.AddModelError(nameof(diffData), "Add walk difficulty data is required.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(diffData.Code))
            {
                ModelState.AddModelError(nameof(diffData.Code), "The code can't be null or empty or white space.");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
