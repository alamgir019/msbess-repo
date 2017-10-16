using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using WebAdmin.BLL;
namespace WebAdmin
{
    public partial class Empty : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USERID"] == null)
                {
                    //ShowClientMessage(Page, "Session Expired. You are redirected to Login Again!", "error");
                    Thread.Sleep(1000);
                    Response.Redirect("~/Login.aspx");
                }
            }
            //Thread.Sleep(200);
            //ScriptManager.RegisterStartupScript(this.UpdatePanelMaster, typeof(string), "ValidatorUpdateDisplay", this.GetValidatationScript(), true);
            //ScriptManager.RegisterStartupScript(this.UpdatePanelMaster, typeof(string), "deleteLink", this.getDeleteConfirmScript(), true);
            //ScriptManager.RegisterStartupScript(this.UpdatePanelMaster, typeof(string), "datepicker", this.getPageOnLoadScript(), true);
            //ScriptManager.RegisterStartupScript(this.UpdatePanelMaster, typeof(string), "iCheck", this.getiCheckScript(), true);
        }

        //protected string GetValidatationScript()
        //{
        //    string strScript = @"function ValidatorUpdateDisplay(val) {
        //        if (typeof (val.display) == 'string') {
        //            if (val.display == 'None')
        //            {
        //                return;
        //            }
        //            if (val.display == 'Dynamic')
        //            {
        //                val.style.display = val.isvalid ? 'none' : 'inline';
        //                return;
        //            }
        //        }
        //        val.style.visibility = val.isvalid? 'hidden' : 'visible';
        //                if (val.isvalid) {
        //                    $('#'+ val.controltovalidate).removeClass('required');
        //                }
        //                else {
        //                    $('#'+ val.controltovalidate).addClass('required');
        //                }
        //            }";
        //    return strScript;
        //}

        //protected string getDeleteConfirmScript()
        //{
        //    string strScript = @" $('.deleteLink').click(function () {
        //        return confirm('Are you sure you wish to delete this record?');
        //    });  ";

        //    return strScript;
        //}

        //protected string getPageOnLoadScript()
        //{
        //    string strScript = @" $(function () {
        //        //Date picker
        //        $('.datepicker').datepicker({
        //            autoclose: true,
        //            format: 'dd/mm/yyyy'
        //        });
        //        $('.validator').on('show')
        //    });";

        //    return strScript;
        //}
        //protected string getiCheckScript()
        //{
        //    string strScript = @"  $(function()
        //        {
        //            $('input').iCheck({
        //            checkboxClass: 'icheckbox_square-blue',
        //                radioClass: 'iradio_square-blue',
        //                increaseArea: '20%' // optional
        //            });
        //        });";

        //    return strScript;
        //}

        //public static void ShowClientMessage(Page page, string sMsg, string msgType)
        //{
        //    ScriptManager.RegisterClientScriptBlock(page, typeof(string), Guid.NewGuid().ToString(), " $.notify('" + sMsg + "','" + msgType + "');", true);
        //}

    }
}