using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTOs;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkController : Controller
    {
        private readonly IWalksRepository walksRepository;
        private readonly IMapper mapper;

        public WalkController(IWalksRepository walksRepository, IMapper mapper)
        {
            this.walksRepository = walksRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walks = await walksRepository.GetAllWalks();
            var walksDto = mapper.Map<List<Models.Domain.Walk>>(walks);
            return Ok(walksDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkById")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walk = await walksRepository.GetWalkById(id);
            var walkDto = mapper.Map<Models.Domain.Walk>(walk);
            return Ok(walkDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalk([FromBody] AlterWalkDto newWalk)
        {
            var walk = new Models.Domain.Walk()
            {
                Name = newWalk.Name,
                Length = newWalk.Length,
                RegionId = newWalk.RegionId,
                WalkDifficultyId = newWalk.WalkDifficultyId
            };

            walk = await walksRepository.AddWalk(walk);
            var addedwalk = mapper.Map<Models.DTOs.WalkDto>(walk);
            return CreatedAtAction(nameof(GetWalkById), new { id = addedwalk.Id }, addedwalk);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> ActionResult([FromRoute] Guid id, [FromBody] Models.DTOs.AlterWalkDto newDataWalk)
        {
            var walk = new Models.Domain.Walk()
            {
                Id = id,
                Name = newDataWalk.Name,
                Length = newDataWalk.Length,
                RegionId = newDataWalk.RegionId,
                WalkDifficultyId = newDataWalk.WalkDifficultyId
            };

            walk = await walksRepository.UpdateWalk(walk);
            if (walk == null)
            {
                return BadRequest();
            }

            var updatedWalk=mapper.Map<Models.DTOs.WalkDto>(walk);
            return CreatedAtAction(nameof(GetWalkById), new { id = updatedWalk.Id }, updatedWalk);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walk = await walksRepository.DeleteWalk(id);
            return CreatedAtAction(nameof(GetWalkById), new { id = walk.Id }, walk);
        }
    }
}
