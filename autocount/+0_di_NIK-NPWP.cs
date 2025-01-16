private void tableCell8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
{
    try 
    {
        DevExpress.XtraReports.UI.XRTableCell tableCell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.DetailBand detailBand = (DevExpress.XtraReports.UI.DetailBand)tableCell.Band;
        
        object registerNo = detailBand.Report.GetCurrentColumnValue("DebtorRegisterNo");
        
        if (registerNo != null)
        {
            string noIdentitas = registerNo.ToString().Trim();
            string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

            if (cleanNumber.Length == 15)
            {
                tableCell.Text =  "0" + cleanNumber ;
            }
            else if (cleanNumber.Length == 16)
            {
                tableCell.Text = cleanNumber;
            }
            else
            {
                tableCell.Text = "-";
            }
        }
        else
        {
            tableCell.Text = "-";
        }
    }
    catch (Exception ex)
    {
        ((DevExpress.XtraReports.UI.XRTableCell)sender).Text = "-";
    }
}

private void tableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
	try 
    {
        DevExpress.XtraReports.UI.XRTableCell tableCell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.DetailBand detailBand = (DevExpress.XtraReports.UI.DetailBand)tableCell.Band;
        
        object registerNo = detailBand.Report.GetCurrentColumnValue("DebtorRegisterNo");
        
        if (registerNo != null)
        {
            string noIdentitas = registerNo.ToString().Trim();
            string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

            if (cleanNumber.Length == 15)
            {
                tableCell.Text = "000000" ;
            }
            else if (cleanNumber.Length == 16)
            {
                tableCell.Text = cleanNumber + "000000";
            }
            else
            {
                tableCell.Text = "-";
            }
        }
        else
        {
            tableCell.Text = "-";
        }
    }
    catch (Exception ex)
    {
        ((DevExpress.XtraReports.UI.XRTableCell)sender).Text = "-";
    }
}
