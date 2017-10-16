<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlNomineeInfo.ascx.cs" Inherits="WebAdmin.UserControls.EIS.ctlNomineeInfo" %>

<div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Nominee Information</h3>
            </div>
            <!-- Nav tabs -->
            <ul class="nav nav-tabs nav-justified" role="tablist">
                <li role="presentation" class="active"><a href="#setup" aria-controls="setup" role="tab" data-toggle="tab">
                    <h4>Setup</h4>
                </a></li>
                <li role="presentation"><a href="#list" aria-controls="list" role="tab" data-toggle="tab">
                    <h4>List</h4>
                </a></li>
            </ul>

            <!-- Tab panes -->
            <div class="tab-content">
                <div role="tabpanel" class="tab-pane fade in active" id="setup">
                    <div class="form-horizontal">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="txtEmpId" class="col-sm-4 control-label">
                                            Employee ID
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmpId">
                                                              <sup class="fa-validate">*</sup>
                                                  </asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-8">
                                            <div class="input-group">
                                                <asp:TextBox runat="server" ID="txtEmpId" CssClass="form-control" placeholder="Employee ID"></asp:TextBox>
                                                <div class="input-group-addon">
                                                    <asp:LinkButton ID="btnEmpSearch" OnClick="btnEmpSearch_Click" runat="server" class="fa fa-search" aria-hidden="true" CausesValidation="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="txtDepartment" class="col-sm-4 control-label">Department</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="txtDepartment" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfID" runat="server" />
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="txtEmpName" class="col-sm-4 control-label">Employee Name</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="txtEmpName" Enabled="false" CssClass="form-control" placeholder="Employee Name"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="txtJobTitle" class="col-sm-4 control-label">Job Title</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="txtJobTitle" Enabled="false" CssClass="form-control" placeholder="Job Title"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="txtCompany" class="col-sm-4 control-label">Company</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="txtCompany" Enabled="false" CssClass="form-control" placeholder="Company"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="txtProject" class="col-sm-4 control-label">Company</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox runat="server" ID="txtProject" Enabled="false" CssClass="form-control" placeholder="Company"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box box-default bg-inner-form">

                                <div class="row row-custom-top">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="txtNomineeName" class="col-sm-4 control-label">
                                                Nominee Name
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNomineeName">
                                                             <sup class="fa-validate">*</sup>
                                             </asp:RequiredFieldValidator>
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox runat="server" ID="txtNomineeName" CssClass="form-control" placeholder="Nominee Name"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="ddlRelation" class="col-sm-4 control-label">
                                                Relation
                                             <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlRelation"
                                                 Operator="NotEqual" ValueToCompare="-1">
                                                         <sup class="fa-validate">*</sup>
                                             </asp:CompareValidator>
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList runat="server" ID="ddlRelation" CssClass="form-control">
                                                    <asp:ListItem Text="Nil" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="lblDOB" class="col-sm-4 control-label">
                                                Date of Birth
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDOB">
                                                             <sup class="fa-validate">*</sup>
                                             </asp:RequiredFieldValidator>
                                            </label>
                                            <div class="col-sm-4">
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtDOB" runat="server" class="form-control datepicker"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                    ControlToValidate="txtDOB" ErrorMessage="INVALID" class="text-danger validator" Display="Dynamic"
                                                    ValidationExpression="^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-./])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="ddlGender" class="col-sm-4 control-label">
                                                Gender
                                             <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="*" ControlToValidate="ddlGender"
                                                 Operator="NotEqual" ValueToCompare="0">
                                                         <sup class="fa-validate">*</sup>
                                             </asp:CompareValidator>
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList runat="server" ID="ddlGender" CausesValidation="True"
                                                    CssClass="form-control">
                                                    <asp:ListItem Value="0">Nil</asp:ListItem>
                                                    <asp:ListItem Value="M">Male</asp:ListItem>
                                                    <asp:ListItem Value="F">Female</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="txtAddress" class="col-sm-4 control-label">Address</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" placeholder="Address" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="txtRemarks" class="col-sm-4 control-label">Remarks</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" placeholder="Remarks" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <label for="txtShare" class="col-sm-4 control-label">Nominee Type</label>
                                        <div class="col-sm-8">
                                            <div class="checkbox icheck">
                                                <label>
                                                    <asp:CheckBoxList ID="cblNomTypeList" runat="server">
                                                        <asp:ListItem Value="M">Medical Beneficiary</asp:ListItem>
                                                        <asp:ListItem Value="D">Death Beneficiary</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="txtShare" class="col-sm-4 control-label">
                                                Share (%)                                                
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox runat="server" ID="txtShare" CssClass="form-control" placeholder="% of Share" MaxLength="5"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" MaximumValue="100" MinimumValue="0" ErrorMessage="Out of Range!" ControlToValidate="txtShare" Type="Double" class="text-danger validator"></asp:RangeValidator>
                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers, Custom"
                                                    ValidChars="." TargetControlID="txtShare"  />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box-footer">
                        <div class="col-md-offset-2">
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" class="btn btn-success col-md-3 btn-custom" CausesValidation="false" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" class="btn btn-primary col-md-3 btn-custom" />
                            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" class="btn btn-warning col-md-3 btn-custom" CausesValidation="false" />
                        </div>
                    </div>
                </div>
                <div role="tabpanel" class="tab-pane fade" id="list">
                    <div class="form-horizontal custom-form">
                        <div class="box-body">
                            <!-- /.box-header -->
                            <div class="box-body table-responsive no-padding">
                                <asp:GridView ID="grList" runat="server" Width="100%" EmptyDataText="No Record Found"
                                    AutoGenerateColumns="False" DataKeyNames="EmpID,NomineeId,NomineeName,RelationId,Gender,BirthDate,IsMedical,IsDeath,Share,Address,Remarks"
                                    class="table table-bordered table-hover" OnRowCommand="grList_RowCommand">

                                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True"></SelectedRowStyle>
                                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:BoundField DataField="NomineeName" HeaderText="Nominee">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RelationName" HeaderText="Relation">
                                            <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BirthDate" HeaderText="Birth Date">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Gender" HeaderText="Gender">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IsMedical" HeaderText="Is Medical">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IsDeath" HeaderText="Is Death">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Share" HeaderText="Share">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Address" HeaderText="Address">
                                            <ItemStyle CssClass="ItemStylecss" Width="40%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                            <ItemStyle CssClass="ItemStylecss" Width="40%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:ButtonField CommandName="EditClick" HeaderText="EDIT" ControlStyle-CssClass="fa fa-pencil-square-o text-primary text-center" HeaderStyle-CssClass="text-center">
                                            <ItemStyle CssClass="ItemStylecss text-center" Width="5%"></ItemStyle>
                                        </asp:ButtonField>
                                        <asp:ButtonField CommandName="DeleteClick" HeaderText="DELETE" ControlStyle-CssClass="fa fa-trash text-danger text-center" ItemStyle-CssClass="deleteLink" HeaderStyle-CssClass="text-center">
                                            <ItemStyle CssClass="deleteLink text-center" Width="5%"></ItemStyle>
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

