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

            if (uom == "PCS" || uom == "PCA")
            {
                labelUOM = "UM.0021";
            }
		else if (uom == "HANK" ||uom == "JS" ||uom == "KG" ||uom == "MSP" ||uom == "T" )
            {
                labelUOM = "UM.0033";
            }
		else if (uom == "BALL" || uom == "BKS" || uom == "BOX" || uom == "BOXX" || 
                uom == "DOS" || uom == "DOS1" || uom == "DOS2" || uom == "DOSS" || 
                uom == "DOZ" || uom == "DUS" || uom == "DUSS" || uom == "KOTAK" || uom == "KT" || uom == "KTK" 
		    ||uom == "KTK15" ||uom == "PACK" ||uom == "PETI" ||uom == "PAK" ||uom == "ROL" ||uom == "ROLL")
            {
                labelUOM = "UM.0022";
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

private string CleanNumberOnly(string input)
{
    if (string.IsNullOrEmpty(input)) return "";
    
    string result = "";
    foreach (char c in input)
    {
        if (char.IsDigit(c))
        {
            result += c;
        }
    }
    return result;
}

private void tableCell18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) 
{
    try 
    {
        DevExpress.XtraReports.UI.XRTableCell cell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        string npwp = "";
        string nik = "";
        
        if (cell.Text != null) 
        {
            string rawNpwp = cell.Text.ToString();
            npwp = CleanNumberOnly(rawNpwp);
        }
            
        if (cell.Tag != null)   
        {
            string rawNik = cell.Tag.ToString();
            nik = CleanNumberOnly(rawNik);
        }
        
        if (!string.IsNullOrEmpty(npwp))
        {
            if (npwp.Length == 15)  
            {
                cell.Text = "0" + npwp; 
            }
            else
            {
                cell.Text = "0000000000000000";       
            }
        }
        else if (!string.IsNullOrEmpty(nik))  
        {
            cell.Text = "0000000000000000"; 
        }
        else
        {
            cell.Text = "0000000000000000";             
        }
    }
    catch (Exception)
    {
        ((DevExpress.XtraReports.UI.XRTableCell)sender).Text = "-";
    }
}

private void tableCell3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) 
{
    try 
    {
        DevExpress.XtraReports.UI.XRTableCell cell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        string npwp = "";
        string nik = "";
        
        if (cell.Text != null) 
            npwp = cell.Text.ToString();
            
        if (cell.Tag != null)   
            nik = cell.Tag.ToString();
        
        if (!string.IsNullOrEmpty(npwp))
        {
            if (npwp.Length == 15)
            {
                cell.Text = "TIN"; 
            }
            else
            {
                cell.Text = "National ID";       
            }
        }
        else if (!string.IsNullOrEmpty(nik))  
        {
            cell.Text = "National ID"; 
        }
        else
        {
            cell.Text = "0000000000000000";             
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
        string npwp = "";
        string nik = "";
        
        if (cell.Text != null) 
            npwp = cell.Text.ToString();
            
        if (cell.Tag != null)   
            nik = cell.Tag.ToString();
        
        if (!string.IsNullOrEmpty(nik))
        {
            if (npwp.Length == 15)
            {
                cell.Text = nik; 
            }
            else
            {
                cell.Text = nik;       
            }
        }
        else if (!string.IsNullOrEmpty(nik))  
        {
            cell.Text = nik; 
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
        string npwp = "";
        string nik = "";
        
        if (cell.Text != null) 
            npwp = cell.Text.ToString();
            
        if (cell.Tag != null)   
            nik = cell.Tag.ToString();
        
        if (!string.IsNullOrEmpty(npwp))
        {
            if (npwp.Length == 15)
            {
                cell.Text = "0" + npwp; 
            }
            else
            {
                cell.Text = nik;       
            }
        }
        else if (!string.IsNullOrEmpty(nik))  
        {
            cell.Text = nik; 
        }
        else
        {
            cell.Text = "0000000000000000";             
        }
    }
    catch (Exception)
    {
        ((DevExpress.XtraReports.UI.XRTableCell)sender).Text = "-";
    }
}


