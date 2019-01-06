<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TravelStatusList.aspx.cs" Inherits="WebAdmin.Pages.TravelStatusList" %>
<%@ Register Src="~/UserControls/Travel/ctlTravelStatusList.ascx" TagPrefix="uc1" TagName="ctlTravelStatusList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ctlTravelStatusList runat="server" id="ctlTravelStatusList" />
</asp:Content>
