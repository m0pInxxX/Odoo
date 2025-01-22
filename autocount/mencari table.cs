    try 
    {
        DevExpress.XtraReports.UI.XRTableCell tableCell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.DetailBand detailBand = (DevExpress.XtraReports.UI.DetailBand)tableCell.Band;
        
        // Coba beberapa kemungkinan nama field
        string[] possibleFields = new[] {
            
        };

        foreach (var fieldName in possibleFields)
        {
            try 
            {
                var value = detailBand.Report.GetCurrentColumnValue(fieldName);
                if (value != null)
                {
                    tableCell.Text += string.Format("Found in {0}: {1}\n", fieldName, value);
                }
            }
            catch { }
        }
    }
    catch (Exception ex)
    {
        ((DevExpress.XtraReports.UI.XRTableCell)sender).Text = ex.Message;
    }
