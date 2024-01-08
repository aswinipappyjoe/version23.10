using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PappyjoeMVC.Model
{
    class Room_model
    {
        Connection db = new Connection();

        public int addroom(string rooms)
        {
            int i = db.execute("insert into tbl_room(room_no)values('" + rooms + "')");
            return i;
        }
        public DataTable Rooms()
        {
            DataTable dt = db.table("select * from tbl_room");
            return dt;
        }
        public DataTable booked_rooms()
        {
            DataTable dt = db.table("select * from tbl_room_details w");
            return dt;
        }
        public DataTable Patient_search(string _Patientid)
        {
            DataTable dtdr = db.table("select id, CONCAT(pt_name, ', ', substring(gender,1,1), ', ',primary_mobile_number) as patient from tbl_patient where (pt_name like '" + _Patientid + "%'   or pt_id like '" + _Patientid + "%' or primary_mobile_number like '" + _Patientid + "%') and Profile_Status = 'Active' order by pt_name");
            return dtdr;
        }
        public DataTable get_patient_details(string newptid)
        {
            DataTable dtb = db.table("select id,pt_id,pt_name,primary_mobile_number from tbl_patient where id='" + newptid + "'");
            return dtb;
        }
        public int update_room(string id,string room )
        {
            int i = db.execute("update tbl_room set room_no='"+room+"' where id='"+id+"'");
            return i;
        }
        public void delete(string id)
        {
            db.execute("delete from tbl_room where id='" + id + "'");
        }
    }
}
