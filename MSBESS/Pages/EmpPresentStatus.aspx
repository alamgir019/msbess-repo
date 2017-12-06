<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmpPresentStatus.aspx.cs" Inherits="WebAdmin.Pages.EmpPresentStatus" %>

<%@ Register Src="~/UserControls/EIS/ctlEmpPresentStatus.ascx" TagPrefix ="uc1" TagName="ctlEmpPresentStatus" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
    <uc1:ctlEmpPresentStatus runat="server" id="ctlEmpPresentStatus" />
</asp:Content>
