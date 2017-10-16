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
