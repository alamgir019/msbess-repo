<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlLeaveAppView.ascx.cs" Inherits="WebAdmin.UserControls.Leave.ctlLeaveAppView" %>
<div class="row" style="max-width: 8.27in;">
    <div id="printableArea" class="col-md-12">
        <div class="box box-default no-border">
            <div class="box-header">
                <h3><b>Marie Stopes Bangladesh</b></h3>
                <h4><b>Leave Application Form</b></h4>
            </div>
            <div class="form-horizontal">
                <div class="box-body no-border">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="lblApplicantName" class="col-sm-5">Applicant: </label>
                                <asp:Label Text="" runat="server" ID="lblApplicantName" class="col-sm-7 "></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="lblOffice" class="col-sm-5">Office: </label>
                                <asp:Label Text="" runat="server" ID="lblOffice" class="col-sm-7"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="lblDateofApplication" class="col-sm-5">Applied On : </label>
                                <asp:Label Text="" runat="server" ID="lblDateofApplication" class="col-sm-7"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="lblAppliedFor" class="col-sm-5">Leave Type: </label>
                                <asp:Label Text="" runat="server" ID="lblAppliedFor" class="col-sm-7"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="lblLeaveApplied" class="col-sm-5">Leave Duration: </label>
                                <asp:Label Text="" runat="server" ID="lblLeaveApplied" class="col-sm-7"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="lblPurpose" class="col-sm-5">Purpose: </label>
                                <asp:Label Text="" runat="server" ID="lblPurpose" class="col-sm-7"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="lblContact" class="col-sm-5">Leave Address: </label>
                                <asp:Label Text="" runat="server" ID="lblContact" class="col-sm-7"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="lblResumingDate" class="col-sm-5">Resuming Date: </label>
                                <asp:Label Text="" runat="server" ID="lblResumingDate" class="col-sm-7"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="lblRecommendedBy" class="col-sm-5">Recommended By:</label>
                                <asp:Label Text="" runat="server" ID="Label2" class="col-sm-7"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="lblRecommendedOn" class="col-sm-5">Recommended On:</label>
                                <asp:Label Text="" runat="server" ID="Label3" class="col-sm-7"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="lblApprovedBy" class="col-sm-5">Approved By:</label>
                                <asp:Label Text="" runat="server" ID="lblApprovedBy" class="col-sm-7"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="lblApprovedOn" class="col-sm-5">Approved On:</label>
                                <asp:Label Text="" runat="server" ID="lblApprovedOn" class="col-sm-7"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">

                        <%--<label for="lblLeaveBalanceDate" class="col-sm-2">Leave Balance Record On </label>--%>
                        <asp:Label Text="" runat="server" ID="lblLeaveBalanceDate" class="col-sm-12"></asp:Label>

                    </div>
                </div>
                <asp:HiddenField runat="server" ID="hfLDates" />
                <asp:HiddenField runat="server" ID="hfLEnjoyed" />
                <asp:HiddenField runat="server" ID="hdfSpervisorEmail" />
                <%--grid grLeaveStatus --%>
                <div class="table-responsive no-padding box-body">
                    <asp:GridView ID="grLeaveStatus" runat="server" Width="100%" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False" DataKeyNames="EmpId,LTypeID,LTypeTitle,lvPrevYearCarry,LCarryOverd,LEntitled,LCashed,LeaveEnjoyed,LeaveElapsed,lvOpening"
                        class="table table-bordered table-hover">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True"></SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Type" HeaderStyle-HorizontalAlign="Left">
                                <ItemStyle Width="20%" CssClass="ItemStylecssLeft" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="LCarryOverd" HeaderText="Carry Over" HeaderStyle-HorizontalAlign="Right">
                                <ItemStyle Width="20%" CssClass="ItemStylecssCenter" HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="LEntitled" HeaderText="Credit" HeaderStyle-HorizontalAlign="Right">
                                <ItemStyle Width="20%" CssClass="ItemStylecssRight" HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="LeaveEnjoyed" HeaderText="Availed" HeaderStyle-HorizontalAlign="Right">
                                <ItemStyle Width="20%" CssClass="ItemStylecssRight" HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Balance" HeaderStyle-HorizontalAlign="Right">
                                <ItemStyle Width="20%" CssClass="ItemStylecssRight" HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <asp:Label Text="" runat="server" ID="lblLeavePeriod" class="col-sm-12"></asp:Label>
                    </div>
                </div>
                <%--grid grLeaveDtls--%>
                <div class="table-responsive no-padding box-body">
                    <asp:GridView ID="grLeaveDtls" runat="server" Width="100%" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False" DataKeyNames="LvAppID,LTypeTitle,LTypeId"
                        class="table table-bordered table-hover">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True"></SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                        <Columns>
                            <asp:BoundField DataField="LTypeTitle" HeaderText="Leave Type" HeaderStyle-HorizontalAlign="Left">
                                <ItemStyle Width="10%" CssClass="ItemStylecssLeft" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="AppDate" HeaderText="Applied On">
                                <ItemStyle Width="15%" CssClass="ItemStylecssCenter" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="LeaveStart" HeaderText="Leave From">
                                <ItemStyle Width="15%" CssClass="ItemStylecssCenter" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="LeaveEnd" HeaderText="Leave To">
                                <ItemStyle Width="15%" CssClass="ItemStylecssCenter" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="LDurInDays" HeaderText="Days">
                                <ItemStyle Width="10%" CssClass="ItemStylecssRight" HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="AppStatus" HeaderText="Status">
                                <ItemStyle Width="10%" CssClass="ItemStylecssCenter" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By">
                                <ItemStyle Width="15%" CssClass="ItemStylecssCenter" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ApproveDate" HeaderText="Approved On">
                                <ItemStyle Width="10%" CssClass="ItemStylecssCenter" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div class="row" id="divApproveBy" runat="server">
        <div class="col-sm-6">
            <div class="form-group">
                <label for="txtApproveBy" class="col-sm-4 text-danger">Approve By:</label>
                <asp:TextBox runat="server" ID="txtApproveBy" class="col-sm-4" OnTextChanged="txtApproveBy_TextChanged" AutoPostBack="true"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-6">
            <div class="form-group">
                <label for="txtApproveByName" class="col-sm-4 text-danger">Name:</label>
                <asp:TextBox runat="server" ID="txtApproveByName" class="col-sm-8" Enabled="false"></asp:TextBox>
            </div>
        </div>
    </div>
<div class="box-footer no-border">
    <div class="col-md-offset-2">
        <asp:Button ID="btnRecommend" runat="server" Text="Recommend" OnClick="btnRecommend_Click" class="btn btn-primary col-md-2 btn-custom" />
        <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" class="btn btn-primary col-md-2 btn-custom" />
        <asp:Button ID="btnRegret" runat="server" Text="Regret" OnClick="btnRegret_Click" class="btn btn-success col-md-2 btn-custom" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" class="btn btn-danger col-md-2 btn-custom" />
        <asp:Button ID="btnClose" runat="server" Text="Close" class="btn btn-warning col-md-2 btn-custom" OnClientClick="window.close();" />
        <asp:Button ID="btnPrint" runat="server" Text="Print" class="btn btn-info col-md-2 btn-custom" OnClientClick="printDiv('printableArea');" />
    </div>
</div>
</div>


