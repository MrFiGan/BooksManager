<%@ Page Title="Edit Book" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditBook.aspx.cs" Inherits="Client.EditBook" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Edit Book</h2>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="EditBookValidationGroup" CssClass="alert alert-danger" />

    <asp:HiddenField ID="hdnBookId" runat="server" />

    <asp:TextBox ID="txtTitle" runat="server" placeholder="Title" CssClass="form-control" />
    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ValidationGroup="EditBookValidationGroup" ErrorMessage="Title is required." CssClass="text-danger" />

    <asp:TextBox ID="txtAuthor" runat="server" placeholder="Author" CssClass="form-control" />
    <asp:RequiredFieldValidator ID="rfvAuthor" runat="server" ControlToValidate="txtAuthor" ValidationGroup="EditBookValidationGroup" ErrorMessage="Author is required." CssClass="text-danger" />

    <asp:TextBox ID="txtISBN" runat="server" placeholder="ISBN" CssClass="form-control" />
    <asp:RequiredFieldValidator ID="rfvISBN" runat="server" ControlToValidate="txtISBN" ValidationGroup="EditBookValidationGroup" ErrorMessage="ISBN is required." CssClass="text-danger" />

    <asp:TextBox ID="txtYear" runat="server" placeholder="Publication Year" CssClass="form-control" />
    <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="txtYear" ValidationGroup="EditBookValidationGroup" ErrorMessage="Publication Year is required." CssClass="text-danger" />
    <asp:CompareValidator ID="cvYear" runat="server" ControlToValidate="txtYear" Type="Integer" Operator="DataTypeCheck" ValidationGroup="EditBookValidationGroup" ErrorMessage="Publication Year should be a number." CssClass="text-danger" />

    <asp:TextBox ID="txtQuantity" runat="server" placeholder="Quantity" CssClass="form-control" />
    <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ControlToValidate="txtQuantity" ValidationGroup="EditBookValidationGroup" ErrorMessage="Quantity is required." CssClass="text-danger" />
    <asp:CompareValidator ID="cvQuantity" runat="server" ControlToValidate="txtQuantity" Type="Integer" Operator="DataTypeCheck" ValidationGroup="EditBookValidationGroup" ErrorMessage="Quantity Year should be a number." CssClass="text-danger" />

    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
        <asp:ListItem Text="Select a Category" Value="" />
    </asp:DropDownList>

    <br />
    <asp:Button ID="btnUpdateBook" runat="server" Text="Update Book" OnClick="btnUpdateBook_Click" CssClass="btn btn-primary" ValidationGroup="EditBookValidationGroup" />
</asp:Content>
