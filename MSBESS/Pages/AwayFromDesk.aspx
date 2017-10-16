<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AwayFromDesk.aspx.cs" Inherits="WebAdmin.Pages.AwayFromDesk" %>
<%@ Register Src="~/UserControls/Attendance/ctlAwayFromDesk.ascx" TagPrefix="uc1" TagName="ctlAwayFromDesk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ctlAwayFromDesk runat="server" id="ctlLeaveApplication" />
</asp:Content>
