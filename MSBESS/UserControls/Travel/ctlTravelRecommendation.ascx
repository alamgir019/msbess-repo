﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlTravelRecommendation.ascx.cs" Inherits="WebAdmin.UserControls.Travel.ctlTravelRecommendation" %>
<div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Recommend Travel List</h3>
            </div>
            <div class="form-horizontal">
<asp:HiddenField runat="server" ID="hdfApproveBy" />
                <div class="row ">
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="box-body">
                                <div class="box-body table-responsive no-padding">

                                    <asp:GridView ID="grRecommendTravelList" runat="server" Width="100%" Font-Size="9px" EmptyDataText="No Record Found"
                                        class="table table-bordered table-hover"  AutoGenerateColumns="False"
                                        DataKeyNames="TravelId,TravelModeTitle,TravelModeID,AppDate,DepartureDate,ReturnDate,TotalDays,InsertedBy,Purpose,TravelInstruction,EmpId,Fullname,SupervisorId,TravelStatus,AppStatusTitle"
                                        OnRowCommand="grRecommendTravelList_RowCommand" PageSize="10">
                                        <HeaderStyle BackColor="#B3CDE4" Font-Bold="True" Font-Size="Small"></HeaderStyle>
                                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" Font-Size="Small"></SelectedRowStyle>
                                        <AlternatingRowStyle BackColor="#EFF3FB" Font-Size="Small"></AlternatingRowStyle>
                                        <RowStyle Font-Size="Small" />                                       
                                        <Columns>
                                            <asp:BoundField HeaderText="SL">
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EmpId" HeaderText="APPLICANT">
                                                <ItemStyle Width="20%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DepartureDate" HeaderText="TRAVEL DURATION">
                                                <ItemStyle Width="20%"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Approve By">
                                                <ItemTemplate>
                                                    <asp:TextBox  ID="txtApproveBy" DataValueField="Id" OnTextChanged="txtApproveBy_TextChanged" DataTextField="Approve By" runat="server" AutoPostBack=true></asp:TextBox>                                
                                                    <asp:TextBox ID="txtApproveByName" runat="server" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="ItemStylecss" Width="20%" />
                                            </asp:TemplateField>
                                            <asp:ButtonField CommandName="ViewClick" HeaderText="VIEW" ControlStyle-CssClass="fa fa-list-alt text-primary text-center" HeaderStyle-CssClass="text-center">
                                                <ItemStyle CssClass="ItemStylecss text-center"></ItemStyle>
                                            </asp:ButtonField>
                                            <asp:ButtonField CommandName="RecommendClick" HeaderText="Recommend" ControlStyle-CssClass="fa fa-check-square text-success text-center" HeaderStyle-CssClass="text-center">
                                                <ItemStyle CssClass="ItemStylecss text-center"></ItemStyle>
                                            </asp:ButtonField>
                                            <%--<asp:ButtonField CommandName="CancelClick" HeaderText="CANCEL" ControlStyle-CssClass="fa fa-trash text-danger text-center" ItemStyle-CssClass="deleteLink" HeaderStyle-CssClass="text-center">
                                                <ItemStyle CssClass="cancelLink text-center"></ItemStyle>
                                            </asp:ButtonField>--%>
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