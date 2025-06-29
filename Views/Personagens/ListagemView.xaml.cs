
using AppRpgEtec.Models;
using AppRpgEtec.ViewModels.Personagens;
using AppRpgEtec.Models.Enuns;

#if ANDROID
using Bumptech.Glide;
using AppRpgEtec.Platforms.Android;
#endif
namespace AppRpgEtec.Views.Personagens;

public partial class ListagemView : ContentPage
{
    ListagemPersonagemViewModel viewModel;
    public ListagemView()
    {
        InitializeComponent();

        viewModel = new ListagemPersonagemViewModel();
        BindingContext = viewModel;
        Title = "Personagens - App Rpg Etec";

        listView.ItemAppearing += (s, e) =>
        {
#if ANDROID
            if (e.Item is Personagem personagem)
            {
                var viewCell = listView.TemplatedItems
                    .FirstOrDefault(c => c.BindingContext == personagem) as ViewCell;

                if (viewCell?.View is Grid grid)
                {
                    var image = grid.Children.OfType<Image>().FirstOrDefault(v => v.StyleId == "person" || v.ClassId == "person");

                    if (image == null)
                    {
                        image = grid.Children.OfType<Image>().FirstOrDefault();
                    }

                    if (image != null)
                    {
                        if(personagem.Classe == ClasseEnum.Cavaleiro)
                        { 
                        var imageView = image.Handler.PlatformView as Android.Widget.ImageView;
                        Android.Net.Uri uri = Android.Net.Uri.Parse("file:///android_asset/menuarmas.png");
                        Glide.With(MainActivity.Instance)
                             .Load(uri)
                             .Into(imageView);
                        }
                        else if (personagem.Classe == ClasseEnum.Clerigo)
                        {
                            var imageView = image.Handler.PlatformView as Android.Widget.ImageView;
                            Android.Net.Uri uri = Android.Net.Uri.Parse("file:///android_asset/person.png");
                            Glide.With(MainActivity.Instance)
                                 .Load(uri)
                                 .Into(imageView);

                        }
                        else if (personagem.Classe == ClasseEnum.Mago)
                        {
                            var imageView = image.Handler.PlatformView as Android.Widget.ImageView;
                            Android.Net.Uri uri = Android.Net.Uri.Parse("file:///android_asset/menupersonagens.png");
                            Glide.With(MainActivity.Instance)
                                 .Load(uri)
                                 .Into(imageView);

                        }
                        else 
                        {
                            var imageView = image.Handler.PlatformView as Android.Widget.ImageView;
                            Android.Net.Uri uri = Android.Net.Uri.Parse("file:///android_asset/person.png");
                            Glide.With(MainActivity.Instance)
                                 .Load(uri)
                                 .Into(imageView);
                        }
                    }
                }
            }
#endif
        }; 

    } 

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = viewModel.ObterPersonagens();
    }
}
