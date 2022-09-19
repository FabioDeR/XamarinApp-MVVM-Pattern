using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TabiTravel.XamarinAPP.Effects
{
    public class EmailLabel : Label
    {
        public static readonly BindableProperty MailProperty =
    BindableProperty.Create(nameof(Mail), typeof(string), typeof(EmailLabel), null);

        public string Mail
        {
            get { return (string)GetValue(MailProperty); }
            set { SetValue(MailProperty, value); }
        }

        public EmailLabel()
        {
            var themeColor = ColorConverters.FromHex("#009BDD");
            var uri = $"mailto:{Mail}";

            TextColor = themeColor;
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => await Launcher.OpenAsync(uri))
            });
        }
    }
}
