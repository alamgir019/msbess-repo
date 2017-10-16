<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovedLeaveList.aspx.cs" Inherits="WebAdmin.Pages.ApprovedLeaveList" %>
<%@ Register Src="~/UserControls/Leave/ctlApprovedLeaveList.ascx" TagPrefix ="uc1" TagName="ctlApproaveList" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
    <uc1:ctlApproaveList runat="server" id="ctlEmpInfo" />
</asp:Content>
