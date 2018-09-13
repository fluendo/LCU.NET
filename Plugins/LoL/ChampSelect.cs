﻿using LCU.NET.API_Models;
using RestSharp;
using System.Threading.Tasks;

namespace LCU.NET.Plugins.LoL
{
    public interface IChampSelect
    {
        Task<LolChampSelectChampSelectSession> GetSessionAsync();
        Task PatchMySelectionAsync(LolChampSelectChampSelectMySelection selection);
        Task<LolChampSelectChampSelectTimer> GetTimerAsync();
        Task<int> GetCurrentChampion();
        Task PatchActionById(LolChampSelectChampSelectAction action, int id);
        Task<LolChampSelectChampSelectPickableChampions> GetPickableChampions();
        Task<LolChampSelectChampSelectBannableChampions> GetBannableChampions();
    }

    public class ChampSelect : IChampSelect
    {
        private ILeagueClient Client;
        internal ChampSelect(ILeagueClient client)
        {
            this.Client = client;
        }

        public const string Endpoint = "/lol-champ-select/v1/session";
        
        public Task<LolChampSelectChampSelectSession> GetSessionAsync()
            => Client.MakeRequestAsync<LolChampSelectChampSelectSession>(Endpoint, Method.GET);
        
        public Task PatchMySelectionAsync(LolChampSelectChampSelectMySelection selection)
            => Client.MakeRequestAsync(Endpoint + "/my-selection", Method.PATCH, selection);
        
        public Task<LolChampSelectChampSelectTimer> GetTimerAsync()
            => Client.MakeRequestAsync<LolChampSelectChampSelectTimer>(Endpoint + "/timer", Method.GET);
        
        public Task<int> GetCurrentChampion()
            => Client.MakeRequestAsync<int>("/lol-champ-select/v1/current-champion", Method.GET);
        
        public Task PatchActionById(LolChampSelectChampSelectAction action, int id)
            => Client.MakeRequestAsync(Endpoint + $"/actions/{id}", Method.PATCH, action);
        
        public Task<LolChampSelectChampSelectPickableChampions> GetPickableChampions()
            => Client.MakeRequestAsync<LolChampSelectChampSelectPickableChampions>("/lol-champ-select/v1/pickable-champions", Method.GET);
        
        public Task<LolChampSelectChampSelectBannableChampions> GetBannableChampions()
            => Client.MakeRequestAsync<LolChampSelectChampSelectBannableChampions>("/lol-champ-select/v1/bannable-champions", Method.GET);
    }
}
