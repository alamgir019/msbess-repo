<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LeaveStatement.aspx.cs" Inherits="WebAdmin.Pages.LeaveStatement" %>
<%@ Register Src="~/UserControls/Leave/ctlLeaveStatement.ascx" TagPrefix="uc1" TagName="ctlLeaveStatement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ctlLeaveStatement runat="server" id="ctlLeaveStatement" />
</asp:Content>

