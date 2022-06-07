using System.Windows;

namespace LabelMaker
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Adds appropriate values to all combo boxes
        /// </summary>
        private void fillAllComboBoxes()
        {
            fillComboBoxType();
            fillComboBoxDataRate();
            fillComboBoxWDM();
            fillComboBoxWave();
            fillComboBoxCompliance();
            fillComboBoxSMMM();
            fillComboBoxDist();
            fillComboBoxTemp();
            fillComboBoxBreakout();
            fillComboBoxSecondEnd();
            fillComboBoxTestLabel();
            fillComboBoxCountry();
            fillComboBoxSearch();
        }

        private void fillComboBoxType()
        {
            Type_comboBox.Items.Add("SFP");
            Type_comboBox.Items.Add("SFP+");
            Type_comboBox.Items.Add("XFP");
            Type_comboBox.Items.Add("SFP GPON CLASS B+");
            Type_comboBox.Items.Add("SFP GPON CLASS C+");
            Type_comboBox.Items.Add("1x9");
            Type_comboBox.Items.Add("CSFP");
            Type_comboBox.Items.Add("QSFP");
            Type_comboBox.Items.Add("QSFP+");
            Type_comboBox.Items.Add("QSFP28");
            Type_comboBox.Items.Add("QSFP56");
            Type_comboBox.Items.Add("CFP");
            Type_comboBox.Items.Add("CFP2");
            Type_comboBox.Items.Add("CFP4");
            Type_comboBox.Items.Add("X2");
            Type_comboBox.Items.Add("SFP56");
            Type_comboBox.Items.Add("SFP DD");
            Type_comboBox.Items.Add("OSFP");
        }

        private void fillComboBoxDataRate()
        {
            DataRate_comboBox.Items.Add("100M");
            DataRate_comboBox.Items.Add("1G");
            DataRate_comboBox.Items.Add("10G");
            DataRate_comboBox.Items.Add("Tri-Rate");
            DataRate_comboBox.Items.Add("Multi-Rate");
            DataRate_comboBox.Items.Add("Dual Rate");
            DataRate_comboBox.Items.Add("Triple Rate");
            DataRate_comboBox.Items.Add("155M");
            DataRate_comboBox.Items.Add("622M");
            DataRate_comboBox.Items.Add("2.5G");
            DataRate_comboBox.Items.Add("8G");
            DataRate_comboBox.Items.Add("4G");
            DataRate_comboBox.Items.Add("2.5G/1.2G");
            DataRate_comboBox.Items.Add("2x 1G");
            DataRate_comboBox.Items.Add("1.25G");
            DataRate_comboBox.Items.Add("25G");
            DataRate_comboBox.Items.Add("100G");
            DataRate_comboBox.Items.Add("40G");
            DataRate_comboBox.Items.Add("200G");
            DataRate_comboBox.Items.Add("400G");
            DataRate_comboBox.Items.Add("800G");
            DataRate_comboBox.Items.Add("1TB");
            DataRate_comboBox.Items.Add("100M-2.7G");
            DataRate_comboBox.Items.Add("2.7G");
            DataRate_comboBox.Items.Add("1000 BASE-T");
            DataRate_comboBox.Items.Add("10/100/1000 BASE-T");
            DataRate_comboBox.Items.Add("10GBASE-T");
            DataRate_comboBox.Items.Add("1G/2G");
        }

        private void fillComboBoxWDM()
        {
            WDM_comboBox.Items.Add("CWDM");
            WDM_comboBox.Items.Add("DWDM");
        }

        public void fillComboBoxCompliance()
        {
            Compliance_comboBox.Items.Add("CWDM4");
            Compliance_comboBox.Items.Add("SWDM-40");
            Compliance_comboBox.Items.Add("CLR4");
            Compliance_comboBox.Items.Add("ER4");
            Compliance_comboBox.Items.Add("LR4");
            Compliance_comboBox.Items.Add("SR4");
            Compliance_comboBox.Items.Add("ZR4");
            Compliance_comboBox.Items.Add("SWDM4");
            Compliance_comboBox.Items.Add("SR4/OTU4");
            Compliance_comboBox.Items.Add("LR4/OTU4");
            Compliance_comboBox.Items.Add("ER4/OTU4");
            Compliance_comboBox.Items.Add("ER8");
            Compliance_comboBox.Items.Add("LR8");
            Compliance_comboBox.Items.Add("SR8");
            Compliance_comboBox.Items.Add("ZR8");
            Compliance_comboBox.Items.Add("PSM4");
            Compliance_comboBox.Items.Add("PSM4-IR4");
            Compliance_comboBox.Items.Add("4WDM-40");
        }

        private void fillComboBoxWave()
        {
            Wave_comboBox.Items.Add("850nm");
            Wave_comboBox.Items.Add("1310nm");
            Wave_comboBox.Items.Add("1550nm");
            Wave_comboBox.Items.Add("TX1310/RX1490");
            Wave_comboBox.Items.Add("TX1490/RX1310");
            Wave_comboBox.Items.Add("TX1270/RX1330");
            Wave_comboBox.Items.Add("TX1330/RX1270");
            Wave_comboBox.Items.Add("TX1490/RX1550");
            Wave_comboBox.Items.Add("TX1550/RX1490");
            Wave_comboBox.Items.Add("TX1310/RX1550");
            Wave_comboBox.Items.Add("TX1550/RX1310");
            Wave_comboBox.Items.Add("TX1590/RX1490");
            Wave_comboBox.Items.Add("TX1570/RX1490");
            Wave_comboBox.Items.Add("TX1490/TX1570");
            Wave_comboBox.Items.Add("TX1490/RX1590");
            Wave_comboBox.Items.Add("15xx.xx");
            Wave_comboBox.Items.Add("Tunable - C-BAND");
            Wave_comboBox.Items.Add("Tunable - L-BAND");
            Wave_comboBox.Items.Add("LAN WDM");
            Wave_comboBox.Items.Add("1xx0");
            Wave_comboBox.Items.Add("1270nm");
            Wave_comboBox.Items.Add("1290nm");
            Wave_comboBox.Items.Add("1330nm");
            Wave_comboBox.Items.Add("1350nm");
            Wave_comboBox.Items.Add("1370nm");
            Wave_comboBox.Items.Add("1390nm");
            Wave_comboBox.Items.Add("1410nm");
            Wave_comboBox.Items.Add("1430nm");
            Wave_comboBox.Items.Add("1450nm");
            Wave_comboBox.Items.Add("1470nm");
            Wave_comboBox.Items.Add("1490nm");
            Wave_comboBox.Items.Add("1510nm");
            Wave_comboBox.Items.Add("1530nm");
            Wave_comboBox.Items.Add("1570nm");
            Wave_comboBox.Items.Add("1590nm");
            Wave_comboBox.Items.Add("1610nm");
            Wave_comboBox.Items.Add("1563.86nm");
            Wave_comboBox.Items.Add("1563.05nm");
            Wave_comboBox.Items.Add("1562.23nm");
            Wave_comboBox.Items.Add("1561.42nm");
            Wave_comboBox.Items.Add("1560.61nm");
            Wave_comboBox.Items.Add("1559.79nm");
            Wave_comboBox.Items.Add("1558.98nm");
            Wave_comboBox.Items.Add("1558.17nm");
            Wave_comboBox.Items.Add("1557.36nm");
            Wave_comboBox.Items.Add("1556.55nm");
            Wave_comboBox.Items.Add("1555.75nm");
            Wave_comboBox.Items.Add("1554.94nm");
            Wave_comboBox.Items.Add("1554.13nm");
            Wave_comboBox.Items.Add("1553.33nm");
            Wave_comboBox.Items.Add("1552.52nm");
            Wave_comboBox.Items.Add("1551.72nm");
            Wave_comboBox.Items.Add("1550.92nm");
            Wave_comboBox.Items.Add("1550.12nm");
            Wave_comboBox.Items.Add("1549.32nm");
            Wave_comboBox.Items.Add("1548.51nm");
            Wave_comboBox.Items.Add("1547.72nm");
            Wave_comboBox.Items.Add("1546.92nm");
            Wave_comboBox.Items.Add("1546.12nm");
            Wave_comboBox.Items.Add("1545.32nm");
            Wave_comboBox.Items.Add("1544.53nm");
            Wave_comboBox.Items.Add("1543.73nm");
            Wave_comboBox.Items.Add("1542.94nm");
            Wave_comboBox.Items.Add("1542.14nm");
            Wave_comboBox.Items.Add("1541.35nm");
            Wave_comboBox.Items.Add("1540.56nm");
            Wave_comboBox.Items.Add("1539.77nm");
            Wave_comboBox.Items.Add("1538.98nm");
            Wave_comboBox.Items.Add("1538.19nm");
            Wave_comboBox.Items.Add("1537.40nm");
            Wave_comboBox.Items.Add("1536.61nm");
            Wave_comboBox.Items.Add("1535.82nm");
            Wave_comboBox.Items.Add("1535.04nm");
            Wave_comboBox.Items.Add("1534.25nm");
            Wave_comboBox.Items.Add("1533.47nm");
            Wave_comboBox.Items.Add("1532.68nm");
            Wave_comboBox.Items.Add("1531.90nm");
            Wave_comboBox.Items.Add("1531.12nm");
            Wave_comboBox.Items.Add("1530.33nm");
            Wave_comboBox.Items.Add("1529.55nm");
            Wave_comboBox.Items.Add("1528.77nm");
            Wave_comboBox.Items.Add("N/A");
        }

        private void fillComboBoxSMMM()
        {
            SMMM_comboBox.Items.Add("SM");
            SMMM_comboBox.Items.Add("MM");
        }

        private void fillComboBoxDist()
        {
            Dist_comboBox.Items.Add("2K");
            Dist_comboBox.Items.Add("10K");
            Dist_comboBox.Items.Add("15K");
            Dist_comboBox.Items.Add("20K");
            Dist_comboBox.Items.Add("30K");
            Dist_comboBox.Items.Add("40K");
            Dist_comboBox.Items.Add("50K");
            Dist_comboBox.Items.Add("60K");
            Dist_comboBox.Items.Add("70K");
            Dist_comboBox.Items.Add("80K");
            Dist_comboBox.Items.Add("95K");
            Dist_comboBox.Items.Add("100K");
            Dist_comboBox.Items.Add("30M");
            Dist_comboBox.Items.Add("100M");
            Dist_comboBox.Items.Add("120K");
            Dist_comboBox.Items.Add("150M");
            Dist_comboBox.Items.Add("220M");
            Dist_comboBox.Items.Add("300M");
            Dist_comboBox.Items.Add("400M");
            Dist_comboBox.Items.Add("550M");
        }

        private void fillComboBoxTemp()
        {
            Temp_comboBox.Items.Add("C-TEMP");
            Temp_comboBox.Items.Add("E-TEMP");
            Temp_comboBox.Items.Add("I-TEMP");
        }

        private void fillComboBoxBreakout()
        {
            BreakoutQuantity_comboBox.Items.Add("Breakout 4x");
            BreakoutQuantity_comboBox.Items.Add("Breakout 8x");
        }

        private void fillComboBoxSecondEnd()
        {
            SecondEnd_comboBox.Items.Add("SFP+ 10G");
            SecondEnd_comboBox.Items.Add("SFP56 50G");
            SecondEnd_comboBox.Items.Add("QSFP+");
            SecondEnd_comboBox.Items.Add("QSFP28");
            SecondEnd_comboBox.Items.Add("QSFP56");
            SecondEnd_comboBox.Items.Add("QSFP-DD");
            SecondEnd_comboBox.Items.Add("OSFP");
        }

        private void fillComboBoxTestLabel()
        {
            TestLabel_ComboBox.Items.Add("SFP Label");
            TestLabel_ComboBox.Items.Add("SFP+ Label");
            TestLabel_ComboBox.Items.Add("XFP Label");
            TestLabel_ComboBox.Items.Add("QSFP Label");
            TestLabel_ComboBox.Items.Add("Copper Label");
            TestLabel_ComboBox.Items.Add("Outer Box Label");
        }

        private void fillComboBoxCountry()
        {
            Country_comboBox.Items.Add("China");
            Country_comboBox.Items.Add("Malaysia");
            Country_comboBox.Items.Add("Japan");
            Country_comboBox.Items.Add("Taiwan");
            Country_comboBox.Items.Add("Thailand");
            Country_comboBox.SelectedItem = "China";
        }

        private void fillComboBoxSearch()
        {
            Search_comboBox.Items.Add("EPN");
            Search_comboBox.Items.Add("IPN");
            Search_comboBox.SelectedItem = "EPN";
        }
    }
}