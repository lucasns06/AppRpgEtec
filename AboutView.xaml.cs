#if ANDROID
using AppRpgEtec.Platforms.Android;
using Bumptech.Glide;
using System.Threading.Tasks;
#endif

namespace AppRpgEtec;

public partial class AboutView : ContentPage
{
	public AboutView()
	{
        InitializeComponent();

            #if ANDROID
        Loaded += (s, e) =>
        {
            var imageView = eu.Handler.PlatformView as Android.Widget.ImageView;

            Android.Net.Uri uri = Android.Net.Uri.Parse("file:///android_asset/eu.jpg");

            Glide.With(MainActivity.Instance)
                                    .Load(uri)
                                    .Into(imageView);
        };
#endif
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync("https://github.com/lucasns06");
    }
}