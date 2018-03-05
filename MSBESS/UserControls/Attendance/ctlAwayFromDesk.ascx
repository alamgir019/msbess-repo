<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlAwayFromDesk.ascx.cs" Inherits="WebAdmin.UserControls.Attendance.ctlAwayFromDesk" %>
<div class="row">
    <div class="col-sm-12">
        <asp:HiddenField runat="server" ID="hdfSINO" />
         <asp:HiddenField runat="server" ID="hdfOutTime" />
        <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Desk Away Info Entry</h4>
                    </div>
                    <div class="modal-body">
                       <h5><asp:Label runat="server" ID="lblDateTime"></asp:Label></h5> 
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtReason" placeholder="Remarks" MaxLength="200"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtReason">
                                     <sup class="fa-validate">*</sup>
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-primary" CausesValidation="true" OnClick="btnSave_Click" />
                        <asp:Button runat="server" ID="btnClose" Text="Close" CssClass="btn btn-danger" OnClick="btnClose_Click"  CausesValidation="false" />
                    </div>
                    
                    <div class="box-body table-responsive no-padding">dfgdfgdfgdgd
                        <asp:GridView ID="grAttnAdj" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                            AutoGenerateColumns="False" DataKeyNames="SL" class="table table-bordered table-hover" PageSize="10">
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
<script type="text/javascript">
        $(window).load(function () {
            $('#myModal').modal('show');
        });
</script>
