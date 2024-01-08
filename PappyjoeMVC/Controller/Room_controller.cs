using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PappyjoeMVC.Model;

namespace PappyjoeMVC.Controller
{
    class Room_controller
    {
        Room_model mdl = new Room_model();
        public int addroom(string room)
        {
            int i = mdl.addroom(room);
            return i;
        }
        public DataTable Rooms()
        {
            DataTable dt = mdl.Rooms();
            return dt;
        }
        public DataTable search_patient(string search)
        {
            DataTable dtb = mdl.Patient_search(search); //model.search_patient(search);
            return dtb;
        }
        public DataTable get_patient_details(string newptid)
        {
            DataTable dtb = mdl.get_patient_details(newptid);
            return dtb;
        }
        public int update_room(string id, string room)
        {
            int i = mdl.update_room(id,room);
            return i;
        }
        public void delete(string id)
        {
            mdl.delete(id);
        }
    }
}
