using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PappyjoeMVC.View
{
    public partial class ListItem : UserControl
    {
        public ListItem()
        {
            InitializeComponent();
        }

        private int id;
        private string room;
        private string status;
        private string patient;

        public int RoomId
        {
            get { return id; }
            set { id = RoomId; lb_roomid.Text = value.ToString(); }
        }

        public string Room
        {
            get { return room; }
            set { room = value; lb_room.Text = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; lb_status.Text = value; }
        }

        public string Patient
        {
            get { return patient; }
            set { patient = value; lb_patient.Text = value; }
        }
    }
}
