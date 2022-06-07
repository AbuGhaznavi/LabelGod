using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Npgsql;

namespace LabelMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            fillComboBoxType();
            fillComboBoxDataRate();
            fillComboBoxWDM();
            fillComboBoxSMMM();
            fillComboBoxWave();
            fillComboBoxDist();
            fillComboBoxTemp();
            fillComboBoxCompliance();
        }

        public void fillComboBoxType()
        {
            Type_CB.Items.Add("SFP");
            Type_CB.Items.Add("SFP+");
            Type_CB.Items.Add("XFP");
            Type_CB.Items.Add("SFP GPON CLASS B+");
            Type_CB.Items.Add("SFP GPON CLASS C+");
            Type_CB.Items.Add("1x9");
            Type_CB.Items.Add("CSFP");
            Type_CB.Items.Add("QSFP");
            Type_CB.Items.Add("QSFP+");
            Type_CB.Items.Add("QSFP28");
            Type_CB.Items.Add("CFP");
            Type_CB.Items.Add("CFP2");
            Type_CB.Items.Add("CFP4");
            Type_CB.Items.Add("X2");
            Type_CB.Items.Add("SFP56");
            Type_CB.Items.Add("SFP DD");
            Type_CB.Items.Add("OSFP");
        }

        private void fillComboBoxDataRate()
        {
            DataRate_CB.Items.Add("100M");
            DataRate_CB.Items.Add("1G");
            DataRate_CB.Items.Add("10G");
            DataRate_CB.Items.Add("Tri-Rate");
            DataRate_CB.Items.Add("Multi-Rate");
            DataRate_CB.Items.Add("Dual Rate");
            DataRate_CB.Items.Add("Triple Rate");
            DataRate_CB.Items.Add("155M");
            DataRate_CB.Items.Add("622M");
            DataRate_CB.Items.Add("2.5G");
            DataRate_CB.Items.Add("8G");
            DataRate_CB.Items.Add("4G");
            DataRate_CB.Items.Add("2.5G/1.2G");
            DataRate_CB.Items.Add("2x 1G");
            DataRate_CB.Items.Add("1.25G");
            DataRate_CB.Items.Add("25G");
            DataRate_CB.Items.Add("100G");
            DataRate_CB.Items.Add("40G");
            DataRate_CB.Items.Add("400G");
            DataRate_CB.Items.Add("800G");
            DataRate_CB.Items.Add("1TB");
            DataRate_CB.Items.Add("100M-2.7G");
            DataRate_CB.Items.Add("2.7G");
            DataRate_CB.Items.Add("1000 BASE-T");
            DataRate_CB.Items.Add("10/100/1000 BASE-T");
            DataRate_CB.Items.Add("10GBASE-T");
            DataRate_CB.Items.Add("1G/2G");
        }

        private void fillComboBoxWDM()
        {
            WDM_CB.Items.Add("CWDM");
            WDM_CB.Items.Add("DWDM");
        }

        public void fillComboBoxCompliance()
        {
            Compliance_CB.Items.Add("CWDM4");
            Compliance_CB.Items.Add("SWDM-40");
            Compliance_CB.Items.Add("CLR4");
            Compliance_CB.Items.Add("ER4");
            Compliance_CB.Items.Add("LR4");
            Compliance_CB.Items.Add("SR4");
            Compliance_CB.Items.Add("ZR4");
            Compliance_CB.Items.Add("SWDM4");
            Compliance_CB.Items.Add("SR4/OTU4");
            Compliance_CB.Items.Add("LR4/OTU4");
            Compliance_CB.Items.Add("ER4/OTU4");
            Compliance_CB.Items.Add("LR8");
            Compliance_CB.Items.Add("SR8");
            Compliance_CB.Items.Add("ER8");
            Compliance_CB.Items.Add("ZR8");
            Compliance_CB.Items.Add("PSM4");
            Compliance_CB.Items.Add("PSM4-IR4");
            Compliance_CB.Items.Add("4WDM-40");
        }

        private void fillComboBoxWave()
        {
            Wavelength_CB.Items.Add("850nm");
            Wavelength_CB.Items.Add("1310nm");
            Wavelength_CB.Items.Add("1550nm");
            Wavelength_CB.Items.Add("TX1310/RX1490");
            Wavelength_CB.Items.Add("TX1490/RX1310");
            Wavelength_CB.Items.Add("TX1270/RX1330");
            Wavelength_CB.Items.Add("TX1330/RX1270");
            Wavelength_CB.Items.Add("TX1490/RX1550");
            Wavelength_CB.Items.Add("TX1550/RX1490");
            Wavelength_CB.Items.Add("TX1310/RX1550");
            Wavelength_CB.Items.Add("TX1550/RX1310");
            Wavelength_CB.Items.Add("TX1590/RX1490");
            Wavelength_CB.Items.Add("TX1570/RX1490");
            Wavelength_CB.Items.Add("TX1490/TX1570");
            Wavelength_CB.Items.Add("TX1490/RX1590");
            Wavelength_CB.Items.Add("15xx.xx");
            Wavelength_CB.Items.Add("Tunable - C-BAND");
            Wavelength_CB.Items.Add("Tunable - L-BAND");
            Wavelength_CB.Items.Add("LAN WDM");
            Wavelength_CB.Items.Add("1xx0");
            Wavelength_CB.Items.Add("1270nm");
            Wavelength_CB.Items.Add("1290nm");
            Wavelength_CB.Items.Add("1330nm");
            Wavelength_CB.Items.Add("1350nm");
            Wavelength_CB.Items.Add("1370nm");
            Wavelength_CB.Items.Add("1390nm");
            Wavelength_CB.Items.Add("1410nm");
            Wavelength_CB.Items.Add("1430nm");
            Wavelength_CB.Items.Add("1450nm");
            Wavelength_CB.Items.Add("1470nm");
            Wavelength_CB.Items.Add("1490nm");
            Wavelength_CB.Items.Add("1510nm");
            Wavelength_CB.Items.Add("1530nm");
            Wavelength_CB.Items.Add("1570nm");
            Wavelength_CB.Items.Add("1590nm");
            Wavelength_CB.Items.Add("1610nm");
            Wavelength_CB.Items.Add("1563.86nm");
            Wavelength_CB.Items.Add("1563.05nm");
            Wavelength_CB.Items.Add("1562.23nm");
            Wavelength_CB.Items.Add("1561.42nm");
            Wavelength_CB.Items.Add("1560.61nm");
            Wavelength_CB.Items.Add("1559.79nm");
            Wavelength_CB.Items.Add("1558.98nm");
            Wavelength_CB.Items.Add("1558.17nm");
            Wavelength_CB.Items.Add("1557.36nm");
            Wavelength_CB.Items.Add("1556.55nm");
            Wavelength_CB.Items.Add("1555.75nm");
            Wavelength_CB.Items.Add("1554.94nm");
            Wavelength_CB.Items.Add("1554.13nm");
            Wavelength_CB.Items.Add("1553.33nm");
            Wavelength_CB.Items.Add("1552.52nm");
            Wavelength_CB.Items.Add("1551.72nm");
            Wavelength_CB.Items.Add("1550.92nm");
            Wavelength_CB.Items.Add("1550.12nm");
            Wavelength_CB.Items.Add("1549.32nm");
            Wavelength_CB.Items.Add("1548.51nm");
            Wavelength_CB.Items.Add("1547.72nm");
            Wavelength_CB.Items.Add("1546.92nm");
            Wavelength_CB.Items.Add("1546.12nm");
            Wavelength_CB.Items.Add("1545.32nm");
            Wavelength_CB.Items.Add("1544.53nm");
            Wavelength_CB.Items.Add("1543.73nm");
            Wavelength_CB.Items.Add("1542.94nm");
            Wavelength_CB.Items.Add("1542.14nm");
            Wavelength_CB.Items.Add("1541.35nm");
            Wavelength_CB.Items.Add("1540.56nm");
            Wavelength_CB.Items.Add("1539.77nm");
            Wavelength_CB.Items.Add("1538.98nm");
            Wavelength_CB.Items.Add("1538.19nm");
            Wavelength_CB.Items.Add("1537.40nm");
            Wavelength_CB.Items.Add("1536.61nm");
            Wavelength_CB.Items.Add("1535.82nm");
            Wavelength_CB.Items.Add("1535.04nm");
            Wavelength_CB.Items.Add("1534.25nm");
            Wavelength_CB.Items.Add("1533.47nm");
            Wavelength_CB.Items.Add("1532.68nm");
            Wavelength_CB.Items.Add("1531.90nm");
            Wavelength_CB.Items.Add("1531.12nm");
            Wavelength_CB.Items.Add("1530.33nm");
            Wavelength_CB.Items.Add("1529.55nm");
            Wavelength_CB.Items.Add("1528.77nm");
            Wavelength_CB.Items.Add("N/A");
        }

        private void fillComboBoxSMMM()
        {
            Mode_CB.Items.Add("SM");
            Mode_CB.Items.Add("MM");
        }

        private void fillComboBoxDist()
        {
            Distance_CB.Items.Add("2K");
            Distance_CB.Items.Add("10K");
            Distance_CB.Items.Add("15K");
            Distance_CB.Items.Add("20K");
            Distance_CB.Items.Add("30K");
            Distance_CB.Items.Add("40K");
            Distance_CB.Items.Add("50K");
            Distance_CB.Items.Add("60K");
            Distance_CB.Items.Add("70K");
            Distance_CB.Items.Add("80K");
            Distance_CB.Items.Add("95K");
            Distance_CB.Items.Add("100K");
            Distance_CB.Items.Add("30M");
            Distance_CB.Items.Add("100M");
            Distance_CB.Items.Add("120K");
            Distance_CB.Items.Add("150M");
            Distance_CB.Items.Add("220M");
            Distance_CB.Items.Add("300M");
            Distance_CB.Items.Add("400M");
            Distance_CB.Items.Add("550M");
        }

        private void fillComboBoxTemp()
        {
            Temp_CB.Items.Add("C-TEMP");
            Temp_CB.Items.Add("E-TEMP");
            Temp_CB.Items.Add("I-TEMP");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }


        private NpgsqlConnection conn;
        private NpgsqlCommand cmd;
        string connstring = String.Format("Server={0};" + "User Id={1};Database={2}; Integrated Security = True;", "US-SANDSQL-01.picstelecom.com", "jjohnson@PICSTELECOM.COM", "postgres");

        private void button1_Click(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
            cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO main (pn, type, data_rate, wdm, wavelength, sm_mm, distance, temperature) VALUES " +
                "(" + PN_TB.Text + " ,\'" + Type_CB.SelectedItem + "\' ,\'" + DataRate_CB.SelectedItem + "\' ,\'" + WDM_CB.SelectedItem + "\' ,\'" + Wavelength_CB.SelectedItem + "\' ,\'" + Mode_CB.SelectedItem + "\' ,\'" + Distance_CB.SelectedItem + "\' ,\'" + Temp_CB.SelectedItem + "\')";
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Table updated");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Fail");
                conn.Close();
            }
        }
    }
}
