<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlMonthlyAttendance.ascx.cs" Inherits="WebAdmin.UserControls.Attendance.ctlMonthlyAttendance" %>
<div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Monthly Attendance List</h3>
            </div>
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="ddlMonth" class="col-sm-4 control-label">Month:</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlMonth" runat="server" class="form-control" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="ddlYear" class="col-sm-4 control-label">Year:</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlYear" runat="server" class="form-control" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <asp:Button runat="server" ID="btnShow" Text="Show Atttendance" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                        </div>
                    </div>
                    <%--end of row--%>
                    <div class="box-body table-responsive no-padding">
                        <asp:GridView ID="grAttnAdj" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                            AutoGenerateColumns="False" DataKeyNames="SL" class="table table-bordered table-hover" PageSize="10"
                            OnRowDataBound="grAttnAdj_RowDataBound">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" Font-Size="Small"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" Font-Size="Small"></SelectedRowStyle>
                            <%--<AlternatingRowStyle BackColor="#EFF3FB" Font-Size="Small"></AlternatingRowStyle>--%>
                            <RowStyle Font-Size="Small" />
                            <Columns>

                                <asp:BoundField DataField="EmpId" HeaderText="Emp.No">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FullName" HeaderText="Employee Name">
                                    <ItemStyle CssClass="ItemStylecss" Width="16%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DesigName" HeaderText="Designation">
                                    <ItemStyle CssClass="ItemStylecss" Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DeptName" HeaderText="Department">
                                    <ItemStyle CssClass="ItemStylecss" Width="14%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AttndDate" HeaderText="Attn. Date">
                                    <ItemStyle CssClass="ItemStylecss" Width="7%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SignInTime" HeaderText="In Time">
                                    <ItemStyle CssClass="ItemStylecss" Width="8%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SignOutTime" HeaderText="Out Time">
                                    <ItemStyle CssClass="ItemStylecss" Width="8%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Status" HeaderText="Status">
                                    <ItemStyle CssClass="ItemStylecss" Width="6%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                    <ItemStyle CssClass="ItemStylecss" Width="7%" />
                                </asp:BoundField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
