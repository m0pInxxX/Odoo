private void tableCell3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
{
    try 
    {
        DevExpress.XtraReports.UI.XRTableCell cell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.DetailBand detailBand = (DevExpress.XtraReports.UI.DetailBand)cell.Band;
        
        object registerNo = detailBand.Report.GetCurrentColumnValue("DebtorRegisterNo");
        
        if (registerNo != null)
        {
            string noIdentitas = registerNo.ToString().Trim();
            
            if (noIdentitas.Length == 15)
            {
                cell.Text = "Ini LimaBelas NPWP";
            }
            else if (noIdentitas.Length == 16)
            {
                cell.Text = "Ini EnamBelas NIK";
            }
            else if (noIdentitas.Length == 21)
            {
                cell.Text = "Ini NPWP dengan 000000";
            }
            else
            {
                cell.Text = "Ini Kurang dari 15 atau lebih dari 16";
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

private void tableCell6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
                cell.Text = cleanNumber;
            }
            else if (noIdentitas.Length == 21)
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
