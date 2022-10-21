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
        public TrelloList MyList { get; set; }
        public TrelloCardsPage(TrelloList list)
        {
            InitializeComponent();
            this.MyList = list;
            lblListName.Text = list.Name;

            LoadCards();
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
                Navigation.PushAsync(new SingleCardPage());
            }
            lvwCards.SelectedItem = null;
        }
    }
}