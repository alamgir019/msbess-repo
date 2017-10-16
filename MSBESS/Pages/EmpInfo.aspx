<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmpInfo.aspx.cs" Inherits="WebAdmin.Pages.EmpInfo" %>

<%@ Register Src="~/UserControls/EIS/ctlEmployeeInfo.ascx" TagPrefix ="uc1" TagName="ctlEmpInfo" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
    <uc1:ctlEmpInfo runat="server" id="ctlEmpInfo" />
</asp:Content>
