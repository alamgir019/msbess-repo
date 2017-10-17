using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdmin.UserControls.EIS
{
    public partial class ctlEmpFileUpload : System.Web.UI.UserControl
    {
        //int depth = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["ISADMIN"].ToString().Trim() == "N")
                {
                    hfID.Value = Session["EMPID"].ToString().Trim();
                }
                this.LoadDirectory();
            }
        }
        protected void LoadDirectory()
        {
            MyTree.Nodes.Clear();
            MyTree.Dispose();
            string filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EmpDocFile"];
            System.IO.DirectoryInfo RootDir = new System.IO.DirectoryInfo(Server.MapPath(filePath));
            TreeNode RootNode = OutputDirectory(RootDir, null,0);
            MyTree.Nodes.Add(RootNode);
            MyTree.ExpandDepth = 1;
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EmpDocFile"];
            System.IO.DirectoryInfo RootDir = new System.IO.DirectoryInfo(filePath);
            string extn = "";
            string fullPath = "";
            string fileName = "";
            if (fluTin.HasFile && fluNid.HasFile && fluDriveLic.HasFile && fluBmdc.HasFile)
            {
                try
                {
                    string[] extns = { ".pdf",".png",".jpg",".jpeg"};
                    //tin upload
                    fullPath = filePath+"TIN/"+ hfID.Value + "/";
                    fileName = Path.GetFileName(fluTin.FileName);
                    extn = Path.GetExtension(fileName);
                    if (extns.Contains(extn.ToLower()))
                    {

                        bool exists = System.IO.Directory.Exists(Server.MapPath(fullPath));

                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(fullPath));
                        fluTin.SaveAs(Server.MapPath(fullPath) + fileName);
                    }
                    //nid upload
                    fullPath = filePath+"NID/"+ hfID.Value + @"/";
                    fileName = Path.GetFileName(fluNid.FileName);
                    extn = Path.GetExtension(fileName);
                    if (extns.Contains(extn.ToLower()))
                    {

                        bool exists = System.IO.Directory.Exists(Server.MapPath(fullPath));

                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(fullPath));
                        fluNid.SaveAs(Server.MapPath(fullPath) + fileName);
                    }
                    //driving licence upload
                    fullPath = filePath+"DrivLic/"+ hfID.Value + @"/";
                    fileName = Path.GetFileName(fluDriveLic.FileName);
                    extn = Path.GetExtension(fileName);
                    if (extns.Contains(extn.ToLower()))
                    {

                        bool exists = System.IO.Directory.Exists(Server.MapPath(fullPath));

                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(fullPath));
                        fluDriveLic.SaveAs(Server.MapPath(fullPath) + fileName);
                    }
                    //bmdc upload
                    fullPath = filePath+"BMDC/" + hfID.Value + @"/";
                    fileName = Path.GetFileName(fluBmdc.FileName);
                    extn = Path.GetExtension(fileName);
                    if (extns.Contains(extn.ToLower()))
                    {
                        bool exists = System.IO.Directory.Exists(Server.MapPath(fullPath));
                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(fullPath));
                        fluBmdc.SaveAs(Server.MapPath(fullPath) + fileName);
                    }
                    SiteMaster.ShowClientMessage(Page, "File uploaded successfully!", "success");
                }
                catch (Exception ex)
                {
                    SiteMaster.ShowClientMessage(Page, "Upload status: The file could not be uploaded. The following error occured: " + ex.Message, "success");
                }
            }
            //TabContainer1.ActiveTabIndex = 0;
        }

        TreeNode OutputDirectory(System.IO.DirectoryInfo directory, TreeNode parentNode,int depth)
        {
            depth++;
            if (directory == null) return null;

            TreeNode DirNode = new TreeNode(directory.Name, directory.Name);

            System.IO.DirectoryInfo[] SubDirectories = directory.GetDirectories();

            for (int DirectoryCount = 0; DirectoryCount < SubDirectories.Length; DirectoryCount++)
            {
                if (depth==2)
                {
                    if (SubDirectories[DirectoryCount].Name.ToUpper() == Session["EMPID"].ToString().Trim().ToUpper())
                    {
                        depth--;
                        OutputDirectory(SubDirectories[DirectoryCount], DirNode, depth);
                        break;
                    }
                }
                else
                {
                    OutputDirectory(SubDirectories[DirectoryCount], DirNode, depth);
                }
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
                InsertViewLogInfo(FileName);

            }

        }


        private void ReadPdfFile(string pdfFile)
        {
            Session["FILEPATH"] = "";
            string filePath = System.Web.Configuration.WebConfigurationManager.AppSettings["EmpDocFile"];

            string path = filePath + "/" + pdfFile;

            Session["FILEPATH"] = path;
            ///File View to a new page
            StringBuilder sb = new StringBuilder();
            string strURL = "EmpFileViewer.aspx";
            sb.Append("<script>");
            sb.Append("window.open('" + strURL + "', '', 'directories=0,titlebar=0,toolbar=0,location=0,status=0,menubar=0,fullscreen=no,scrollbars=no,resizable=yes');");//

            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit",
                                     sb.ToString(), false);            
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmSubmit", sb.ToString(),false);

        }
        private void InsertViewLogInfo(string FileName)
        {
            //string ViewID = Common.getMaxId("MSBPageViewDtls", "ViewID");
            //tblObj.InsertViewLogInfo(ViewID, Session["USERID"].ToString(), FileName, Common.SetDateTime(DateTime.Now.ToString()));
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Default.aspx");
        }
    }
}