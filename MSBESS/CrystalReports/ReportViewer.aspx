<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="WebAdmin.Pages.ReportViewer" %>
<%@ Register Src="~/UserControls/Report/ctlReportViewer.ascx" TagPrefix="uc1" TagName="ctlReportViewer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
    <uc1:ctlReportViewer runat="server" id="ctlReportViewer" />
</asp:Content>