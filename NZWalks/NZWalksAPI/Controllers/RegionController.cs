using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
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
        public IActionResult GetAllregions()
        {
            var regions = regionRepository.GetAll();
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

            var regionsDto = mapper.Map<Models.DTOs.RegionDto>(regions);

            return Ok(regionsDto);
        }
    }
}
