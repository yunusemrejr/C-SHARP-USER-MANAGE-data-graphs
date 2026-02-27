using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WindowsFormsApp2.Models;
using WindowsFormsApp2.Data;
using WindowsFormsApp2.Utilities;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private UserRepository _userRepository;
        private DataTable _currentDataTable;

        public Form1()
        {
            InitializeComponent();
            _userRepository = new UserRepository();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllData();
                ConfigureDataGridView();
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading application: {ex.Message}\n\nPlease ensure MySQL server is running and database is configured correctly.", 
                    "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAllData()
        {
            try
            {
                _currentDataTable = _userRepository.GetAllUsers();
                
                // Update DataGridView
                dataGridView1.DataSource = _currentDataTable;
                
                // Update Charts
                UpdateCharts();
                
                // Clear input fields
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Data Load Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCharts()
        {
            try
            {
                if (_currentDataTable != null && _currentDataTable.Rows.Count > 0)
                {
                    // Chart 1: Numeric Value by Mission
                    chart_1.DataSource = _currentDataTable;
                    chart_1.Series["NumericValue"].XValueMember = "mission";
                    chart_1.Series["NumericValue"].YValueMembers = "NV";
                    chart_1.DataBind();

                    // Chart 2: Category Average of Numeric Value
                    chart_2.DataSource = _currentDataTable;
                    chart_2.Series["Series2"].XValueMember = "category";
                    chart_2.Series["Series2"].YValueMembers = "NV";
                    chart_2.DataBind();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating charts: {ex.Message}", "Chart Update Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = true;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
        }

        private void UpdateStatistics()
        {
            try
            {
                var stats = _userRepository.GetStatistics();
                
                if (stats.ContainsKey("TotalUsers"))
                {
                    lblTotalUsers.Text = $"Total Users: {stats["TotalUsers"]}";
                }
                if (stats.ContainsKey("AverageNV"))
                {
                    lblAverageNV.Text = $"Average NV: {Convert.ToDouble(stats["AverageNV"]):F2}";
                }
                if (stats.ContainsKey("MaxNV"))
                {
                    lblMaxNV.Text = $"Max NV: {Convert.ToDouble(stats["MaxNV"]):F2}";
                }
                if (stats.ContainsKey("MinNV"))
                {
                    lblMinNV.Text = $"Min NV: {Convert.ToDouble(stats["MinNV"]):F2}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating statistics: {ex.Message}", "Statistics Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Add User
            if (!ValidateInputs())
                return;

            try
            {
                int id = int.Parse(textBox1.Text);
                
                // Check if user already exists
                if (_userRepository.UserExists(id))
                {
                    MessageBox.Show("A user with this ID already exists. Please use a different ID or use the Update function.", 
                        "Duplicate ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                User newUser = new User
                {
                    ID = id,
                    NV = float.Parse(textBox4.Text),
                    Username = textBox2.Text.Trim(),
                    UserSurname = textBox3.Text.Trim(),
                    Category = textBox5.Text.Trim(),
                    Mission = textBox6.Text.Trim()
                };

                if (_userRepository.AddUser(newUser))
                {
                    MessageBox.Show("User added successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllData();
                    UpdateStatistics();
                }
                else
                {
                    MessageBox.Show("Failed to add user. Please try again.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Delete User
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a User ID to delete.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!InputValidator.IsValidInteger(textBox1.Text, out int id))
            {
                MessageBox.Show("Please enter a valid numeric User ID.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Are you sure you want to delete user with ID {id}?", 
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_userRepository.DeleteUser(id))
                    {
                        MessageBox.Show("User deleted successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAllData();
                        UpdateStatistics();
                    }
                    else
                    {
                        MessageBox.Show("User not found or could not be deleted.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Update User
            if (!ValidateInputs())
                return;

            try
            {
                int id = int.Parse(textBox1.Text);
                
                // Check if user exists
                if (!_userRepository.UserExists(id))
                {
                    MessageBox.Show("User with this ID does not exist. Please use the Add function to create a new user.", 
                        "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                User updatedUser = new User
                {
                    ID = id,
                    NV = float.Parse(textBox4.Text),
                    Username = textBox2.Text.Trim(),
                    UserSurname = textBox3.Text.Trim(),
                    Category = textBox5.Text.Trim(),
                    Mission = textBox6.Text.Trim()
                };

                if (_userRepository.UpdateUser(updatedUser))
                {
                    MessageBox.Show("User updated successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllData();
                    UpdateStatistics();
                }
                else
                {
                    MessageBox.Show("Failed to update user. Please try again.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAllData();
            UpdateStatistics();
            MessageBox.Show("Data refreshed successfully!", "Refresh", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportHelper.ExportDataGridViewToCSV(dataGridView1);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txtSearch.Text.Trim();
                
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    LoadAllData();
                }
                else
                {
                    _currentDataTable = _userRepository.SearchUsers(searchTerm);
                    dataGridView1.DataSource = _currentDataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching: {ex.Message}", "Search Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                try
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    
                    textBox1.Text = row.Cells["ID"].Value?.ToString() ?? "";
                    textBox4.Text = row.Cells["NV"].Value?.ToString() ?? "";
                    textBox2.Text = row.Cells["username"].Value?.ToString() ?? "";
                    textBox3.Text = row.Cells["usersurname"].Value?.ToString() ?? "";
                    textBox5.Text = row.Cells["category"].Value?.ToString() ?? "";
                    textBox6.Text = row.Cells["mission"].Value?.ToString() ?? "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading row data: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private bool ValidateInputs()
        {
            // Validate ID
            if (!InputValidator.IsValidInteger(textBox1.Text, out int id))
            {
                MessageBox.Show("Please enter a valid numeric User ID.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return false;
            }

            if (!InputValidator.IsPositiveInteger(id))
            {
                MessageBox.Show("User ID must be a positive number.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return false;
            }

            // Validate Numeric Value
            if (!InputValidator.IsValidFloat(textBox4.Text, out float nv))
            {
                MessageBox.Show("Please enter a valid Numeric Value.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Focus();
                return false;
            }

            // Validate Username
            if (!InputValidator.IsNotEmpty(textBox2.Text))
            {
                MessageBox.Show("Username cannot be empty.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return false;
            }

            if (!InputValidator.HasMaxLength(textBox2.Text, 50))
            {
                MessageBox.Show("Username cannot exceed 50 characters.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return false;
            }

            // Validate User Surname
            if (!InputValidator.IsNotEmpty(textBox3.Text))
            {
                MessageBox.Show("User Surname cannot be empty.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return false;
            }

            if (!InputValidator.HasMaxLength(textBox3.Text, 50))
            {
                MessageBox.Show("User Surname cannot exceed 50 characters.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return false;
            }

            // Validate Category
            if (!InputValidator.IsNotEmpty(textBox5.Text))
            {
                MessageBox.Show("Category cannot be empty.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox5.Focus();
                return false;
            }

            // Validate Mission
            if (!InputValidator.IsNotEmpty(textBox6.Text))
            {
                MessageBox.Show("Mission cannot be empty.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox6.Focus();
                return false;
            }

            return true;
        }

        private void ClearInputFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            txtSearch.Clear();
            textBox1.Focus();
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void tabPage3_Click(object sender, EventArgs e) { }
    }
}
