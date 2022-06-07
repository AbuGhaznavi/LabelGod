using System;
using System.Windows;
using System.Windows.Controls;

namespace LabelMaker
{
    public partial class Labels : Window
    {
        public static void fillComboBoxType(ComboBox Type_CB)
        {
            Type_CB.Items.Add("SFP");
            Type_CB.Items.Add("SFP+");
            Type_CB.Items.Add("SFP28");
            Type_CB.Items.Add("XFP");
            Type_CB.Items.Add("SFP GPON CLASS B+");
            Type_CB.Items.Add("SFP GPON CLASS C+");
            Type_CB.Items.Add("1x9");
            Type_CB.Items.Add("CSFP");
            Type_CB.Items.Add("QSFP");
            Type_CB.Items.Add("QSFP+");
            Type_CB.Items.Add("QSFP28");
            Type_CB.Items.Add("QSFP56");
            Type_CB.Items.Add("CFP");
            Type_CB.Items.Add("CFP2");
            Type_CB.Items.Add("CFP4");
            Type_CB.Items.Add("X2");
        }

        public static void fillComboBoxDataRate(ComboBox DataRate_CB)
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
            DataRate_CB.Items.Add("200G");
            DataRate_CB.Items.Add("400G");
            DataRate_CB.Items.Add("800G");
            DataRate_CB.Items.Add("100M-2.7G");
            DataRate_CB.Items.Add("2.7G");
            DataRate_CB.Items.Add("1000 BASE-T");
            DataRate_CB.Items.Add("10/100/1000 BASE-T");
            DataRate_CB.Items.Add("10GBASE-T");
            DataRate_CB.Items.Add("1G/2G");
        }

        public static void fillComboBoxWDM(ComboBox WDM_CB)
        {
            WDM_CB.Items.Add("CWDM");
            WDM_CB.Items.Add("DWDM");
        }


        public static void fillComboBoxCompliance(ComboBox Compliance_CB)
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


        public static void fillComboBoxWave(ComboBox Wave_CB)
        {
            Wave_CB.Items.Add("850nm");
            Wave_CB.Items.Add("1310nm");
            Wave_CB.Items.Add("1550nm");
            Wave_CB.Items.Add("TX1310/RX1490");
            Wave_CB.Items.Add("TX1490/RX1310");
            Wave_CB.Items.Add("TX1270/RX1330");
            Wave_CB.Items.Add("TX1330/RX1270");
            Wave_CB.Items.Add("TX1490/RX1550");
            Wave_CB.Items.Add("TX1550/RX1490");
            Wave_CB.Items.Add("TX1310/RX1550");
            Wave_CB.Items.Add("TX1550/RX1310");
            Wave_CB.Items.Add("TX1590/RX1490");
            Wave_CB.Items.Add("TX1570/RX1490");
            Wave_CB.Items.Add("TX1490/TX1570");
            Wave_CB.Items.Add("TX1490/RX1590");
            Wave_CB.Items.Add("15xx.xx");
            Wave_CB.Items.Add("Tunable C-BAND");
            Wave_CB.Items.Add("Tunable L-BAND");
            Wave_CB.Items.Add("LAN WDM");
            Wave_CB.Items.Add("1xx0");
            Wave_CB.Items.Add("1270nm");
            Wave_CB.Items.Add("1290nm");
            Wave_CB.Items.Add("1330nm");
            Wave_CB.Items.Add("1350nm");
            Wave_CB.Items.Add("1370nm");
            Wave_CB.Items.Add("1390nm");
            Wave_CB.Items.Add("1410nm");
            Wave_CB.Items.Add("1430nm");
            Wave_CB.Items.Add("1450nm");
            Wave_CB.Items.Add("1470nm");
            Wave_CB.Items.Add("1490nm");
            Wave_CB.Items.Add("1510nm");
            Wave_CB.Items.Add("1530nm");
            Wave_CB.Items.Add("1570nm");
            Wave_CB.Items.Add("1590nm");
            Wave_CB.Items.Add("1610nm");
            Wave_CB.Items.Add("1563.86nm");
            Wave_CB.Items.Add("1563.05nm");
            Wave_CB.Items.Add("1562.23nm");
            Wave_CB.Items.Add("1561.42nm");
            Wave_CB.Items.Add("1560.61nm");
            Wave_CB.Items.Add("1559.79nm");
            Wave_CB.Items.Add("1558.98nm");
            Wave_CB.Items.Add("1558.17nm");
            Wave_CB.Items.Add("1557.36nm");
            Wave_CB.Items.Add("1556.55nm");
            Wave_CB.Items.Add("1555.75nm");
            Wave_CB.Items.Add("1554.94nm");
            Wave_CB.Items.Add("1554.13nm");
            Wave_CB.Items.Add("1553.33nm");
            Wave_CB.Items.Add("1552.52nm");
            Wave_CB.Items.Add("1551.72nm");
            Wave_CB.Items.Add("1550.92nm");
            Wave_CB.Items.Add("1550.12nm");
            Wave_CB.Items.Add("1549.32nm");
            Wave_CB.Items.Add("1548.51nm");
            Wave_CB.Items.Add("1547.72nm");
            Wave_CB.Items.Add("1546.92nm");
            Wave_CB.Items.Add("1546.12nm");
            Wave_CB.Items.Add("1545.32nm");
            Wave_CB.Items.Add("1544.53nm");
            Wave_CB.Items.Add("1543.73nm");
            Wave_CB.Items.Add("1542.94nm");
            Wave_CB.Items.Add("1542.14nm");
            Wave_CB.Items.Add("1541.35nm");
            Wave_CB.Items.Add("1540.56nm");
            Wave_CB.Items.Add("1539.77nm");
            Wave_CB.Items.Add("1538.98nm");
            Wave_CB.Items.Add("1538.19nm");
            Wave_CB.Items.Add("1537.40nm");
            Wave_CB.Items.Add("1536.61nm");
            Wave_CB.Items.Add("1535.82nm");
            Wave_CB.Items.Add("1535.04nm");
            Wave_CB.Items.Add("1534.25nm");
            Wave_CB.Items.Add("1533.47nm");
            Wave_CB.Items.Add("1532.68nm");
            Wave_CB.Items.Add("1531.90nm");
            Wave_CB.Items.Add("1531.12nm");
            Wave_CB.Items.Add("1530.33nm");
            Wave_CB.Items.Add("1529.55nm");
            Wave_CB.Items.Add("1528.77nm");
            Wave_CB.Items.Add("N/A");
        }

        public static void fillComboBoxSMMM(ComboBox SMMM_CB)
        {
            SMMM_CB.Items.Add("SM");
            SMMM_CB.Items.Add("MM");
        }

        public static void fillComboBoxDist(ComboBox Dist_CB)
        {
            Dist_CB.Items.Add("2K");
            Dist_CB.Items.Add("10K");
            Dist_CB.Items.Add("15K");
            Dist_CB.Items.Add("20K");
            Dist_CB.Items.Add("30K");
            Dist_CB.Items.Add("40K");
            Dist_CB.Items.Add("50K");
            Dist_CB.Items.Add("60K");
            Dist_CB.Items.Add("70K");
            Dist_CB.Items.Add("80K");
            Dist_CB.Items.Add("95K");
            Dist_CB.Items.Add("100K");
            Dist_CB.Items.Add("30M");
            Dist_CB.Items.Add("100M");
            Dist_CB.Items.Add("120K");
            Dist_CB.Items.Add("150M");
            Dist_CB.Items.Add("220M");
            Dist_CB.Items.Add("300M");
            Dist_CB.Items.Add("400M");
            Dist_CB.Items.Add("550M");
            Dist_CB.Items.Add("1M");
            Dist_CB.Items.Add("2M");
            Dist_CB.Items.Add("3M");
            Dist_CB.Items.Add("4M");
            Dist_CB.Items.Add("5M");
            Dist_CB.Items.Add("6M");
            Dist_CB.Items.Add("7M");
            Dist_CB.Items.Add("8M");
            Dist_CB.Items.Add("9M");
            Dist_CB.Items.Add("10M");
        }

        public static void fillComboBoxTemp(ComboBox Temp_CB)
        {
            Temp_CB.Items.Add("C-TEMP");
            Temp_CB.Items.Add("E-TEMP");
            Temp_CB.Items.Add("I-TEMP");
        }

        public static void fillComboBoxBreakout(ComboBox BQ_CB)
        {
            BQ_CB.Items.Add("Breakout 4x");
            BQ_CB.Items.Add("Breakout 8x");
        }

        public static void fillComboBoxSecondEnd(ComboBox SE_CB)
        {
            SE_CB.Items.Add("SFP+ 10G");
            SE_CB.Items.Add("SFP56 50G");
            SE_CB.Items.Add("QSFP+");
            SE_CB.Items.Add("QSFP28");
            SE_CB.Items.Add("QSFP56");
            SE_CB.Items.Add("QSFP-DD");
            SE_CB.Items.Add("OSFP");
        }

        public static void fillComboBoxTestLabel(ComboBox TL_CB)
        {
            TL_CB.Items.Add("SFP Label");
            TL_CB.Items.Add("SFP+ Label");
            TL_CB.Items.Add("XFP Label");
            TL_CB.Items.Add("QSFP Label");
            TL_CB.Items.Add("Copper Label");
            TL_CB.Items.Add("Outer Box Label");
        }

        public static void fillComboBoxCountry(ComboBox Country_CB)
        {
            Country_CB.Items.Add("China");
            Country_CB.Items.Add("Malaysia");
            Country_CB.Items.Add("Japan");
            Country_CB.Items.Add("Taiwan");
            Country_CB.Items.Add("Thailand");
            Country_CB.SelectedItem = "China";
        }

        public static void fillComboBoxDAC(ComboBox CABLE_CB)
        {
            CABLE_CB.Items.Add("DAC");
            CABLE_CB.Items.Add("AOC");
        }

        public static void fillComboBoxRangeType(ComboBox RT_CB)
        {

            RT_CB.Items.Add("SR4");
            RT_CB.Items.Add("SR8");
            RT_CB.Items.Add("SR10");
            RT_CB.Items.Add("LR4");
            RT_CB.Items.Add("LR4/OTU4");
            RT_CB.Items.Add("CLR4");
            RT_CB.Items.Add("CWDM4");
            RT_CB.Items.Add("ZR4");
            RT_CB.Items.Add("CR4");
            RT_CB.Items.Add("FR4");
            RT_CB.Items.Add("DR4");
            RT_CB.Items.Add("DR4+"); 
            RT_CB.Items.Add("LR8");
            RT_CB.Items.Add("COHERENT DCO");


        }
        public static void fillComboBoxLabel(ComboBox LABEL_CB)
        {
            LABEL_CB.Items.Add("1");
            LABEL_CB.Items.Add("2");
            LABEL_CB.Items.Add("4");
            LABEL_CB.Items.Add("5");
            LABEL_CB.Items.Add("6");
            LABEL_CB.Items.Add("7");
            LABEL_CB.Items.Add("8");
            LABEL_CB.Items.Add("9");
            LABEL_CB.Items.Add("10");
            LABEL_CB.Items.Add("11");
            LABEL_CB.Items.Add("12");
        }
    }
}