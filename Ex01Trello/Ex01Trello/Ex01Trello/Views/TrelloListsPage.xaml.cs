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
    public partial class TrelloListsPage : ContentPage
    {
        public TrelloBoard MyBoard { get; set; }
        public TrelloListsPage(TrelloBoard board)
        {
            InitializeComponent();

            this.MyBoard = board;
            this.Title = board.Name;
            LoadLists();
        }

        private async Task LoadLists()
        {
            lvwTrelloLists.ItemsSource = await TrelloRepository.GetTrelloListsAsync(MyBoard.BoardId);
        }

        private void lvwTrelloLists_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}