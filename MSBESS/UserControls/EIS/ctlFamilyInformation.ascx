<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlFamilyInformation.ascx.cs" Inherits="WebAdmin.UserControls.EIS.ctlFamilyInformation" %>
<div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Family Information</h3>
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
                                            <div class=" input-group">
                                                <asp:TextBox ID="txtEmpId" runat="server" class="form-control" placeholder="Employee ID"></asp:TextBox>
                                                <div class="input-group-addon">
                                                    <asp:LinkButton ID="btnEmpSearch" runat="server" OnClick="btnEmpSearch_Click" class="fa fa-search" aria-hidden="true" CausesValidation="false" />
                                                </div>
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="hfID" runat="server" />
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="txtDepartment" class="col-sm-4 control-label">Department</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtDepartment" runat="server" class="form-control" placeholder="Department" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="txtEmpName" class="col-sm-4 control-label">Employee Name</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtEmpName" runat="server" class="form-control" placeholder="Employee Name" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="txtCompany" class="col-sm-4 control-label">Company</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtCompany" runat="server" class="form-control" placeholder="Company" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="txtProject" class="col-sm-4 control-label">Project</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtProject" runat="server" class="form-control" placeholder="Project" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label for="txtJobTitle" class="col-sm-4 control-label">Job Title</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtJobTitle" runat="server" class="form-control" placeholder="Job Title" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="box box-default bg-inner-form">
                                <div class="row row-custom-top">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="inputNomineeName" class="col-sm-4 control-label">
                                                Family Member Name
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName">
                                                             <sup class="fa-validate">*</sup>
                                                    </asp:RequiredFieldValidator>
                                            </label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtName" runat="server" class="form-control" placeholder="Name"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="inputNomineeAddress" class="col-sm-4 control-label">
                                                Address
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress">
                                                             <sup class="fa-validate">*</sup>
                                                       </asp:RequiredFieldValidator>

                                            </label>
                                            <div class="col-sm-8">

                                                <asp:TextBox ID="txtAddress" runat="server" class="form-control" placeholder="Address"></asp:TextBox>


                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="inputPhone" class="col-sm-4 control-label">Phone</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtPhone" runat="server" class="form-control" placeholder="Phone"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label for="inputRelation" class="col-sm-4 control-label">
                                                Relation
                                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="*" ControlToValidate="ddlRelation"
                                                        Operator="NotEqual" ValueToCompare="-1">
                                                         <sup class="fa-validate">*</sup>
                                                    </asp:CompareValidator>
                                            </label>
                                            <div class="col-sm-8">

                                                <asp:DropDownList ID="ddlRelation" runat="server"
                                                    CausesValidation="True"
                                                    class="form-control"
                                                    placeholder="Relation">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.body footer -->
                    <div class="box-footer">
                        <div class="col-md-offset-2">
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" class="btn btn-success col-md-3 btn-custom" OnClick="btnRefresh_Click" CausesValidation="false" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary col-md-3 btn-custom" OnClick="btnSave_Click" />
                            <asp:Button ID="btnClose" runat="server" Text="Close" class="btn btn-warning col-md-3 btn-custom" OnClick="btnClose_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>

                <div role="tabpanel" class="tab-pane fade" id="list">
                    <div class="form-horizontal custom-form">
                        <div class="box-body">
                            <!-- /.box-header -->
                            <div class="box-body table-responsive no-padding">
                                <asp:GridView ID="grList" runat="server" Width="100%" EmptyDataText="No Record Found"
                                    AutoGenerateColumns="False" DataKeyNames="EmpID,FmID,FmName,RelationID,RelationName,FmAddr,FmPhone"
                                    class="table table-bordered table-hover"
                                    OnRowCommand="grList_RowCommand">
                                    <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True"></SelectedRowStyle>
                                    <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                                    <Columns>

                                        <asp:BoundField DataField="SL" HeaderText="SL">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FmName" HeaderText="MEMBER NAME">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RelationName" HeaderText="RELATION">
                                            <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FmAddr" HeaderText="ADDRESS">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FmPhone" HeaderText="PHONE">
                                            <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
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


