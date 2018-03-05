<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlEmpFileUpload.ascx.cs" Inherits="WebAdmin.UserControls.EIS.ctlEmpFileUpload" %>
<div class="row">
    <div class="col-md-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">File Management</h3>
            </div>
            <!-- Nav tabs -->
            <ul class="nav nav-tabs nav-justified" role="tablist">
                <li role="presentation"><a href="#upload" aria-controls="upload" role="tab" data-toggle="tab">
                    <h4>Upload File</h4>
                </a></li>
                <li role="presentation" class="active" ><a href="#view" aria-controls="view" role="tab" data-toggle="tab">
                    <h4>View File</h4>
                </a></li>
            </ul>
            <!-- Tab panes -->
            <div class="tab-content">
                <div role="tabpanel" class="tab-pane fade" id="upload">
                    <div class="form-horizontal">
                        <asp:HiddenField ID="hfID" runat="server" />
                        <div class="box-body">
                            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
                                <ContentTemplate>
                                    <div class="row">                                
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="fluTin" class="col-sm-4 control-label">TIN Certificate</label>
                                                <div class="col-sm-8">
                                                    <asp:FileUpload ID="fluTin" class="form-control" Height="100%" runat="server" /> 
                                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fluTin"
                                                            ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="fluNid" class="col-sm-4 control-label">National ID</label>
                                                <div class="col-sm-8">
                                                    <asp:FileUpload ID="fluNid" class="form-control" Height="100%" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="fluNid"
                                                            ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="fluDriveLic" class="col-sm-4 control-label">Driving License</label>
                                                <div class="col-sm-8">
                                                    <asp:FileUpload ID="fluDriveLic" runat="server" class="form-control" Height="100%" /> 
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="fluDriveLic"
                                                            ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">                                
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label for="fluBmdc" class="col-sm-4 control-label">BMDC Certificate</label>
                                                <div class="col-sm-8">
                                                    <asp:FileUpload ID="fluBmdc" runat="server" class="form-control" Height="100%" />
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="fluBmdc"
                                                            ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="box-footer">
                                            <div class="col-md-offset-2">
                                                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" class="btn btn-success col-md-3 btn-custom" CausesValidation="false" OnClick="btnRefresh_Click" />
                                                <asp:Button ID="btnUpload" runat="server" Text="Upload" class="btn btn-primary col-md-3 btn-custom" OnClick="btnUpload_Click" />
                                                <asp:Button ID="btnClose" runat="server" Text="Close" class="btn btn-warning col-md-3 btn-custom" CausesValidation="false" OnClick="btnClose_Click" />
                                            </div>
                                        </div>    
                                </ContentTemplate>  
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnUpload" />
                                </Triggers>   
                            </asp:UpdatePanel>           
                        </div>
                    </div>
                </div>
                <div role="tabpanel" class="tab-pane fade in active" id="view">
                    <div class="form-horizontal custom-form">
                        <div class="box-body">
                            <!-- /.box-header -->
                            <div class="box-body table-responsive no-padding">
                                <div class="row">
                                <asp:TreeView ID="MyTree" PathSeparator="|" ExpandDepth="0" runat="server" ImageSet="Arrows"
                                    AutoGenerateDataBindings="False" OnSelectedNodeChanged="MyTree_SelectedNodeChanged">
                                    <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px"
                                        ForeColor="#5555DD"></SelectedNodeStyle>
                                    <NodeStyle VerticalPadding="0px" Font-Names="Tahoma" Font-Size="10pt" HorizontalPadding="5px"
                                        ForeColor="Black" NodeSpacing="0px"></NodeStyle>
                                    <ParentNodeStyle Font-Bold="False" />
                                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD"></HoverNodeStyle>
                                </asp:TreeView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
