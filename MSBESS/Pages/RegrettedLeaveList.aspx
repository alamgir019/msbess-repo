<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegrettedLeaveList.aspx.cs" Inherits="WebAdmin.Pages.RegrettedLeaveList" %>
<%@ Register Src="~/UserControls/Leave/ctlRegrettedLeaveList.ascx" TagPrefix ="uc1" TagName="ctlLeaveRegretted" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
    <uc1:ctlLeaveRegretted runat="server" id="ctlEmpInfo" />
</asp:Content>
