<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlTravelToApprove.ascx.cs" Inherits="WebAdmin.UserControls.Travel.ctlTravelToApprove" %>
<div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Travel Approve List</h3>
            </div>
            <div class="form-horizontal">                
                <div class="row ">
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="box-body">
                                <div class="box-body table-responsive no-padding">
                                    <asp:GridView
                                        ID="grTravelList" runat="server" AutoGenerateColumns="False" Width="100%"
                                        DataKeyNames="TravelId,TravelModeTitle,TravelModeID,AppDate,DepartureDate,ReturnDate,TotalDays,InsertedBy,Purpose,TravelInstruction,EmpId,Fullname,SupervisorId,TravelStatus,AppStatusTitle"
                                        EmptyDataText="No Record Found" class="table table-bordered table-hover" OnRowCommand="grTravelApp_RowCommand" PageSize="10">
                                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" Font-Size="Small"></HeaderStyle>
                                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" Font-Size="Small"></SelectedRowStyle>
                                        <AlternatingRowStyle BackColor="#EFF3FB" Font-Size="Small"></AlternatingRowStyle>
                                        <RowStyle Font-Size="Small" />
                                        <Columns>
                                            <asp:BoundField HeaderText="SL">
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EmpId" HeaderText="APPLICANT">
                                                <ItemStyle Width="40%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DepartureDate" HeaderText="TRAVEL DURATION">
                                                <ItemStyle Width="35%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:ButtonField CommandName="ViewClick" HeaderText="VIEW" ControlStyle-CssClass="fa fa-list-alt text-primary text-center" HeaderStyle-CssClass="text-center">
                                                <ItemStyle CssClass="ItemStylecss text-center"></ItemStyle>
                                            </asp:ButtonField>
                                            <asp:ButtonField CommandName="ApproveClick" HeaderText="APPROVE" ControlStyle-CssClass="fa fa-check-square text-success text-center" HeaderStyle-CssClass="text-center">
                                                <ItemStyle CssClass="ItemStylecss text-center"></ItemStyle>
                                            </asp:ButtonField>
                                            <%--<asp:ButtonField CommandName="DenyClick" HeaderText="REGRET" ControlStyle-CssClass="fa fa-minus-square text-warning text-center" HeaderStyle-CssClass="text-center">
                                                <ItemStyle CssClass="ItemStylecss text-center"></ItemStyle>
                                            </asp:ButtonField>
                                            <asp:ButtonField CommandName="CancelClick" HeaderText="CANCEL" ControlStyle-CssClass="fa fa-trash text-danger text-center" ItemStyle-CssClass="deleteLink" HeaderStyle-CssClass="text-center">
                                                <ItemStyle CssClass="cancelLink text-center"></ItemStyle>
                                            </asp:ButtonField>--%>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:HiddenField runat="server" ID="hfLDates" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
