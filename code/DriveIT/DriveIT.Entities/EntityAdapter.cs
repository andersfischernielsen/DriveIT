using System.Collections.Generic;

namespace DriveIT.Entities
{
    public class EntityAdapter : IPersistentStorage
    {

        public Car GetCarWithId(int id)
        {
            using (var context = new DriveITContext())
            {
                return context.Cars.Find(id);
            }
        }

        public IEnumerable<Car> GetAllCars()
        {
            using (var context = new DriveITContext())
            {
                return context.Cars;
            }
        }
    }
}