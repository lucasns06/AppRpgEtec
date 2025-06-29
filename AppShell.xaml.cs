using AppRpgEtec.ViewModels;
using AppRpgEtec.Views.Armas;
using AppRpgEtec.Views.Personagens;
#if ANDROID
using Bumptech.Glide;
using AppRpgEtec.Platforms.Android;
#endif

namespace AppRpgEtec
{
    public partial class AppShell : Shell
    {
        AppShellViewModel viewModel;
        public AppShell()
        {
            InitializeComponent();

            viewModel = new AppShellViewModel();
            BindingContext = viewModel;
            string login = Preferences.Get("UsuarioUsername", string.Empty);    
            lblLogin.Text = login;  
            
            Routing.RegisterRoute("cadPersonagemView", typeof(CadastroPersonagemView));
            Routing.RegisterRoute("cadArmaView", typeof(CadastroArmaView));

        }
    }
}
