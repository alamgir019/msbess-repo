<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TravelToApprove.aspx.cs" Inherits="WebAdmin.Pages.TravelToApprove" %>
<%@ Register Src="~/UserControls/Travel/ctlTravelToApprove.ascx" TagPrefix ="uc1" TagName="ctlTravelToApprove" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
    <uc1:ctlTravelToApprove runat="server" id="ctlTravelToApprove" />
</asp:Content>
