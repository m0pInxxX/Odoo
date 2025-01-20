private void tableCell7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
{
    try 
    {
        DevExpress.XtraReports.UI.XRTableCell cell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.DetailBand detailBand = (DevExpress.XtraReports.UI.DetailBand)cell.Band;
        
        object companyNameObj = detailBand.Report.GetCurrentColumnValue("DebtorCompanyName");
        
        if (companyNameObj == null)
        {
            cell.Text = "-";
            return;
        }

        string companyName = companyNameObj.ToString().Trim();

        // Daftar prefix yang akan dipindahkan ke belakang
        string[] prefixes = new[] { "PT.", "PT ", "CV.", "CV ", "PT..", "CV..", "PT .", "CV ." };
        
        foreach (var prefix in prefixes)
        {
            if (companyName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                // Ambil nama tanpa prefix
                string nameWithoutPrefix = companyName.Substring(prefix.Length).Trim();
                
                // Bersihkan semua titik dari nama
                nameWithoutPrefix = nameWithoutPrefix.Replace(".", "").Trim();
                
                // Ambil prefix tanpa tanda titik
                string cleanPrefix = prefix.TrimEnd('.', ' ');
                
                cell.Text = string.Format("{0} {1}", nameWithoutPrefix, cleanPrefix);
                return;
            }
        }
        
        // Jika tidak ada prefix, tetap bersihkan titik
        cell.Text = companyName.Replace(".", "").Trim();
    }
    catch (Exception)
    {
        ((DevExpress.XtraReports.UI.XRTableCell)sender).Text = "-";
    }
}