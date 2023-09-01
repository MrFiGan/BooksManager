<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Client._Default" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* Style for GridView */
        .bookGridView {
            width: 100%;
            border-collapse: collapse;
        }

        /* Style for header cells */
        .bookGridView th {
            padding: 10px;
            text-align: center;
            background-color: #f2f2f2;
        }

        /* Style for data cells */
        .bookGridView td {
            padding: 10px;
            text-align: center;
            border: 1px solid #ddd;
        }
    </style>

    <h1 id="aspnetTitle">Book List</h1>
    <asp:GridView ID="bookGridView" runat="server" AutoGenerateColumns="False" CssClass="bookGridView">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Author" HeaderText="Author" />
            <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
            <asp:BoundField DataField="PublicationYear" HeaderText="Publication Year" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:Button runat="server" Text="Update" OnClick="UpdateButton_Click" CssClass="btn btn-primary" />
                    <asp:Button runat="server" Text="Delete" OnClick="DeleteButton_Click" CssClass="btn btn-danger" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
