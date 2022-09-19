using System;
using System.Collections.Generic;
using System.Text;
using TabiTravel.Service.Interface;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.ViewModels.POI
{
    public class BasePOIVM : BaseVM
    {
        protected IInterestService InterestService;
        public BasePOIVM(INavigation navigation, Page page) : base(navigation, page)
        {
            InterestService = DependencyService.Get<IInterestService>(DependencyFetchTarget.NewInstance);
        }
    }
}
