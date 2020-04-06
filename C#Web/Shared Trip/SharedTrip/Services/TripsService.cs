using SharedTrip.Models;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Add(string startPoint, string endPoint, string departureTime, string imagePath, int seats, string description)
        {
            var trip = new Trip
            {
               StartPoint  = startPoint,
               EndPoint = endPoint,
               DepartureTime = departureTime,
               ImagePath = imagePath,
               Seats = seats,
               Description = description,
            };

            this.db.Trips.Add(trip);
            this.db.SaveChanges();


        }

        public IEnumerable<TripInfoViewModel> GetAll()
        {

            var allTrips = this.db.Trips.Select(x => new TripInfoViewModel

            {
               StartPoint = x.StartPoint,
               EndPoint = x.EndPoint,
               DepartureTime = x.DepartureTime,
               Seats = x.Seats,
               Description = x.Description,
            }).ToList();
            return allTrips;
        }

        public DetailsViewModel GetDetails(string tripId)
        {
            var trip = this.db.Trips.Where(x => x.Id == tripId)
                .Select(x => new DetailsViewModel
                {
                    StartPoint = x.StartPoint,
                    EndPoint = x.EndPoint,
                    DepartureTime = x.DepartureTime,               //x.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    ImagePath = x.ImagePath,
                    Seats = x.Seats,
                    Description = x.Description,
                }).FirstOrDefault();

            return trip;
        }
    }
}
