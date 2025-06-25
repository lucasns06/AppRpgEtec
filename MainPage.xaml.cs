#if ANDROID
using AppRpgEtec.Platforms.Android;
using Bumptech.Glide;
#endif
namespace AppRpgEtec
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
#if ANDROID
            Loaded += (s, e) =>
            {
                var personView = person.Handler.PlatformView as Android.Widget.ImageView;
                Android.Net.Uri uriPerson = Android.Net.Uri.Parse("file:///android_asset/person.png");
                Glide.With(MainActivity.Instance).Load(uriPerson).Into(personView);

                var menuPersonagensView = menupersonagens.Handler.PlatformView as Android.Widget.ImageView;
                Android.Net.Uri uriMenuP = Android.Net.Uri.Parse("file:///android_asset/menupersonagens.svg");
                Glide.With(MainActivity.Instance).Load(uriMenuP).Into(menuPersonagensView);

                var menuArmasView = menuarmas.Handler.PlatformView as Android.Widget.ImageView;
                Android.Net.Uri uriArmas = Android.Net.Uri.Parse("file:///android_asset/menuarmas.svg");
                Glide.With(MainActivity.Instance).Load(uriArmas).Into(menuArmasView);

            };
#endif
        }
    }

}
