<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MonthlyPayslip.aspx.cs" Inherits="WebAdmin.Pages.MonthlyPayslip" %>
<%@ Register Src="~/UserControls/Payroll/ctlMonthlyPayslip.ascx" TagPrefix ="uc1" TagName="ctlMonthlyPayslip" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
    <uc1:ctlMonthlyPayslip runat="server" Id="ctlFamilyInfo" />
</asp:Content>
