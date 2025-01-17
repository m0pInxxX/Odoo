private void tableCell123_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
{
    try 
    {
        DevExpress.XtraReports.UI.XRTableCell cell = sender as DevExpress.XtraReports.UI.XRTableCell;
        DevExpress.XtraReports.UI.DetailBand detailBand = cell.Band as DevExpress.XtraReports.UI.DetailBand;
        
        object obj = detailBand.Report.GetCurrentColumnValue("UOM");

        if (obj != null)
        {
            string uom = obj.ToString().Trim().ToUpper();
            string labelUOM;

            if (uom == "BTL" || uom == "CUP" || uom == "DRUM" || uom == "JRG" || 
                uom == "KLG" || uom == "LOAF" || uom == "PACK" || uom == "PCS" || 
                uom == "POUCH" || uom == "SCH" || uom == "SHEET" || uom == "TPL")
            {
                labelUOM = "UM.0021";
            }
            else if (uom == "BALL" || uom == "KTN" || uom == "PAIL" || uom == "ZAK")
            {
                labelUOM = "UM.0018";
            }
            else if (uom == "RIM" || uom == "SET")
            {
                labelUOM = "UM.0019";
            }
            else if (uom == "LBR")
            {
                labelUOM = "UM.0020";
            }
            else if (uom == "LSN")
            {
                labelUOM = "UM.0017";
            }
            else if (uom == "BOX")
            {
                labelUOM = "UM.0022";
            }
		else if (uom == "KG")
            {
                labelUOM = "UM.0003";
            }

            else
            {
                labelUOM = uom;
            }

            tableCell123.Text = labelUOM;
        }
        else
        {
            tableCell123.Text = "";
        }
    }
    catch
    {
        tableCell123.Text = "";
    }
}

private void tableCell3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
{
    try 
    {
        DevExpress.XtraReports.UI.XRTableCell tableCell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.DetailBand detailBand = (DevExpress.XtraReports.UI.DetailBand)tableCell.Band;
        
        object branchCode = detailBand.Report.GetCurrentColumnValue("BranchCode");
        
        if (branchCode != null && !string.IsNullOrEmpty(branchCode.ToString()) && branchCode != DBNull.Value)
        {
            object registerNo = detailBand.Report.GetCurrentColumnValue("BranchAddress4");
            if (registerNo != null)
            {
                string noIdentitas = registerNo.ToString().Trim();
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

                if (cleanNumber.Length == 15)
                {
                    tableCell.Text = "TIN";
                }
		    else if (cleanNumber.Length == 16)
                {
                    tableCell.Text = "National ID";
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
        else
        {
            object registerNo = detailBand.Report.GetCurrentColumnValue("DebtorAddress4");
            if (registerNo != null)
            {
                string noIdentitas = registerNo.ToString().Trim();
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

                if (cleanNumber.Length == 15)
                {
                    tableCell.Text = "TIN";
                }
		    else if (cleanNumber.Length == 16)
                {
                    tableCell.Text = "National ID";
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
    }
    catch
    {
        ((DevExpress.XtraReports.UI.XRTableCell)sender).Text = "0000000000000000";
    }
}

private void tableCell18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) 
{
    try 
    {
        DevExpress.XtraReports.UI.XRTableCell tableCell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.DetailBand detailBand = (DevExpress.XtraReports.UI.DetailBand)tableCell.Band;
        
        object branchCode = detailBand.Report.GetCurrentColumnValue("BranchCode");
        
        if (branchCode != null && !string.IsNullOrEmpty(branchCode.ToString()) && branchCode != DBNull.Value)
        {
            object registerNo = detailBand.Report.GetCurrentColumnValue("BranchAddress4");
            if (registerNo != null)
            {
                string noIdentitas = registerNo.ToString().Trim();
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

                if (cleanNumber.Length == 15)
                {
                    tableCell.Text = "0" + cleanNumber;
                }
		    else if (cleanNumber.Length == 16)
                {
                    tableCell.Text = "0000000000000000";
                }
                else
                {
                    tableCell.Text = "0000000000000000";
                }
            }
            else
            {
                tableCell.Text = "0000000000000000";
            }
        }
        else
        {
            object registerNo = detailBand.Report.GetCurrentColumnValue("DebtorAddress4");
            if (registerNo != null)
            {
                string noIdentitas = registerNo.ToString().Trim();
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

                if (cleanNumber.Length == 15)
                {
                    tableCell.Text = "0" + cleanNumber;
                }
 		    else if (cleanNumber.Length == 16)
                {
                    tableCell.Text = "0000000000000000";
                }

                else
                {
                    tableCell.Text = "0000000000000000";
                }
            }
            else
            {
                tableCell.Text = "0000000000000000";
            }
        }
    }
    catch
    {
        ((DevExpress.XtraReports.UI.XRTableCell)sender).Text = "0000000000000000";
    }
}

/*private void tableCell17_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
    try 
    {
        DevExpress.XtraReports.UI.XRTableCell tableCell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.DetailBand detailBand = (DevExpress.XtraReports.UI.DetailBand)tableCell.Band;
        
        object branchCode = detailBand.Report.GetCurrentColumnValue("BranchCode");
        
        if (branchCode != null && !string.IsNullOrEmpty(branchCode.ToString()) && branchCode != DBNull.Value)
        {
            object registerNo = detailBand.Report.GetCurrentColumnValue("BranchAddress4");
            if (registerNo != null)
            {
                string noIdentitas = registerNo.ToString().Trim();
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

                if (cleanNumber.Length == 15)
                {
                    tableCell.Text = "0" + cleanNumber;
                }
		    else if (cleanNumber.Length == 16)
                {
                    tableCell.Text = cleanNumber;
                }
                else
                {
                    tableCell.Text = "0000000000000000";
                }
            }
            else
            {
                tableCell.Text = "0000000000000000";
            }
        }
        else
        {
            object registerNo = detailBand.Report.GetCurrentColumnValue("DebtorAddress4");
            if (registerNo != null)
            {
                string noIdentitas = registerNo.ToString().Trim();
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

                if (cleanNumber.Length == 15)
                {
                    tableCell.Text = "0" + cleanNumber;
                }
 		    else if (cleanNumber.Length == 16)
                {
                    tableCell.Text = cleanNumber;
                }

                else
                {
                    tableCell.Text = "0000000000000000";
                }
            }
            else
            {
                tableCell.Text = "0000000000000000";
            }
        }
    }
    catch
    {
        ((DevExpress.XtraReports.UI.XRTableCell)sender).Text = "0000000000000000";
    }
}*/

private void tableCell6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) 
{
    try 
    {
        DevExpress.XtraReports.UI.XRTableCell tableCell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.DetailBand detailBand = (DevExpress.XtraReports.UI.DetailBand)tableCell.Band;
        
        object branchCode = detailBand.Report.GetCurrentColumnValue("BranchCode");
        
        if (branchCode != null && !string.IsNullOrEmpty(branchCode.ToString()) && branchCode != DBNull.Value)
        {
            object registerNo = detailBand.Report.GetCurrentColumnValue("BranchAddress4");
            if (registerNo != null)
            {
                string noIdentitas = registerNo.ToString().Trim();
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

                if (cleanNumber.Length == 15)
                {
                    tableCell.Text = "-";
                }
		    else if (cleanNumber.Length == 16)
                {
                    tableCell.Text = cleanNumber;
                }
                else
                {
                    tableCell.Text = "Tidak Ada Isi";
                }
            }
            else
            {
                tableCell.Text = "Tidak Ada Isi";
            }
        }
        else
        {
            object registerNo = detailBand.Report.GetCurrentColumnValue("DebtorAddress4");
            if (registerNo != null)
            {
                string noIdentitas = registerNo.ToString().Trim();
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

                if (cleanNumber.Length == 15)
                {
                    tableCell.Text = "-";
                }
		    else if (cleanNumber.Length == 16)
                {
                    tableCell.Text = cleanNumber;
                }
                else
                {
                    tableCell.Text = "Tidak Ada Isi";
                }
            }
            else
            {
                tableCell.Text = "Tidak Ada Isi";
            }
        }
    }
    catch
    {
        ((DevExpress.XtraReports.UI.XRTableCell)sender).Text = "0000000000000000";
    }
}

private void tableCell7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
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
        string[] prefixes = new[] { "PT.", "PT ", "CV.", "CV ", "PT..", "CV..", "PT .", "CV ." };
        
        foreach (var prefix in prefixes)
        {
            if (companyName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                string nameWithoutPrefix = companyName.Substring(prefix.Length).Trim();
                
                nameWithoutPrefix = nameWithoutPrefix.Replace(".", "").Trim();
                
                string cleanPrefix = prefix.TrimEnd('.', ' ');
                
                cell.Text = string.Format("{0} {1}", nameWithoutPrefix, cleanPrefix);
                return;
            }
        }
        
        cell.Text = companyName.Replace(".", "").Trim();
    }
    catch (Exception)
    {
        ((DevExpress.XtraReports.UI.XRTableCell)sender).Text = "-";
    }
}

private void tableCell8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) 
{
    try 
    {
        DevExpress.XtraReports.UI.XRTableCell tableCell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.DetailBand detailBand = (DevExpress.XtraReports.UI.DetailBand)tableCell.Band;
        
        object branchCode = detailBand.Report.GetCurrentColumnValue("BranchCode");
        string fullAddress = "";
        
        if (branchCode != null && !string.IsNullOrEmpty(branchCode.ToString()) && branchCode != DBNull.Value)
        {
            object addr1 = detailBand.Report.GetCurrentColumnValue("InvAddr1");
            object addr2 = detailBand.Report.GetCurrentColumnValue("InvAddr2");
            object addr3 = detailBand.Report.GetCurrentColumnValue("InvAddr3");

            if (addr1 != null && addr1 != DBNull.Value) 
                fullAddress += addr1.ToString().Trim();
            
            if (addr2 != null && addr2 != DBNull.Value)
            {
                if (!string.IsNullOrEmpty(fullAddress)) fullAddress += " ";
                fullAddress += addr2.ToString().Trim();
            }
            
            if (addr3 != null && addr3 != DBNull.Value)
            {
                if (!string.IsNullOrEmpty(fullAddress)) fullAddress += " ";
                fullAddress += addr3.ToString().Trim();
            }
        }
        else
        {
            object ba1 = detailBand.Report.GetCurrentColumnValue("BranchAddress1");
            object ba2 = detailBand.Report.GetCurrentColumnValue("BranchAddress2");
            object ba3 = detailBand.Report.GetCurrentColumnValue("BranchAddress3");

            if (ba1 != null && ba1 != DBNull.Value) 
                fullAddress += ba1.ToString().Trim();
            
            if (ba2 != null && ba2 != DBNull.Value)
            {
                if (!string.IsNullOrEmpty(fullAddress)) fullAddress += " ";
                fullAddress += ba2.ToString().Trim();
            }
            
            if (ba3 != null && ba3 != DBNull.Value)
            {
                if (!string.IsNullOrEmpty(fullAddress)) fullAddress += " ";
                fullAddress += ba3.ToString().Trim();
            }
        }

        tableCell.Text = !string.IsNullOrEmpty(fullAddress) ? fullAddress : "-";
    }
    catch (Exception)
    {
        ((DevExpress.XtraReports.UI.XRTableCell)sender).Text = "-";
    }
}

private void tableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
    try 
    {
        DevExpress.XtraReports.UI.XRTableCell tableCell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.DetailBand detailBand = (DevExpress.XtraReports.UI.DetailBand)tableCell.Band;
        
        object branchCode = detailBand.Report.GetCurrentColumnValue("BranchCode");
        
        if (branchCode != null && !string.IsNullOrEmpty(branchCode.ToString()) && branchCode != DBNull.Value)
        {
            object registerNo = detailBand.Report.GetCurrentColumnValue("BranchAddress4");
            if (registerNo != null)
            {
                string noIdentitas = registerNo.ToString().Trim();
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

                if (cleanNumber.Length == 15)
                {
                    tableCell.Text = cleanNumber + "0";
                }
		    else if (cleanNumber.Length == 16)
                {
                    tableCell.Text = "000000";
                }
                else
                {
                    tableCell.Text = "000000";
                }
            }
            else
            {
                tableCell.Text = "000000";
            }
        }
        else
        {
            object registerNo = detailBand.Report.GetCurrentColumnValue("DebtorAddress4");
            if (registerNo != null)
            {
                string noIdentitas = registerNo.ToString().Trim();
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

                if (cleanNumber.Length == 15)
                {
                    tableCell.Text = cleanNumber + "0";
                }
		    else if (cleanNumber.Length == 16)
                {
                    tableCell.Text = "000000";
                }
                else
                {
                    tableCell.Text = "000000";
                }
            }
            else
            {
                tableCell.Text = "000000";
            }
        }
    }
    catch
    {
        ((DevExpress.XtraReports.UI.XRTableCell)sender).Text = "0000000000000000";
    }
}
