<%@ Page Title="Add Book" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddBook.aspx.cs" Inherits="Client.AddBook" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Add New Book</h2>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="AddBookValidationGroup" CssClass="alert alert-danger" />

    <asp:TextBox ID="txtTitle" runat="server" placeholder="Title" CssClass="form-control" />
    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ValidationGroup="AddBookValidationGroup" ErrorMessage="Title is required." CssClass="text-danger" />

    <asp:TextBox ID="txtAuthor" runat="server" placeholder="Author" CssClass="form-control" />
    <asp:RequiredFieldValidator ID="rfvAuthor" runat="server" ControlToValidate="txtAuthor" ValidationGroup="AddBookValidationGroup" ErrorMessage="Author is required." CssClass="text-danger" />

    <asp:TextBox ID="txtISBN" runat="server" placeholder="ISBN" CssClass="form-control" />
    <asp:RequiredFieldValidator ID="rfvISBN" runat="server" ControlToValidate="txtISBN" ValidationGroup="AddBookValidationGroup" ErrorMessage="ISBN is required." CssClass="text-danger" />

    <asp:TextBox ID="txtPublicationYear" runat="server" placeholder="Publication Year" CssClass="form-control" />
    <asp:RequiredFieldValidator ID="rfvPublicationYear" runat="server" ControlToValidate="txtPublicationYear" ValidationGroup="AddBookValidationGroup" ErrorMessage="Publication Year is required." CssClass="text-danger" />
    <asp:CompareValidator ID="cvYear" runat="server" ControlToValidate="txtPublicationYear" Type="Integer" Operator="DataTypeCheck" ValidationGroup="AddBookValidationGroup" ErrorMessage="Publication Year Year should be a number." CssClass="text-danger" />

    <asp:TextBox ID="txtQuantity" runat="server" placeholder="Quantity" CssClass="form-control" />
    <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ControlToValidate="txtQuantity" ValidationGroup="AddBookValidationGroup" ErrorMessage="Quantity is required." CssClass="text-danger" />
    <asp:CompareValidator ID="cvQuantity" runat="server" ControlToValidate="txtQuantity" Type="Integer" Operator="DataTypeCheck" ValidationGroup="AddBookValidationGroup" ErrorMessage="Quantity Year should be a number." CssClass="text-danger" />

    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
        <asp:ListItem Text="Select a Category" Value="" />
    </asp:DropDownList>

    <br />
    <asp:Button ID="btnAddBook" runat="server" Text="Add Book" OnClick="btnAddBook_Click" CssClass="btn btn-primary" ValidationGroup="AddBookValidationGroup" />
</asp:Content>
