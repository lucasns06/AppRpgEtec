using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRpgEtec.Models;

namespace AppRpgEtec.Services.Personagens
{
    public class PersonagemService : Request
    {
        private readonly Request _request;
        private const string _apiUrlBase = "https://rpgapilucasns.azurewebsites.net/Personagens";

        private string _token;
        public PersonagemService(string token)
        {
            _request = new Request();
            _token = token;
        }
        public async Task<int> PostPersonagemAsync(Personagem p)
        {
            return await _request.PostReturnIntAsync(_apiUrlBase, p, _token);
        }
        public async Task<ObservableCollection<Personagem>> GetPersonagensAsync()
        {
            string urlComplementar = string.Format("{0}", "/GetAll"); 
            ObservableCollection<Personagem> listaPersonagens = await _request.GetAsync<ObservableCollection<Personagem>>(_apiUrlBase + urlComplementar, _token);
            return listaPersonagens;
        }
        public async Task<Personagem> GetPersonagemAsync(int personagemId)
        {
            string urlComplementar = string.Format("/{0}", personagemId); 
            var personagem = await _request.GetAsync<Personagem>(_apiUrlBase + urlComplementar, _token);
            return personagem;
        }
        public async Task<int> PutPersonagemAsync(Personagem p)
        {
            var result = await _request.PutAsync(_apiUrlBase, p, _token); 
            return result;
        }
        public async Task<int> DeletePersonagemAsync(int personagemId)
        {
            string urlComplementar = string.Format("/{0}", personagemId); 
            var result = await _request.DeleteAsync(_apiUrlBase + urlComplementar, _token); 
            return result;
        }
        public async Task<ObservableCollection<Personagem>> GetByNomeAproximadoAsync(string busca)
        {
            string urlComplementar = $"/GetByNomeAproximado/{busca}";

            ObservableCollection<Personagem> listaPersonagens = await _request.GetAsync<ObservableCollection<Personagem>>(_apiUrlBase + urlComplementar, _token);
            
            return listaPersonagens;
        }
    }
}
