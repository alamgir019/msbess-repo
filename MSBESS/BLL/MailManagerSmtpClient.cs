﻿using System;
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

        public string strFromAddr = "";
        public string strToEmpId = "";
        public string strToAddr = "";
        public string strSubject = "";
        public string strBody = "";
        public string strErrText = "";
        public string MailServer = "";
        public string SystemEmail = "";
        public string SystemEmailUserName = "";
        public string SystemEmailPwd = "";
        public string Enablessl = "";
        public int MailPort;
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
            SystemEmailPwd = "8847M443m3";
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
            string strSpvID, string strSpvEmail, string lvAppStatus)
        {           
            string strFwdBy = "";
            DataTable dtFromEmp = new DataTable();
            dtFromEmp = objEmpInfoMgr.SelectEmpInfoSbuWise(strEmpID, "-1");

            if (dtFromEmp.Rows.Count > 0)
            {
                strFromAddr = dtFromEmp.Rows[0]["OfficeEmail"].ToString().Trim();
                strToEmpId = strSpvID;
                //if (dtFromEmp.Rows[0]["DesigId"].ToString() == "183")
                //{
                //    lvAppStatus = "R";
                //}
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
           
            dtLeaveApp = objLeaveMgr.SelectRequestLeaveAppMst(Convert.ToInt32(strLvAppID), strEmpID, lvAppStatus, strLvPackStartDate, strLvPackEndDate, strToEmpId);

            // Get COPY TO EMAIL Address
            string strCopyToName = "";
            string strCopyAddr = "";
            

            if (dtLeaveApp.Rows.Count > 0)
            {
                DateTime ResumeDate = Convert.ToDateTime(dtLeaveApp.Rows[0]["ResumeDate"].ToString());
                string strVPath = "http://10.0.1.70:82/LogIn";
                strSubject = "Request for processing leave.";
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
                       + " Click here to login for Process: " + strVPath;
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
                    strErrText = "Mail has been sent to responsible Person";
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
                    MailMessage objMsg = new MailMessage(strFromAddr, strToAddr, strSubject, strBody);
                    //objMsg.IsBodyHtml = true;
                    SmtpClient MySmtpClient = new SmtpClient(MailServer);//"smtp.gmail.com");
                    MySmtpClient.Port = MailPort;
                    MySmtpClient.EnableSsl = Convert.ToBoolean(Enablessl);
                    MySmtpClient.Credentials = new System.Net.NetworkCredential(SystemEmailUserName, SystemEmailPwd); //"alamgir.bfew@gmail.com", "01924199116");
                    //strErrText = "from:"+strFromAddr+" to:"+strToAddr+" subject:"+ strSubject;

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
        public string LeaveMail(string toEmpID, string strLvAppID,string fromEmpId, string strLvPackStartDate,
            string strLvPackEndDate, string leaveCondition)
        {
            // Requesting Employee Info
            strErrText = "";
            string strApvBy = "";
            DataTable dtToEmp = new DataTable();
            dtToEmp = objEmpInfoMgr.SelectEmpInfoSbuWise(toEmpID, "-1");

            if (dtToEmp.Rows.Count > 0)
            {
                strToAddr = dtToEmp.Rows[0]["OfficeEmail"].ToString().Trim();
            }

            DataTable dtFromEmp = objEmpInfoMgr.SelectEmpInfoSbuWise(fromEmpId, "-1");
            if (dtFromEmp.Rows.Count>0)
            {
                strFromAddr = dtToEmp.Rows[0]["OfficeEmail"].ToString().Trim();
                strApvBy = dtFromEmp.Rows[0]["FullName"].ToString().Trim() + ", " + dtFromEmp.Rows[0]["DesigName"].ToString().Trim();
            }
            else
            {
                strFromAddr = SystemEmail;
            }
            DataTable dtLeaveApp = new DataTable();
            dtLeaveApp = objLeaveMgr.SelectRequestLeaveAppMst(Convert.ToInt32(strLvAppID), toEmpID, leaveCondition, strLvPackStartDate, strLvPackEndDate, strToEmpId);
            
            if (dtLeaveApp.Rows.Count > 0)
            {
                DateTime ResumeDate = Convert.ToDateTime(dtLeaveApp.Rows[0]["ResumeDate"].ToString());
                string strVPath = "http://10.0.1.70:82/LogIn";
                string condition = "";
                if (leaveCondition=="A")
                {
                    condition = "Approved";
                }
                else if (leaveCondition=="D")
                {
                    condition = "Regretted";
                }
                else if (leaveCondition=="C")
                {
                    condition = "Cancelled";
                }
                strSubject = condition + " as requested.";
                strBody = " Your leave application is " + condition + ". "
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
                        + dtToEmp.Rows[0]["DeptName"].ToString() + ", " + dtToEmp.Rows[0]["DivisionName"].ToString()
                        + " \n "
                        + "Date of Application:  " + dtLeaveApp.Rows[0]["AppDate"].ToString()
                        + " \n "
                        + "Leave type: " + dtLeaveApp.Rows[0]["LTypeTitle"].ToString()
                        + " \n "
                        + "From: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveStart"].ToString())
                        + " \n "
                        + "To: " + Common.DisplayDate(dtLeaveApp.Rows[0]["LeaveEnd"].ToString())
                        + " \n ";
                if (leaveCondition=="A")
                {
                    strBody += "Back in office: " + Common.DisplayDate(ResumeDate.ToShortDateString())+ " \n ";

                }
                        strBody += "Reason for leave: " + dtLeaveApp.Rows[0]["LTreason"].ToString()
                        + " \n "
                        + "Contact number: " + dtLeaveApp.Rows[0]["PhoneNo"].ToString()
                        + " \n "
                       + "===================================================="
                       + " \n\n "
                       + " Click here to login for leave details: " + strVPath;                
            }
            try
            {
                if (strFromAddr != "" && strToAddr != "")
                {
                    MailMessage objMsg = new MailMessage(strFromAddr, strToAddr, strSubject, strBody);
                    SmtpClient MySmtpClient = new SmtpClient(MailServer);//"smtp.gmail.com");
                    MySmtpClient.Port = MailPort;
                    MySmtpClient.EnableSsl = Convert.ToBoolean(Enablessl);
                    MySmtpClient.Credentials = new System.Net.NetworkCredential(SystemEmailUserName, SystemEmailPwd); 

                    MySmtpClient.Send(objMsg);
                    strErrText = "Mail has been sent";
                    //MySmtpClient.SendAsync(objMsg, objMsg.Subject);
                    //MySmtpClient.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);          
                }
            }
            catch (Exception ex)
            {
                strErrText = "Mail is not send. Please configure the internet.";
            }

            dtToEmp.Rows.Clear();
            dtToEmp.Dispose();
            return strErrText;
        }


        public string SendMSBMail()
        {
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
                    MySmtpClient.UseDefaultCredentials = false;
                    MySmtpClient.Credentials = new System.Net.NetworkCredential(SystemEmailUserName, SystemEmailPwd); //"alamgir.bfew@gmail.com", "01924199116");

                    MySmtpClient.Send(objMsg);
                    strErrText = "Mail has been sent to recommendar";
                    //MySmtpClient.SendAsync(objMsg, objMsg.Subject);
                    //MySmtpClient.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);

                }

            }
            catch (Exception ex)
            {
                strErrText = ex.Message;// "Mail is not send. Please configure the internet.";
            }
            return strErrText;
        }


        #endregion
    }
}