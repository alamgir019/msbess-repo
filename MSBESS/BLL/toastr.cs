using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
/// <summary>
/// Using Toastr from CodeSeven https://github.com/CodeSeven/toastr
/// </summary>
/// <remarks></remarks>
namespace WebAdmin.BLL
{
   public class toastr
    {
        public string SessionName = "toastr";
        private string t_Title;
        private string t_Text;
        private ToastTypes t_Type;
        private ToastPositions t_Position;

        public enum ToastTypes { info, success, warning, error };
        public enum ToastPositions { toast_top_right, toast_bottom_right, toast_bottom_left, toast_top_left, toast_top_full_width, toast_bottom_full_width, toast_top_center, toast_bottom_center };

        /// <summary>
        /// Show info toast at top right
        /// </summary>
        /// <param name="text">Text to be shown in the toast</param>
        /// <remarks></remarks>
        public void New(string text)
        {
            t_Text = text;
            t_Title = string.Empty;
            t_Type = ToastTypes.info;
            t_Position = ToastPositions.toast_top_right;
        }
        /// <summary>
        /// Show toast at top right
        /// </summary>
        /// <param name="text">Text to be shown in the toast</param>
        /// <param name="type">Type of Toast. Could be info, success, warning or errors.</param>
        /// <remarks></remarks>
        public void New(string text, ToastTypes type)
        {
            t_Text = text;
            t_Type = type; 
            t_Title = string.Empty;
            t_Position = ToastPositions.toast_top_right;
        }
        /// <summary>
        /// Show info toast
        /// </summary>
        /// <param name="text">Text to be shown in the toast</param>
        /// <param name="position">Position of Toast. Several options available</param>
        /// <remarks></remarks>
        public void New(string text, ToastPositions position)
        {
            t_Text = text;
            t_Type = ToastTypes.info;
            t_Title = string.Empty;
            t_Position = position;
        }
        /// <summary>
        /// Show toast
        /// </summary>
        /// <param name="text">Text to be shown in the toast</param>
        /// <param name="type">Type of Toast. Could be info, success, warning or errors.</param>
        /// <param name="position">Position of Toast. Several options available</param>
        /// <remarks></remarks>
        public void New(string text, ToastTypes type, ToastPositions position)
        {
            t_Text = text;
            t_Type = type;
            t_Title = string.Empty;
            t_Position = position;
        }
        public string toastTitle
        {
            get
            {
                return t_Title;
            }
        }
        public string toastText
        {
            get
            {
                return t_Text;
            }
        }

        public string toastType
        {
            get
            {
                return t_Type.ToString();
            }
        }
        public string toastPosition
        {
            get
            {
                return t_Position.ToString().Replace("_", "-");
            }
        }

        /// <summary>
        /// Retrieve Toast from Session and remove Sessionvalue afterwards
        /// </summary>
        /// <returns>Toast to be displayed</returns>
        /// <remarks></remarks>

        public toastr GetToast()
        {
            if(System.Web.HttpContext.Current.Session[SessionName] !=null)
            {
                GetToast = CType(System.Web.HttpContext.Current.Session(SessionName), toastr)
            }
        }
        Friend Shared Function GetToast() As toastr
        If Not System.Web.HttpContext.Current.Session(SessionName) Is Nothing Then
            GetToast = CType(System.Web.HttpContext.Current.Session(SessionName), toastr)
            System.Web.HttpContext.Current.Session.Remove(SessionName)
        Else
            GetToast = Nothing
        End If
    End Function
    }
}