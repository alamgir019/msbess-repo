<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlReportViewer.ascx.cs" Inherits="WebAdmin.UserControls.Report.ctlReportViewer" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<div class="row">
    <div class="col-md-12">
        <!-- Horizontal Form -->
        <div class="box box-primary">
            <fieldset style="text-align: left; background-color: White">
                <CR:CrystalReportViewer ID="CRVT" runat="server" AutoDataBind="true" />
            </fieldset>
        </div>
    </div>
</div>