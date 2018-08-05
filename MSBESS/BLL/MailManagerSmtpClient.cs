using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Net.Mail;
using System.ComponentModel;

using System.Configuration;


namespace WebAdmin.BLL
{
    public class MailManagerSmtpClient
    {
        EmpInfoManager objEmpInfoMgr = new EmpInfoManager();
        LeaveManager objLeaveMgr = new LeaveManager();
        //Payroll_EmpPARMgr objPARMgr = new Payroll_EmpPARMgr();
        //DBConnector objDC = new DBConnector();

        string strFromAddr = "";
        string strToEmpId = "";
        string strToAddr = "";
        string strSubject = "";
        string strBody = "";
        string strErrText = "";
        string MailServer = "";
        string SystemEmail = "";
        string SystemEmailUserName = "";
        string SystemEmailPwd = "";
        string Enablessl = "";
        int MailPort;
        public MailManagerSmtpClient()
        {
            //
            // TODO: Add constructor logic here
            //
            //MailServer = ConfigurationManager.AppSettings["MyMailServer"].ToString();
            //MailPort = Convert.ToInt32(ConfigurationManager.AppSettings["MyMailServerPort"]);
            //SystemEmail = ConfigurationManager.AppSettings["MyEmailID"].ToString();
            //SystemEmailUserName = ConfigurationManager.AppSettings["MyEmailUserName"].ToString();
            //SystemEmailPwd = ConfigurationManager.AppSettings["MyEmailPwd"].ToString();
            //Enablessl = ConfigurationManager.AppSettings["Enssl"];

            //for client pc
            MailServer = "mail.mariestopesbd.org";
            MailPort = 465;
            SystemEmail = "hris@msmtp.mariestopesbd.org";
            SystemEmailUserName = "hris";
            SystemEmailPwd = "hri$9876";
            Enablessl = "false";

            //for developer pc
            //MailServer = "smtp.gmail.com";
            //MailPort = 587;
            //SystemEmail = "alamgir.bfew@gmail.com";
            //SystemEmailUserName = "alamgir.bfew@gmail.com";
            //SystemEmailPwd = "01924199116";
            //Enablessl = "true";


        }


        #region LEAVE MAIL CONFIGURATION
        // Leave Application Mail
        public string RequestForApproval(string strEmpID, string strLvAppID, string strLvPackStartDate,
            string strLvPackEndDate, string strUserEmpId, string strUserName, string strDesig, string strLocation, string strIsSysAdmin,
            string strSpvID, string strSpvEmail)
        {           
            string strFwdBy = "";
            DataTable dtFromEmp = new DataTable();
            dtFromEmp = objEmpInfoMgr.SelectEmpInfoSbuWise(strEmpID, "-1");

            if (dtFromEmp.Rows.Count > 0)
            {
                strFromAddr = dtFromEmp.Rows[0]["OfficeEmail"].ToString().Trim();
                strToEmpId = strSpvID;
            }
            else
            {
                strFromAddr = SystemEmail;
                strToEmpId = strSpvID;
            }
            if (strIsSysAdmin == "N")
            {
                if (strUserEmpId != "0")
                {
                    if (strEmpID.Trim().ToLower() == strUserEmpId.Trim().ToLower())
                    {
                        strUserName = "";
                        strDesig = "";
                        strLocation = "";
                    }
                    else
                    {
                        strFwdBy = strUserName + ", " + strDesig + ", " + strLocation;
                    }
                }
                else
                {
                    strFwdBy = strUserName;
                }
            }
            else
            {
                strFwdBy = strUserName + "(SysAdmin), " + strDesig + ", " + strLocation;
            }

            strToAddr = strSpvEmail;
            DataTable dtLeaveApp = new DataTable();

            //strLvAppID = "11";
            //strEmpID = "E003591";
            //strLvPackStartDate = "2017-05-01";
            //strLvPackEndDate = "2017-05-02";
            //strToEmpId = "E005276";
            dtLeaveApp = objLeaveMgr.SelectRequestLeaveAppMst(Convert.ToInt32(strLvAppID), strEmpID, "P", strLvPackStartDate, strLvPackEndDate, strToEmpId);

            // Get COPY TO EMAIL Address
            string strCopyToName = "";
            string strCopyAddr = "";
            

            if (dtLeaveApp.Rows.Count > 0)
            {
                DateTime ResumeDate = Convert.ToDateTime(dtLeaveApp.Rows[0]["ResumeDate"].ToString());
                string strVPath = "http://10.0.1.70:82/LogIn";
                strSubject = "Request for recommending leave.";
                strBody = " Leave applicant: " + dtFromEmp.Rows[0]["FullName"].ToString() + ", "
                        + dtFromEmp.Rows[0]["DesigName"].ToString() + ", " + dtFromEmp.Rows[0]["DeptName"].ToString() + ", " + dtFromEmp.Rows[0]["DivisionName"].ToString()
                        + " \n\n "
                        + "Request forwarded by: " + strFwdBy
                        + " \n\n "
                        + "Copied to: " + strCopyToName
                        + " \n\n "
                        + "Please approve the following leave request: "
                       + " \n "
                        + "Leave type: " + dtLeaveApp.Rows[0]["LTypeTitle"].ToString()
                      + " \n "
                        + "From: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveStart"].ToString())
                      + " \n "
                        + "To: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveEnd"].ToString())
                     + " \n "
                        + "Resume office on: " + Common.DisplayDate(ResumeDate.ToShortDateString())
                     + " \n "
                        + "Reason for leave: " + dtLeaveApp.Rows[0]["LTreason"].ToString()
                     + " \n "
                        + "Contact number: " + dtLeaveApp.Rows[0]["PhoneNo"].ToString()
                      + " \n\n "
                        + "With thanks "
                       + " \n\n "
                        + dtFromEmp.Rows[0]["FullName"].ToString()
                      + " \n "
                        + dtFromEmp.Rows[0]["DesigName"].ToString()
                      + " \n "
                       + "======================================"
                       + " Click here to login for recommendation: " + strVPath;
            }
            try
            {
                if (strFromAddr != "" && strToAddr != "")
                {
                    //strFromAddr = "alamgir.bfew@gmail.com";
                    //strToAddr= "alamgir.bfew@gmail.com";
                    MailMessage objMsg = new MailMessage(strFromAddr, strToAddr, strSubject, strBody);
                    //objMsg.IsBodyHtml = true;
                    SmtpClient MySmtpClient = new SmtpClient(MailServer);//"smtp.gmail.com");
                    MySmtpClient.Port = MailPort;
                    MySmtpClient.EnableSsl = Convert.ToBoolean(Enablessl);
                    MySmtpClient.Credentials = new System.Net.NetworkCredential(SystemEmailUserName, SystemEmailPwd); //"alamgir.bfew@gmail.com", "01924199116");

                    MySmtpClient.Send(objMsg);
                    strErrText = "Mail has been sent to recommendar";
                    //MySmtpClient.SendAsync(objMsg, objMsg.Subject);
                    //MySmtpClient.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);

                }
                
            }
            catch(Exception ex)
            {
                strErrText = ex.Message;// "Mail is not send. Please configure the internet.";
            }
            dtFromEmp.Rows.Clear();
            dtFromEmp.Dispose();

            return strErrText;
        }


        public string RequestFromRecommendar(string strEmpID, string strLvAppID, string strLvPackStartDate,
            string strLvPackEndDate, string strUserEmpId, string strUserName, string strDesig, string strLocation, string strIsSysAdmin,
            string strSpvID, string strSpvEmail)
        {
            string strFwdBy = "";
            DataTable dtFromEmp = new DataTable();
            dtFromEmp = objEmpInfoMgr.SelectEmpInfoSbuWise(strEmpID, "-1");

            if (dtFromEmp.Rows.Count > 0)
            {
                strFromAddr = dtFromEmp.Rows[0]["OfficeEmail"].ToString().Trim();
                strToEmpId = strSpvID;
            }
            else
            {
                strFromAddr = SystemEmail;
                strToEmpId = strSpvID;
            }
            if (strIsSysAdmin == "N")
            {
                if (strUserEmpId != "0")
                {
                    if (strEmpID.Trim().ToLower() == strUserEmpId.Trim().ToLower())
                    {
                        strUserName = "";
                        strDesig = "";
                        strLocation = "";
                    }
                    else
                    {
                        strFwdBy = strUserName + ", " + strDesig + ", " + strLocation;
                    }
                }
                else
                {
                    strFwdBy = strUserName;
                }
            }
            else
            {
                strFwdBy = strUserName + "(SysAdmin), " + strDesig + ", " + strLocation;
            }

            strToAddr = strSpvEmail;
            DataTable dtLeaveApp = new DataTable();

            //strLvAppID = "11";
            //strEmpID = "E003591";
            //strLvPackStartDate = "2017-05-01";
            //strLvPackEndDate = "2017-05-02";
            //strToEmpId = "E005276";
            dtLeaveApp = objLeaveMgr.SelectRequestLeaveAppMst(Convert.ToInt32(strLvAppID), strEmpID, "P", strLvPackStartDate, strLvPackEndDate, strToEmpId);

            // Get COPY TO EMAIL Address
            string strCopyToName = "";
            string strCopyAddr = "";


            if (dtLeaveApp.Rows.Count > 0)
            {
                DateTime ResumeDate = Convert.ToDateTime(dtLeaveApp.Rows[0]["ResumeDate"].ToString());
                string strVPath = "http://10.0.1.70:82/LogIn";
                strSubject = "Request for approving leave.";
                strBody = " Leave applicant: " + dtFromEmp.Rows[0]["FullName"].ToString() + ", "
                        + dtFromEmp.Rows[0]["DesigName"].ToString() + ", " + dtFromEmp.Rows[0]["DeptName"].ToString() + ", " + dtFromEmp.Rows[0]["DivisionName"].ToString()
                        + " \n\n "
                        + "Request forwarded by: " + strFwdBy
                        + " \n\n "
                        + "Copied to: " + strCopyToName
                        + " \n\n "
                        + "Please approve the following leave request: "
                       + " \n "
                        + "Leave type: " + dtLeaveApp.Rows[0]["LTypeTitle"].ToString()
                      + " \n "
                        + "From: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveStart"].ToString())
                      + " \n "
                        + "To: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveEnd"].ToString())
                     + " \n "
                        + "Resume office on: " + Common.DisplayDate(ResumeDate.ToShortDateString())
                     + " \n "
                        + "Reason for leave: " + dtLeaveApp.Rows[0]["LTreason"].ToString()
                     + " \n "
                        + "Contact number: " + dtLeaveApp.Rows[0]["PhoneNo"].ToString()
                      + " \n\n "
                        + "With thanks "
                       + " \n\n "
                        + dtFromEmp.Rows[0]["FullName"].ToString()
                      + " \n "
                        + dtFromEmp.Rows[0]["DesigName"].ToString()
                      + " \n "
                       + "======================================"
                       + " Click here to login for approve: " + strVPath;
            }
            try
            {
                if (strFromAddr != "" && strToAddr != "")
                {
                    //strFromAddr = "alamgir.bfew@gmail.com";
                    //strToAddr= "alamgir.bfew@gmail.com";
                    MailMessage objMsg = new MailMessage(strFromAddr, strToAddr, strSubject, strBody);
                    //objMsg.IsBodyHtml = true;
                    SmtpClient MySmtpClient = new SmtpClient(MailServer);//"smtp.gmail.com");
                    MySmtpClient.Port = MailPort;
                    MySmtpClient.EnableSsl = Convert.ToBoolean(Enablessl);
                    MySmtpClient.Credentials = new System.Net.NetworkCredential(SystemEmail, SystemEmailPwd); //"alamgir.bfew@gmail.com", "01924199116");

                    MySmtpClient.Send(objMsg);
                    strErrText = "Mail has been sent to supervisor";
                    //MySmtpClient.SendAsync(objMsg, objMsg.Subject);
                    //MySmtpClient.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);

                }

            }
            catch (Exception ex)
            {
                strErrText = ex.Message;// "Mail is not send. Please configure the internet.";
            }
            dtFromEmp.Rows.Clear();
            dtFromEmp.Dispose();

            return strErrText;
        }

        void smtp_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string msg = "";
            if (e.Cancelled == true)
            {
                msg = "Email sending cancelled!";
            }
            else if (e.Error != null)
            {
                msg = e.Error.Message;
            }
            else
            {
                msg = "Email sent sucessfully!";
            }
        }

        // Leave Approve Mail 
        public string LeaveApproval(string strEmpID, string strLvAppID,
         string strUserEmpId, string strUserName, string strDesig, string strLocation, string strIsSysAdmin, string strUserEmail, string strOffice, string strTeam)
        {
            // Requesting Employee Info
            strErrText = "";
            string strApvBy = "";
            DataTable dtToEmp = new DataTable();
            dtToEmp = objEmpInfoMgr.SelectEmpInfoSbuWise(strEmpID, "-1");

            if (dtToEmp.Rows.Count > 0)
            {
                strToAddr = dtToEmp.Rows[0]["OfficeEmail"].ToString().Trim();
            }
            else
            {
                strFromAddr = SystemEmail;
            }
            if (strIsSysAdmin == "N")
            {
                if (strUserEmpId != "0")
                {
                    if (strEmpID.Trim().ToLower() == strUserEmpId.Trim().ToLower())
                    {
                        strUserName = "";
                        strDesig = "";
                        strLocation = "";
                    }
                    else
                    {
                        strApvBy = strUserName + ", " + strDesig + ", " + strOffice + ", " + strTeam;
                    }
                }
                else
                {
                    strApvBy = strUserName;
                }
            }
            else
            {
                strApvBy = strUserName + "(SysAdmin), " + strDesig + ", " + strLocation;
            }
            strFromAddr = strUserEmail;
            DataTable dtLeaveApp = new DataTable();
            dtLeaveApp = objLeaveMgr.SelectRequestLeaveAppMst(Convert.ToInt32(strLvAppID), strEmpID, "A");

            // Get COPY TO EMAIL Address
            //string strCopyToName = "";
            //string strCopyAddr = "";

            //string strCopyToName = strUserName;
            //string strCopyAddr = strFromAddr;

            //DataTable dtLvCopyTo = new DataTable();
            //LeaveApplicationManager objLvMgr = new LeaveApplicationManager();
            //dtLvCopyTo = objLvMgr.SelectLeaveCopyTo(strLvAppID);
            //foreach (DataRow dRow in dtLvCopyTo.Rows)
            //{
            //    if (strCopyToName == "")
            //    {
            //        strCopyToName = dRow["SPVFULLNAME"].ToString();
            //        strCopyAddr = dRow["CopyToEmail"].ToString();
            //    }
            //    else
            //    {
            //        strCopyToName = strCopyToName + ", " + dRow["SPVFULLNAME"].ToString();
            //        strCopyAddr = strCopyAddr + ";" + dRow["CopyToEmail"].ToString();
            //    }
            //}

            if (dtLeaveApp.Rows.Count > 0)
            {
                DateTime ResumeDate = Convert.ToDateTime(dtLeaveApp.Rows[0]["ResumeDate"].ToString());
                string strVPath = "http://10.181.66.18:1050/aspire";//http://10.13.1.109:1050/aspire //http://202.84.36.226:1050/payhr
                strSubject = "Approved as requested.";
                strBody = " Your leave application is approved. "
                        + " \n\n "
                        + "Thanks, "
                        + " \n "
                        + strApvBy
                        + " \n "
                        + "===================================================="
                        + " \n "
                        + "Leave Details:"
                        + " \n "
                        + "--------------"
                        + " \n "
                        + "Leave Applicant: " + dtToEmp.Rows[0]["FullName"].ToString() + ", "
                        + dtToEmp.Rows[0]["JobTitle"].ToString() + ", " + dtToEmp.Rows[0]["DeptName"].ToString() + ", " + dtToEmp.Rows[0]["DivisionName"].ToString()
                        + " \n "
                        + "Date of Application:  " + dtLeaveApp.Rows[0]["AppDate"].ToString()
                        + " \n "
                        + "Leave type: " + dtLeaveApp.Rows[0]["LTypeTitle"].ToString()
                        + " \n "
                        + "From: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveStart"].ToString())
                        + " \n "
                        + "To: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveEnd"].ToString())
                        + " \n "
                        + "Back in office: " + Common.DisplayDate(ResumeDate.ToShortDateString())
                        + " \n "
                        + "Reason for leave: " + dtLeaveApp.Rows[0]["LTreason"].ToString()
                        + " \n "
                        + "Contact number: " + dtLeaveApp.Rows[0]["PhoneNo"].ToString()
                        + " \n "
                       + "===================================================="
                       + " \n\n ";
            }
            try
            {
                if (strFromAddr != "" && strToAddr != "")
                {
                    //SmtpClient MySmtpClient = new SmtpClient(MailServer, MailPort);

                    //MySmtpClient.UseDefaultCredentials = false;
                    //MySmtpClient.Credentials = new System.Net.NetworkCredential(SystemEmailUserName, SystemEmailPwd);
                    //System.Net.Mail.MailMessage objMsg = new System.Net.Mail.MailMessage(strFromAddr, strToAddr, strSubject, strBody);
                    //MySmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    
                    //MySmtpClient.Send(objMsg);

                    strFromAddr = "alamgir.bfew@gmail.com";
                    strToAddr = "alamgir.bfew@gmail.com";
                    MailMessage objMsg = new MailMessage(strFromAddr, strToAddr, strSubject, strBody);
                   
                    SmtpClient MySmtpClient = new SmtpClient("smtp.gmail.com");
                    MySmtpClient.Port = 25;
                    MySmtpClient.EnableSsl = true;
                    MySmtpClient.Credentials = new System.Net.NetworkCredential("alamgir.bfew@gmail.com", "01924199116");

                    //MySmtpClient.SendAsync(objMsg, objMsg.Subject);
                    //MySmtpClient.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);

                }
            }
            catch
            {
                strErrText = "Mail is not send. Please configure the internet.";
            }

            dtToEmp.Rows.Clear();
            dtToEmp.Dispose();
            return strErrText;
        }

        #endregion
    }
}