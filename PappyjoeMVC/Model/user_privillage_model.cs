using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Model;
namespace PappyjoeMVC.Model
{
    class user_privillage_model
    {
        Connection db = new Connection();
        // main forms
        public string inventry_main(string doctor_id)
        {
            string id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='INVENTORY' and Permission='A'");
            return id;
        }
        public string user_exsists(string doctor_id)
        {
            string id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " ");
            return id;
        }
        public string Calender_main(string doctor_id)
        {
            string id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='CALENDAR' and Permission='A'");
            return id;
        }
        public string Consultation_main(string doctor_id)
        {
            string id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='CONSULTATION' and Permission='A'");
            return id;
        }
        public string Communication_main(string doctor_id)
        {
            string id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='COMMUNICATION' and Permission='A'");
            return id;
        }
        public string reports_main(string doctor_id)
        {
            string id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='RPT' and Permission='A'");

            //string id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='Reports' and Permission='A'");
            return id;
        }
        public string expense_main(string doctor_id)
        {
            string id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EXPENSE' and Permission='A'");
            return id;
        }
        public string profile_main(string doctor_id)
        {
            string id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='Profile' and Permission='A'");
            return id;
        }
        public string sales_main(string doctor_id)
        {
            string id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='Sales' and Permission='A'");
            return id;
        }
        public string emr_main(string doctor_id)
        {  
            string emr = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMR' and Permission='A'");
           return emr;
         }
        public string labtrackinf_main(string doctor_id)
        {
            string lbtrack = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='LABTRACKING' and Permission='A'");
            return lbtrack;
        }
        /// <summary>
        ///  end
        /// </summary>
        public  string settings_add(string doctor_id)
        {
          string  id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='CLMS' and Permission='A'");
            return id;
        }
        //Appointments
        public string Add_privillege(string doctor_id)
        {
            string ss = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='APT' and Permission='A'");

            //string ss = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='APT' and Permission='AP'");
            return ss;
        }
        public string show_privillege(string doctor_id)
        {
            string ss = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='APT' and Permission='S'");
            return ss;
        }
        public string privilege_D(string doctor_id)
        {
            string id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='APT' and Permission='D'");
            return id;
        }
        public string privilege_E(string doctor_id)
        {
            string id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='APT' and Permission='E'");
            return id;
        }
        ///Patients
        public  string add_patients(string doctor_id)
        {
          string id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='PAT' and Permission='A'");//CLMS
            return id;
        }
        public string edit_patients(string doctor_id)
        {
            string PATedit = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='PAT' and Permission='E'");
            return PATedit;
        }
        // Attachment
        public string add_attachments(string doctor_id)
        {
            string EMRFadd = db.scalar("select id from tbl_user_privilege where UserID=" + doctor_id + " and Category='EMRF' and Permission='A'");
            return EMRFadd;
        }
        public string delete_attachment(string id)
        {
            string e = db.scalar("select id from tbl_user_privilege where UserID=" + id + " and Category='EMRF' and Permission='D'");
            return e;
        }
        public string Show_attachment(string id)
        {
            string e = db.scalar("select id from tbl_user_privilege where UserID=" + id + " and Category='EMRF' and Permission='S'");
            return e;
        }
        //vitals
        public string add_vitals(string id)
        {
            string e = db.scalar("select id from tbl_user_privilege where UserID=" + id + " and Category='EMRV' and Permission='A'");
            return e;
        }
        public string show_vitals(string id)
        {
            string e = db.scalar("select id from tbl_user_privilege where UserID=" + id + " and Category='EMRV' and Permission='S'");
            return e;
        }
        public string edit_vitals(string id)
        {
            string e = db.scalar("select id from tbl_user_privilege where UserID=" + id + " and Category='EMRV' and Permission='E'");
            return e;
        }
        //clinic findings
        public string findings_Add(string doctor_id)
        {
            string id = db.scalar("select id from tbl_user_privilege where UserID=" + doctor_id + " and Category='EMRCF' and Permission='A'");
            return id;
        }
        public string findings_show(string doctor_id)
        {
            string id = db.scalar("select id from tbl_user_privilege where UserID=" + doctor_id + " and Category='EMRCF' and Permission='S'");
            return id;
        }
        public string user_priv_EMRC_E(string doctor_id)
        {
            string id = db.scalar("select id from tbl_user_privilege where UserID=" + doctor_id + " and Category='EMRCF' and Permission='E'");
            return id;
        }
        public string usr_priv_EMRCF_D(string doctor_id)
        {
            string id = db.scalar("select id from tbl_user_privilege where UserID=" + doctor_id + " and Category='EMRCF' and Permission='D'");
            return id;
        }
        //treatment plan
        public string treatment_add(string doctor_id)
        {
            string privid = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRTP' and Permission='A'");
            return privid;
        }
        public string treatment_show(string doctor_id)
        {
            string privid = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRTP' and Permission='S'");
            return privid;
        }
        //finished plan

        public string finishedtreatment_add(string doctor_id)
        {
            string privid = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRFP' and Permission='A'");
            return privid;
        }
        public string finishedtreatment_show(string doctor_id)
        {
            string privid = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRFP' and Permission='S'");
            return privid;
        }
        public string delete_privillage(string doctor_id)
        {
            string privid = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRFP' and Permission='D'");
            return privid;
        }
        //PRESCRIPTION
        public string prescription_add(string doctor_id)
        {
            string EMRFadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRP' and Permission='A'");
            return EMRFadd;
        }
        public string prescription_show(string doctor_id)
        {
            string EMRFadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRP' and Permission='S'");
            return EMRFadd;
        }
        public string edit_pres(string doctor_id)
        {
            string privid = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRP' and Permission='E'");
            return privid;
        }
        public string delete_pres(string doctor_id)
        {
            string privid = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRP' and Permission='D'");
            return privid;
        }
        //lab
        public string lab_add(string doctor_id)
        {
            string EMRFadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRL' and Permission='A'");
            return EMRFadd;
        }
        public string lab_show(string doctor_id)
        {
            string EMRFadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRL' and Permission='S'");
            return EMRFadd;
        }
        //invoice
        public string invoice_add(string doctor_id)
        {
            string EMRFadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRI' and Permission='A'");
            return EMRFadd;
        }
        public string invoice_show(string doctor_id)
        {
            string EMRFadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRI' and Permission='S'");
            return EMRFadd;
        }
        public string invoice_edit(string doctor_id)
        {
            string EMRFadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='EMRI' and Permission='E'");
            return EMRFadd;
        }
        public string invoice_delete(string doctor_id)
        {
          string  id = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category=' EMRI' and Permission='D'");
            return id;
        }
        //payments
        public string payments_add(string doctor_id)
        {
            string EMRFadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='PMT' and Permission='A'");
            return EMRFadd;
        }
        public string payments_show(string doctor_id)
        {
            string EMRFadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='PMT' and Permission='S'");
            return EMRFadd;
        }
        public string refund_add(string doctor_id)
        {
            string EMRFadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='PMTRF' and Permission='A'");
            return EMRFadd;
        }

        //inventory


        public string sales_add(string doctor_id)
        {
            string EMRFadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='Sales' and Permission='A'");
            return EMRFadd;
        }

        public string purchase_add(string doctor_id)
        {
            string EMRFadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='Purchase' and Permission='A'");
            return EMRFadd;
        }
        public string stockledger_add(string doctor_id)
        {
            string EMRFadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='StockLedger' and Permission='A'");
            return EMRFadd;
        }
        public string StockTransfer(string doctor_id)
        {
            string EMRFadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='StockTransfer' and Permission='A'");
            return EMRFadd;
        }
        public string StockAdjustment_add(string doctor_id)
        {
            string EMRFadd = db.scalar("select id from tbl_User_Privilege where UserID=" + doctor_id + " and Category='StockAdjustment' and Permission='A'");
            return EMRFadd;
        }
    }
}
