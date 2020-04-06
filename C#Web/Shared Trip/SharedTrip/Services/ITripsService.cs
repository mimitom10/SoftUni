using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        void Add(string startPoint, string endPoint, string DepartureTime, string ImagePath, int Seats, string description);

        DetailsViewModel GetDetails(string tripId);

        IEnumerable<TripInfoViewModel> GetAll();

    }
}
