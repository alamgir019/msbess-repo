<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlLeaveStatement.ascx.cs" Inherits="WebAdmin.UserControls.Leave.ctlLeaveStatement" %>
<div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Leave Statement</h3>
            </div>
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="lblEmpID" class="col-sm-4">Employee ID:</label>
                                    <div class="col-sm-8">
                                        <asp:Label runat="server" ID="lblEmpID"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="lblEmpName" class="col-sm-4">Employee Name:</label>
                                    <div class="col-sm-8">
                                        <asp:Label runat="server" ID="lblEmpName"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="lblPosition" class="col-sm-4">Position:</label>
                                    <div class="col-sm-8">
                                        <asp:Label runat="server" ID="lblPosition"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="lblOffice" class="col-sm-4">Office:</label>
                                    <div class="col-sm-8">
                                        <asp:Label runat="server" ID="lblOffice"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="lblPosition" class="col-sm-4">Joining Date:</label>
                                    <div class="col-sm-8">
                                        <asp:Label runat="server" ID="lblJoinDate"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-horizontal custom-form">
                <div class="box-body">
                    <div class="row row-custom-top">
                        <div class="col-sm-12">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="lblPrintDate" class="col-sm-4">Leave balance as on </label>
                                    <div class="col-sm-8">
                                        <asp:Label runat="server" ID="lblPrintDate"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                            </div>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body table-responsive no-padding">
                        <asp:GridView ID="grLeaveBalance" runat="server" Width="100%" EmptyDataText="No Record Found"
                            AutoGenerateColumns="False" DataKeyNames="EmpId,LTypeID,LTypeTitle,lvPrevYearCarry,LCarryOverd,LEntitled,LCashed,LeaveEnjoyed,LeaveElapsed,lvOpening"
                            class="table table-bordered table-hover">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True"></SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                                <asp:BoundField DataField="LTypeTitle" HeaderText="LEAVE TYPE">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="LCarryOverd" HeaderText="CARRY OVER">
                                    <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="LEntitled" HeaderText="CREDIT">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="LeaveEnjoyed" HeaderText="AVAILED">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="" HeaderText="BALANCE">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="row row-custom-top">
                        <div class="col-sm-12">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="lblLvStartPeriod" class="col-sm-4">Leave Records from </label>
                                    <div class="col-sm-3">
                                        <asp:Label runat="server" ID="lblLvStartPeriod"></asp:Label>
                                    </div>
                                    <label for="lblLvEndPeriod" class="col-sm-2">to </label>
                                    <div class="col-sm-3">
                                        <asp:Label runat="server" ID="lblLvEndPeriod"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                            </div>
                        </div>
                    </div>
                    <div class="box-body table-responsive no-padding">
                        <asp:GridView ID="grLeaveHistory" runat="server" Width="100%" EmptyDataText="No Record Found"
                            AutoGenerateColumns="False" DataKeyNames=""
                            class="table table-bordered table-hover">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True"></SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                                <asp:BoundField DataField="" HeaderText="SL">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="LTypeTitle" HeaderText="LEAVE TYPE">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AppDate" HeaderText="APPLIED ON">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="LeaveStart" HeaderText="LEAVE START">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="LeaveEnd" HeaderText="LEAVE TO">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="LDurInDays" HeaderText="DAYS">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AppStatus" HeaderText="Leave Status">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ApprovedBy" HeaderText="APPROVED BY">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Approver" HeaderText="APPROVER NAME">
                                    <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="approveDate" HeaderText="APPROVE ON">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
