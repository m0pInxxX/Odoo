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
            
            if (noIdentitas.Length == 15)
            {
                tableCell.Text =  "0" + noIdentitas ;
            }
            else if (noIdentitas.Length == 16)
            {
                tableCell.Text = noIdentitas;
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
            
            if (noIdentitas.Length == 15)
            {
                tableCell.Text = "000000" ;
            }
            else if (noIdentitas.Length == 16)
            {
                tableCell.Text = noIdentitas + "000000";
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
