<%@ Page Title="Delete Book" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeleteBook.aspx.cs" Inherits="Client.DeleteBook" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Delete Book</h2>
    <p>Are you sure you want to delete this book?</p>
    
    <asp:HiddenField ID="hdnBookId" runat="server" />

    <asp:Button ID="btnConfirmDelete" runat="server" Text="Confirm Delete" OnClick="btnConfirmDelete_Click" CssClass="btn btn-danger" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-secondary" />
</asp:Content>
