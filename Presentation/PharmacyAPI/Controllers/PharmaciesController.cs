using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pharmacy.Persistence.Repositories;
using PharmaSky.Domain.Entities;
using PharmaSky.Dto;
using System.Text;

namespace PharmacyAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "NobetciEczane,Admin")]
    public class PharmaciesController : ControllerBase
    {
        private readonly PharmacyRepository _pharmacyRepository;

        public PharmaciesController(PharmacyRepository pharmacyRepository)
        {
            _pharmacyRepository = pharmacyRepository;
        }

        [HttpGet("GetPharmaciesByCountyId/{countyId}")]
        public async Task<IActionResult> GetPharmacies(int countyId)
        {
            var results = await _pharmacyRepository.GetPharmaciesByCountyAsync(countyId);

            if (results == null || !results.Any())
            {
                return NotFound("Bu ilçede nöbetçi eczane bulunamadı.");
            }

            var entities = results.Select(r => new OnCallPharmacy
            {
                Name = r.Name,
                Distance = r.Distance,
                Phone = r.Phone,
                Address = r.Address,
                PharmacyId = r.PharmacyId,
                County = r.County,
                CountyId = r.CountyId
            }).ToList();

            return Ok(entities);
        }


        [HttpGet("GetAllPharmacies")]
        public async Task<IActionResult> GetAllPharmacies()
        {
            var results = await _pharmacyRepository.GetAllPharmaciesAsync();

            if (results == null || !results.Any())
            {
                return NotFound("Herhangi bir nöbetçi eczane bulunamadı.");
            }

            var entities = results.Select(r => new PharmacyDto
            {
                PharmacyId = r.PharmacyId,
                Name = r.Name,
                Address = r.Address,
                Phone = r.Phone,
                Distance = r.Distance,
                CountyId = r.CountyId,
                CountyName = r.County != null ? r.County.CountyName : "Bilinmiyor"
            }).ToList();


            return Ok(entities);
        }


        [HttpGet("GetAllCounties")]
        public async Task<ActionResult<List<County>>> GetCounties()
        {
            var counties = await _pharmacyRepository.GetAllCountiesAsync();

            var entities = counties.Select(r => new County
            {
                CountyName = r.CountyName,
                CountyId = r.CountyId,
                OnCallPharmacies = r.OnCallPharmacies
            }).ToList();

            return Ok(entities);
        }

    }
}
