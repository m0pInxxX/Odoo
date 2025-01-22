private void tableCell8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
{
    try 
    {
        DevExpress.XtraReports.UI.XRTableCell cell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.DetailBand detailBand = (DevExpress.XtraReports.UI.DetailBand)cell.Band;
        
        object registerNo = detailBand.Report.GetCurrentColumnValue("DebtorRegisterNo");
        
        if (registerNo != null)
        {
            string noIdentitas = registerNo.ToString().Trim();
            
            if (noIdentitas.Length == 15 || noIdentitas.Length == 16)
            {
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));
                cell.Text = "0" + cleanNumber;
            }
            else if (noIdentitas.Length >= 20)
            {
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));
                cell.Text = cleanNumber;
            }
            else
            {
                cell.Text = "-";
            }
        }
        else
        {
            cell.Text = "-";
        }
    }
    catch (Exception)
    {
        ((DevExpress.XtraReports.UI.XRTableCell)sender).Text = "-";
    }
}

private void tableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
{
    try 
    {
        DevExpress.XtraReports.UI.XRTableCell cell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.DetailBand detailBand = (DevExpress.XtraReports.UI.DetailBand)cell.Band;
        
        object registerNo = detailBand.Report.GetCurrentColumnValue("DebtorRegisterNo");
        
        if (registerNo != null)
        {
            string noIdentitas = registerNo.ToString().Trim();
            
            if (noIdentitas.Length == 15 || noIdentitas.Length == 16)
            {
                cell.Text = "000000";
            }
            else if (noIdentitas.Length >= 20)
            {
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));
                cell.Text = "0" + cleanNumber;
            }
            else
            {
                cell.Text = "-";
            }
        }
        else
        {
            cell.Text = "-";
        }
    }
    catch (Exception)
    {
        ((DevExpress.XtraReports.UI.XRTableCell)sender).Text = "-";
    }
}
