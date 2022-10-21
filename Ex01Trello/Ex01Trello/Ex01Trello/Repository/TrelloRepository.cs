using Ex01Trello.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ex01Trello.Repository
{
    public class TrelloRepository
    {
        public const string _APIKEY = "f0b24de6b2488641fcc2e9126370c4c8";
        public const string _USERTOKEN = "854d84d047b6820fcba3e5d951782d31d4a3798f489464a09418bb60aafd4553";
        public const string _BASEURL = "https://api.trello.com/1/";

        public static HttpClient GetHttpClient()
        {
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Add("Accept", "application/json");
            return Client;
        }

        public static async Task<List<TrelloBoard>> GetBoardsAsync()
        {
            string url = $"https://trello.com/1/members/my/boards?key={_APIKEY}&token={_USERTOKEN}";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    List<TrelloBoard> boards = JsonConvert.DeserializeObject<List<TrelloBoard>>(json);

                    return boards;
                }
                catch (Exception ex)
                {
                    //always add a breakpoint here
                    throw ex;
                }
            }

        }

        public static async Task<List<TrelloList>> GetTrelloListsAsync(string boardId)
        {
            string url = $"{_BASEURL}boards/{boardId}/lists?key={_APIKEY}&token={_USERTOKEN}";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    if (json == null) return null;
                    List<TrelloList> lists = JsonConvert.DeserializeObject<List<TrelloList>>(json);
                    return lists;
                }
                catch (Exception ex)
                {
                    //always add a breakpoint here
                    throw ex;
                }
            }
        }

        public static async Task<List<TrelloCard>> GetTrelloCards(string listID)
        {
            string url = $"{_BASEURL}lists/{listID}/cards?key={_APIKEY}&token={_USERTOKEN}";
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    if (json == null)
                    {
                        return null;
                    }
                    List<TrelloCard> lists = JsonConvert.DeserializeObject<List<TrelloCard>>(json);
                    return lists;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static async Task UpdateTrelloCardAsync(TrelloCard selectedCard)
        {
            string url = $"{_BASEURL}cards/{selectedCard.CardId}?key={_APIKEY}&token={_USERTOKEN}";

            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(selectedCard);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(url, content);
                    if(!response.IsSuccessStatusCode)
                    {
                        string errmsg = $"Unsuccesfull PUT to url: {url} and object: {json}";
                        throw new Exception(errmsg);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static async Task AddTrelloCardAsync(TrelloList list, TrelloCard newCard)
        {
            
        }
    }
}
