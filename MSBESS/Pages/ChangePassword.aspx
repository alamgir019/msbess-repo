<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="WebAdmin.Pages.ChangePassword" %>
<%@ Register Src="~/UserControls/EIS/ctlChangePassword.ascx" TagPrefix ="uc1" TagName="ctlChangePassword" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
    <uc1:ctlChangePassword runat="server" id="ctlEmpInfo" />
</asp:Content>
