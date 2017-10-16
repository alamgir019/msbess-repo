<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlChangePassword.ascx.cs" Inherits="WebAdmin.UserControls.EIS.ctlChangePassword" %>
<div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Change Password</h3>
            </div>
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="txtUserId" class="col-sm-4 control-label">User Id:</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtUserId" runat="server" class="form-control" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="txtOldPass" class="col-sm-4 control-label">Old Password:</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtOldPass" TextMode="Password" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOldPass"
                                        runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="txtNewPass" class="col-sm-4 control-label">New Password:</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtNewPass" TextMode="Password" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtNewPass"
                                        runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="txtConfNewPass" class="col-sm-4 control-label">Confirm New Password:</label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtConfNewPass" TextMode="Password" runat="server" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtConfNewPass"
                                        runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-offset-10">
                                <asp:Button runat="server" Text="Change Password" ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
