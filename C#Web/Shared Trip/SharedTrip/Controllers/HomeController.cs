namespace SharedTrip.App.Controllers
{
    using SharedTrip.ViewModels.Home;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class HomeController : Controller
    {

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserLoggedIn())
            {
                var viewModel = new IndexViewModel();
                return this.View(viewModel, "/Trips/All");
            }

            return this.View();
        }
    }
}