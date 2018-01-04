using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdmin.UserControls.EIS
{
    public partial class ctlEmpFileReader : System.Web.UI.UserControl
    {
        string filePath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                this.LoadDirectory();
            }
        }

        protected void LoadDirectory()
        {
            MyTree.Nodes.Clear();
            MyTree.Dispose();
            filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EmpFilePath"];
            //filePath = "D:\\BASESOFT\\MSBHR\\Documents\\PolicyFiles\\";
            System.IO.DirectoryInfo RootDir = new System.IO.DirectoryInfo(Server.MapPath(filePath));
            //System.IO.DirectoryInfo RootDir = new System.IO.DirectoryInfo(filePath);
            TreeNode RootNode = OutputDirectory(RootDir, null);
            MyTree.Nodes.Add(RootNode);
            MyTree.ExpandDepth = 1;
        }

        TreeNode OutputDirectory(System.IO.DirectoryInfo directory, TreeNode parentNode)
        {
            if (directory == null) return null;

            TreeNode DirNode = new TreeNode(directory.Name, directory.Name);

            System.IO.DirectoryInfo[] SubDirectories = directory.GetDirectories();

            for (int DirectoryCount = 0; DirectoryCount < SubDirectories.Length; DirectoryCount++)
            {
                OutputDirectory(SubDirectories[DirectoryCount], DirNode);
            }

            System.IO.FileInfo[] Files = directory.GetFiles();

            for (int FileCount = 0; FileCount < Files.Length; FileCount++)
            {
                DirNode.ChildNodes.Add(new TreeNode(Files[FileCount].Name, Files[FileCount].Name));
            }

            if (parentNode == null)
            {
                return DirNode;
            }

            else
            {

                parentNode.ChildNodes.Add(DirNode);

                return parentNode;
            }
        }

        protected void MyTree_SelectedNodeChanged(object sender, EventArgs e)
        {

            TreeNode node = this.MyTree.SelectedNode;
            String pathStr = node.Text;
            string separator = "/";


            MyTree.PathSeparator = Convert.ToChar(separator);


            while (node.Parent != null)
            {
                pathStr = node.Parent.Text + this.MyTree.PathSeparator + pathStr;
                node = node.Parent;

            }
            string modpath = "";

            if (pathStr.IndexOf('/') > 0)
            {
                modpath = pathStr.Split(new char[] { '/' }, 2)[1];
            }


            string chk = Path.GetExtension(pathStr).ToUpper();
            Session["extension"] = chk;

            if (chk.Equals(".pdf".ToUpper()) || chk.Equals(".jpg".ToUpper()) || chk.Equals(".jpeg".ToUpper()) || chk.Equals(".png".ToUpper()))
            {
                ReadPdfFile(modpath);
                string[] result = modpath.Split(new string[] { "/" }, StringSplitOptions.None);

                string FileName = result[result.Length - 1].ToString();
                //InsertViewLogInfo(FileName);

            }

        }
        //private void InsertViewLogInfo(string FileName)
        //{
        //    string ViewID = Common.getMaxId("MSBPageViewDtls", "ViewID");
        //    tblObj.InsertViewLogInfo(ViewID, Session["USERID"].ToString(), FileName, Common.SetDateTime(DateTime.Now.ToString()));
        //}

        private void ReadPdfFile(string pdfFile)
        {
            Session["FILEPATH"] = "";
            filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EmpFilePath"];

            string path = filePath + "\\" + pdfFile; //@"E:\work\PLAN BANGLADESH\" + pdfFile;    

            Session["FILEPATH"] = path;
            //Session["PageName"] = "EmpFileReader";
            ///File View to a new page
            StringBuilder sb = new StringBuilder();
            string strURL = "EmpFileViewer.aspx";
            sb.Append("<script>");
            sb.Append("window.open('" + strURL + "', '', 'directories=0,titlebar=0,toolbar=0,location=0,status=0,menubar=0,fullscreen=no,scrollbars=no,resizable=yes');");//

            //"directories=0,titlebar=0,toolbar=0,location=0,status=0,menubar=0,scrollbars=no,resizable=no,width=400,height=350"
            //sb.Append("window.open('" + strURL + "', '', '');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                     sb.ToString(), false);
            //ScriptManager.RegisterStartupScript(this.GetType(), "ConfirmSubmit", sb.ToString());

        }
    }
}