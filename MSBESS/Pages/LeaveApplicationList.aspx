<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LeaveApplicationList.aspx.cs" Inherits="WebAdmin.Pages.LeaveApproval" %>
<%@ Register Src="~/UserControls/Leave/ctlLeaveApplicationList.ascx" TagPrefix ="uc1" TagName="ctlLeaveAppr" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
    <uc1:ctlLeaveAppr runat="server" id="ctlEmpInfo" />
</asp:Content>
