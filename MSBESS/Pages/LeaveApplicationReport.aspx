<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LeaveApplicationReport.aspx.cs" Inherits="WebAdmin.Pages.LeaveApplicationReport" %>
<%@ Register Src="~/UserControls/Leave/ctlLeaveApplicationReport.ascx" TagPrefix ="uc1" TagName="ctlLeaveApplicationReport" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
    <uc1:ctlLeaveApplicationReport runat="server" id="ctlEmpInfo" />
</asp:Content>
