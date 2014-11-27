using System.Collections.Generic;
using DriveIT.Entities;

namespace DriveIT.EntityFramework
{
    public class EntityAdapter : IPersistentStorage
    {
        private readonly DriveITContext _context;

        public EntityAdapter(DriveITContext context)
        {
            _context = context;
        }

        public virtual Car GetCarWithId(int id)
        {
            using (_context)
            {
                return _context.Cars.Find(id);
            }
        }

        public virtual IEnumerable<Car> GetAllCars()
        {
            using (_context)
            {
                return _context.Cars;
            }
        }

        public async void CreateCar(Car carToCreate)
        {
            using (_context)
            {
                _context.Cars.Add(carToCreate);
                await _context.SaveChangesAsync();
                //TODO: Implement checking to see if the request happened succesfully.
            }
        }

        public async void DeleteCar(Car carToDelete)
        {
            using (_context)
            {
                _context.Cars.Remove(carToDelete);
                await _context.SaveChangesAsync();
                //TODO: Implement checking to see if the request happened succesfully.
            }
        }
    }
}