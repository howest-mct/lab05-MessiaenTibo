using Ex01Trello.Models;
using Ex01Trello.Repository;
using Ex01Trello.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ex01Trello
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //TestTrelloRepository();

            //ShowDebugInfo();

            LoadBoardAsync();
        }

        private async Task LoadBoardAsync()
        {
            lvwBoards.ItemsSource = await TrelloRepository.GetBoardsAsync();
        }

        private void ShowDebugInfo()
        {
            Debug.WriteLine($"Device: {Device.RuntimePlatform}");
        }

        private async Task TestTrelloRepository()
        {
            List<TrelloBoard> boardsList = await TrelloRepository.GetBoardsAsync();
            foreach(var item in boardsList)
            {
                Debug.WriteLine($"Board: {item.Name}");
            }

            //make use of LINQ
            TrelloBoard selectedBoard = boardsList.Where(b => b.IsFavorite).FirstOrDefault();

            //GET TRELLOLISTS OF SELECTEDBOARD
            List<TrelloList> trelloList = await TrelloRepository.GetTrelloListsAsync(selectedBoard.BoardId);
            foreach (var item in trelloList)
            {
                Debug.WriteLine($"List: {item.Name}");
            }

            //SELECT RANDOM LIST;
            TrelloList selectedList = trelloList[new Random().Next(0, trelloList.Count)];

            //GET CARDS OF SELECTEDLIST
            List<TrelloCard> cardList = await TrelloRepository.GetTrelloCards(selectedList.ListId);
            TrelloCard selectedCard = cardList[new Random().Next(0, cardList.Count)];
            foreach (var item in cardList)
            {
                Debug.WriteLine($"card: {item.Name}");
            }

            //UPDATE SELECTEDCARD
            Debug.WriteLine($"Selected card: {selectedCard.Name}");
            selectedCard.Name = "Look at my updated card";
            await TrelloRepository.UpdateTrelloCardAsync(selectedCard);


        }

        private void lvwBoards_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //stap1: haal het geselecteerde board op
            TrelloBoard selectBoard = (TrelloBoard)lvwBoards.SelectedItem;
            //zelfde als hierboven
            //TrelloBoard selectBoard = lvwBoards.SelectedItem as TrelloBoard;

            //stap2
            if(selectBoard != null)
            {
                Navigation.PushAsync(new TrelloListsPage(selectBoard));
            }
            lvwBoards.SelectedItem = null;
        }
    }
}
