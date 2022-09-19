using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabiTravel.XamarinAPP.ViewModels;
using TabiTravel.XamarinModel.Enum;
using TabiTravel.XamarinModel.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabiTravel.XamarinAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TranslationListPage : ContentPage
    {
        TranslatePageVM TranslatePageVM;
        public int IdentityId { get; set; }
        private EnumTranslate EnumTranlate;
        public TranslationListPage(int id,EnumTranslate enumTranslate)
        {
            InitializeComponent();
            TranslatePageVM = new TranslatePageVM(Navigation, this,enumTranslate,id);
            BindingContext = TranslatePageVM;
            IdentityId = id;
            EnumTranlate = enumTranslate;
        }
        protected override void OnAppearing()
        {
            TranslatePageVM.GetTranslateList();
            base.OnAppearing();
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TranslationEditPage(IdentityId,EnumTranlate));
            Navigation.RemovePage(this);
        }

        
    }
}