<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmpPresentStatus.aspx.cs" Inherits="WebAdmin.Pages.EmpPresentStatus" %>

<%@ Register Src="~/UserControls/EIS/ctlEmpPresentStatus.ascx" TagPrefix ="uc1" TagName="ctlEmpPresentStatus" %> 
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
    <link rel="stylesheet" href="http://cdn.rawgit.com/jdewit/bootstrap-timepicker/gh-pages/css/bootstrap-timepicker.min.css" />
<script type="text/javascript" src="http://cdn.rawgit.com/jdewit/bootstrap-timepicker/gh-pages/js/bootstrap-timepicker.js"></script>
    <uc1:ctlEmpPresentStatus runat="server" id="ctlEmpPresentStatus" />
</asp:Content>
