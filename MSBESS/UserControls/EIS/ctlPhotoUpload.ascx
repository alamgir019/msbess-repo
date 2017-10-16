<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctlPhotoUpload.ascx.cs" Inherits="WebAdmin.UserControls.EIS.ctlPhotoUpload" %>


<script type="text/javascript">

    function uploadComplete(sender) {
        $.notify("Photo Uploaded Successfully! Please Click Update Button to Save the Photo!", "success");
    }



    function uploadError(sender) {
        $.notify("File upload failed! ", "error");
    }

</script>

<div class="row">
    <div class="col-md-6">
        <div class="box box-primary">
            <div class="box-header with-border">
                <div class="box-title">
                    Photo Upload/Change
                </div>
            </div>
            <div class="form">
                <div class="box-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <label for="txtEmpId">
                                    Employee ID
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmpId">
                                     <sup class="fa-validate">*</sup>
                              </asp:RequiredFieldValidator>
                                </label>
                                <div class=" input-group">
                                    <asp:TextBox ID="txtEmpId" runat="server" class="form-control" placeholder="Employee ID" ClientIDMode="Static"></asp:TextBox>
                                    <div class="input-group-addon">
                                        <asp:LinkButton ID="btnEmpSearch" OnClick="btnEmpSearch_Click" runat="server" class="fa fa-search" aria-hidden="true" CausesValidation="false" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="txtFullName">Employee Name</label>
                                <asp:TextBox ID="txtFullName" runat="server" class="form-control" placeholder="Employee Name" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-8">
                            <div class="form-group">
                                <asp:Label runat="server" ID="myThrobber" Style="display: none;">
                                    <img alt="" src="../Content/images/uploading.gif" class="img-responsive" height="40" width="40"/></asp:Label>
                             <%--   <ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload1" runat="server"
                                    OnUploadComplete="AjaxFileUpload1_UploadComplete" ThrobberID="myThrobber" MaximumNumberOfFiles="1" AllowedFileTypes="jpg,jpeg,png" />--%>
                                <label class="btn btn-default btn-file">
                                    Photo Upload 
                                    <ajaxToolkit:AsyncFileUpload OnClientUploadError="uploadError"
                                        OnClientUploadComplete="uploadComplete" runat="server" OnUploadedComplete="AsyncFileUpload1_OnUploadedComplete"
                                        ID="AsyncFileUpload1" UploaderStyle="Traditional"
                                        UploadingBackColor="#CCFFFF" ThrobberID="myThrobber" CssClass="" />
                                </label>

                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group text-center">
                                <label for="imgEmpImage">PHOTO</label>
                                <asp:Image ID="imgEmpImage" runat="server" class="img-responsive" Height="150px" Width="150px" ImageUrl="~/Content/images/NoImageSmall.jpg" ClientIDMode="Static" />
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
