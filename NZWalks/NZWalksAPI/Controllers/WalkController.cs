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
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository difficultyRepository;

        public WalkController(IWalksRepository walksRepository, IMapper mapper, IRegionRepository regionRepository, IWalkDifficultyRepository difficultyRepository)
        {
            this.walksRepository = walksRepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.difficultyRepository = difficultyRepository;
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
            //if (!await AlterWalkDtoIdsValidator.ValidateWalkModel(newWalk))
            //{
            //    return BadRequest(ModelState);
            //}
            var region = await regionRepository.GetById(newWalk.RegionId);
            var difficulty = await difficultyRepository.GetDifficultyById(newWalk.WalkDifficultyId);
            if (region == null || difficulty == null)
            {
                ModelState.AddModelError(nameof(newWalk), "invalid RegionId or WalkDifficultyId");
                return BadRequest(ModelState);
                //return BadRequest("invalid RegionId or WalkDifficultyId");
            }
            var walk = new Walk()
            {
                Name = newWalk.Name,
                Length = newWalk.Length,
                RegionId = newWalk.RegionId,
                WalkDifficultyId = newWalk.WalkDifficultyId
            };

            walk = await walksRepository.AddWalk(walk);
            var addedwalk = mapper.Map<WalkDto>(walk);
            return CreatedAtAction(nameof(GetWalkById), new { id = addedwalk.Id }, addedwalk);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> ActionResult([FromRoute] Guid id, [FromBody] AlterWalkDto newDataWalk)
        {
            //if (!await AlterWalkDtoIdsValidator.ValidateWalkModel(newDataWalk))
            //{
            //    return BadRequest(ModelState);
            //}
            var region = await regionRepository.GetById(newDataWalk.RegionId);
            var difficulty = await difficultyRepository.GetDifficultyById(newDataWalk.WalkDifficultyId);
            if (region == null || difficulty == null)
            {
                ModelState.AddModelError(nameof(newDataWalk), "invalid RegionId or WalkDifficultyId");
                return BadRequest(ModelState);
                //return BadRequest("invalid RegionId or WalkDifficultyId");
            }
            var walk = new Walk()
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

            var updatedWalk=mapper.Map<WalkDto>(walk);
            return CreatedAtAction(nameof(GetWalkById), new { id = updatedWalk.Id }, updatedWalk);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walk = await walksRepository.DeleteWalk(id);
            return CreatedAtAction(nameof(GetWalkById), new { id = walk.Id }, walk);
        }

        //#region Private Methods

        //public async Task<bool> ValidateWalkModel(AlterWalkDto walkData)
        //{
        //    var region = await regionRepository.GetById(walkData.RegionId);
        //    if (region == null)
        //    {
        //        ModelState.AddModelError(nameof(walkData.RegionId), "Invalid region");
        //    }
        //    var difficulty = await difficultyRepository.GetDifficultyById(walkData.WalkDifficultyId);
        //    if (difficulty == null)
        //    {
        //        ModelState.AddModelError(nameof(walkData.WalkDifficultyId), "Invalid walk difficulty");
        //    }
        //    if (ModelState.ErrorCount > 0)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //#endregion
    }
}
