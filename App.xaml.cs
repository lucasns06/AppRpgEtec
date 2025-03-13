namespace AppRpgEtec
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new NavigationPage(new Views.Usuarios.LoginView()));
            return window;
        }
    }
}