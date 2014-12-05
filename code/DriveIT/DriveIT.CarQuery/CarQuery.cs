using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DriveIT.Models;

namespace DriveIT.CarQuery
{
    public class CarQuery
    {
        public static async Task<CarDto> FillCarData(CarDto carToFill)
        {
            var model = carToFill.Model;
            var make = carToFill.Make;

            if (string.IsNullOrEmpty(make) || string.IsNullOrWhiteSpace(make))
                throw new ArgumentNullException(carToFill.Make, "Make of the CarDto object must not be null.");

            if (string.IsNullOrEmpty(model) || string.IsNullOrWhiteSpace(model))
                throw new ArgumentNullException(carToFill.Model, "Model of the CarDto object must not be null.");

            var query = "make=" + make + "&model=" + model + "&fuel=" + carToFill.Fuel;

            if (carToFill.Year.HasValue)
            {
                query += "&year=" + carToFill.Year.Value;
            }

            TrimArray result = null;
            if (carToFill.Drive == null)
            {
                result = await JSONWrapper.Read<TrimArray>(query);
                carToFill.Drive = result.Trims[0].model_drive;
            }

            if (carToFill.NoughtTo100 == 0)
            {
                if (result == null) result = await JSONWrapper.Read<TrimArray>(query);
                if (result.Trims[0].model_0_to_100_kph.HasValue) 
                    carToFill.NoughtTo100 = result.Trims[0].model_0_to_100_kph.Value;
            }

            if (carToFill.TopSpeed == 0)
            {
                if (result == null) result = await JSONWrapper.Read<TrimArray>(query);
                if (result.Trims[0].model_top_speed_kph.HasValue) 
                    carToFill.TopSpeed = result.Trims[0].model_top_speed_kph.Value;
            }

            if (string.IsNullOrEmpty(carToFill.Transmission) || string.IsNullOrWhiteSpace(carToFill.Transmission))
            {
                if (result == null) result = await JSONWrapper.Read<TrimArray>(query);
                carToFill.Transmission = result.Trims[0].model_transmission_type;
            }

            return carToFill;
        }
    }
}
