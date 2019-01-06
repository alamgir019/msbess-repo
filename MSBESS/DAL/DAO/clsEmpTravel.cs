using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAdmin.DAL.DAO
{
    public class clsEmpTravel
    {
        public string EmpId { get; set; }
        public string TravelId { get; set; }
        public string AppDate { get; set; }
        public string VisitTo { get; set; }
        public string Purpose { get; set; }
        public string DepartureDate { get; set; }
        public string ReturnDate { get; set; }
        public string OfficeJoinDate { get; set; }
        public string TotalDays { get; set; }
        public string TravelMode { get; set; }
        public string AdvAmount { get; set; }
        public string TravelInstruction { get; set; }
        public string TravelStatus { get; set; }
        public string InsertedBy { get; set; }
        public string InsertedDate { get; set; }
        public string ProjectId { get; set; }
        public string RecomendedBy { get; set; }
        public string RecomendDate { get; set; }
        public clsEmpTravel()
        {
        }
        
    }
}