﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppRpgEtec.Models;
using AppRpgEtec.Services.Personagens;

namespace AppRpgEtec.ViewModels.Disputas
{
    public class DisputaViewModel : BaseViewModel
    {
        private PersonagemService pService;
        public ObservableCollection<Personagem> PersonagensEncontrados {  get; set; }
        public Personagem Atacante { get; set; }
        public Personagem Oponente { get; set; }
        private Personagem personagemSelecionado;
        private string textoBuscaDigitado = string.Empty;
        public ICommand PesquisarPersonagensCommand { get; set; }
        public DisputaViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new PersonagemService(token);

            Atacante = new Personagem();
            Oponente = new Personagem();    

            PersonagensEncontrados = new ObservableCollection<Personagem>();

            PesquisarPersonagensCommand = new Command<string>(async (string pesquisa) => { await PesquisarPersonagens(pesquisa); });

        }
        public async Task PesquisarPersonagens(string textoPesquisaPersonagem)
        {
            try
            {
                PersonagensEncontrados = await pService.GetByNomeAproximadoAsync(textoPesquisaPersonagem);
                OnPropertyChanged(nameof(PersonagensEncontrados));
            }
            catch (Exception ex)  
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
        public string DescricaoPersonagemAtacante
        {
            get => Atacante.Nome;
        }
        public string DescricaoPersonagemOponente
        {
            get => Oponente.Nome;
        }
        public async void SelecionarPersonagem(Personagem p)
        {
            try
            {
                string tipoCombatente = await Application.Current.MainPage.DisplayActionSheet("Atacante ou Oponente?", "Cancelar", "", "Atacante", "Oponente");
            
                if(tipoCombatente == "Atacante")
                {
                    Atacante = p;
                    OnPropertyChanged(nameof(DescricaoPersonagemAtacante));
                }
                else if (tipoCombatente == "Oponente")
                {
                    Oponente = p;
                    OnPropertyChanged(nameof(DescricaoPersonagemOponente));
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + " Detalhes " + ex.InnerException, "Ok");
            }
        }
        public Personagem PersonagemSelecionado 
        {
            set
            {
                if(value != null)
                {
                    personagemSelecionado = value;
                    SelecionarPersonagem(personagemSelecionado);
                    OnPropertyChanged();
                    PersonagensEncontrados.Clear();
                }
            }
        }

        public string TextoBuscaDigitado 
        { 
            get {  return textoBuscaDigitado; } 
            set
            {
                if(value != null && !string.IsNullOrEmpty(value) && value.Length > 0)
                {
                    textoBuscaDigitado = value;
                    _ = PesquisarPersonagens(textoBuscaDigitado);
                }
                else
                {
                    PersonagensEncontrados.Clear();
                }
            }
        }
    }
}
