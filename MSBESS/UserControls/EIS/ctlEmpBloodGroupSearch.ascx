<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlEmpBloodGroupSearch.ascx.cs" Inherits="WebAdmin.UserControls.EIS.ctlEmpBloodGroupSearch" %>
<div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Blood Group Information</h3>
            </div>
            <div class="form-horizontal">
                <div class="box-body">
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="txtEmpId" class="col-sm-4 control-label">Employee ID</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtEmpId" runat="server" class="form-control" placeholder="Employee ID"></asp:TextBox>
                                    <%--<div class="input-group-addon">
                                            <asp:LinkButton ID="btnEmpSearch" runat="server" OnClick="btnEmpSearch_Click" class="fa fa-search" aria-hidden="true" CausesValidation="false" />
                                        </div>--%>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="ddlRelation" class="col-sm-4 control-label">Posting District</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlDistrict" runat="server"
                                        CausesValidation="True"
                                        class="form-control"
                                        placeholder="Relation">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="ddlEmpStatus" class="col-sm-4 control-label">Employee Status</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlEmpStatus" runat="server"
                                        CausesValidation="True"
                                        class="form-control"
                                        placeholder="Employee Status">
                                        <asp:ListItem Value="A">Active</asp:ListItem>
                                        <asp:ListItem Value="I">In Active</asp:ListItem>
                                        <asp:ListItem Value="M">All</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="ddlBloodGroup" class="col-sm-4 control-label">Blood Group</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlBloodGroup" runat="server"
                                        CausesValidation="True"
                                        class="form-control"
                                        placeholder="Blood Group">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="ddlEmpType" class="col-sm-4 control-label">Employee Type</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlEmpType" runat="server"
                                        CausesValidation="True"
                                        class="form-control"
                                        placeholder="Employee Type">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="col-sm-offset-2">
                                <asp:Button runat="server" Text="Show" class="btn btn-primary col-md-3 btn-custom" Style="margin-top: auto !important"  OnClick="btnShow_Click"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-horizontal custom-form">
                <div class="box-body">
                    <!-- /.box-header -->
                    <div class="box-body table-responsive no-padding">
                        <asp:GridView ID="grBloodGroup" runat="server" Width="100%" EmptyDataText="No Record Found"
                            AutoGenerateColumns="False" DataKeyNames=""
                            class="table table-bordered table-hover">
                            <HeaderStyle BackColor="#B3CDE4" Font-Bold="True"></HeaderStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True"></SelectedRowStyle>
                            <AlternatingRowStyle BackColor="#EFF3FB"></AlternatingRowStyle>
                            <Columns>
                                  <asp:BoundField DataField="EmpId" HeaderText="EMPLOYEE ID">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FullName" HeaderText="EMPLOYEE NAME">
                                    <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DesigName" HeaderText="DESIGNATION">
                                    <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="JoiningDate" HeaderText="JOINING DATE">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SectorName" HeaderText="SECTOR">
                                    <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PostingPlaceName" HeaderText="POSTING PLACE">
                                    <ItemStyle CssClass="ItemStylecss" Width="15%"></ItemStyle>
                                </asp:BoundField> 
                                <asp:BoundField DataField="BloodGroupName" HeaderText="BLOOD GROUP">
                                    <ItemStyle CssClass="ItemStylecss" Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CellPhone" HeaderText="CONTACT NO">
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
