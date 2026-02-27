using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp2.Utilities
{
    /// <summary>
    /// Provides export functionality for data
    /// </summary>
    public static class ExportHelper
    {
        /// <summary>
        /// Exports DataTable to CSV file
        /// </summary>
        public static bool ExportToCSV(DataTable dataTable, string filePath)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                // Add column headers
                string[] columnNames = new string[dataTable.Columns.Count];
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    columnNames[i] = dataTable.Columns[i].ColumnName;
                }
                sb.AppendLine(string.Join(",", columnNames));

                // Add rows
                foreach (DataRow row in dataTable.Rows)
                {
                    string[] fields = new string[dataTable.Columns.Count];
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        fields[i] = EscapeCSVField(row[i].ToString());
                    }
                    sb.AppendLine(string.Join(",", fields));
                }

                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to CSV: {ex.Message}", "Export Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Escapes CSV field if it contains special characters
        /// </summary>
        private static string EscapeCSVField(string field)
        {
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            {
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            }
            return field;
        }

        /// <summary>
        /// Exports DataGridView to CSV file with SaveFileDialog
        /// </summary>
        public static void ExportDataGridViewToCSV(DataGridView dgv)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                    FilterIndex = 1,
                    RestoreDirectory = true,
                    FileName = $"UserData_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();

                    // Add column headers
                    string[] columnNames = new string[dgv.Columns.Count];
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        columnNames[i] = dgv.Columns[i].HeaderText;
                    }
                    sb.AppendLine(string.Join(",", columnNames));

                    // Add rows
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string[] fields = new string[dgv.Columns.Count];
                            for (int i = 0; i < dgv.Columns.Count; i++)
                            {
                                fields[i] = EscapeCSVField(row.Cells[i].Value?.ToString() ?? "");
                            }
                            sb.AppendLine(string.Join(",", fields));
                        }
                    }

                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show($"Data exported successfully to:\n{sfd.FileName}", 
                        "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting data: {ex.Message}", "Export Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
