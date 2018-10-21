<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlLeaveApplicationList.ascx.cs" Inherits="WebAdmin.UserControls.Leave.ctlLeaveApproval" %>
<div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Leave Approve List</h3>
            </div>
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="form-group">
                        <label for="inputDateFrom" class="col-sm-2 control-label">Application Date From</label>
                        <div class="col-sm-2">
                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <asp:TextBox ID="txtFromDate" runat="server" class="form-control pull-right datepicker"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ControlToValidate="txtFromDate" ErrorMessage="INVALID" class="text-danger validator"
                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group">

                        <label for="inputDateTo" class="col-sm-2 control-label">To </label>
                        <div class="col-sm-2">
                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <asp:TextBox ID="txtToDate" runat="server" class="form-control pull-right datepicker"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorAppDate" runat="server"
                                ControlToValidate="txtToDate" ErrorMessage="INVALID" class="text-danger validator"
                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-sm-2">
                            <button type="submit" class="btn btn-info">Search</button>
                        </div>
                    </div>
                </div>
                <asp:HiddenField runat="server" ID="hfLDates" />
                <asp:HiddenField runat="server" ID="hfLEnjoyed" />
                <!-- /.box-body -->
                <div class="row ">
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="box-body">
                                <!-- /.box-header -->
                                <div class="box-body table-responsive no-padding">

                                    <asp:GridView
                                        ID="grLeaveList" runat="server" AutoGenerateColumns="False" Width="100%"
                                        DataKeyNames="LvAppID,LTypeTitle,LTypeId,LNature,AppDate,LeaveStart,LeaveEnd,LDurInDays,InsertedBy,LTReason,LAbbrName,EmpId,Fullname,SupervisorId"
                                        EmptyDataText="No Record Found" class="table table-bordered table-hover" OnRowCommand="grLeaveApp_RowCommand" PageSize="10">
                                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" Font-Size="Small"></HeaderStyle>
                                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" Font-Size="Small"></SelectedRowStyle>
                                        <AlternatingRowStyle BackColor="#EFF3FB" Font-Size="Small"></AlternatingRowStyle>
                                        <RowStyle Font-Size="Small" />
                                        <Columns>
                                            <asp:BoundField HeaderText="SL">
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EmpId" HeaderText="APPLICANT">
                                                <ItemStyle Width="40%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LeaveStart" HeaderText="LEAVE DURATION">
                                                <ItemStyle Width="35%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:ButtonField CommandName="ViewClick" HeaderText="VIEW" ControlStyle-CssClass="fa fa-list-alt text-primary text-center" HeaderStyle-CssClass="text-center">
                                                <ItemStyle CssClass="ItemStylecss text-center"></ItemStyle>
                                            </asp:ButtonField>
                                            <asp:ButtonField CommandName="ApproveClick" HeaderText="APPROVE" ControlStyle-CssClass="fa fa-check-square text-success text-center" HeaderStyle-CssClass="text-center">
                                                <ItemStyle CssClass="ItemStylecss text-center"></ItemStyle>
                                            </asp:ButtonField>
                                            <asp:ButtonField CommandName="DenyClick" HeaderText="REGRET" ControlStyle-CssClass="fa fa-minus-square text-warning text-center" HeaderStyle-CssClass="text-center">
                                                <ItemStyle CssClass="ItemStylecss text-center"></ItemStyle>
                                            </asp:ButtonField>
                                            <asp:ButtonField CommandName="CancelClick" HeaderText="CANCEL" ControlStyle-CssClass="fa fa-trash text-danger text-center" ItemStyle-CssClass="deleteLink" HeaderStyle-CssClass="text-center">
                                                <ItemStyle CssClass="cancelLink text-center"></ItemStyle>
                                            </asp:ButtonField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
