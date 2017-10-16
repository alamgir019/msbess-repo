<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmpBloodGroupSearch.aspx.cs" Inherits="WebAdmin.Pages.EmpBloodGroupSearch" %>
<%@ Register Src="~/UserControls/EIS/ctlEmpBloodGroupSearch.ascx" TagPrefix ="uc1" TagName="ctlEmpBloodGroupSearch" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
    <uc1:ctlEmpBloodGroupSearch runat="server" id="ctlEmpInfo" />
</asp:Content>
