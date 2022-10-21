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
            this.Title = list.Name;

            LoadCards();
        }

        private async Task LoadCards()
        {
            lvwCards.ItemsSource = await TrelloRepository.GetTrelloCards(MyList.ListId);
        }
    }
}