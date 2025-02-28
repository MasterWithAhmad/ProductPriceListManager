using product_price_list_manager.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace product_price_list_manager.UI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            LoadProducts();
            LoadCategories();
        }

        private void LoadProducts()
        {
            try
            {
                string query = "SELECT ProductID, ProductName, Category, Price, StockQuantity, LastUpdated FROM Products";
                DataTable dt = DBHelper.ExecuteQuery(query);

                dgvProducts.DataSource = dt;

                // Format Price Column to Include Dollar Sign
                dgvProducts.Columns["Price"].DefaultCellStyle.Format = "C"; // Currency Format ($)

                // Format Headers with Spaces
                dgvProducts.Columns["ProductID"].HeaderText = "Product ID";
                dgvProducts.Columns["ProductName"].HeaderText = "Product Name";
                dgvProducts.Columns["Category"].HeaderText = "Category";
                dgvProducts.Columns["StockQuantity"].HeaderText = "Stock Quantity";
                dgvProducts.Columns["LastUpdated"].HeaderText = "Last Updated";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategories()
        {
            try
            {
                string query = "SELECT DISTINCT Category FROM Products ORDER BY Category";
                DataTable dt = DBHelper.ExecuteQuery(query);

                cmbSort.Items.Clear();
                cmbSort.Items.Add("All"); // Add 'All' option

                foreach (DataRow row in dt.Rows)
                {
                    cmbSort.Items.Add(row["Category"].ToString());
                }

                cmbSort.SelectedIndex = 0; // Default to 'All'
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string productId = dgvProducts.SelectedRows[0].Cells["ProductID"].Value.ToString();
            string productName = dgvProducts.SelectedRows[0].Cells["ProductName"].Value.ToString();
            decimal currentPrice = Convert.ToDecimal(dgvProducts.SelectedRows[0].Cells["Price"].Value);

            frmUpdatePrice updateForm = new frmUpdatePrice(productId, productName, currentPrice);
            updateForm.ShowDialog();
            LoadProducts(); 
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProducts.Rows.Count == 0)
                {
                    MessageBox.Show("No data to export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "CSV files (.csv)|.csv",
                    FileName = "Product_List.csv"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();

                    // Get Headers
                    string[] columnNames = dgvProducts.Columns.Cast<DataGridViewColumn>()
                        .Select(column => column.HeaderText)
                        .ToArray();
                    sb.AppendLine(string.Join(",", columnNames));

                    // Get Rows
                    foreach (DataGridViewRow row in dgvProducts.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string[] cells = row.Cells.Cast<DataGridViewCell>()
                                .Select(cell =>
                                {
                                    if (cell.OwningColumn.Name == "LastUpdated" && cell.Value is DateTime dateValue)
                                        return dateValue.ToString("yyyy-MM-dd HH:mm:ss"); // Ensure correct format
                                    return cell.Value?.ToString().Replace(",", " ") ?? "";
                                })
                                .ToArray();

                            sb.AppendLine(string.Join(",", cells));
                        }
                    }

                    File.WriteAllText(saveDialog.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("Export successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSort.SelectedIndex == 0) // "All" selected
                {
                    LoadProducts();
                    return;
                }

                string query = "SELECT * FROM Products WHERE Category = @category";
                SqlParameter[] parameters = { new SqlParameter("@category", cmbSort.SelectedItem.ToString()) };
                dgvProducts.DataSource = DBHelper.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sorting products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpLastUpdated_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT * FROM Products WHERE LastUpdated >= @date";
                SqlParameter[] parameters = { new SqlParameter("@date", dtpLastUpdated.Value) };
                dgvProducts.DataSource = DBHelper.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering by date: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT * FROM Products WHERE ProductName LIKE @search";
                SqlParameter[] parameters = { new SqlParameter("@search", "%" + txtSearch.Text.Trim() + "%") };
                dgvProducts.DataSource = DBHelper.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
