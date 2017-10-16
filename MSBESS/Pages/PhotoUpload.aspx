<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PhotoUpload.aspx.cs" Inherits="WebAdmin.Pages.PhotoUpload" %>
<%@ Register Src="~/UserControls/EIS/ctlPhotoUpload.ascx" TagPrefix="uc1" TagName="ctlPhotoUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ctlPhotoUpload runat="server" id="ctlPhotoUpload" />
</asp:Content>
