<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaxCalculation.aspx.cs" Inherits="WebAdmin.Pages.TaxCalculation" %>
<%@ Register Src="~/UserControls/Payroll/ctlTaxCalculation.ascx" TagPrefix ="uc1" TagName="ctlTaxCalculation" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
    <uc1:ctlTaxCalculation runat="server" Id="ctlFamilyInfo" />
</asp:Content>


