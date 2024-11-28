using Airline.Models;
using Microsoft.EntityFrameworkCore;

namespace Airline.DAL
{
    public class DestinationDBstorage
    {
        private readonly ApplicationDbContext _context;
        public DestinationDBstorage(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<Destination>> getAllDestinations ()
        {
            return await _context.Destinations.ToListAsync();
        }

        public void AddDestination(Destination destination)
        {
             _context.Destinations.Add(destination);
             _context.SaveChanges();
        }

        public async Task<Destination> FindById(int? id)
        {
            return await _context.Destinations.FindAsync(id);
        }

        public async Task Update(Destination dest)
        {
            _context.Update(dest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var dest = await FindById(id);

            _context.Destinations.Remove(dest);
            await _context.SaveChangesAsync();
        }
    }
}
