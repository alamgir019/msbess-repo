using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Threading;
using WebAdmin.BLL;
using System.Data;
using WebAdmin.App_Data;
using System.IO;
namespace WebAdmin
{
    public partial  class SiteMaster : MasterPage
    {
        LoginManager objLogin = new LoginManager();
        EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
        dsEmployee objDS = new dsEmployee();
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USERID"] == null)
                {
                    ShowClientMessage(Page, "Session Expired. You are redirected to Login Again!", "error");
                    Thread.Sleep(1000);
                    Response.Redirect("~/Login.aspx");
                }


                lblSideBarUserName.InnerText = HttpContext.Current.Session["USERID"].ToString();
                lblUserIDFullName.InnerText = HttpContext.Current.Session["USERNAME"].ToString();
                //imgUserAccount.Src = HttpContext.Current.Session["PHOTO"].ToString();
                //imgUserSideBar.Src= HttpContext.Current.Session["PHOTO"].ToString();
                //imgUserAccountDown.Src= HttpContext.Current.Session["PHOTO"].ToString();
                DataTable dtEmpInfo = objEmpInfoMgr.SelectEmpInfo(HttpContext.Current.Session["EMPID"].ToString());
                if (dtEmpInfo.Rows.Count > 0)
                {
                    if (Common.CheckNullString(dtEmpInfo.Rows[0]["EmpImage"].ToString().Trim()) != "")
                    {
                        MemoryStream ms = new MemoryStream((byte[])dtEmpInfo.Rows[0]["EmpImage"]);
                        Byte[] imgByte = ms.ToArray();
                        string base64String = Convert.ToBase64String(imgByte, 0, imgByte.Length);
                        imgEmpImage.ImageUrl = "data:image/png;base64," + base64String;
                        imgUserAccountDown.ImageUrl = "data:image/png;base64," + base64String;
                        //imgUserAccount.ImageUrl = "data:image/png;base64," + base64String;
                    }
                    else
                    {
                        imgEmpImage.ImageUrl = "~/Content/images/NoImageSmall.jpg";
                        imgUserAccountDown.ImageUrl = "~/Content/images/NoImageSmall.jpg";
                        //imgUserAccount.ImageUrl = "~/Content/images/NoImageSmall.jpg";
                    }
                }
                else
                {
                    imgEmpImage.ImageUrl = "~/Content/images/NoImageSmall.jpg";
                    imgUserAccountDown.ImageUrl = "~/Content/images/NoImageSmall.jpg";
                    //imgUserAccount.ImageUrl = "~/Content/images/NoImageSmall.jpg";
                }


                lblFullName.InnerText = HttpContext.Current.Session["USERNAME"].ToString();
                lblDesig.InnerText = HttpContext.Current.Session["DESIGNATION"].ToString();
                lblJoinDate.InnerText = "Employee Since " + HttpContext.Current.Session["JOINDATE"].ToString();
                //Session["USERID"] = "Admin";
                //Session["FISCALYRID"] = "1";

                // For leave menu item check
                if (Session["ISADMIN"].ToString().Trim() == "N")
                {
                    DataTable dtSupervisee = objEmpInfoMgr.GetSuperVisiorWiseEmp(Session["EMPID"].ToString().Trim(), Session["DIVISIONID"].ToString().Trim(), objDS.dtEmpList);
                    if (dtSupervisee.Rows.Count <= 1)
                    {
                        liLeaveRecommendation.Visible = false;
                        liLeaveApplicationList.Visible = false;
                        liRegrettedLeaveList.Visible = false;
                        liApprovedLeaveList.Visible = false;
                    }
                }
                else
                {
                    liLeaveRecommendation.Visible = false;
                    liLeaveApplicationList.Visible = false;
                    liRegrettedLeaveList.Visible = false;
                    liApprovedLeaveList.Visible = false;
                }
            }
                Thread.Sleep(200);
                ScriptManager.RegisterStartupScript(this.UpdatePanelMaster, typeof(string), "ValidatorUpdateDisplay", this.GetValidatationScript(), true);
                ScriptManager.RegisterStartupScript(this.UpdatePanelMaster, typeof(string), "deleteLink", this.getDeleteConfirmScript(), true);
                ScriptManager.RegisterStartupScript(this.UpdatePanelMaster, typeof(string), "datepicker", this.getPageOnLoadScript(), true);
                ScriptManager.RegisterStartupScript(this.UpdatePanelMaster, typeof(string), "iCheck", this.getiCheckScript(), true);          
        }
        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        protected string GetValidatationScript()
        {
            string strScript = @"function ValidatorUpdateDisplay(val) {
                if (typeof (val.display) == 'string') {
                    if (val.display == 'None')
                    {
                        return;
                    }
                    if (val.display == 'Dynamic')
                    {
                        val.style.display = val.isvalid ? 'none' : 'inline';
                        return;
                    }
                }
                val.style.visibility = val.isvalid? 'hidden' : 'visible';
                        if (val.isvalid) {
                            $('#'+ val.controltovalidate).removeClass('required');
                        }
                        else {
                            $('#'+ val.controltovalidate).addClass('required');
                        }
                    }";
            return strScript;
        }

        protected string getDeleteConfirmScript()
        {
            string strScript = @" $('.deleteLink').click(function () {
                return confirm('Are you sure you wish to delete this record?');
            });  ";

            return strScript;
        }

        protected string getPageOnLoadScript()
        {
            string strScript = @" $(function () {
                //Date picker
                $('.datepicker').datepicker({
                    autoclose: true,
                    format: 'dd/mm/yyyy'
                });
                $('.validator').on('show')
            });";

            return strScript;
        }
        protected string getiCheckScript()
        {
            string strScript = @"  $(function()
                {
                    $('input').iCheck({
                    checkboxClass: 'icheckbox_square-blue',
                        radioClass: 'iradio_square-blue',
                        increaseArea: '20%' // optional
                    });
                });";

            return strScript;
        }
       

        //protected string getToastScript()
        //{
        //    string strScript = @"!function(e){e(['jquery'],function(e){return function(){function t(e,t,n){return g({type:O.error,iconClass:m().iconClasses.error,message:e,optionsOverride:n,title:t})}function n(t,n){return t||(t=m()),v=e('#'+t.containerId),v.length?v:(n&&(v=d(t)),v)}function o(e,t,n){return g({type:O.info,iconClass:m().iconClasses.info,message:e,optionsOverride:n,title:t})}function s(e){C=e}function i(e,t,n){return g({type:O.success,iconClass:m().iconClasses.success,message:e,optionsOverride:n,title:t})}function a(e,t,n){return g({type:O.warning,iconClass:m().iconClasses.warning,message:e,optionsOverride:n,title:t})}function r(e,t){var o=m();v||n(o),u(e,o,t)||l(o)}function c(t){var o=m();return v||n(o),t&&0===e(':focus',t).length?void h(t):void(v.children().length&&v.remove())}function l(t){for(var n=v.children(),o=n.length-1;o>=0;o--)u(e(n[o]),t)}function u(t,n,o){var s=!(!o||!o.force)&&o.force;return!(!t||!s&&0!==e(':focus',t).length)&&(t[n.hideMethod]({duration:n.hideDuration,easing:n.hideEasing,complete:function(){h(t)}}),!0)}function d(t){return v=e('<div/>').attr('id',t.containerId).addClass(t.positionClass),v.appendTo(e(t.target)),v}function p(){return{tapToDismiss:!0,toastClass:'toast',containerId:'toast-container',debug:!1,showMethod:'fadeIn',showDuration:300,showEasing:'swing',onShown:void 0,hideMethod:'fadeOut',hideDuration:1e3,hideEasing:'swing',onHidden:void 0,closeMethod:!1,closeDuration:!1,closeEasing:!1,closeOnHover:!0,extendedTimeOut:1e3,iconClasses:{error:'toast-error',info:'toast-info',success:'toast-success',warning:'toast-warning'},iconClass:'toast-info',positionClass:'toast-top-right',timeOut:5e3,titleClass:'toast-title',messageClass:'toast-message',escapeHtml:!1,target:'body',closeHtml:'<button type='button'>&times;</button>',closeClass:'toast-close-button',newestOnTop:!0,preventDuplicates:!1,progressBar:!1,progressClass:'toast-progress',rtl:!1}}function f(e){C&&C(e)}function g(t){function o(e){return null==e&&(e=''),e.replace(/&/g,'&amp;').replace(/'/g,'&quot;').replace(/'/g,'&#39;').replace(/</g,'&lt;').replace(/>/g,'&gt;')}function s(){c(),u(),d(),p(),g(),C(),l(),i()}function i(){var e='';switch(t.iconClass){case'toast-success':case'toast-info':e='polite';break;default:e='assertive'}I.attr('aria-live',e)}function a(){E.closeOnHover&&I.hover(H,D),!E.onclick&&E.tapToDismiss&&I.click(b),E.closeButton&&j&&j.click(function(e){e.stopPropagation?e.stopPropagation():void 0!==e.cancelBubble&&e.cancelBubble!==!0&&(e.cancelBubble=!0),E.onCloseClick&&E.onCloseClick(e),b(!0)}),E.onclick&&I.click(function(e){E.onclick(e),b()})}function r(){I.hide(),I[E.showMethod]({duration:E.showDuration,easing:E.showEasing,complete:E.onShown}),E.timeOut>0&&(k=setTimeout(b,E.timeOut),F.maxHideTime=parseFloat(E.timeOut),F.hideEta=(new Date).getTime()+F.maxHideTime,E.progressBar&&(F.intervalId=setInterval(x,10)))}function c(){t.iconClass&&I.addClass(E.toastClass).addClass(y)}function l(){E.newestOnTop?v.prepend(I):v.append(I)}function u(){if(t.title){var e=t.title;E.escapeHtml&&(e=o(t.title)),M.append(e).addClass(E.titleClass),I.append(M)}}function d(){if(t.message){var e=t.message;E.escapeHtml&&(e=o(t.message)),B.append(e).addClass(E.messageClass),I.append(B)}}function p(){E.closeButton&&(j.addClass(E.closeClass).attr('role','button'),I.prepend(j))}function g(){E.progressBar&&(q.addClass(E.progressClass),I.prepend(q))}function C(){E.rtl&&I.addClass('rtl')}function O(e,t){if(e.preventDuplicates){if(t.message===w)return!0;w=t.message}return!1}function b(t){var n=t&&E.closeMethod!==!1?E.closeMethod:E.hideMethod,o=t&&E.closeDuration!==!1?E.closeDuration:E.hideDuration,s=t&&E.closeEasing!==!1?E.closeEasing:E.hideEasing;if(!e(':focus',I).length||t)return clearTimeout(F.intervalId),I[n]({duration:o,easing:s,complete:function(){h(I),clearTimeout(k),E.onHidden&&'hidden'!==P.state&&E.onHidden(),P.state='hidden',P.endTime=new Date,f(P)}})}function D(){(E.timeOut>0||E.extendedTimeOut>0)&&(k=setTimeout(b,E.extendedTimeOut),F.maxHideTime=parseFloat(E.extendedTimeOut),F.hideEta=(new Date).getTime()+F.maxHideTime)}function H(){clearTimeout(k),F.hideEta=0,I.stop(!0,!0)[E.showMethod]({duration:E.showDuration,easing:E.showEasing})}function x(){var e=(F.hideEta-(new Date).getTime())/F.maxHideTime*100;q.width(e+'%')}var E=m(),y=t.iconClass||E.iconClass;if('undefined'!=typeof t.optionsOverride&&(E=e.extend(E,t.optionsOverride),y=t.optionsOverride.iconClass||y),!O(E,t)){T++,v=n(E,!0);var k=null,I=e('<div/>'),M=e('<div/>'),B=e('<div/>'),q=e('<div/>'),j=e(E.closeHtml),F={intervalId:null,hideEta:null,maxHideTime:null},P={toastId:T,state:'visible',startTime:new Date,options:E,map:t};return s(),r(),a(),f(P),E.debug&&console&&console.log(P),I}}function m(){return e.extend({},p(),b.options)}function h(e){v||(v=n()),e.is(':visible')||(e.remove(),e=null,0===v.children().length&&(v.remove(),w=void 0))}var v,C,w,T=0,O={error:'error',info:'info',success:'success',warning:'warning'},b={clear:r,remove:c,error:t,getContainer:n,info:o,options:{},subscribe:s,success:i,version:'2.1.3',warning:a};return b}()})}('function'==typeof define&&define.amd?define:function(e,t){'undefined'!=typeof module&&module.exports?module.exports=t(require('jquery')):window.toastr=t(window.jQuery)});";
        //    return strScript;
        //}
       
        public static void ShowClientMessage(Page page,string sMsg,string msgType)
        {
            ScriptManager.RegisterClientScriptBlock(page, typeof(string), Guid.NewGuid().ToString(), " $.notify('" + sMsg + "','"+msgType+"');", true);
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(Session["USERID"].ToString().Trim()) == false)
            //    objLogin.InsertUserInOutHistory(Session["LOGINID"].ToString(), Session["USERID"].ToString().Trim(), Common.ReturnDateTimeInString(DateTime.Now.ToString(), true, Constant.strDateFormat),
            //                      Common.ReturnDateTimeInString(DateTime.Now.ToString(), true, Constant.strDateFormat), "S", "Y");

            Response.Cookies.Clear();
            Session["USERID"] = "";
            Session["USERNAME"] = "";
            Session["EMPID"] = "";
            Session["EMAILID"] = "";
            Session["COUNTRYDIRECTOR"] = "";
            Session["OFFICE"] = "";
            Session["PROGRAM"] = "";
            Session["OFFICEID"] = "";
            Session["PROGRAMID"] = "";
            Session["TEAM"] = "";
            Session["TEAMID"] = "";
            Session["EMPLOYEEID"] = "";
            Session["ISADMIN"] = "";
            Session["ISSHIFTINCHR"] = "";
            Session["DESIGNATION"] = "";
            Session["LOCATION"] = "";
            Session["FISCALYRID"] = "";
            Response.Redirect("~/Login.aspx");

            
        }
    }

}