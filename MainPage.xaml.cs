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
                Android.Net.Uri uriMenuP = Android.Net.Uri.Parse("file:///android_asset/menupersonagens.png");
                Glide.With(MainActivity.Instance).Load(uriMenuP).Into(menuPersonagensView);

                var menuArmasView = menuarmas.Handler.PlatformView as Android.Widget.ImageView;
                Android.Net.Uri uriArmas = Android.Net.Uri.Parse("file:///android_asset/menuarmas.png");
                Glide.With(MainActivity.Instance).Load(uriArmas).Into(menuArmasView);

                var menuDisputasView = menudisputas.Handler.PlatformView as Android.Widget.ImageView;
                Android.Net.Uri uriDisp = Android.Net.Uri.Parse("file:///android_asset/menudisputas.png");
                Glide.With(MainActivity.Instance).Load(uriDisp).Into(menuDisputasView);

                var exitView = exit.Handler.PlatformView as Android.Widget.ImageView;
                Android.Net.Uri uriExit = Android.Net.Uri.Parse("file:///android_asset/exit.png");
                Glide.With(MainActivity.Instance).Load(uriExit).Into(exitView);
            };
#endif
        }
    }

}
