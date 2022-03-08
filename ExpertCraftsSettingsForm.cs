using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace NavigationTest.ExpertCrafts
{
    public partial class ExpertCraftsSettingsForm : Form
    {
        public ExpertCraftsSettingsForm()
        {
            InitializeComponent();
            SetDoubleBuffer(dataGridView1, true);
        }
        

        private void tabPage2_Click(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void ResizeWindow()
        {
            int width = 14;
            dataGridView1.AutoResizeColumns();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                if (!column.Visible) continue;
                if (column.AutoSizeMode != DataGridViewAutoSizeColumnMode.None)
                    width += Math.Max(column.Width,
                        column.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true));
                else width += column.Width;
                width += column.DividerWidth;
                width += 5;
            }
            width = Math.Max(380, width);
            ClientSize = new Size(width + 2, ClientSize.Height);
            dataGridView1.Size = new Size(width, dataGridView1.Size.Height);
            dataGridView1.Update();
            dataGridView1.Refresh();
        }  
        
        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            
        }        
        
        private void ExpertCraftsSettingsForm_Load(object sender, EventArgs e)
        {
            
            propertyGrid1.SelectedObject = ExpertCraftsSettings.Instance;
            
            //bindingSource1.DataSource = ExpertCraftsSettings.Instance.ItemsToBuy;
            dataGridView1.DataSource = ExpertCraftsSettings.Instance.ItemsToBuy;//bindingSource1;
            label1.Text = ExpertCraftsSettings.Instance.ItemsToBuy.Count.ToString();
            //bindingSource1.ResetBindings(true);
            dataGridView1.Update();
            dataGridView1.AutoResizeColumns();
            ResizeWindow();
        }        
        
        private void SaveAndUpdate()
        {
            ExpertCraftsSettings.Instance.Save();
            //bindingSource1.DataSource = ExpertCraftsSettings.Instance.ItemsToBuy;
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.DataSource = ExpertCraftsSettings.Instance.ItemsToBuy;//bindingSource1;
            //bindingSource1.ResetBindings(true);
            ResizeWindow();
        }        
        
        private void Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ItemId.Text)) return;
            uint quantity = string.IsNullOrEmpty(Quantity.Text) ? 1 : uint.Parse(Quantity.Text);
            foreach (var itemid in ItemId.Text.Split(',').Select(uint.Parse))
            {
                if (ExpertCraftsSettings.Instance.ItemsToBuy.Any(i => i.item == itemid)) continue;
            }

            ItemId.Text = "";
            
            SaveAndUpdate();
        }  
        
        private void SetDoubleBuffer(Control dataGridView, bool doublebuffered)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                                         BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                                         null, 
                                         dataGridView, 
                                         new object[] {doublebuffered});
        }
        
        /*
        private void Delete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ItemId.Text))
            {
                uint itemid = (uint) int.Parse(ItemId.Text);
                MBBuyItem toRemove = ExpertCraftsSettings.Instance.ItemsToBuy.First(item => item.ItemId.Equals(itemid));
                ExpertCraftsSettings.Instance.ItemsToBuy.Remove(toRemove);
            }
            else
            {

                if (dataGridView1.SelectedCells.Count <= 0) return;
                List<DataGridViewRow> rows = (from DataGridViewCell cell in dataGridView1.SelectedCells let rowIndex = cell.RowIndex select dataGridView1.Rows[cell.RowIndex]).Distinct().ToList();
                if (!rows.Any()) return;
                foreach (var row in rows)
                {
                    uint itemid = (uint) row.Cells[0].Value;
                    MBBuyItem toRemove = ExpertCraftsSettings.Instance.ItemsToBuy.First(item => item.ItemId.Equals(itemid));
                    ExpertCraftsSettings.Instance.ItemsToBuy.Remove(toRemove);
                }
            }

            SaveAndUpdate();
        }        
        */

        private void label1_Click(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
        }
    }
}