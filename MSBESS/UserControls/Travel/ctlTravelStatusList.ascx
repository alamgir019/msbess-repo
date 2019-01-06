<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlTravelStatusList.ascx.cs" Inherits="WebAdmin.UserControls.Travel.ctlTravelStatusList" %>
<div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Travel Status</h3>
            </div>
            <div class="form-horizontal">
              <div class="row ">
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="box-body">
                                <div class="box-body table-responsive no-padding">
                                    <asp:GridView ID="grTravelStatusList" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                                        AutoGenerateColumns="False"
                                        DataKeyNames="TravelId,TravelModeTitle,TravelModeID,AppDate,DepartureDate,ReturnDate,TotalDays,InsertedBy,Purpose,TravelInstruction,EmpId,Fullname,SupervisorId,TravelStatus,AppStatusTitle"
                                        class="table table-bordered table-hover" 
                                        OnRowCommand="grTravelStatusList_RowCommand"
                                        PageSize="10">
                                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" Font-Size="Small"></HeaderStyle>
                                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" Font-Size="Small"></SelectedRowStyle>
                                        <AlternatingRowStyle BackColor="#EFF3FB" Font-Size="Small"></AlternatingRowStyle>
                                        <RowStyle Font-Size="Small" />
                                        <Columns>
                                            <asp:BoundField HeaderText="SL">
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EmpId" HeaderText="Applicant">
                                                <ItemStyle Width="35%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DepartureDate" HeaderText="Travel Duration">
                                                <ItemStyle Width="30%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AppStatusTitle" HeaderText="Application Status">
                                                <ItemStyle Width="15%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:ButtonField CommandName="ViewClick" HeaderText="View" ControlStyle-CssClass="fa fa-list-alt text-primary text-center" HeaderStyle-CssClass="text-center">
                                                <ItemStyle CssClass="ItemStylecss text-center"></ItemStyle>
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
</div>