using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.Effects
{
    public class PhoneLabel : Label
    {
        public static readonly BindableProperty PhoneNumberProperty =
    BindableProperty.Create(nameof(PhoneNumber), typeof(string), typeof(PhoneLabel), null);

        public string PhoneNumber
        {
            get { return (string)GetValue(PhoneNumberProperty); }
            set { SetValue(PhoneNumberProperty, value); }
        }

        public PhoneLabel()
        {
            var themeColor = ColorConverters.FromHex("#009BDD");

            TextColor = themeColor;
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => PlacePhoneCall(PhoneNumber))
            });
        }

        public async void PlacePhoneCall(string number)
        {
            var page = new Page();
            try
            {
                PhoneDialer.Open(number);
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
                await page.DisplayAlert("Error", anEx.ToString() , "OK");
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
                await page.DisplayAlert("Error", ex.ToString(), "OK");
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                await page.DisplayAlert("Error", ex.ToString(), "OK");
            }
        }
    }
}
