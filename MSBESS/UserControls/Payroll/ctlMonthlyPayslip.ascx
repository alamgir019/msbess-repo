<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlMonthlyPayslip.ascx.cs" Inherits="WebAdmin.UserControls.Payroll.ctlMonthlyPayslip" %>
<div class="row">
    <div class="col-md-12">
        <div class="row">
            <div class="col-sm-3">
                <div class="form-group">
                    <label for="ddlMonth" class="col-sm-4 control-label">Month:</label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="ddlMonth" runat="server" class="form-control" ClientIDMode="Static"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-group">
                    <label for="ddlYear" class="col-sm-4 control-label">Year:</label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="ddlYear" runat="server" class="form-control" ClientIDMode="Static"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <asp:RadioButtonList ID="rdbSalaryType" runat="server"
                    RepeatDirection="Horizontal" AutoPostBack="false">
                    <asp:ListItem Selected="True" Value="S">Salary</asp:ListItem>
                    <asp:ListItem Value="B">Only Bonus</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="col-sm-3">
                <asp:Button runat="server" ID="btnShow" Text="Show Payslip" OnClick="btnShow_Click" CssClass="btn btn-primary" />
            </div>
        </div>
        <!-- Horizontal Form -->
        <div class="box box-primary" style="margin-top: 10px">
            <div class="box-header with-border">
                <div class="col-sm-12">
                    <div class="col-sm-6">
                        <h3 class="box-title">Monthly Payslip</h3>
                    </div>
                    <div class="col-sm-6">
                        <div class="col-sm-offset-11 ">
                            <asp:LinkButton ID="btnPayslipPrint" runat="server" class="fa fa-print fa-2x" aria-hidden="true" CausesValidation="false" OnClientClick="printDiv('divPrintArea');" />
                        </div>
                    </div>
                </div>
            </div>
            <%--<div class="row" id="divPrintArea">--%>
            <div class="box box-body" id="divPrintArea">
                <div class="row" style="margin-left:5px;">
                    <div class="col-xs-6 col-sm-2">
                        <img src="../Content/images/MSB-Logo.jpg" class="img-responsive" />
                    </div>
                  
                </div>
                <div class="col-sm-12">
                    <h4>
                        <asp:Label ID="lblMonth" runat="server" class="control-label" ClientIDMode="Static"></asp:Label></h4>
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label for="lblEmpID" class="col-sm-4 control-label">Employee ID: </label>
                            <div class="col-sm-8">
                                <asp:Label ID="lblEmpId" runat="server" class="control-label" ClientIDMode="Static"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label for="lblFullName" class="col-sm-4 control-label">Employee Name: </label>
                            <div class="col-sm-8">
                                <asp:Label ID="lblFULLNAME" runat="server" class="control-label" ClientIDMode="Static"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label for="lblGradeName" class="col-sm-4 control-label">Grade: </label>
                            <div class="col-sm-8">
                                <asp:Label ID="lblGradeName" runat="server" class="control-label" ClientIDMode="Static"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label for="JobTitleName" class="col-sm-4 control-label">Job Title: </label>
                            <div class="col-sm-8">
                                <asp:Label ID="lblJobTitleName" runat="server" class="control-label" ClientIDMode="Static"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label for="lblBankAccNo" class="col-sm-4 control-label">Bank Account No: </label>
                            <div class="col-sm-8">
                                <asp:Label ID="lblBankAccNo" runat="server" class="control-label" ClientIDMode="Static"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label for="lblDesigName" class="col-sm-4 control-label">Designation: </label>
                            <div class="col-sm-8">
                                <asp:Label ID="lblDesigName" runat="server" class="control-label" ClientIDMode="Static"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label for="lblDivisionName" class="col-sm-4 control-label">Division Name: </label>
                            <div class="col-sm-8">
                                <asp:Label ID="lblDivisionName" runat="server" class="control-label" ClientIDMode="Static"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label for="lblDeptName" class="col-sm-4 control-label">Department Name: </label>
                            <div class="col-sm-8">
                                <asp:Label ID="lblDeptName" runat="server" class="control-label" ClientIDMode="Static"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label for="lblProjectName" class="col-sm-4 control-label">Project Name: </label>
                            <div class="col-sm-8">
                                <asp:Label ID="lblProjectName" runat="server" class="control-label" ClientIDMode="Static"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label for="lblSalLocName" class="col-sm-4 control-label">Salary Location: </label>
                            <div class="col-sm-8">
                                <asp:Label ID="lblSalLocName" runat="server" class="control-label" ClientIDMode="Static"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>

                <%--grGrossandBenefits--%>
                <div class="box-body table-responsive no-padding">
                    <asp:GridView ID="grGrossandBenefits" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False"
                        DataKeyNames="HTYPE"
                        class="table table-bordered table-hover"
                        PageSize="10">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" Font-Size="Small"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" Font-Size="Small"></SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB" Font-Size="Small"></AlternatingRowStyle>
                        <RowStyle Font-Size="Small" />

                        <Columns>
                            <asp:BoundField DataField="HeadName" HeaderText="Salary Items">
                                <ItemStyle Width="40%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PAYAMT" HeaderText="Taka">
                                <ItemStyle Width="35%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <%--grDeduct--%>
                <div class="box-body table-responsive no-padding">
                    <asp:GridView ID="grDeduct" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False"
                        DataKeyNames="HTYPE"
                        class="table table-bordered table-hover"
                        PageSize="10">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" Font-Size="Small"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" Font-Size="Small"></SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB" Font-Size="Small"></AlternatingRowStyle>
                        <RowStyle Font-Size="Small" />

                        <Columns>
                            <asp:BoundField DataField="HeadName" HeaderText="Deduction(s)">
                                <ItemStyle Width="40%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PAYAMT" HeaderText="Taka">
                                <ItemStyle Width="35%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <%--grNetPay--%>
                <div class="box-body table-responsive no-padding">
                    <asp:GridView ID="grNetPay" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                        AutoGenerateColumns="False"
                        class="table table-bordered table-hover"
                        PageSize="10">
                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" Font-Size="Small"></HeaderStyle>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" Font-Size="Small"></SelectedRowStyle>
                        <AlternatingRowStyle BackColor="#EFF3FB" Font-Size="Small"></AlternatingRowStyle>
                        <RowStyle Font-Size="Small" />

                        <Columns>
                            <asp:BoundField DataField="HeadName" HeaderText="Deduction(s)">
                                <ItemStyle Width="40%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PAYAMT" HeaderText="Taka">
                                <ItemStyle Width="35%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-sm-12">
                    <asp:Label ID="lblRemarks" runat="server" class="control-label" ClientIDMode="Static"></asp:Label>
                </div>
                <div class="col-sm-12">
                    <asp:Label ID="lblTakaInWord" runat="server" class="control-label" ClientIDMode="Static"></asp:Label>
                </div>
                <div class="col-sm-12">
                    <h5>Note: This is a computer generated payslip and does not require any signature. If
                    any discrepancy is found please inform HR Team within 7 days of the issuance.</h5>
                </div>
            </div>
            <%--</div>--%>
        </div>
    </div>
</div>

