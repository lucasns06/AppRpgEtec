#if ANDROID
using AppRpgEtec.Platforms.Android;
using Bumptech.Glide;
#endif
using AppRpgEtec.ViewModels.Usuarios;

namespace AppRpgEtec.Views.Usuarios;

public partial class LoginView : ContentPage
{
	UsuarioViewModel usuarioViewModel;
	public LoginView()
	{
		InitializeComponent();
		usuarioViewModel = new UsuarioViewModel();
		BindingContext = usuarioViewModel;
#if ANDROID
        Loaded += (s, e) =>
        {
            var imageView = rpgimage.Handler.PlatformView as Android.Widget.ImageView;

            Android.Net.Uri uri = Android.Net.Uri.Parse("file:///android_asset/rpgimage.png");

            Glide.With(MainActivity.Instance)
                                    .Load(uri)
                                    .Into(imageView);
        };
#endif
    }
}