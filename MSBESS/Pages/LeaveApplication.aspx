<%@ Page  Title="Leave Application" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LeaveApplication.aspx.cs" Inherits="WebAdmin.Pages.LeaveApplication" %>
<%@ Register Src="~/UserControls/Leave/ctlLeaveApplication.ascx" TagPrefix="uc1" TagName="ctlLeaveApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ctlLeaveApplication runat="server" id="ctlLeaveApplication" />
</asp:Content>
