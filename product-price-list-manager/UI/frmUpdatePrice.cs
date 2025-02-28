using product_price_list_manager.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace product_price_list_manager.UI
{
    public partial class frmUpdatePrice : Form
    {
        private string _productId;
        public frmUpdatePrice(string productId, string productName, decimal currentPrice)
        {
            InitializeComponent();
            _productId = productId;
            lblProductName.Text = productName;
            lblOldPrice.Text = currentPrice.ToString("C");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtNewPrice.Text, out decimal newPrice) || newPrice <= 0)
            {
                MessageBox.Show("Please enter a valid price.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to update the price?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;

            try
            {
                string query = "UPDATE Products SET Price = @price, LastUpdated = GETDATE() WHERE ProductID = @id";
                SqlParameter[] parameters = {
                    new SqlParameter("@price", newPrice),
                    new SqlParameter("@id", _productId)
                };

                if (DBHelper.ExecuteNonQuery(query, parameters))
                {
                    MessageBox.Show("Price updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update price.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating price: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
