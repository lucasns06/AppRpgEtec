#if ANDROID
using AppRpgEtec.Platforms.Android;
using Bumptech.Glide;
#endif
using AppRpgEtec.ViewModels.Armas;
using AppRpgEtec.Models;
namespace AppRpgEtec.Views.Armas;
public partial class ListagemView : ContentPage
{
    private ListagemArmaViewModel viewModel;
    public ListagemView()
	{
		InitializeComponent();

        viewModel = new ListagemArmaViewModel();
        BindingContext = viewModel;
        Title = "Armas";

        listView.ItemAppearing += (s, e) =>
        {
#if ANDROID
            if (e.Item is Arma arma)
            {
                var viewCell = listView.TemplatedItems.FirstOrDefault(c => c.BindingContext == arma) as ViewCell;
                if (viewCell?.View is HorizontalStackLayout layout)
                {
                    var image = layout.Children.OfType<Image>().FirstOrDefault();
                    if (image != null)
                    {
                        var imageView = image.Handler.PlatformView as Android.Widget.ImageView;
                        Android.Net.Uri uri = Android.Net.Uri.Parse("file:///android_asset/arma.png");
                        Glide.With(MainActivity.Instance).Load(uri).Into(imageView);
                    }
                }
            }
#endif
        };

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.ObterArmas();
    }
}