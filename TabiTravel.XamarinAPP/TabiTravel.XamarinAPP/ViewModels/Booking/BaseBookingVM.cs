using System;
using System.Collections.Generic;
using System.Text;
using TabiTravel.Service.Interface;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels.Booking
{
    public class BaseBookingVM : BaseVM
    {
        protected IBookingService BookingService { get;}
        public BaseBookingVM(INavigation navigation, Page page) : base(navigation, page)
        {
            BookingService = DependencyService.Get<IBookingService>(DependencyFetchTarget.NewInstance);
        }
    }
}
