<%@ Page  Title="Policies" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmpFileReader.aspx.cs" Inherits="WebAdmin.Pages.EmpFileReader" %>
<%@ Register Src="~/UserControls/EIS/ctlEmpFileReader.ascx" TagPrefix="uc1" TagName="ctlEmpFileReader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ctlEmpFileReader runat="server" id="ctlEmpFileReader" />
</asp:Content>