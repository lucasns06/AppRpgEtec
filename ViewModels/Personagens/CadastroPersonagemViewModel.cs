using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppRpgEtec.Models;
using AppRpgEtec.Models.Enuns;
using AppRpgEtec.Services.Personagens;

namespace AppRpgEtec.ViewModels.Personagens
{
    [QueryProperty("PersonagemSelecionadoId", "pId")]
    public class CadastroPersonagemViewModel : BaseViewModel
    {
        private PersonagemService pService;
        private int id;
        private string nome;
        private int pontosVida;
        private int forca;
        private int defesa;
        private int inteligencia;
        private int disputas;
        private int vitorias;
        private int derrotas;
        private string personagemSelecionadoId;
        private ObservableCollection<TipoClasse> listaTiposClasse;
        private TipoClasse tipoClasseSelecionado;
        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }
        public CadastroPersonagemViewModel()
        {
            string token = Preferences.Get("UsuarioToken",string.Empty); 
            pService = new PersonagemService(token);
            _ = ObterClasses();

            SalvarCommand = new Command(async () => { await SalvarPersonagem(); }, () => ValidarCampos());
            CancelarCommand = new Command(async => CancelarCadastro());
        }

        public int Id
        {
            get => id; 
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }
        public string Nome 
        { 
            get => nome; 
            set 
            { 
                nome = value;
                OnPropertyChanged();
                ((Command)SalvarCommand).ChangeCanExecute();
            } 
        }
        public int PontosVida 
        {
            get => pontosVida;
            set 
            {
                pontosVida = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CadastroHabilitado));
                ((Command)SalvarCommand).ChangeCanExecute();
            }
        }
        public int Forca 
        { 
            get => forca;
            set 
            {
                forca = value;
                OnPropertyChanged();
                ((Command)SalvarCommand).ChangeCanExecute();
            }
        }
        public int Defesa 
        { 
            get => defesa; 
            set 
            {
                defesa = value;
                OnPropertyChanged();
                ((Command)SalvarCommand).ChangeCanExecute();
            } 
        }
        public int Inteligencia 
        { 
            get => inteligencia; 
            set
            {
                inteligencia = value;
                OnPropertyChanged();
            }
        }
        public int Disputas
        {
            get => disputas;
            set
            {
                disputas = value;
                OnPropertyChanged();
            }
        }
        public int Vitorias
        {
            get => vitorias;
            set
            {
                vitorias = value;
                OnPropertyChanged();
            }
        }
        public int Derrotas 
        { 
            get => derrotas; 
            set
            {
                derrotas = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TipoClasse> ListaTiposClasse 
        { 
            get => listaTiposClasse;
            set
            {
                if (value != null)
                {
                    listaTiposClasse = value;
                    OnPropertyChanged();
                }
            }
        }
        public TipoClasse TipoClasseSelecionado 
        { 
            get { return tipoClasseSelecionado; } 
            set
            {
                if(value != null)
                {
                    tipoClasseSelecionado = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PersonagemSelecionadoId 
        { 
            get {  return personagemSelecionadoId; }
            set
            {
                if(value != null)
                {
                    personagemSelecionadoId = Uri.UnescapeDataString(value);
                    CarregarPersonagem();
                }
            } 
        }

        public async Task ObterClasses()
        {
            try
            {
                ListaTiposClasse = new ObservableCollection<TipoClasse>();
                ListaTiposClasse.Add(new TipoClasse() { Id = 1, Descricao = "Cavaleiro" });
                ListaTiposClasse.Add(new TipoClasse() { Id = 2, Descricao = "Mago" });
                ListaTiposClasse.Add(new TipoClasse() { Id = 3, Descricao = "Clerigo" });
                OnPropertyChanged(nameof(ListaTiposClasse));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes" + ex.InnerException, "Ok");
            }
        }
        public async Task SalvarPersonagem()
        {
            try
            {
                Personagem model = new Personagem()
                {
                    Nome = this.nome,
                    PontosVida = this.PontosVida,
                    Defesa = this.Defesa,
                    Derrotas = this.Derrotas,
                    Disputas = this.Disputas,
                    Forca = this.Forca,
                    Inteligencia = this.Inteligencia,
                    Vitorias = this.Vitorias,
                    Id = this.Id,
                    Classe = (ClasseEnum)tipoClasseSelecionado.Id
                };
                if (model.Id == 0)
                    await pService.PostPersonagemAsync(model);
                else
                    await pService.PutPersonagemAsync(model);

                await Application.Current.MainPage
                    .DisplayAlert("Mensagem", "Dados salvos com sucesso!", "Ok");

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
        public async Task CancelarCadastro()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async void CarregarPersonagem()
        {
            try
            {
                Personagem p = await pService.GetPersonagemAsync(int.Parse(PersonagemSelecionadoId));

                this.Nome = p.Nome;
                this.PontosVida = p.PontosVida;
                this.Defesa = p.Defesa;
                this.Derrotas = p.Derrotas; 
                this.Disputas = p.Disputas; 
                this.Forca = p.Forca;
                this.Inteligencia = p.Inteligencia;
                this.Vitorias = p.Vitorias;
                this.Id = p.Id;

                TipoClasseSelecionado = this.ListaTiposClasse.FirstOrDefault(tClasse => tClasse.Id == (int)p.Classe);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
        public bool CadastroHabilitado
        {
            get
            {
                return (pontosVida >  0);
            }
        }
        public bool ValidarCampos()
        {
            return !string.IsNullOrEmpty(Nome)
                && CadastroHabilitado
                && Forca != 0
                && Defesa != 0; 
        }
    }
}
