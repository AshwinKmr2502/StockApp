using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace XamarinForms3RestApiApp
{
	public partial class MainPage : ContentPage
	{
        private const string Url = "https://api.worldtradingdata.com/api/v1/stock?symbol=AAPL,MSFT,HSBA.L&api_token=vlDhNYQ4bsfKM6gWogmZ9U33r02tiBQ3DEBlsBRyAUVYXST6vLzQom0znNKJ"; 
        private readonly HttpClient _client = new HttpClient(); //Creating a new instance of HttpClient. (Microsoft.Net.Http)
        private List<Post> _posts; //Refreshing the state of the UI in realtime when updating the ListView's Collection

        /// <summary>
        /// Constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }
        /// <inheritdoc />
        /// <summary>
        /// This method gets called before the UI appears.
        /// Async and await to get the value of the Task and for user experience
        /// </summary>
        protected override async void OnAppearing()
        {
            string content = await _client.GetStringAsync(Url); //Sends a GET request to the specified Uri and returns the response body as a string in an asynchronous operation
            Post m = JsonConvert.DeserializeObject<Post>(content);
            MyListView.ItemsSource = m.data; //Assigning the ObservableCollection to MyListView in the XAML of this file
            base.OnAppearing();
        }

         void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
             Navigation.PushAsync(new ListItemPage() {BindingContext = e.Item });
        }
    }
}
