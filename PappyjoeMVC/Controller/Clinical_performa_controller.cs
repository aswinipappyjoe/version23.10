using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PappyjoeMVC.Model;

namespace PappyjoeMVC.Controller
{
    class Clinical_performa_controller
    {
        Clinical_performa_model mdl = new Clinical_performa_model();
        public int add_pnl1(string ptid, string drid, string date,string name, string age, string sex, string marital, string child, string occupation, string address, string dateadmissn, string datedis, string cheifcomp, string asscomp, string history, string famhis, string prehis, string drug, string pers, string food, string habit, string activity, string menscycle)
        {
            int i = mdl.add_pnl1( ptid,  drid, date, name,  age,  sex,  marital,  child,  occupation,  address,  dateadmissn,  datedis,  cheifcomp,  asscomp,  history,  famhis,  prehis,  drug,  pers,  food,  habit,  activity,  menscycle);
            return i;
        }
        public int add_pnl2(string pt_id, string dr_id, string date, string decubitus, string built, string nutrition, string facies, string pallor, string cyanosis, string icterus, string lymph_nodes, string pigmentation, string oedema, string clubbing, string temperature, string pulse, string resp, string bp, string head, string arms, string thorax, string abdomen, string lower)
        {
            int i = mdl.add_pnl2( pt_id,  dr_id, date, decubitus,  built,  nutrition,  facies,  pallor,  cyanosis,  icterus,  lymph_nodes,  pigmentation,  oedema,  clubbing,  temperature,  pulse,  resp,  bp,  head,  arms,  thorax,  abdomen,  lower);
            return i;
        }
        public int add_pnl3(string pt_id, string dr_id, string date, string nadi, string mala, string mutra, string sabda, string swara, string swaritara, string sparsha, string drik, string akruti, string prakruti, string sara, string satwa, string satmya, string samhanana, string ahara, string abyarana, string jarana, string vyayama, string vaya, string pramana, string vikruti, string dosa, string dusya, string roopa, string sroto, string nidana, string poorva, string nroopa, string upashaya, string samprupti, string sdosha, string sdusya, string sagni, string ssrota, string asroto, string udbhava, string adisthana, string vyaktha, string swabhava, string rogamarga)
        {
            int i = mdl.add_pnl3( pt_id,  dr_id, date, nadi,  mala,  mutra,  sabda,  swara,  swaritara,  sparsha,  drik,  akruti,  prakruti,  sara,  satwa,  satmya,  samhanana,  ahara,  abyarana,  jarana,  vyayama,  vaya,  pramana,  vikruti,  dosa,  dusya,  roopa,  sroto,  nidana,  poorva,  nroopa,  upashaya,  samprupti,  sdosha,  sdusya,  sagni,  ssrota,  asroto,  udbhava,  adisthana,  vyaktha,  swabhava,  rogamarga);
            return i;
        }
        public int add_pnl4(string pt_id, string dr_id, string date, string inspection, string shape, string veins, string bulging, string width, string movement, string palpation, string apex, string ausculation, string auscu_areas, string mitral, string tricuspio, string pulmonary, string aortal, string s1s2, string murmur, string percussion, string contour, string skin, string umblical, string movmnt, string visible, string superficl, string consistncy, string tendernss, string any, string fluid, string liver, string gall, string spleen, string kidney, string auscultation)
        {
            int i = mdl.add_pnl4( pt_id,  dr_id, date, inspection,  shape,  veins,  bulging,  width,  movement,  palpation,  apex,  ausculation,  auscu_areas,  mitral,  tricuspio,  pulmonary,  aortal,  s1s2,  murmur,  percussion,  contour,  skin,  umblical,  movmnt,  visible,  superficl,  consistncy,  tendernss,  any,  fluid,  liver,  gall,  spleen,  kidney,  auscultation);
            return i;
        }
        public int add_pnl5(string pt_id, string dr_id, string date, string peristaltic, string percussion, string shifting, string respiratry, string infection, string size, string shape, string symmetry, string respmoments, string obvswelling, string palpation, string centtrally, string deviated, string vocal, string course, string ausc, string breath, string voca, string adventitious, string percu, string rurogeni, string inspect, string palpa, string nerv, string highfunc, string conciuos, string intelle, string memry, string cranial, string motor, string inspec, string bulk, string presence, string mustone, string power, string cordi, string palp, string quanta, string gradation, string grade0, string grade1, string grade2, string grade3, string grade4, string grade5, string reflex, string deep_tendon)
        {
            int i = mdl.add_pnl5( pt_id,  dr_id, date, peristaltic,  percussion,  shifting,  respiratry,  infection,  size,  shape,  symmetry,  respmoments,  obvswelling,  palpation,  centtrally,  deviated,  vocal,  course,  ausc,  breath,  voca,  adventitious,  percu,  rurogeni,  inspect,  palpa,  nerv,  highfunc,  conciuos,  intelle,  memry,  cranial,  motor,  inspec,  bulk,  presence,  mustone,  power,  cordi,  palp,  quanta,  gradation,  grade0,  grade1,  grade2,  grade3,  grade4,  grade5,  reflex,  deep_tendon);
            return i;
        }
        public int add_pnl6(string pt_id, string dr_id, string date, string bicep, string supina, string tricep, string knee, string angle, string superref, string plantarref, string abdoref, string musculo, string inspec, string palpat, string auscu, string percu, string kneeex, string inspection, string swelling, string redness, string palpation, string tender, string pitting, string palpable, string temper, string heamo, string sero, string bio, string urin, string radio, string other, string diffrent, string provisional, string final)
        {
            int i = mdl.add_pnl6( pt_id,  dr_id, date, bicep,  supina,  tricep,  knee,  angle,  superref,  plantarref,  abdoref,  musculo,  inspec,  palpat,  auscu,  percu,  kneeex,  inspection,  swelling,  redness,  palpation,  tender,  pitting,  palpable,  temper,  heamo,  sero,  bio,  urin,  radio,  other,  diffrent,  provisional,  final);
            return i;
        }
        public DataTable date(string ptid)
        {
            DataTable dt = mdl.date(ptid);
            return dt;
        }
        public DataTable cp1(string id)
        {
            DataTable dt = mdl.cp1(id);
            return dt;
        }
        public DataTable cp2(string id)
        {
            DataTable dt = mdl.cp2(id);
            return dt;
        }
        public DataTable cp3(string id)
        {
            DataTable dt = mdl.cp3(id);
            return dt;
        }
        public DataTable cp4(string id)
        {
            DataTable dt = mdl.cp4(id);
            return dt;
        }
        public DataTable cp5(string id)
        {
            DataTable dt = mdl.cp5(id);
            return dt;
        }
        public DataTable cp6(string id)
        {
            DataTable dt = mdl.cp6(id);
            return dt;
        }
    }
}
