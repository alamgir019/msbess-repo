<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LeaveRecommendation.aspx.cs" Inherits="WebAdmin.Pages.LeaveRecommendation" %>
<%@ Register Src="~/UserControls/Leave/ctlLeaveRecommendation.ascx" TagPrefix="uc1" TagName="ctlLeaveRec" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ctlLeaveRec runat="server" id="ctlLeaveApplication" />
</asp:Content>
