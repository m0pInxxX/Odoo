private void tableCell3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
                tableCell.Text = "Ini LimaBelas NPWP";
            }
            else if (noIdentitas.Length == 16)
            {
                tableCell.Text = "Ini EnamBelas NIK";
            }
            else
            {
                tableCell.Text = "Ini Kurang dari 15 atau lebih dari 16";
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

private void tableCell6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
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
                tableCell.Text = "ini lima belas/NPWP";
            }
            else if (noIdentitas.Length == 16)
            {
                tableCell.Text = "-";
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
