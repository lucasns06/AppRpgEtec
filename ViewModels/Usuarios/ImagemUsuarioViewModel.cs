using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRpgEtec.Services.Usuarios;

namespace AppRpgEtec.ViewModels.Usuarios
{
    public class ImagemUsuarioViewModel : BaseViewModel
    {
        private UsuarioService uService;
        private static string conexaoAzureStorage = "";
        private static string container = "arquivos";

        public ImagemUsuarioViewModel()
        {
            //string token = Preferences.Get("UsuarioToken", string.Empty);
            //uService = new UsuarioService(token);
        }
        private ImageSource fonteImage;

        public ImageSource FonteImage 
        { 
            get => fonteImage; 
            set
            {
                fonteImage = value;
                OnPropertyChanged();
            }
        }
        private byte[] foto;
        public byte[] Foto 
        { 
            get => foto; 
            set
            {
                foto = value;
                OnPropertyChanged();
            }
        }
        public async void Fotografar()
        {
            try
            {

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
    }
}
