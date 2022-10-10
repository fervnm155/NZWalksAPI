using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTOs;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllregions()
        {
            var regions = await regionRepository.GetAll();
            //var regionsDto = new List<Models.DTOs.RegionDto>();

            //regions.ToList().ForEach(region =>
            //{
            //    var regionDto = new Models.DTOs.RegionDto()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population
            //    };
            //    regionsDto.Add(regionDto);
            //});

            var regionsDto = mapper.Map<List<RegionDto>>(regions);

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionById")]
        public async Task<IActionResult> GetRegionById(Guid id)
        {
            var region = await regionRepository.GetById(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion(AlterRegionDto newRegion)
        {
            //model validation using fluent validation

            var region = new Models.Domain.Region()
            {
                Name = newRegion.Name,
                Code = newRegion.Code,
                Area = newRegion.Area,
                Lat = newRegion.Lat,
                Long = newRegion.Long,
                Population = newRegion.Population
            };

            region = await regionRepository.AddRegion(region);
            var addedRegion = mapper.Map<RegionDto>(region);
            return CreatedAtAction(nameof(GetRegionById), new { id = addedRegion.Id }, addedRegion);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            var region=await regionRepository.DeleteRegion(id);
            if (region == null)
            {
                return BadRequest(ModelState);
            }
            return CreatedAtAction(nameof(GetRegionById), new { id = region.Id }, region);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] AlterRegionDto newDataRegion)
        {
            //model validation using fluent validation
            
            var region = new Region()
            {
                Id = id,
                Name = newDataRegion.Name,
                Code = newDataRegion.Code,
                Area = newDataRegion.Area,
                Lat = newDataRegion.Lat,
                Long = newDataRegion.Long,
                Population = newDataRegion.Population
            };

            region = await regionRepository.UpdateRegion(region);
            if(region == null)
            {
                return BadRequest(ModelState);
            }
            var updatedRegion = mapper.Map<RegionDto>(region);
            return CreatedAtAction(nameof(GetRegionById), new { id = updatedRegion.Id }, updatedRegion);
        }
    }
}
