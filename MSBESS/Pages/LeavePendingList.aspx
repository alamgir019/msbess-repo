<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LeavePendingList.aspx.cs" Inherits="WebAdmin.Pages.LeavePendingList" %>
<%@ Register Src="~/UserControls/Leave/ctlPendingLeaveList.ascx" TagPrefix="uc1" TagName="ctlPendingLeaveList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ctlPendingLeaveList runat="server" id="ctlPendingLeaveList" />
</asp:Content>
