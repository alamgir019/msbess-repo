<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlEmpTravel.ascx.cs" Inherits="WebAdmin.UserControls.Leave.ctlEmpTravel" %>
<div class="row">
    <div class="col-md-12">
        <!-- Horizontal Form -->
        <div class="box box-primary">
            <div class="col-md-8">
                <div class="box-header with-border">
                    <h3 class="box-title">Leave Application</h3>
                </div>
            </div>
            <div class="col-md-4">
            </div>
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="txtEmpId" class="col-sm-4 control-label">
                                    Employee ID
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmpID">
                                     <sup class="fa-validate">*</sup>
                              </asp:RequiredFieldValidator>
                                </label>
                                <div class="col-sm-8">
                                    <div class=" input-group">
                                        <asp:TextBox ID="txtEmpID" runat="server" class="form-control" placeholder="Employee ID" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="txtFullName" class="col-sm-4 control-label">Applicant</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtFullName" runat="server" class="form-control" placeholder="Applicant" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="txtSupervisor" class="col-sm-4 control-label">Supervisor</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtSupervisor" runat="server" class="form-control" placeholder="Supervisor" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdfIsUpdate" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfTvAppID" runat="server" ClientIDMode="Static" />
                    <%--
                    <asp:HiddenField ID="hdfLvPackStartDate" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfLvPackEndDate" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfGender" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfLTypeNature" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfPreLTypeId" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfIsOffdayCounted" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfLAbbrName" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfLEnjoyed" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfLDates" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfPreLDates" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfLeavePakId" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfPreLEnjoyed" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfFiscalYrId" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfAppStatus" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfSupervisorId" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfSupervisorEmail" runat="server" ClientIDMode="Static" />--%>
                </div>
                <!-- /.box-body -->
                <div class="row row-custom">
                    <div class="col-md-4">
                        <div class="box box-danger">
                            <div class="box-header with-border">
                                <h3 class="box-title">Instruction to Admin</h3>
                            </div>
                            <div class="form-horizontal custom-form bg-success">
                                <div class="box-body">
                                    <div class="form-group">
                                        <label for="txtInstruction">Instruction</label>
                                        <asp:TextBox ID="txtInstruction" runat="server" class="form-control" placeholder="Instruction" Rows="6" TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="box box-success">
                            <div class="box-header with-border">
                                <h3 class="box-title">Complete Travel Application</h3>
                            </div>
                            <div class="form-horizontal custom-form bg-warning">
                                <div class="box-body">
                                    <div class="form-group">
                                        <label for="inputAppDate" class="col-sm-4 control-label">
                                            App. Date
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="dtpAppDate">
                                                             <sup class="fa-validate">*</sup>
                                            </asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="dtpAppDate" runat="server" class="form-control datepicker" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorAppDate" runat="server"
                                                ControlToValidate="dtpAppDate" ErrorMessage="INVALID" class="text-danger validator"
                                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtVisitTo" class="col-sm-4 control-label">
                                            Visit To
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtVisitTo">
                                                             <sup class="fa-validate">*</sup>
                                             </asp:RequiredFieldValidator>
                                        </label>                                        
                                        <div class="col-sm-8">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtVisitTo" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="dtpTravelStart" class="col-sm-4 control-label">
                                            Date From
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="dtpTravelStart">
                                                             <sup class="fa-validate">*</sup>
                                             </asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="dtpTravelStart" runat="server" class="form-control datepicker" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorFromDate" runat="server"
                                                ControlToValidate="dtpTravelStart" ErrorMessage="INVALID" class="text-danger validator"
                                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="dtpTravelEnd" class="col-sm-4 control-label">
                                            Date To
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="dtpTravelEnd">
                                                             <sup class="fa-validate">*</sup>
                                             </asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="dtpTravelEnd" runat="server" class="form-control datepicker" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorToDate" runat="server"
                                                ControlToValidate="dtpTravelEnd" ErrorMessage="INVALID" class="text-danger validator"
                                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputResumeDate" class="col-sm-4 control-label">
                                            Resume Date
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="dtpResumeDate">
                                                             <sup class="fa-validate">*</sup>
                                             </asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="dtpResumeDate" runat="server" class="form-control datepicker" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorResumeDate" runat="server"
                                                ControlToValidate="dtpResumeDate" ErrorMessage="INVALID" class="text-danger validator"
                                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="ddlTravelMode" class="col-sm-4 control-label">
                                            Travel Mode
                                            <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="*" ControlToValidate="ddlTravelMode"
                                                Operator="NotEqual" ValueToCompare="-1">
                                                         <sup class="fa-validate">*</sup>
                                            </asp:CompareValidator>
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlTravelMode" runat="server"
                                                AutoPostBack="True" CausesValidation="True"
                                                CssClass="form-control" OnSelectedIndexChanged="ddlTravelMode_SelectedIndexChanged"
                                                placeholder="ddlTravel Mode" ClientIDMode="Static">
                                                <asp:ListItem Value="0">Nil</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtTotalDays" class="col-sm-4 control-label">
                                            Total Days
                                        </label>
                                        <div class="col-sm-6">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtTotalDays" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="ddlProject" class="col-sm-4 control-label">
                                            Project to be charged
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="*" ControlToValidate="ddlProject"
                                                Operator="NotEqual" ValueToCompare="-1">
                                                         <sup class="fa-validate">*</sup>
                                            </asp:CompareValidator>
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlProject" runat="server"
                                                AutoPostBack="True" CausesValidation="True" CssClass="form-control"
                                                placeholder="Select Project" ClientIDMode="Static">
                                                <asp:ListItem Value="0">Nil</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="box box-warning">
                            <div class="box-header with-border">
                                <h3 class="box-title">Fill in Remarks</h3>
                            </div>
                            <div class="form-horizontal custom-form  bg-primary">
                                <div class="box-body">
                                    <div class="form-group">
                                        <label for="txtLTReason">Purpose</label>
                                        <asp:TextBox ID="txtPurpose" runat="server" class="form-control" placeholder="Purpose" Rows="3" TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.body footer -->
                <div class="box-footer">
                    <div class="col-md-offset-2">
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" class="btn btn-success col-md-3 btn-custom" CausesValidation="false" OnClick="btnRefresh_Click" />
                        <asp:Button ID="btnSave" runat="server" Text="Apply" class="btn btn-primary col-md-3 btn-custom" OnClick="btnSave_Click" />
                        <asp:Button ID="btnClose" runat="server" Text="Close" class="btn btn-warning col-md-3 btn-custom" CausesValidation="false" OnClick="btnClose_Click" />
                    </div>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</div>
