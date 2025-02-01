using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public class DGV_Operations
    {

        //-------------------------------------------------------
        public static void SelectOneRow(DataGridView dgv, int rID)
        {
            if (rID < 0 || rID > dgv.Rows.Count - 1)
                return;

            dgv.Rows[rID].Selected = true;
        }
        //-------------------------------------------------------
        public static void ShowAllRows(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
                dgv.Rows[i].Visible = true;
        }
        //-------------------------------------------------------
        public static void FilterRows(DataGridView dgv, int minColID, int maxColID, string searchText)
        {
            searchText = searchText.ToLower().Trim();

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                bool vis = false;

                for (int j = minColID; j <= maxColID; j++)
                {
                    if (DGV_Operations.CellValueAsString(dgv[j, i]).ToLower().Contains(searchText))//case insensitive
                    {
                        vis = true;
                        break;
                    }
                }

                dgv.Rows[i].Visible = vis;
            }
        }
        //-------------------------------------------------------
        public static void FilterRows_MyOrder(bool doFilter, DataGridView dgv, int comboSelID, string searchText, int startColumn)
        {
            int minColID = -1,
                maxColID = -1;

            if (!doFilter || comboSelID == -1)
            {
                ShowAllRows(dgv);
                return;
            }

            if (comboSelID == 0)
            {
                minColID = startColumn;
                maxColID = dgv.Columns.Count - 1; //multi column
            }
            else
            {
                minColID = maxColID = startColumn + comboSelID - 1; //single column
            }

            FilterRows(dgv, minColID, maxColID, searchText);
        }
        //-------------------------------------------------------
        public static void CopyDataToTextBoxes(DataGridView dgv, int rowIndex, int startColumn, params Control[] txts)
        {
            DataGridViewCellCollection cells = dgv.Rows[rowIndex].Cells;

            for (int i = 0; i < txts.Length; i++)
                txts[i].Text = DGV_Operations.CellValueAsString(cells[i + startColumn]);
        }
        //-------------------------------------------------------
        public static void SelectAllCheckBoxes(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
                if (dgv.Rows[i].Visible)
                    dgv.Rows[i].Cells[0].Value = true;
        }
        //-------------------------------------------------------
        public static void UnselectAllCheckBoxes(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
                if (dgv.Rows[i].Visible)
                    dgv.Rows[i].Cells[0].Value = false;
        }
        //-------------------------------------------------------
        public static void InvertAllCheckBoxes(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
                if (dgv.Rows[i].Visible)
                    if (Convert.ToBoolean(dgv.Rows[i].Cells[0].Value) == true)
                        dgv.Rows[i].Cells[0].Value = false;
                    else
                        dgv.Rows[i].Cells[0].Value = true;
        }
        //-------------------------------------------------------
        public static string CellValueAsString(DataGridViewCell cell)
        {
            try
            {
                if (cell == null)
                    return "";
                if (cell.Value == null)
                    return "";

                return cell.Value.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        //-------------------------------------------------------
        public static string SummationOnColumn(DataGridView dgv, int colID)
        {
            decimal totalVal = 0;

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                string valStr = DGV_Operations.CellValueAsString(dgv.Rows[i].Cells[colID]);
                decimal val = 0;

                if (!AdvancedConvertor.ToDecimal(valStr, ref val))
                    return "!!!";
                else
                    totalVal += val;
            }

            return totalVal.ToString("0.###");
        }
        //-------------------------------------------------------
        public static void ColorizeComputedColumnHeader(DataGridView dgv, params int[] colIDs)
        {
            for (int i = 0; i < colIDs.Length; i++)
            {
                dgv.Columns[colIDs[i]].HeaderCell.Style.BackColor = Color.YellowGreen;
                dgv.Columns[colIDs[i]].HeaderCell.Tag = "ComputedColumnHeader";
            }
        }
        //-------------------------------------------------------
        public static void ColorizeFixedColumnHeader(DataGridView dgv, params int[] colIDs)
        {
            for (int i = 0; i < colIDs.Length; i++)
            {
                dgv.Columns[colIDs[i]].HeaderCell.Style.BackColor = Color.LightGray;
                dgv.Columns[colIDs[i]].HeaderCell.Tag = "FixedColumnHeader";
            }
        }
        //-------------------------------------------------------
        public static void ColorizeNestedColumnHeader(DataGridView dgv, params int[] colIDs)
        {
            for (int i = 0; i < colIDs.Length; i++)
            {
                dgv.Columns[colIDs[i]].HeaderCell.Style.BackColor = Color.DeepSkyBlue;
                dgv.Columns[colIDs[i]].HeaderCell.Tag = "NestedColumnHeader";
            }
        }
        //-------------------------------------------------------
        //public static void ColorizeNestedColumnHeader(DataGridView dgv, DataGridViewCellMouseEventHandler cellMouseClick, params int[] colIDs) //also handle cellMouseClick
        //{
        //    for (int i = 0; i < colIDs.Length; i++)
        //    {
        //        dgv.Columns[colIDs[i]].HeaderCell.Style.BackColor = Color.DeepSkyBlue;
        //        dgv.Columns[colIDs[i]].HeaderCell.Tag = "NestedColumnHeader";
        //    }

        //    HandleKeyDown(dgv, cellMouseClick);
        //}
        //-------------------------------------------------------
        public static DataGridViewCell GoToNextVisibleCell(DataGridView dgv)
        {
            DataGridViewCell cell = dgv.CurrentCell;

            while (cell != null)
            {
                if (cell.ColumnIndex == dgv.ColumnCount - 1) // last column
                {
                    if (cell.RowIndex + 1 < dgv.RowCount) // not last row
                        cell = dgv[0, cell.RowIndex + 1];
                    else
                    {
                        cell = null;
                        break;
                    }
                }
                else
                {
                    cell = dgv[cell.ColumnIndex + 1, cell.RowIndex];
                }

                if (cell.Visible == false)
                    continue;

                break;
            }

            if (cell != null)
                dgv.CurrentCell = cell;

            return cell;
        }
        //-------------------------------------------------------
        public static DataGridViewCell GoToPrevVisibleCell(DataGridView dgv)
        {
            DataGridViewCell cell = dgv.CurrentCell;

            while (cell != null)
            {
                if (cell.ColumnIndex == 0) // first column
                {
                    if (cell.RowIndex - 1 >= 0) // not first row
                        cell = dgv[dgv.ColumnCount - 1, cell.RowIndex - 1];
                    else
                    {
                        cell = null;
                        break;
                    }
                }
                else
                {
                    cell = dgv[cell.ColumnIndex - 1, cell.RowIndex];
                }

                if (cell.Visible == false)
                    continue;

                break;
            }

            if (cell != null)
                dgv.CurrentCell = cell;

            return cell;
        }
        //-------------------------------------------------------
        public static void HandleKeyDown(DataGridView dgv, DataGridViewCellMouseEventHandler cellMouseClick)
        {
            //DataGridView dgv = (DataGridView)sender;

            dgv.KeyDown += (o, e) =>
            {
                DataGridViewCell cell = dgv.CurrentCell;

                if (cell == null)
                    return;


                //~~~~~~~~~~~~~~~~~~~
                if (e.KeyCode == Keys.Enter)
                {
                    object tag = dgv.Columns[cell.ColumnIndex].HeaderCell.Tag;

                    if (tag != null)
                    {
                        DataGridViewCellMouseEventArgs cellClickParam = new DataGridViewCellMouseEventArgs(cell.ColumnIndex, cell.RowIndex, 0, 0, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));

                        if (tag.ToString() == "NestedColumnHeader")
                        {
                            //cellMouseClick?.Invoke(dgv, cellClickParam);
                            e.Handled = true;
                            return;
                        }
                    }

                    SendKeys.Send("{TAB}");
                    e.Handled = true;
                    return;
                }
                else if (e.KeyCode == Keys.Tab)
                {
                    if (e.Shift)
                    {
                        while (true)
                        {
                            cell = GoToPrevVisibleCell(dgv);

                            if (cell == null)
                            {
                                Control parent = dgv.Parent;
                                if (parent != null)
                                    parent.SelectNextControl(dgv, false, true, true, true);

                                e.Handled = false;
                                break;
                            }

                            e.Handled = true;

                            object tag = dgv.Columns[cell.ColumnIndex].HeaderCell.Tag;

                            if (tag != null)
                            {
                                if (tag.ToString() == "ComputedColumnHeader" || tag.ToString() == "FixedColumnHeader")
                                    continue;
                            }

                            break;
                        }
                    }
                    else
                    {
                        while (true)
                        {
                            cell = GoToNextVisibleCell(dgv);

                            if (cell == null)
                            {
                                Control parent = dgv.Parent;
                                if (parent != null)
                                    parent.SelectNextControl(dgv, true, true, true, true);

                                e.Handled = false;
                                break;
                            }

                            e.Handled = true;

                            object tag = dgv.Columns[cell.ColumnIndex].HeaderCell.Tag;

                            if (tag != null)
                            {
                                if (tag.ToString() == "ComputedColumnHeader" || tag.ToString() == "FixedColumnHeader")
                                    continue;
                            }

                            break;
                        }
                    }
                }
                //~~~~~~~~~~~~~~~~~~~
            };

        }
        //-------------------------------------------------------
    }


}
