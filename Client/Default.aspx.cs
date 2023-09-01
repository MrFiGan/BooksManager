using Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class _Default : Page
    {
        private static List<Book> _booksList;

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _booksList = new List<Book>();
                await LoadBooks();
            }
        }

        private void BindBooksGridView()
        {
            bookGridView.DataSource = _booksList;
            bookGridView.DataBind();
        }

        private async Task LoadBooks()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiConfiguration.BaseUrl); 
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(ApiConfiguration.BookPath);

                if (response.IsSuccessStatusCode)
                {
                    _booksList = await response.Content.ReadAsAsync<List<Book>>();
                    BindBooksGridView();
                }
            }
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            int rowIndex = row.RowIndex;

            string serializedBook = SerializeBook(_booksList[rowIndex]);

            Response.Redirect($"EditBook.aspx?book={HttpUtility.UrlEncode(serializedBook)}");
        }

        private string SerializeBook(Book book)
        {
            string serialized = JsonConvert.SerializeObject(book);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serialized));
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            int rowIndex = row.RowIndex;
            Guid bookId = _booksList[rowIndex].Id;

            Response.Redirect($"DeleteBook.aspx?Id={bookId}");
        }
    }
}