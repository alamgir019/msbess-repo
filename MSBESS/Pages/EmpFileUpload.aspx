<%@ Page  Title="File Management" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmpFileUpload.aspx.cs" Inherits="WebAdmin.Pages.EmpFileUpload" %>
<%@ Register Src="~/UserControls/EIS/ctlEmpFileUpload.ascx" TagPrefix="uc1" TagName="ctlEmpFileUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ctlEmpFileUpload runat="server" id="ctlEmpFileUpload" />
</asp:Content>
