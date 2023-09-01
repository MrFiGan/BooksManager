using System;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Client
{
    public partial class DeleteBook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    hdnBookId.Value = Request.QueryString["id"];
                }
                else
                {
                    // Handle case when no id is provided
                }
            }
        }

        protected async void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnBookId.Value))
            {
                Guid bookId = new Guid(hdnBookId.Value);

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiConfiguration.BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.DeleteAsync($"{ApiConfiguration.BookPath}/{bookId}");

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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}