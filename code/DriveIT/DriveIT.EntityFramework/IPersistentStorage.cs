using System.Collections.Generic;
using DriveIT.Entities;

namespace DriveIT.EntityFramework
{
    public interface IPersistentStorage
    {
        Car GetCarWithId(int id);
        IEnumerable<Car> GetAllCars();
        void CreateCar(Car carToCreate);
        void DeleteCar(Car carToDelete);
    }
}
