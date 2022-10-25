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
    public partial class SingleCardPage : ContentPage
    {
        public TrelloBoard MyBoard { get; set; }
        public TrelloList MyList { get; set; }
        public SingleCardPage(TrelloBoard board, TrelloList list)
        {
            InitializeComponent();
            //Title
            this.Title = "Add a new card";

            //Board
            MyBoard = board;
            lblBoard.Text = board.Name;

            //List
            MyList = list;
            lblList.Text = list.Name;

        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnSave_Clicked(object sender, EventArgs e)
        {
            //Create new card
            TrelloCard newCard = new TrelloCard();
            newCard.Name = editName.Text;

            //Save the card
            TrelloRepository.AddTrelloCardAsync(MyList, newCard);

            Navigation.PopAsync();
        }
    }
}