<script src="../crystalreportviewers13/js/crviewer/crv.js"></script>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayrollReportViewer.aspx.cs" Inherits="WebAdmin.CrystalReports.PayrollReportViewer" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

  <!-- Horizontal Form -->
    <form id="form1" runat="server">
    <div class="box box-primary">
        <fieldset style="text-align: left; background-color: White">
            <CR:CrystalReportViewer ID="CRVT" runat="server" EnableDatabaseLogonPrompt="False"
                OnBeforeRender="CRVT_BeforeRender" OnUnload="CRVT_Unload" PrintMode="ActiveX"
                 GroupTreeStyle-BackColor="White" BackColor="White" 
                HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" 
                RenderingDPI="100" ToolPanelView="None" />
        </fieldset>
    </div>
</form>
