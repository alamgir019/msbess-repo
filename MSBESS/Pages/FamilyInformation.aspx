<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FamilyInformation.aspx.cs" Inherits="WebAdmin.Pages.FamilyInformation" %>
<%@ Register Src="~/UserControls/EIS/ctlFamilyInformation.ascx" TagPrefix ="uc1" TagName="ctlFamilyInformation" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
    <uc1:ctlFamilyInformation runat="server" Id="ctlFamilyInfo" />
</asp:Content>
