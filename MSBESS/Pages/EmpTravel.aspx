<%@ Page  Title="Employee Travel" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmpTravel.aspx.cs" Inherits="WebAdmin.Pages.EmpTravel" %>
<%@ Register Src="~/UserControls/Travel/ctlEmpTravel.ascx" TagPrefix="uc1" TagName="ctlEmpTravel" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ctlEmpTravel runat="server" id="ctlEmpTravel" />
</asp:Content>
