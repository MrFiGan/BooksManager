using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using Client.Models;
using System.Threading.Tasks;
using Client.Api;

namespace Client
{
    public partial class AddBook : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCategory.DataSource = await ApiUtilities.LoadCategories();
                ddlCategory.DataTextField = "Name";
                ddlCategory.DataValueField = "Id";
                ddlCategory.DataBind();
            }
        }

        protected async void btnAddBook_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string title = txtTitle.Text;
                string author = txtAuthor.Text;
                string isbn = txtISBN.Text;
                int publicationYear = Convert.ToInt32(txtPublicationYear.Text);
                int quantity = Convert.ToInt32(txtQuantity.Text);
                Guid categoryId = new Guid(ddlCategory.SelectedValue); 

                Book newBook = new Book(title, author, isbn, publicationYear, quantity, categoryId);

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiConfiguration.BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync(ApiConfiguration.BookPath, newBook);

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