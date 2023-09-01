using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using Client.Models;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Client.Api;

namespace Client
{
    public partial class EditBook : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["book"]))
                {
                    string serializedBook = HttpUtility.UrlDecode(Request.QueryString["book"]);
                    Book book = DeserializeBook(serializedBook);

                    hdnBookId.Value=book.Id.ToString();
                    txtTitle.Text = book.Title;
                    txtAuthor.Text = book.Author;
                    txtISBN.Text = book.ISBN;
                    txtYear.Text = book.PublicationYear.ToString();
                    txtQuantity.Text = book.Quantity.ToString();

                    ddlCategory.DataSource = await ApiUtilities.LoadCategories();
                    ddlCategory.DataTextField = "Name";
                    ddlCategory.DataValueField = "Id";
                    ddlCategory.DataBind(); 

                    ddlCategory.SelectedValue = book.CategoryId.ToString();
                }
                else
                {
                    // Handle case when no information is provided
                }
            }
        }

        private Book DeserializeBook(string serializedBook)
        {
            string decoded = Encoding.UTF8.GetString(Convert.FromBase64String(serializedBook));
            return JsonConvert.DeserializeObject<Book>(decoded);
        }

        protected async void btnUpdateBook_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Guid bookId = new Guid(hdnBookId.Value);

                Book updatedBook = new Book()
                {
                    Id = bookId,
                    Title = txtTitle.Text,
                    Author = txtAuthor.Text,
                    ISBN = txtISBN.Text,
                    PublicationYear = Convert.ToInt32(txtYear.Text),
                    Quantity = Convert.ToInt32(txtQuantity.Text),
                    CategoryId = new Guid(ddlCategory.SelectedValue)
                };

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiConfiguration.BaseUrl); 
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PutAsJsonAsync($"{ApiConfiguration.BookPath}/{bookId}", updatedBook);

                    if (response.IsSuccessStatusCode)
                    {
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        // Handle error here
                    }
                }
            }
        }
    }
}