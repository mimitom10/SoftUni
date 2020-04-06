using SharedTrip.Services;
using SharedTrip.ViewModels.Trips;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {

        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = new AllTripsViewModel
            {
                Trips = this.tripsService.GetAll(),

            };
            return this.View(viewModel);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddInputModel input)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(input.StartPoint))
            {
                return this.Error("Starting point cannot be empty!");
            }

            if (string.IsNullOrWhiteSpace(input.EndPoint))
            {
                return this.Error("End Point cannot be empty!");
            }

            if (input.Seats < 2 || input.Seats > 6)
            {
                return this.Error("Seats should be between 2 and 6");
            }

            if (input.Description.Length > 80)
            {
                return this.Error("Description coud not be more than 80 characters.");
            }

            if (string.IsNullOrWhiteSpace(input.Description))
            {
                return this.Error("Description is required.");
            }

            this.tripsService.Add(input.StartPoint, input.EndPoint, input.DepartureTime, input.ImagePath, input.Seats, input.Description);
            return this.Redirect("/Trips/All");
        }


        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.tripsService.GetDetails(tripId);
            if (viewModel == null)
            {
                return this.Error("Trip not found.");
            }

            return this.View(viewModel);
        }


    }
}
