using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PappyjoeMVC.Controller;

namespace PappyjoeMVC.View
{
    public partial class Clinical_Porforma : Form
    {
        public string patient_id = "";
        public string doctor_id = "";
        Clinical_performa_controller cntrl = new Clinical_performa_controller();
        public Clinical_Porforma()
        {
            InitializeComponent();
        }
        public string nadi = "", mala = "", mutra = "", sabda = "", swara = "", swarita = "", sparsha = "", drik = "", akruti = "", prakriti = "", sara = "", satwa = "", satmya = "", samhana = "", ahara = "", abyarana = "", jarana = "", vyayama = "", vaya = "", pramana = "", vkruti = "", dosa = "", dusya = "", roopa = "", sroto = "", nidana = "", poorva = "", rupa = "", upashaya = "", samprapti = "", sadosa = "", sadusya = "", sagni = "", srota = "", srotod = "", udbava = "", adisthana = "", vyakta = "", swabava = "", rogamarga = "";
        
        private void dgv_date_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            string did = dgv_date.Rows[r].Cells[0].Value.ToString();
            DataTable cp1 = this.cntrl.cp1(did);
            fill_1(cp1);
            DataTable cp2 = this.cntrl.cp2(did);
            fill_2(cp2);
            DataTable cp3 = this.cntrl.cp3(did);
            fill_3(cp3);
            DataTable cp4 = this.cntrl.cp4(did);
            fill_4(cp4);
            DataTable cp5 = this.cntrl.cp5(did);
            fill_5(cp5);
            DataTable cp6 = this.cntrl.cp6(did);
            fill_6(cp6);
        }
        public void fill_1(DataTable dt)
        {
            if (dt.Rows.Count>0)
            {
                txt_NAME.Text = dt.Rows[0][4].ToString();
                txt_AGE.Text = dt.Rows[0][5].ToString(); txt_SEX.Text = dt.Rows[0][6].ToString(); txt_MARITALSTATUS.Text = dt.Rows[0][7].ToString(); txt_HOWMANYCHILDREN.Text = dt.Rows[0][8].ToString(); txt_OCCUPATION.Text = dt.Rows[0][9].ToString(); txt_ADDRESS.Text = dt.Rows[0][10].ToString(); txt_DATEOFADMN.Text = dt.Rows[0][11].ToString(); txt_DATEOFDISCHARGE.Text = dt.Rows[0][12].ToString(); txt_CHIEFCOMPLAINTSWITHDURATION.Text = dt.Rows[0][13].ToString(); txt_ASSOCIATEDCOMPLAINTS.Text = dt.Rows[0][14].ToString(); txt_HISTORYOFPRESENTILLNESS.Text = dt.Rows[0][15].ToString(); txt_FAMILYANDSOCIALHISTORY.Text = dt.Rows[0][16].ToString(); txt_PREVIOUSMEDICALHISTORY.Text = dt.Rows[0][17].ToString(); txt_DRUGHISTORY.Text = dt.Rows[0][18].ToString(); txt_PERSONALHISTORY.Text = dt.Rows[0][19].ToString(); txt_FOOD.Text = dt.Rows[0][20].ToString(); txt_HABITS.Text = dt.Rows[0][21].ToString(); txt_ACTIVITIES.Text = dt.Rows[0][22].ToString(); txt_MENSTRUALHISTORY.Text = dt.Rows[0][23].ToString();
            }
        }
        public void fill_2(DataTable dt)
        {
            if (dt.Rows.Count>0)
            {
                txt_decubitus.Text = dt.Rows[0][4].ToString(); txt_built.Text = dt.Rows[0][5].ToString(); txt_nutrition.Text = dt.Rows[0][6].ToString(); txt_facies.Text = dt.Rows[0][7].ToString(); txt_pallor.Text = dt.Rows[0][8].ToString(); txt_cyanosis.Text = dt.Rows[0][9].ToString(); txt_icterus.Text = dt.Rows[0][10].ToString(); txt_lymphnodes.Text = dt.Rows[0][11].ToString(); txt_pigmentation.Text = dt.Rows[0][12].ToString(); txt_eodema.Text = dt.Rows[0][13].ToString(); txt_clubbing.Text = dt.Rows[0][14].ToString(); txt_temperature.Text = dt.Rows[0][15].ToString(); txt_pulse.Text = dt.Rows[0][16].ToString(); txt_resprate.Text = dt.Rows[0][17].ToString(); txt_bp.Text = dt.Rows[0][18].ToString(); txt_headneck.Text = dt.Rows[0][19].ToString(); txt_armspalms.Text = dt.Rows[0][20].ToString(); txt_thorax.Text = dt.Rows[0][21].ToString(); txt_abdomen.Text = dt.Rows[0][22].ToString(); txt_lowerlimbs.Text = dt.Rows[0][23].ToString();
            }
        }
        public void fill_3(DataTable dt)
        {
            if (dt.Rows.Count>0)
            {
                if (dt.Rows[0][4].ToString() != "")
                {
                    cb_nadi.Checked = true;
                }
                if (dt.Rows[0][5].ToString() != "")
                {
                    cb_mala.Checked = true;
                }
                if (dt.Rows[0][6].ToString() != "")
                {
                    cb_mutra.Checked = true;
                }
                if (dt.Rows[0][7].ToString() != "")
                {
                    cb_sabda.Checked = true;
                }
                if (dt.Rows[0][8].ToString() != "")
                {
                    cb_swara.Checked = true;
                }
                if (dt.Rows[0][9].ToString() != "")
                {
                    cb_swarita.Checked = true;
                }
                if (dt.Rows[0][10].ToString() != "")
                {
                    cb_sparsha.Checked = true;
                }
                if (dt.Rows[0][11].ToString() != "")
                {
                    cb_drik.Checked = true;
                }
                if (dt.Rows[0][12].ToString() != "")
                {
                    cb_akruti.Checked = true;
                }
                if (dt.Rows[0][13].ToString() != "")
                {
                    cb_prakriti.Checked = true;
                }
                if (dt.Rows[0][14].ToString() != "")
                {
                    cb_sara.Checked = true;
                }
                if (dt.Rows[0][15].ToString() != "")
                {
                    cb_satwa.Checked = true;
                }
                if (dt.Rows[0][16].ToString() != "")
                {
                    cb_satmya.Checked = true;
                }
                if (dt.Rows[0][17].ToString() != "")
                {
                    cb_samhana.Checked = true;
                }
                if (dt.Rows[0][18].ToString() != "")
                {
                    cb_ahara.Checked = true;
                }
                if (dt.Rows[0][19].ToString() != "")
                {
                    cb_abyarana.Checked = true;
                }
                if (dt.Rows[0][20].ToString() != "")
                {
                    cb_jarana.Checked = true;
                }
                if (dt.Rows[0][21].ToString() != "")
                {
                    cb_vyayama.Checked = true;
                }
                if (dt.Rows[0][22].ToString() != "")
                {
                    cb_vaya.Checked = true;
                }
                if (dt.Rows[0][23].ToString() != "")
                {
                    cb_pramana.Checked = true;
                }
                if (dt.Rows[0][24].ToString() != "")
                {
                    cb_vkruti.Checked = true;
                }
                if (dt.Rows[0][25].ToString() != "")
                {
                    cb_dosa.Checked = true;
                }
                if (dt.Rows[0][26].ToString() != "")
                {
                    cb_dusya.Checked = true;
                }
                if (dt.Rows[0][27].ToString() != "")
                {
                    cb_roopa.Checked = true;
                }
                if (dt.Rows[0][28].ToString() != "")
                {
                    cb_sroto.Checked = true;
                }
                if (dt.Rows[0][29].ToString() != "")
                {
                    cb_nidana.Checked = true;
                }
                if (dt.Rows[0][30].ToString() != "")
                {
                    cb_poorva.Checked = true;
                }
                if (dt.Rows[0][31].ToString() != "")
                {
                    cb_rupa.Checked = true;
                }
                if (dt.Rows[0][32].ToString() != "")
                {
                    cb_upashaya.Checked = true;
                }
                if (dt.Rows[0][33].ToString() != "")
                {
                    cb_samprapti.Checked = true;
                }
                if (dt.Rows[0][34].ToString() != "")
                {
                    cb_sadosa.Checked = true;
                }
                if (dt.Rows[0][35].ToString() != "")
                {
                    cb_sadusya.Checked = true;
                }
                if (dt.Rows[0][36].ToString() != "")
                {
                    cb_sagni.Checked = true;
                }
                if (dt.Rows[0][37].ToString() != "")
                {
                    cb_srota.Checked = true;
                }
                if (dt.Rows[0][38].ToString() != "")
                {
                    cb_srotod.Checked = true;
                }
                if (dt.Rows[0][39].ToString() != "")
                {
                    cb_udbava.Checked = true;
                }
                if (dt.Rows[0][40].ToString() != "")
                {
                    cb_adisthana.Checked = true;
                }
                if (dt.Rows[0][41].ToString() != "")
                {
                    cb_vyakta.Checked = true;
                }
                if (dt.Rows[0][42].ToString() != "")
                {
                    cb_swabava.Checked = true;
                }
                if (dt.Rows[0][43].ToString() != "")
                {
                    cb_rogamarga.Checked = true;
                }
            }
        }
        public void fill_4(DataTable dt)
        {
            if (dt.Rows.Count>0)
            {
                txt_inspec4.Text = dt.Rows[0][4].ToString(); txt_shape4.Text = dt.Rows[0][5].ToString(); txt_sprviens4.Text = dt.Rows[0][6].ToString(); txt_obvsblgng4.Text = dt.Rows[0][7].ToString(); txt_wdthcstlangles4.Text = dt.Rows[0][8].ToString(); txt_mvmntwthresprtn4.Text = dt.Rows[0][9].ToString(); txt_palpatin4.Text = dt.Rows[0][10].ToString(); txt_apx4.Text = dt.Rows[0][11].ToString(); txt_ausc4.Text = dt.Rows[0][12].ToString(); txt_auscarea4.Text = dt.Rows[0][13].ToString(); txt_mitr4.Text = dt.Rows[0][14].ToString(); txt_tric4.Text = dt.Rows[0][15].ToString(); txt_pulmo4.Text = dt.Rows[0][16].ToString(); txt_aort4.Text = dt.Rows[0][17].ToString(); txt_hrtsnd4.Text = dt.Rows[0][18].ToString(); txt_murm4.Text = dt.Rows[0][19].ToString(); txt_pecsn4.Text = dt.Rows[0][20].ToString(); txt_cntr4.Text = dt.Rows[0][21].ToString(); txt_skn4.Text = dt.Rows[0][22].ToString(); txt_umbli4.Text = dt.Rows[0][23].ToString(); txt_mov4.Text = dt.Rows[0][24].ToString(); txt_vsb4.Text = dt.Rows[0][25].ToString(); txt_supr4.Text = dt.Rows[0][26].ToString(); txt_coonstncy.Text = dt.Rows[0][27].ToString(); txt_tendr4.Text = dt.Rows[0][28].ToString(); txt_anyloc4.Text = dt.Rows[0][29].ToString(); txt_fluid4.Text = dt.Rows[0][30].ToString(); txt_lvr4.Text = dt.Rows[0][31].ToString(); txt_gll4.Text = dt.Rows[0][32].ToString(); txt_spleen4.Text = dt.Rows[0][33].ToString(); txt_kidn4.Text = dt.Rows[0][34].ToString(); txt_aus4.Text = dt.Rows[0][35].ToString();
            }
        }
        public void fill_5(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                txt_perstlsnds4.Text = dt.Rows[0][4].ToString(); txt_percssn4.Text = dt.Rows[0][5].ToString(); txt_shiftnfdllns4.Text = dt.Rows[0][6].ToString(); txt_resp4.Text = dt.Rows[0][7].ToString(); txt_insp4.Text = dt.Rows[0][8].ToString(); txt_size4.Text = dt.Rows[0][9].ToString(); txt_shpe4.Text = dt.Rows[0][10].ToString(); txt_symm4.Text = dt.Rows[0][11].ToString(); txt_respi4.Text = dt.Rows[0][12].ToString(); txt__obv4.Text = dt.Rows[0][13].ToString(); txt_palp4.Text = dt.Rows[0][14].ToString(); txt_cent4.Text = dt.Rows[0][15].ToString(); txt_devi4.Text = dt.Rows[0][16].ToString(); txt_voc4.Text = dt.Rows[0][17].ToString(); txt_crse4.Text = dt.Rows[0][18].ToString(); txt_auscl4.Text = dt.Rows[0][19].ToString(); txt_brth4.Text = dt.Rows[0][20].ToString(); txt_vocres4.Text = dt.Rows[0][21].ToString(); txt_adv4.Text = dt.Rows[0][22].ToString(); txt_percss4.Text = dt.Rows[0][23].ToString(); txt_rurogentl4.Text = dt.Rows[0][24].ToString(); txt_insp5.Text = dt.Rows[0][25].ToString(); txt_palp5.Text = dt.Rows[0][26].ToString(); txt_nerv5.Text = dt.Rows[0][24].ToString(); txt_hghr5.Text = dt.Rows[0][28].ToString(); txt_cons5.Text = dt.Rows[0][29].ToString(); txt_intel5.Text = dt.Rows[0][30].ToString(); txt_memry5.Text = dt.Rows[0][31].ToString(); txt_cranil5.Text = dt.Rows[0][32].ToString(); txt_motr5.Text = dt.Rows[0][33].ToString(); txt_inspira5.Text = dt.Rows[0][34].ToString(); txt_bulk5.Text = dt.Rows[0][35].ToString(); txt_presnce5.Text = dt.Rows[0][36].ToString(); txt_tone5.Text = dt.Rows[0][37].ToString(); txt_power5.Text = dt.Rows[0][38].ToString(); txt_cord5.Text = dt.Rows[0][39].ToString(); txt_plp5.Text = dt.Rows[0][40].ToString(); txt_quant5.Text = dt.Rows[0][41].ToString(); txt_gradtn5.Text = dt.Rows[0][42].ToString(); txt_totl5.Text = dt.Rows[0][43].ToString(); txt_grade1.Text = dt.Rows[0][44].ToString(); txt_grde2.Text = dt.Rows[0][45].ToString(); txt_grde3.Text = dt.Rows[0][46].ToString(); txt_grde4.Text = dt.Rows[0][47].ToString(); txt_grde5.Text = dt.Rows[0][48].ToString(); txt_reflx.Text = dt.Rows[0][49].ToString(); txt_deeptndn5.Text = dt.Rows[0][50].ToString();
            }
        }
        public void fill_6(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                txt_bicep6.Text = dt.Rows[0][4].ToString(); txt_supina5.Text = dt.Rows[0][5].ToString(); txt_tricep5.Text = dt.Rows[0][6].ToString(); txt_knee5.Text = dt.Rows[0][7].ToString(); txt_angl5.Text = dt.Rows[0][8].ToString(); txt_suprfc5.Text = dt.Rows[0][9].ToString(); txt_plantarresponse.Text = dt.Rows[0][10].ToString(); txt_abdo6.Text = dt.Rows[0][11].ToString(); txt_musc6.Text = dt.Rows[0][12].ToString(); txt_insp6.Text = dt.Rows[0][13].ToString(); txt_palp6.Text = dt.Rows[0][14].ToString(); txt_aus6.Text = dt.Rows[0][15].ToString(); txt_percu6.Text = dt.Rows[0][16].ToString(); txt_kneerl.Text = dt.Rows[0][17].ToString(); txt_inspi6.Text = dt.Rows[0][18].ToString(); txt_swell6.Text = dt.Rows[0][19].ToString(); txt_red6.Text = dt.Rows[0][20].ToString(); txt_palpatn6.Text = dt.Rows[0][21].ToString(); txt_tender6.Text = dt.Rows[0][22].ToString(); txt_pittng6.Text = dt.Rows[0][23].ToString(); txt_palpa6.Text = dt.Rows[0][24].ToString(); txt_temp6.Text = dt.Rows[0][25].ToString(); txt_hemoto6.Text = dt.Rows[0][26].ToString(); txt_serolo6.Text = dt.Rows[0][27].ToString(); txt_bioch6.Text = dt.Rows[0][28].ToString(); txt_urin6.Text = dt.Rows[0][29].ToString(); txt_radiolog6.Text = dt.Rows[0][30].ToString(); txt_othrreclnt.Text = dt.Rows[0][31].ToString(); txt_diffrntialdia6.Text = dt.Rows[0][32].ToString(); txt_prov6.Text = dt.Rows[0][33].ToString(); txt_finaldiag6.Text = dt.Rows[0][34].ToString();
            }
        }

        private void Clinical_Porforma_Load(object sender, EventArgs e)
        {
            load();
        }

        public void load()
        {
            DataTable date = this.cntrl.date(patient_id);
            if (date.Rows.Count > 0)
            {
                dgv_date.Rows.Clear();
                int row = 0;
                foreach (DataRow dr in date.Rows)
                {
                    dgv_date.Rows.Add();
                    dgv_date.Rows[row].Cells["id"].Value = dr["id"].ToString();
                    dgv_date.Rows[row].Cells["date"].Value = dr["date"].ToString();
                    row++;
                }
            }
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_NAME.Text != "")
                {
                    int i = this.cntrl.add_pnl1(patient_id, doctor_id, DateTime.Now.ToString(), txt_NAME.Text, txt_AGE.Text, txt_SEX.Text, txt_MARITALSTATUS.Text, txt_HOWMANYCHILDREN.Text, txt_OCCUPATION.Text, txt_ADDRESS.Text, txt_DATEOFADMN.Text, txt_DATEOFDISCHARGE.Text, txt_CHIEFCOMPLAINTSWITHDURATION.Text, txt_ASSOCIATEDCOMPLAINTS.Text, txt_HISTORYOFPRESENTILLNESS.Text, txt_FAMILYANDSOCIALHISTORY.Text, txt_PREVIOUSMEDICALHISTORY.Text, txt_DRUGHISTORY.Text, txt_PERSONALHISTORY.Text, txt_FOOD.Text, txt_HABITS.Text, txt_ACTIVITIES.Text, txt_MENSTRUALHISTORY.Text);
                    int j = this.cntrl.add_pnl2(patient_id, doctor_id, DateTime.Now.ToString(), txt_decubitus.Text, txt_built.Text, txt_nutrition.Text, txt_facies.Text, txt_pallor.Text, txt_cyanosis.Text, txt_icterus.Text, txt_lymphnodes.Text, txt_pigmentation.Text, txt_eodema.Text, txt_clubbing.Text, txt_temperature.Text, txt_pulse.Text, txt_resprate.Text, txt_bp.Text, txt_headneck.Text, txt_armspalms.Text, txt_thorax.Text, txt_abdomen.Text, txt_lowerlimbs.Text);
                    if (cb_nadi.Checked == true)
                    {
                        nadi = "nadi";
                    }
                    if (cb_mala.Checked == true)
                    {
                        mala = "mala";
                    }
                    if (cb_mutra.Checked == true)
                    {
                        mutra = "mutra";
                    }
                    if (cb_sabda.Checked == true)
                    {
                        sabda = "sabda";
                    }
                    if (cb_swara.Checked == true)
                    {
                        swara = "swara";
                    }
                    if (cb_swarita.Checked == true)
                    {
                        swarita = "swarita";
                    }
                    if (cb_sparsha.Checked == true)
                    {
                        sparsha = "sparsha";
                    }
                    if (cb_drik.Checked == true)
                    {
                        drik = "drik";
                    }
                    if (cb_akruti.Checked == true)
                    {
                        akruti = "akruti";
                    }
                    if (cb_prakriti.Checked == true)
                    {
                        prakriti = "prakriti";
                    }
                    if (cb_sara.Checked == true)
                    {
                        sara = "sara";
                    }
                    if (cb_satwa.Checked == true)
                    {
                        satwa = "satwa";
                    }
                    if (cb_satmya.Checked == true)
                    {
                        satmya = "satmya";
                    }
                    if (cb_samhana.Checked == true)
                    {
                        samhana = "samhana";
                    }
                    if (cb_ahara.Checked == true)
                    {
                        ahara = "ahara";
                    }
                    if (cb_abyarana.Checked == true)
                    {
                        abyarana = "abyarana";
                    }
                    if (cb_jarana.Checked == true)
                    {
                        jarana = "jarana";
                    }
                    if (cb_vyayama.Checked == true)
                    {
                        vyayama = "vyayama";
                    }
                    if (cb_vaya.Checked == true)
                    {
                        vaya = "vaya";
                    }
                    if (cb_pramana.Checked == true)
                    {
                        pramana = "pramana";
                    }
                    if (cb_vkruti.Checked == true)
                    {
                        vkruti = "vkruti";
                    }
                    if (cb_dosa.Checked == true)
                    {
                        dosa = "dosa";
                    }
                    if (cb_dusya.Checked == true)
                    {
                        dusya = "dusya";
                    }
                    if (cb_roopa.Checked == true)
                    {
                        roopa = "roopa";
                    }
                    if (cb_sroto.Checked == true)
                    {
                        sroto = "sroto";
                    }
                    if (cb_nidana.Checked == true)
                    {
                        nidana = "nidana";
                    }
                    if (cb_poorva.Checked == true)
                    {
                        poorva = "poorva";
                    }
                    if (cb_rupa.Checked == true)
                    {
                        rupa = "rupa";
                    }
                    if (cb_upashaya.Checked == true)
                    {
                        upashaya = "upashaya";
                    }
                    if (cb_samprapti.Checked == true)
                    {
                        samprapti = "samprapti";
                    }
                    if (cb_sadosa.Checked == true)
                    {
                        sadosa = "sadosa";
                    }
                    if (cb_sadusya.Checked == true)
                    {
                        sadusya = "sadusya";
                    }
                    if (cb_sagni.Checked == true)
                    {
                        sagni = "sagni";
                    }
                    if (cb_srota.Checked == true)
                    {
                        srota = "srota";
                    }
                    if (cb_srotod.Checked == true)
                    {
                        srotod = "srotod";
                    }
                    if (cb_udbava.Checked == true)
                    {
                        udbava = "udbava";
                    }
                    if (cb_adisthana.Checked == true)
                    {
                        adisthana = "adisthana";
                    }
                    if (cb_vyakta.Checked == true)
                    {
                        vyakta = "vyakta";
                    }
                    if (cb_swabava.Checked == true)
                    {
                        swabava = "swabava";
                    }
                    if (cb_rogamarga.Checked == true)
                    {
                        rogamarga = "rogamarga";
                    }
                    int k = this.cntrl.add_pnl3(patient_id, doctor_id, DateTime.Now.ToString(), nadi, mala, mutra, sabda, swara, swarita, sparsha, drik, akruti, prakriti, sara, satwa, satmya, samhana, ahara, abyarana, jarana, vyayama, vaya, pramana, vkruti, dosa, dusya, roopa, sroto, nidana, poorva, rupa, upashaya, samprapti, sadosa, sadusya, sagni, srota, srotod, udbava, adisthana, vyakta, swabava, rogamarga);
                    int l = this.cntrl.add_pnl4(patient_id, doctor_id, DateTime.Now.ToString(), txt_inspec4.Text, txt_shape4.Text, txt_sprviens4.Text, txt_obvsblgng4.Text, txt_wdthcstlangles4.Text, txt_mvmntwthresprtn4.Text, txt_palpatin4.Text, txt_apx4.Text, txt_ausc4.Text, txt_auscarea4.Text, txt_mitr4.Text, txt_tric4.Text, txt_pulmo4.Text, txt_aort4.Text, txt_hrtsnd4.Text, txt_murm4.Text, txt_pecsn4.Text, txt_cntr4.Text, txt_skn4.Text, txt_umbli4.Text, txt_mov4.Text, txt_vsb4.Text, txt_supr4.Text, txt_coonstncy.Text, txt_tendr4.Text, txt_anyloc4.Text, txt_fluid4.Text, txt_lvr4.Text, txt_gll4.Text, txt_spleen4.Text, txt_kidn4.Text, txt_aus4.Text);
                    int m = this.cntrl.add_pnl5(patient_id, doctor_id, DateTime.Now.ToString(), txt_perstlsnds4.Text, txt_percssn4.Text, txt_shiftnfdllns4.Text, txt_resp4.Text, txt_insp4.Text, txt_size4.Text, txt_shpe4.Text, txt_symm4.Text, txt_respi4.Text, txt__obv4.Text, txt_palp4.Text, txt_cent4.Text, txt_devi4.Text, txt_voc4.Text, txt_crse4.Text, txt_auscl4.Text, txt_brth4.Text, txt_vocres4.Text, txt_adv4.Text, txt_percss4.Text, txt_rurogentl4.Text, txt_insp5.Text, txt_palp5.Text, txt_nerv5.Text, txt_hghr5.Text, txt_cons5.Text, txt_intel5.Text, txt_memry5.Text, txt_cranil5.Text, txt_motr5.Text, txt_inspira5.Text, txt_bulk5.Text, txt_presnce5.Text, txt_tone5.Text, txt_power5.Text, txt_cord5.Text, txt_plp5.Text, txt_quant5.Text, txt_gradtn5.Text, txt_totl5.Text, txt_grade1.Text, txt_grde2.Text, txt_grde3.Text, txt_grde4.Text, txt_grde5.Text, txt_reflx.Text, txt_deeptndn5.Text);
                    int n = this.cntrl.add_pnl6(patient_id, doctor_id, DateTime.Now.ToString(), txt_bicep6.Text, txt_supina5.Text, txt_tricep5.Text, txt_knee5.Text, txt_angl5.Text, txt_suprfc5.Text, txt_plantarresponse.Text, txt_abdo6.Text, txt_musc6.Text, txt_insp6.Text, txt_palp6.Text, txt_aus6.Text, txt_percu6.Text, txt_kneerl.Text, txt_inspi6.Text, txt_swell6.Text, txt_red6.Text, txt_palpatn6.Text, txt_tender6.Text, txt_pittng6.Text, txt_palpa6.Text, txt_temp6.Text, txt_hemoto6.Text, txt_serolo6.Text, txt_bioch6.Text, txt_urin6.Text, txt_radiolog6.Text, txt_othrreclnt.Text, txt_diffrntialdia6.Text, txt_prov6.Text, txt_finaldiag6.Text);
                    MessageBox.Show("successfully saved the data", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    load();
                }
                else
                {
                    MessageBox.Show("Please fill the fields", "Data missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error !...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
