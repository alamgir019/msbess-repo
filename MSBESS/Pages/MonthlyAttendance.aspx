<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MonthlyAttendance.aspx.cs" Inherits="WebAdmin.Pages.MonthlyAttendance" %>
<%@ Register Src="~/UserControls/Attendance/ctlMonthlyAttendance.ascx" TagPrefix="uc1" TagName="ctlMonthlyAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ctlMonthlyAttendance runat="server" id="ctlLeaveApplication" />
</asp:Content>
