<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.Master" AutoEventWireup="true" CodeBehind="LeaveApplicationView.aspx.cs" Inherits="WebAdmin.Pages.LeaveApplicationView" %>

<%@Register Src="~/UserControls/Leave/ctlLeaveAppView.ascx" TagPrefix="uc1" TagName="ctlLeaveApplicationView"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ctlLeaveApplicationView runat="server" id="ctlLeaveApplication" />

</asp:Content>
