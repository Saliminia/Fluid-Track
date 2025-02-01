using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;

using System.Windows.Forms;

namespace DMR
{
    public partial class UnitConvForm : Form
    {
        KeyValuePair<string, string>[] directGroupsAndUnits = null;
        KeyValuePair<string, string>[] nonDirectGroupsAndUnits = null;
        //=======================================================
        public UnitConvForm()
        {
            InitializeComponent();
        }
        //-------------------------------------------------------
        private void UnitConvForm_Load(object sender, EventArgs e)
        {
            cboxGroup.Items.Clear();

            directGroupsAndUnits = UnitConverter.GetDirectGroupsAndUnits();
            nonDirectGroupsAndUnits = UnitConverter.GetNonDirectGroupsAndUnits();

            for (int i = 0; i < directGroupsAndUnits.Length; i++)
                if (!cboxGroup.Items.Contains(directGroupsAndUnits[i].Key))
                    cboxGroup.Items.Add(directGroupsAndUnits[i].Key);

            for (int i = 0; i < nonDirectGroupsAndUnits.Length; i++)
                if (!cboxGroup.Items.Contains(nonDirectGroupsAndUnits[i].Key))
                    cboxGroup.Items.Add(nonDirectGroupsAndUnits[i].Key);

            cboxGroup.SelectedIndex = 0;
        }
        //-------------------------------------------------------
        string DoConversion(int whichSide /*1: u1->u2  ,  2: u2->u1*/)
        {
            if (cboxGroup.SelectedIndex == -1)
                return "Select Group";

            if (cboxUnit1.SelectedIndex == -1 || cboxUnit2.SelectedIndex == -1)
                return "Select Units";

            string group = cboxGroup.SelectedItem.ToString();

            string fromUnit = "";
            string toUnit = "";
            string valStr = "";

            if (whichSide == 1)
            {
                fromUnit = cboxUnit1.SelectedItem.ToString();
                toUnit = cboxUnit2.SelectedItem.ToString();
                valStr = txtVal1.Text;
            }
            else
            {
                fromUnit = cboxUnit2.SelectedItem.ToString();
                toUnit = cboxUnit1.SelectedItem.ToString();
                valStr = txtVal2.Text;
            }

            return UnitConverter.Convert(group, fromUnit, toUnit, valStr);
        }
        //-------------------------------------------------------
        private void txtVal1_TextChanged(object sender, EventArgs e)
        {
            if (txtVal1.Focused)
                txtVal2.Text = DoConversion(1);
        }
        //-------------------------------------------------------
        private void txtVal2_TextChanged(object sender, EventArgs e)
        {
            if (txtVal2.Focused)
                txtVal1.Text = DoConversion(2);
        }
        //-------------------------------------------------------
        private void cboxUnit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxUnit1.Focused)
                txtVal2.Text = DoConversion(1);
        }
        //-------------------------------------------------------
        private void cboxUnit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxUnit2.Focused)
                txtVal1.Text = DoConversion(2);
        }
        //-------------------------------------------------------
        private void cboxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboxUnit1.Items.Clear();
            cboxUnit2.Items.Clear();

            txtVal1.Text = txtVal2.Text = "";

            if (cboxGroup.SelectedIndex == -1)
                return;

            string group = cboxGroup.SelectedItem.ToString();

            for (int i = 0; i < directGroupsAndUnits.Length; i++)
                if (group == directGroupsAndUnits[i].Key)
                {
                    cboxUnit1.Items.Add(directGroupsAndUnits[i].Value);
                    cboxUnit2.Items.Add(directGroupsAndUnits[i].Value);
                }

            for (int i = 0; i < nonDirectGroupsAndUnits.Length; i++)
                if (group == nonDirectGroupsAndUnits[i].Key)
                {
                    cboxUnit1.Items.Add(nonDirectGroupsAndUnits[i].Value);
                    cboxUnit2.Items.Add(nonDirectGroupsAndUnits[i].Value);
                }

            cboxUnit1.SelectedIndex = 0;
            cboxUnit2.SelectedIndex = 0;
        }
        //-------------------------------------------------------
        private void UnitConvForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        //-------------------------------------------------------

    }
}
