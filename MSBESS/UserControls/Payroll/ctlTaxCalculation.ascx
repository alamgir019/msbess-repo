<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlTaxCalculation.ascx.cs" Inherits="WebAdmin.UserControls.Payroll.ctlTaxCalculation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div class="row">
    <div class="col-md-12">
        <div class="row">
            <div class="col-sm-3">
                <div class="form-group">
                    <label for="ddlMonth" class="col-sm-4 control-label">Month:</label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="ddlMonth" runat="server" class="form-control" ClientIDMode="Static"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-group">
                    <label for="ddlFiscalYear" class="col-sm-4 control-label">Income Year:</label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="ddlFiscalYear" runat="server" class="form-control" ClientIDMode="Static"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-group">
                    <label for="txtAssYear" class="col-sm-4 control-label">Income Year:</label>
                    <asp:TextBox ID="txtAssYear" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="*" ControlToValidate="txtAssYear"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-group">
                    <asp:Button runat="server" ID="btnShow" Text="Show" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
        <div class="box box-primary" style="margin-top: 10px">
            <div class="box-header with-border">
                <h3 class="box-title">Entry</h3>
            </div>
            <div class="box box-body">
                <div class="form-horizontal">
                    <%--start of row--%>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtYBasicSalary" class="col-sm-4 control-label">YBasicSalary:</label>
                                <asp:TextBox ID="txtYBasicSalary" runat="server" class="form-control"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="fltbPayAmnt4" runat="server" TargetControlID="txtYBasicSalary"
                                    FilterType="Custom,Numbers" ValidChars=".,-"></cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtYHouseRent" class="col-sm-4 control-label">YHouseRent:</label>
                                <asp:TextBox ID="txtYHouseRent" runat="server" class="form-control"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtYHouseRent"
                                    FilterType="Custom,Numbers" ValidChars=".,-"></cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtT_HA" class="col-sm-4 control-label">T_HA:</label>
                                <asp:TextBox ID="txtT_HA" runat="server" class="form-control"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtT_HA"
                                    FilterType="Custom,Numbers" ValidChars=".,-"></cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="txtYTransportAllowance" class="col-sm-4 control-label">YTransAllow:</label>
                                <asp:TextBox ID="txtYTransportAllowance" runat="server" class="form-control"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtYTransportAllowance"
                                    FilterType="Custom,Numbers" ValidChars=".,-"></cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                    <%--end of row--%>
                </div>
            </div>
        </div>
    </div>
</div>
