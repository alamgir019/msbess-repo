<%@ Page Title="Nominee"  Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Nominee.aspx.cs" Inherits="WebAdmin.Pages.Nominee" %>

<%@ Register Src="~/UserControls/EIS/ctlNomineeInfo.ascx" TagPrefix="uc1" TagName="ctlNominee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <uc1:ctlNominee runat="server" id="ctlNominee" />
</asp:Content>


