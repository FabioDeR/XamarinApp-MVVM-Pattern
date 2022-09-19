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
    public partial class TranslationEditPage : ContentPage
    {     
       
        
        TranslateDetailPageVM TranslateDetailVM;
        public TranslationEditPage(int id, EnumTranslate enumTranslate)
        {
            InitializeComponent();          
            TranslateDetailVM = new TranslateDetailPageVM(Navigation, this,enumTranslate ,id);
            BindingContext = TranslateDetailVM;
            UpdateButton.IsVisible = false;
            TranslateDetailVM.GetLanguagelist();
        }
        

        public TranslationEditPage(int id, EnumTranslate enumTranslate,string languageIso)
        {
            InitializeComponent();
            TranslateDetailVM = new TranslateDetailPageVM(Navigation, this, enumTranslate, id);
            BindingContext = TranslateDetailVM;
            PickerStack.IsVisible = false;
            SaveButton.IsVisible = false;
            TranslateDetailVM.GetTranslateVMById(languageIso);            
        }



    }
}