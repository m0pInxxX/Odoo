private void tableCell_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
{
    try 
    {
        DevExpress.XtraReports.UI.XRTableCell cell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.DetailBand detailBand = (DevExpress.XtraReports.UI.DetailBand)cell.Band;
        
        // Coba beberapa variasi nama field yang mungkin
        string[] possibleFields = new[] {
            "Company Tax/GST Registration No.",
            "CompanyProfile.Company Tax/GST Registration No.",
            "Invoice Listing - 2 Base Data: Company Profile.Company Tax/GST Registration No.",
            "Company Profile.Company Tax/GST Registration No.",
            "[Company Profile.Company Tax/GST Registration No.]"
        };

        string message = "";
        foreach (var fieldName in possibleFields)
        {
            try
            {
                object value = detailBand.Report.GetCurrentColumnValue(fieldName);
                if (value != null)
                {
                    message += string.Format("Found in {0}: {1}\n", fieldName, value);
                }
            }
            catch (Exception ex)
            {
                message += string.Format("Error with {0}: {1}\n", fieldName, ex.Message);
            }
        }

        cell.Text = string.IsNullOrEmpty(message) ? "All attempts returned null" : message;
    }
    catch (Exception ex)
    {
        ((DevExpress.XtraReports.UI.XRTableCell)sender).Text = "Main Error: " + ex.Message;
    }
} 