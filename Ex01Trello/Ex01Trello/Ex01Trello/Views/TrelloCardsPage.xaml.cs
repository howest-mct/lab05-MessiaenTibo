using Ex01Trello.Models;
using Ex01Trello.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ex01Trello.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrelloCardsPage : ContentPage
    {
        public TrelloBoard MyBoard { get; set; }
        public TrelloList MyList { get; set; }
        public TrelloCardsPage(TrelloBoard board,TrelloList list)
        {
            InitializeComponent();

            //Board
            this.MyBoard = board;

            //List
            this.MyList = list;
            lblListName.Text = list.Name;

            //Adding "click" event to add a card field
            TapGestureRecognizer gesture = new TapGestureRecognizer();
            gesture.Tapped += Gesture_Tapped;//AUTO GENERATE even by typing += , followed by 2 tabs
            lblAddCard.GestureRecognizers.Add(gesture);

            LoadCards();
        }

        //Loading all the cards, to look if a new one is added
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadCards();
        }


        private void Gesture_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SingleCardPage(MyBoard,MyList));
        }

        private async Task LoadCards()
        {
            lvwCards.ItemsSource = await TrelloRepository.GetTrelloCards(MyList.ListId);
        }

        private void btnGoBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void lvwCards_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //stap1: haal het geselecteerde board op
            TrelloCard selectCard = (TrelloCard)lvwCards.SelectedItem;
            //zelfde als hierboven
            //TrelloBoard selectBoard = lvwBoards.SelectedItem as TrelloBoard;

            //stap2
            if (selectCard != null)
            {
                Navigation.PushAsync(new SingleCardPage(MyBoard,MyList));
            }
            lvwCards.SelectedItem = null;
        }

        private async void btnCloseCard_Clicked(object sender, EventArgs e)
        {
            //Get clicked card
            TrelloCard card = (sender as Button).BindingContext as TrelloCard;
            //Close clicked card
            card.IsClosed = true;
            //Update
            await TrelloRepository.UpdateTrelloCardAsync(card);
            //load all cards
            LoadCards();
        }
    }
}