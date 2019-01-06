<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TravelRecommendation.aspx.cs" Inherits="WebAdmin.Pages.LeaveRecommendation" %>
<%@ Register Src="~/UserControls/Travel/ctlTravelRecommendation.ascx" TagPrefix="uc1" TagName="ctlTravelRec" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ctlTravelRec runat="server" id="ctlTravelApplication" />
</asp:Content>
