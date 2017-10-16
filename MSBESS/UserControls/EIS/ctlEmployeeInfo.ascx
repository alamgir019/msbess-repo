<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlEmployeeInfo.ascx.cs" Inherits="WebAdmin.UserControls.EIS.ctlEmployeeInfo" %>
<div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <div class="box-title">
                    Employee Self Information
                </div>
            </div>
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-sm-8">
                            <div class="form-group">
                                <label for="txtEmpId" class="col-sm-3 control-label">
                                    Employee ID
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmpId">
                                     <sup class="fa-validate">*</sup>
                              </asp:RequiredFieldValidator>
                                </label>
                                <div class="col-sm-9">
                                    <div class=" input-group">
                                        <asp:TextBox ID="txtEmpId" runat="server" class="form-control" placeholder="Employee ID" ClientIDMode="Static"></asp:TextBox>
                                        <div class="input-group-addon">
                                            <asp:LinkButton ID="btnEmpSearch" OnClick="btnEmpSearch_Click" runat="server" class="fa fa-search" aria-hidden="true" CausesValidation="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="txtFullName" class="col-sm-3 control-label">Employee Name</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtFullName" runat="server" class="form-control" placeholder="Employee Name" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <asp:Image ID="imgEmpImage" runat="server" class="img-responsive" Height="90px" Width="90px" ImageUrl="~/Content/images/NoImageSmall.jpg" ClientIDMode="Static" />
                      
                        </div>
                        <div class="col-sm-2">
                            <asp:Image ID="imgEmpSignImage" runat="server" class="img-responsive" Height="90px" Width="90px" ImageUrl="~/Content/images/NoImageSmall.jpg" ClientIDMode="Static"/>
                        </div>
                    </div>
                    <div class="box box-default bg-inner-form">
                        <div class="row row-custom-top">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="txtPreAddress" class="control-label col-sm-4">Present Address</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="txtPreAddress" Rows="2" TextMode="MultiLine" CssClass="form-control" placeholder="Present Address" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="txtPrePhone" class="col-sm-4 control-label">Phone</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="txtPrePhone" CssClass="form-control" placeholder="Phone" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="txtPreFax" class="col-sm-4 control-label">Fax</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="txtPreFax" CssClass="form-control" placeholder="Fax" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box box-default bg-inner-form">
                        <div class="row row-custom-top">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="lblPerAddress" class="col-sm-4 control-label">Permanent Address</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="txtPerAddress" TextMode="MultiLine" Rows="2" CssClass="form-control" placeholder="Permanent Address" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="ddlDistrict" class="col-sm-4 control-label">District</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" ID="ddlPerDistrictID" CssClass="form-control" placeholder="District" ClientIDMode="Static">
                                            <asp:ListItem Value="0" Text="Nil"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="txtPerPhone" class="col-sm-4 control-label">Phone</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="txtPerPhone" CssClass="form-control" placeholder="Phone" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="txtPerFax" class="col-sm-4 control-label">Fax</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="txtPerFax" CssClass="form-control" placeholder="Fax" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box box-default bg-inner-form">
                        <div class="row row-custom-top">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="txtFatherName" class="col-sm-4 control-label">Father Name</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtFatherName" runat="server" class="form-control" placeholder="Father Name" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="txtMotherName" class="col-sm-4 control-label">Mother Name</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtMotherName" runat="server" class="form-control" placeholder="Mother Name" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="ddlMaritalStatus" class="col-sm-4 control-label">Marital Status</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" ID="ddlMaritalStatus" CssClass="form-control" ClientIDMode="Static">
                                            <asp:ListItem Value="N">Nil</asp:ListItem>
                                            <asp:ListItem Value="Single">Single</asp:ListItem>
                                            <asp:ListItem Value="Married">Married</asp:ListItem>
                                            <asp:ListItem Value="Separated">Widow</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="txtMarriageDate" class="col-sm-4 control-label">
                                        Marriage Date
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="dtpMarriageDate">
                                                             <sup class="fa-validate">*</sup>
                                             </asp:RequiredFieldValidator>--%>
                                    </label>
                                    <div class="col-sm-4">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="dtpMarriageDate" runat="server" class="form-control pull-right datepicker" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorDOB" runat="server"
                                            ControlToValidate="dtpMarriageDate" ErrorMessage="INVALID" class="text-danger validator"
                                            ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="ddlReligionId" class="col-sm-4 control-label">Religion</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" ID="ddlReligionId" CssClass="form-control" ClientIDMode="Static">
                                            <asp:ListItem Value="0" Text="Nil"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="ddlBloodGroupId" class="col-sm-4 control-label">Blood Group</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" ID="ddlBloodGroupId" CssClass="form-control" ClientIDMode="Static">
                                            <asp:ListItem Value="0" Text="Nil"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="ddlCountry" class="col-sm-4 control-label">Country</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList runat="server" ID="ddlPerCountryID" CssClass="form-control" placeholder="Country" ClientIDMode="Static">
                                            <asp:ListItem Value="0" Text="Nil"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="txtNationality" class="col-sm-4 control-label">Nationality</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="txtNationality" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="txtPassport" class="col-sm-4 control-label">Passport</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="txtPassportNo" CssClass="form-control" placeholder="Passport" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="txtDOB" class="col-sm-4 control-label">
                                        Date of Birth
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="dtpDOB">
                                                             <sup class="fa-validate">*</sup>
                                             </asp:RequiredFieldValidator>
                                    </label>
                                    <div class="col-sm-4">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="dtpDOB" runat="server" class="form-control pull-right datepicker" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                            ControlToValidate="dtpDOB" ErrorMessage="INVALID" class="text-danger validator"
                                            ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="txtNationalId" class="col-sm-4 control-label">National ID</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox runat="server" ID="txtNationalId" CssClass="form-control" placeholder="National ID" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box-footer">
                        <div class="col-md-offset-2">
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" class="btn btn-success col-md-3 btn-custom" CausesValidation="false" OnClick="btnRefresh_Click" />
                            <asp:Button ID="btnSave" runat="server" Text="Update" OnClick="btnSave_Click" class="btn btn-primary col-md-3 btn-custom" />
                            <asp:Button ID="btnClose" runat="server" Text="Close" class="btn btn-warning col-md-3 btn-custom" CausesValidation="false" OnClick="btnClose_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
