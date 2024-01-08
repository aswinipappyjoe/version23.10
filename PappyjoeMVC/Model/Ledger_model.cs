using System;
using System.Data;

namespace PappyjoeMVC.Model
{
    public class Ledger_model
    {
        Connection db = new Connection();
        public DataTable LedgerInvoice(string patient_id)
        {
            DataTable dtb = db.table("select * from tbl_invoices where pt_id='" + patient_id + "' ");
            return dtb;
        }
        public DataTable LedgerPay(string patient_id)
        {
            DataTable dtb = db.table("select advance,receipt_no,payment_date,invoice_no,amount_paid,refund_amount,refund_status,total from tbl_payment where pt_id='" + patient_id + "' AND payment_date!='' order by payment_date desc");
            return dtb;
        }
        public DataTable dt_advance(/*string date*/)
        {
            DataTable dt = db.table("select a.pt_id,a.date,a.amount,a.paymentmethod,a.credit_debit,p.pt_name from tbl_advance a left join tbl_patient p on p.id=a.pt_id   ");//where  a.date ='" + Convert.ToDateTime(date).ToString("yyyy-MM-dd") + "'
            return dt;
        }
        public DataTable getall_advance(string pt_id)
        {
            DataTable dtb = db.table("select Date,Amount,PaymentMethod,form from tbl_advance where Pt_id ='" + pt_id + "' and Credit_Debit = 'Debit'");
            return dtb;
        }
        public DataTable Adv_return(string pt_id)
        {
            DataTable dtb = db.table("select Date,Amount,PaymentMethod,form from tbl_advance where Pt_id	='" + pt_id + "' and Credit_Debit='credit'");
            return dtb;
        }
    }
}
