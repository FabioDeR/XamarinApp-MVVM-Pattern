using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.XamarinAPP.ViewModels;
using TabiTravel.XamarinModel.Enum;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabiTravel.XamarinAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoreMenuPage : Rg.Plugins.Popup.Pages.PopupPage
    {      
        MoreMenuVM MoreMenuVM;
        public MoreMenuPage(Page page,int id,NavigationEnum naviguationEnum,EnumTranslate translateEnum, int? tourId = null)
        {
            InitializeComponent();
            MoreMenuVM = new MoreMenuVM(Navigation, page, id, naviguationEnum, translateEnum, tourId);
            BindingContext = MoreMenuVM;
        }                
    }
}