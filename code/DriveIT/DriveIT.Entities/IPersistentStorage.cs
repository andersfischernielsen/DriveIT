using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveIT.Entities
{
    public interface IPersistentStorage
    {
        Car GetCarWithId(int id);
        IEnumerable<Car> GetAllCars();
    }
}
