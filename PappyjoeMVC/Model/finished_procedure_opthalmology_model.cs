using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PappyjoeMVC.Model
{
    class finished_procedure_opthalmology_model
    {
        Connection db = new Connection();
        public void SaveDate (string glass_prescription_date, string patient_id, string Patient_name , string doctor_id, string doctor_name, string L_dist_spherical, string L_dist_cylindrical, string L_dist_axis, string l_near_spherical, string l_near_cylindrical, string l_near_axis, string l_computer_spherical, string l_computer_cylindrical, string l_computer_axis, string r_dist_spherical, string r_dist_cylindrical, string r_dist_axis,string r_near_spherical, string r_near_cylindrical, string r_near_axis, string r_computer_spherical, string r_computer_cylindrical, string r_computer_axis)
        {
            db.execute("insert into tbl_glass_prescription(glass_prescription_date,patient_id,Patient_name ,doctor_id,doctor_name,L_dist_spherical,L_dist_cylindrical,L_dist_axis,l_near_spherical,l_near_cylindrical,l_near_axis,l_computer_spherical,l_computer_cylindrical,l_computer_axis,r_dist_spherical,r_dist_cylindrical,r_dist_axis,r_near_spherical,r_near_cylindrical,r_near_axis,r_computer_spherical,r_computer_cylindrical,r_computer_axis)" +
                " values('"+ glass_prescription_date + "','" + patient_id + "','" + Patient_name + "','" + doctor_id + "','" + doctor_name + "','" + L_dist_spherical + "','" + L_dist_cylindrical + "','" + L_dist_axis + "','" + l_near_spherical + "','" + l_near_cylindrical + "','" + l_near_axis + "','" + l_computer_spherical + "','" + l_computer_cylindrical + "','" + l_computer_axis + "','" + r_dist_spherical + "','" + r_dist_cylindrical + "','" + r_dist_axis + "','"+ r_near_spherical +"','" + r_near_cylindrical + "','" + r_near_axis + "','" + r_computer_spherical + "','" + r_computer_cylindrical + "','" + r_computer_axis + "')");
        }
        public DataTable get_details(string patient)
        {
            DataTable dtb = db.table("select * from tbl_glass_prescription where patient_id='"+ patient + "'  ");
            return dtb;
        }
        public void updateData(string glassid,string glass_prescription_date, string patient_id, string Patient_name, string doctor_id, string doctor_name, string L_dist_spherical, string L_dist_cylindrical, string L_dist_axis, string l_near_spherical, string l_near_cylindrical, string l_near_axis, string l_computer_spherical, string l_computer_cylindrical, string l_computer_axis, string r_dist_spherical, string r_dist_cylindrical, string r_dist_axis, string r_near_spherical, string r_near_cylindrical, string r_near_axis, string r_computer_spherical, string r_computer_cylindrical, string r_computer_axis)
        {
            db.execute("update tbl_glass_prescription set glass_prescription_date='" + glass_prescription_date + "', patient_id=  '" + patient_id + "', Patient_name='" + Patient_name + "', doctor_id='" + doctor_id + "', doctor_name='" + doctor_name + "', L_dist_spherical='" + L_dist_spherical + "', L_dist_cylindrical='" + L_dist_cylindrical + "', L_dist_axis='" + L_dist_axis + "', l_near_spherical='" + l_near_spherical + "', l_near_cylindrical='" + l_near_cylindrical + "', l_near_axis='" + l_near_axis + "', l_computer_spherical='" + l_computer_spherical + "', l_computer_cylindrical='" + l_computer_cylindrical + "', l_computer_axis='" + l_computer_axis + "',r_dist_spherical='" + r_dist_spherical + "',r_dist_cylindrical='" + r_dist_cylindrical + "',r_dist_axis='" + r_dist_axis + "',r_near_spherical='" + r_near_spherical + "',r_near_cylindrical='" + r_near_cylindrical + "',r_near_axis='" + r_near_axis + "',r_computer_spherical='" + r_computer_spherical + "',r_computer_cylindrical='" + r_computer_cylindrical + "',r_computer_axis='" + r_computer_axis + "' where glass_prescription_date='" + glass_prescription_date + "'and  patient_id=  '" + patient_id + "' and glass_prescription_id ='" + glassid + "'");
        }
        public void delete(string patient_id,string glassid, string glass_prescription_date)
        {
            db.execute("delete from tbl_glass_prescription where patient_id='"+ patient_id + "'and glass_prescription_id='"+ glassid + "'and glass_prescription_date='" + glass_prescription_date + "'");
        }
    }
    //proves 154hufkvkvmc cchchol  ,                                                                                                 
    //    consi 280                                     zzzzzzzzz                        //     postal 50   
           

}
                                     

























































