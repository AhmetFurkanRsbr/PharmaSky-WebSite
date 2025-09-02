using Microsoft.EntityFrameworkCore;
using Pharmacy.Persistence.Context;
using PharmaSky.Domain.Entities;


namespace Pharmacy.Persistence.Repositories
{
    public class PharmacyRepository
    {
        private readonly PharmaSkyDbContext _pharmacyDbContext;

        public PharmacyRepository(PharmaSkyDbContext pharmacyDbContext)
        {
            _pharmacyDbContext = pharmacyDbContext;
        }


        public async Task<List<OnCallPharmacy>> GetPharmaciesByCountyAsync(int countyId)
        {
            return await _pharmacyDbContext.OnCallPharmacies
                .Include(p => p.County) // İlçe bilgilerini de dahil et
                .Where(p => p.CountyId == countyId)
                .ToListAsync();
        }


        public async Task<List<OnCallPharmacy>> GetAllPharmaciesAsync()
        {
            return await _pharmacyDbContext.OnCallPharmacies
                .Include(p => p.County) // İlçe bilgilerini de dahil et
                                 .ToListAsync();
        }

        public async Task<List<County>> GetAllCountiesAsync()
        {
            return await _pharmacyDbContext.Counties
                  .Include(c => c.OnCallPharmacies)
                  .ToListAsync();
        }
    }
}
