﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlEmpPresentStatus.ascx.cs" Inherits="WebAdmin.UserControls.EIS.ctlEmpPresentStatus" %>
<div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <div class="box-title">
                    Employee Presense Status
                </div>
            </div>
            <div class="form-horizontal">
                <div class="box-body">   
                    <asp:HiddenField ID="hdfStatusId" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfStatus" runat="server" Value="0" ClientIDMode="Static" />             
                    <asp:HiddenField ID="hdfIsUpdate" runat="server" ClientIDMode="Static" />
                    <div class="row">
                        <div class="col-sm-6">                                
                            <div class="form-group">
                            <label for="txtEmpId" class="col-sm-3 control-label">
                                Employee ID
                            </label>
                            <div class="col-sm-9">
                                <div class=" input-group">
                                    <asp:TextBox ID="txtEmpId" runat="server" class="form-control" placeholder="Employee ID" ClientIDMode="Static"></asp:TextBox>                                        
                                </div>
                            </div>
                        </div>
                        </div>
                        <div class="col-sm-6">                               
                            <div class="form-group">
                                <label for="txtFullName" class="col-sm-3 control-label">Employee Name</label>
                                <div class="col-sm-9">
                                    <div class=" input-group">
                                        <asp:TextBox ID="txtFullName" runat="server" class="form-control" placeholder="Employee Name" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="dtpStatusDate" class="col-sm-3 control-label">
                                    Date
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dtpStatusDate">
                                                    <sup class="fa-validate">*</sup>
                                    </asp:RequiredFieldValidator>
                                </label>
                                <div class="col-sm-5">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="dtpStatusDate" runat="server" class="form-control pull-right datepicker" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                        ControlToValidate="dtpStatusDate" ErrorMessage="INVALID" class="text-danger validator"
                                        ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">                            
                            <div class="form-group">
                                <label for="txtAwayTime" class="col-sm-3 control-label">Away Time</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtAwayTime" runat="server" ClientIDMode="Static" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="txtReturnTime" class="col-sm-3 control-label">Return Time</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtReturnTime" runat="server" ClientIDMode="Static"></asp:TextBox>  
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="txtRemarks" class="col-sm-3 control-label">Remarks</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtRemarks" runat="server" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>                    
                    <div class="box-footer">
                        <div class="col-md-offset-2">
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" class="btn btn-success col-md-3 btn-custom" OnClick="btnRefresh_Click" CausesValidation="false" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary col-md-3 btn-custom" OnClick="btnSave_Click" />
                            <asp:Button ID="btnClose" runat="server" Text="Close" class="btn btn-warning col-md-3 btn-custom" OnClick="btnClose_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
