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
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtReason">
                                     <sup class="fa-validate">*</sup>
                        </asp:RequiredFieldValidator>--%>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-primary" CausesValidation="true" OnClick="btnSave_Click" />
                        <asp:Button runat="server" ID="btnClose" Text="Close" CssClass="btn btn-danger" OnClick="btnClose_Click"  CausesValidation="false" />
                    </div>  
                      
                     <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Search Desk Away Info</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">                            
                            <div class="col-sm-6">
                                <asp:TextBox ID="dtpFromDate" autocomplete="off" runat="server" class="form-control datepicker" placeholder="From Date" ClientIDMode="Static"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorFromDate" runat="server"
                                ControlToValidate="dtpFromDate" ErrorMessage="INVALID" class="text-danger validator"
                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-sm-6">
                                <asp:TextBox ID="dtpToDate" autocomplete="off" runat="server" class="form-control datepicker" placeholder="To Date" ClientIDMode="Static"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ControlToValidate="dtpToDate" ErrorMessage="INVALID" class="text-danger validator"
                                ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="btn btn-primary" CausesValidation="true" OnClick="btnSearch_Click" />
                        <asp:Button runat="server" ID="btnClear" Text="Clear" CssClass="btn btn-danger" OnClick="btnClear_Click"  CausesValidation="false" />
                    </div>
                    <div class="box-body table-responsive no-padding">
                        <asp:GridView ID="grDeskAway" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                            AutoGenerateColumns="False" DataKeyNames="EmpId" class="table table-bordered table-hover" PageSize="10">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" Font-Size="Small"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" Font-Size="Small"></SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB" Font-Size="Small"></AlternatingRowStyle>
                            <RowStyle Font-Size="Small" />
                            <Columns>
                                <asp:BoundField DataField="EmpId" HeaderText="Emp.No">
                                    <ItemStyle CssClass="ItemStylecss" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LogDate" HeaderText="Log Date">
                                    <ItemStyle CssClass="ItemStylecss" Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OutTime" HeaderText="OutTime">
                                    <ItemStyle CssClass="ItemStylecss" Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="InTime" HeaderText="InTime">
                                    <ItemStyle CssClass="ItemStylecss" Width="14%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Reason" HeaderText="AttnReason">
                                    <ItemStyle CssClass="ItemStylecss" Width="20%" />
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
