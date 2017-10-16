<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebAdmin.Login" %>

<!DOCTYPE html>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>MSBESS | Log in</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="Content/plugins/bootstrap/css/bootstrap.min.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="Content/plugins/font-awesome/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="Content/plugins/ionicons/css/ionicons.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="Content/dist/css/AdminLTE.min.css">
    <!-- iCheck -->
    <link rel="stylesheet" href="Content/plugins/iCheck/square/blue.css">
    <link href="Content/Site.css" rel="stylesheet" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]><![endif]-->
    <script src="Scripts/html5shiv.min.js"></script>
    <script src="Scripts/respond.min.js"></script>
    <script src="Content/plugins/jQuery/jquery-2.2.3.min.js"></script>
    <script src="Content/plugins/notify/notify.min.js"></script>
</head>
<body class="hold-transition login-page">
    <div class="login-box">
        <div class="login-logo">
            <a href="#">
                <img src="Content/images/MSB-Logo.png" class="img-responsive" style="margin: 0 auto;" height="80" width="80" /></a>
        </div>
        <!-- /.login-logo -->
        <div class="box login-box-body bg-success">
            <div class="login-logo">
                <a href="#"><b>MSB</b>ESS</a>
            </div>
            <p class="login-box-msg">Sign in to start your session</p>

            <form runat="server">
                <div class="form-group has-feedback">
                    <asp:TextBox ID="txtUserID" runat="server" placeholder="User ID" class="form-control"></asp:TextBox>
                    <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" TextMode="Password" class="form-control"></asp:TextBox>
                    <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                </div>
                <div class="row">
                    <div class="col-xs-8">
                        <%-- <div class="checkbox icheck">
                            <label>
                                <input type="checkbox">
                                Remember Me
                            </label>
                        </div>--%>
                    </div>
                    <!-- /.col -->
                    <div class="col-xs-4">
                        <asp:Button ID="btnLogin" runat="server" Text="Sign In" class="btn btn-primary btn-block btn-flat" OnClick="btnLogin_Click" />
                    </div>
                    <!-- /.col -->
                </div>
            </form>

            <%-- <a href="#">I forgot my password</a><br>--%>
            <br>

            <footer class="login-footer">
                <div class="pull-right hidden-xs">
                    <b>Version</b> 1.0.0
                </div>
                <strong>Copyright &copy; <a href="http://baseltd.com">BASE Limited</a>.</strong> All rights reserved.
            </footer>
        </div>
        <!-- /.login-box-body -->
    </div>
    <!-- /.login-box -->

    <!-- jQuery 2.2.3 -->
    <%--<script src="Content/plugins/jQuery/jquery-2.2.3.min.js"></script>--%>
    <!-- Bootstrap 3.3.6 -->
    <script src="Content/plugins/bootstrap/js/bootstrap.min.js"></script>
    <!-- iCheck -->
    <script src="Content/plugins/iCheck/icheck.min.js"></script>
    <!-- Notify -->
    <%--<script src="Content/plugins/notify/notify.min.js"></script>--%>

    <script>
        $(function () {
            $('input').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue',
                increaseArea: '20%' // optional
            });

        });
    </script>
</body>
</html>
