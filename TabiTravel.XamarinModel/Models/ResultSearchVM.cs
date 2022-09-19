using System;
using System.Collections.Generic;
using System.Text;
using TabiTravel.XamarinModel.Models.InterestModel;
using TabiTravel.XamarinModel.Models.TourModel;

namespace TabiTravel.XamarinModel.Models
{
    public class ResultSearchVM : BaseModelVM
    {
        private List<GetMyInterestVM> getMyInterestVMs;
        private List<GetMyTourListVM> getMyTourListVMs;

        public List<GetMyInterestVM> GetMyInterestVMs { get => getMyInterestVMs; set { getMyInterestVMs = value;OnPropertyChanged(nameof(GetMyInterestVMs));} }
        public List<GetMyTourListVM> GetMyTourListVMs { get => getMyTourListVMs; set { getMyTourListVMs = value;OnPropertyChanged(nameof(GetMyTourListVMs));} }
    }
}
