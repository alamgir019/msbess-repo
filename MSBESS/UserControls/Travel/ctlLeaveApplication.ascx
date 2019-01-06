<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlLeaveApplication.ascx.cs" Inherits="WebAdmin.UserControls.Leave.ctlLeaveApplication" %>
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
                <div class="col-sm-offset-4" id="divSupervisee" runat="server">
                    <div class="input-group-btn align-right">
                        <button type="button" class="btn btn-warning dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                            SELECT SUPERVISEE
                    <span class="fa fa-caret-down"></span>
                        </button>
                        <%--<asp:Menu ID="Menu1" runat="server" CssClass="dropdown-menu">
                        <Items>
                            <asp:MenuItem Text="E00001" Value="E00001"></asp:MenuItem>
                            <asp:MenuItem Text="E00002" Value="E00002"></asp:MenuItem>
                        </Items>
                    </asp:Menu>--%>
                        <div class="box-body table-responsive no-padding dropdown-menu" style="max-height: 400px; overflow: scroll;">
                            <asp:GridView
                                ID="grSupervisee" runat="server" AutoGenerateColumns="False" Width="100%"
                                DataKeyNames="EmpId,FullName"
                                EmptyDataText="No Record Found"
                                class="table table-bordered table-hover"
                                PageSize="7"  OnRowCommand="grSupervisee_RowCommand"
                                ShowHeader="false"
                                  >
                                <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" Font-Size="Smaller" ></HeaderStyle>
                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" Font-Size="Smaller"></SelectedRowStyle>
                                <AlternatingRowStyle BackColor="#EFF3FB" Font-Size="Smaller"></AlternatingRowStyle>
                                <RowStyle Font-Size="Smaller" />
                                <Columns>
                                    <asp:ButtonField CommandName="Select" HeaderText="SELECT" ControlStyle-CssClass="fa fa-list-alt text-primary text-center" HeaderStyle-CssClass="text-center">
                                                <ItemStyle CssClass="ItemStylecss text-center"></ItemStyle>
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="EmpId" HeaderText="EMP ID">
                                        <ItemStyle CssClass="ItemStylecssCenter"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FullName" HeaderText="NAME">
                                        <ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Center" ></ItemStyle>
                                    </asp:BoundField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
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
                                        <div class="input-group-addon">
                                            <asp:LinkButton ID="btnEmpSearch" OnClick="btnEmpSearch_Click" runat="server" class="fa fa-search" aria-hidden="true" CausesValidation="false" />
                                        </div>
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
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="txtLPackName" class="col-sm-4 control-label">Leave Package</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtLPackName" runat="server" class="form-control" placeholder="Leave Package" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdfLvAppID" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfLvPackStartDate" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfLvPackEndDate" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfGender" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfLTypeNature" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdfIsUpdate" runat="server" ClientIDMode="Static" />
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
                    <asp:HiddenField ID="hdfSupervisorEmail" runat="server" ClientIDMode="Static" />
                </div>
                <!-- /.box-body -->
                <div class="row row-custom">
                    <div class="col-md-4">
                        <div class="box box-danger">
                            <div class="box-header with-border">
                                <h3 class="box-title">Check Leave Balance</h3>
                            </div>
                            <div class="form-horizontal custom-form bg-success">
                                <div class="box-body no-padding">
                                    <!-- /.box-header -->
                                    <div class="box-body table-responsive no-padding">
                                        <asp:GridView
                                            ID="grLeaveStatus" runat="server" AutoGenerateColumns="False" Width="100%"
                                            DataKeyNames="EmpId,LTypeID,LTypeTitle,LCarryOverd,LEntitled,LCashed,LeaveEnjoyed,LeaveElapsed,lvOpening"
                                            EmptyDataText="No Record Found"
                                            class="table table-bordered table-hover"
                                            PageSize="7">
                                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" Font-Size="Smaller"></HeaderStyle>
                                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" Font-Size="Smaller"></SelectedRowStyle>
                                            <AlternatingRowStyle BackColor="#EFF3FB" Font-Bold="True" Font-Size="Smaller"></AlternatingRowStyle>
                                            <RowStyle Font-Size="Smaller" Font-Bold="True" />
                                            <Columns>
                                                <asp:BoundField DataField="LTypeTitle" HeaderText="Leave">
                                                    <ItemStyle CssClass="ItemStylecssCenter" Width="25%"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="lvPrevYearCarry" Visible="False">
                                                    <ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Center" Width="0%"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LCarryOverd" HeaderText="Carry">
                                                    <ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LEntitled" HeaderText="Entitled">
                                                    <ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Total">
                                                    <ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveEnjoyed" HeaderText="Availed">
                                                    <ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Balance">
                                                    <ItemStyle CssClass="ItemStylecssCenter" HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="box box-success">
                            <div class="box-header with-border">
                                <h3 class="box-title">Complete Leave Application</h3>
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
                                        <label for="dtpFromDate" class="col-sm-4 control-label">
                                            Date From
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="dtpLeaveStart">
                                                             <sup class="fa-validate">*</sup>
                                             </asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="dtpLeaveStart" runat="server" class="form-control datepicker" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorFromDate" runat="server"
                                                ControlToValidate="dtpLeaveStart" ErrorMessage="INVALID" class="text-danger validator"
                                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputDateTo" class="col-sm-4 control-label">
                                            Date To
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="dtpLeaveEnd">
                                                             <sup class="fa-validate">*</sup>
                                             </asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-6">
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="dtpLeaveEnd" runat="server" class="form-control datepicker" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorToDate" runat="server"
                                                ControlToValidate="dtpLeaveEnd" ErrorMessage="INVALID" class="text-danger validator"
                                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="inputHalfDay" class="col-sm-4 control-label">Half Day</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlIsHalfDay" runat="server"
                                                AutoPostBack="True" CausesValidation="True"
                                                CssClass="form-control" ClientIDMode="Static"
                                                placeholder="Half Day">
                                                <asp:ListItem
                                                    Value="0">Nil</asp:ListItem>
                                                <asp:ListItem Value="1">1st Half</asp:ListItem>
                                                <asp:ListItem Value="2">2nd Half</asp:ListItem>
                                            </asp:DropDownList>
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
                                        <label for="inputLeaveType" class="col-sm-4 control-label">
                                            Leave Type
                                            <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="*" ControlToValidate="ddlLTypeId"
                                                Operator="NotEqual" ValueToCompare="-1">
                                                         <sup class="fa-validate">*</sup>
                                            </asp:CompareValidator>
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlLTypeId" runat="server"
                                                AutoPostBack="True" CausesValidation="True"
                                                CssClass="form-control" OnSelectedIndexChanged="ddlLTypeId_SelectedIndexChanged"
                                                placeholder="Leave Type" ClientIDMode="Static">
                                                <asp:ListItem Value="0">Nil</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-6">
                                            <label for="inputAvailability">Availability</label>
                                            <asp:TextBox ID="txtAvailable" runat="server" class="form-control" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            <label for="inputLDurInDays">Applied For</label>
                                            <asp:TextBox ID="txtLDurInDays" runat="server" class="form-control" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="box box-warning">
                            <!-- <div class="box"style:"c A0D0F3">-->
                            <div class="box-header with-border">
                                <h3 class="box-title">Fill in Remarks</h3>
                            </div>
                            <div class="form-horizontal custom-form  bg-primary">
                                <div class="box-body">
                                    <div class="form-group">
                                        <label for="txtLTReason">Reason</label>
                                        <asp:TextBox ID="txtLTReason" runat="server" class="form-control" placeholder="Reason" Rows="3" TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtAddrAtLeave">Leave Address</label>
                                        <asp:TextBox ID="txtAddrAtLeave" runat="server" class="form-control" placeholder="Leave Address" Rows="3" TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtPhoneNo">Contact Number</label>
                                        <asp:TextBox ID="txtPhoneNo" runat="server" class="form-control" placeholder="Contact Number" ClientIDMode="Static"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers, Custom"
                                            ValidChars="+" TargetControlID="txtPhoneNo" />
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
                <%--<asp:Label ID="lblMsg" runat="server" Text="Label1"></asp:Label>--%>
            </div>
        </div>
    </div>
</div>
