
using System.Data;
using System.Drawing;
using DevExpress.XtraReports.UI;
using System.Linq;

int count1 = 0;
private int count = 0;
private string lastDocNo = "";
private int lastCount = 0;

private void DetailReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) 
{
  count = 0;
}

private void tableCell42_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
    try {
        DevExpress.XtraReports.UI.XRTableCell tableCell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.DetailBand detailBand = (DevExpress.XtraReports.UI.DetailBand)tableCell.Band;
        
        object docNoObj = detailBand.Report.GetCurrentColumnValue("DocNo");
        string currentDocNo = (docNoObj != null && docNoObj != DBNull.Value) ? docNoObj.ToString().Trim() : "";

        if (currentDocNo != "") {
            if (currentDocNo == lastDocNo) {
                tableCell.Text = lastCount.ToString();
            } else {
                count++;
                lastCount = count;  
                lastDocNo = currentDocNo;  
                tableCell.Text = lastCount.ToString();
            }
        } else {
            tableCell.Text = "0";
        }
    }
    catch (Exception) {
        tableCell42.Text = "0";     }
}

/*
using BCE.AutoCount;

public decimal mySumNotSubjectToTax = 0;
public decimal mySumSubjectToTax = 0;
public decimal mySumTaxs = 0;

private void DetailBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) 
{
    object obj = GetCurrentColumnValue("DocKey");
    if ( obj == null ) return;

    long dockey = BCE.Data.Convert.ToDBInt64(obj);
    decimal notSubjectToTax = 0;
    decimal subjectToTax = 0;

    string sql = "SELECT SubTotalExTax, TaxType FROM IVDTL WHERE DocKey = ?";

    DataTable table = Application.DBSetting.GetDataTable(sql, false, dockey);

    for (int i = 0; i < table.Rows.Count; i++)
    {
        DataRow row = table.Rows[i];
  	      
        if (row["TaxType"].ToString().Length == 0)
        {
            decimal tempNotSubjectToTax = BCE.Data.Convert.ToDBDecimal(row["SubTotalExTax"]);
            notSubjectToTax += tempNotSubjectToTax;
        }
        else
        {
            decimal tempSubjectToTax = BCE.Data.Convert.ToDBDecimal(row["SubTotalExTax"]);
            subjectToTax += tempSubjectToTax;
        }
    }

    BCE.AutoCount.Common.TaxType taxType = BCE.AutoCount.Common.TaxType.Create(Application.DBSetting);

    obj = GetCurrentColumnValue("Footer1TaxType");
    if (obj.ToString().Length > 0)
    {
        try
        {
            decimal taxableAmount = BCE.Data.Convert.ToDecimal(GetCurrentColumnValue("Footer1Amt"));

            BCE.AutoCount.Common.TaxRecord taxRecord = taxType.CalcTaxAmount(obj.ToString(), taxableAmount);

            if (!taxRecord.IsInclusive)
		subjectToTax += BCE.Data.Convert.ToDBDecimal(GetCurrentColumnValue("Footer1Amt"));
	    else
	        subjectToTax += BCE.Data.Convert.ToDBDecimal(GetCurrentColumnValue("Footer1Amt")) - BCE.Data.Convert.ToDBDecimal(GetCurrentColumnValue("Footer1Tax"));
        } catch { }
    }
    else
        notSubjectToTax += BCE.Data.Convert.ToDecimal(GetCurrentColumnValue("Footer1Amt"));

    obj = GetCurrentColumnValue("Footer2TaxType");
    if (obj.ToString().Length > 0)
    {
        try
        {
            decimal taxableAmount = BCE.Data.Convert.ToDecimal(GetCurrentColumnValue("Footer2Amt"));

            BCE.AutoCount.Common.TaxRecord taxRecord = taxType.CalcTaxAmount(obj.ToString(), taxableAmount);

            if (!taxRecord.IsInclusive)
		subjectToTax += BCE.Data.Convert.ToDBDecimal(GetCurrentColumnValue("Footer2Amt"));
	    else
	        subjectToTax += BCE.Data.Convert.ToDBDecimal(GetCurrentColumnValue("Footer2Amt")) - BCE.Data.Convert.ToDBDecimal(GetCurrentColumnValue("Footer2Tax"));
        } catch { }
    }
    else
        notSubjectToTax += BCE.Data.Convert.ToDecimal(GetCurrentColumnValue("Footer2Amt"));

    obj = GetCurrentColumnValue("Footer3TaxType");
    if (obj.ToString().Length > 0)
    {
        try
        {
            decimal taxableAmount = BCE.Data.Convert.ToDecimal(GetCurrentColumnValue("Footer3Amt"));

            BCE.AutoCount.Common.TaxRecord taxRecord = taxType.CalcTaxAmount(obj.ToString(), taxableAmount);

            if (!taxRecord.IsInclusive)
		subjectToTax += BCE.Data.Convert.ToDBDecimal(GetCurrentColumnValue("Footer3Amt"));
	    else
	        subjectToTax += BCE.Data.Convert.ToDBDecimal(GetCurrentColumnValue("Footer3Amt")) - BCE.Data.Convert.ToDBDecimal(GetCurrentColumnValue("Footer3Tax"));
        } catch { }
    }
    else
        notSubjectToTax += BCE.Data.Convert.ToDecimal(GetCurrentColumnValue("Footer3Amt"));

    subjectToTax = Application.RoundCurrency(subjectToTax);
    notSubjectToTax = Application.RoundCurrency(notSubjectToTax);

    decimal tax = BCE.Data.Convert.ToDecimal(GetCurrentColumnValue("ExTax"));

    decimal totalSubTax = subjectToTax + tax;
    if (totalSubTax == 0)
        lblTotalSubTax.Text = "";
    else
        lblTotalSubTax.Text = Application.FormatCurrency(totalSubTax);

    if (tax == 0)
        lblTax.Text = "";
    else
        lblTax.Text = Application.FormatCurrency(tax);

    if (subjectToTax == 0)
        lblSubjectToTax.Text = "";
    else
        lblSubjectToTax.Text = Application.FormatCurrency(subjectToTax);

    if (notSubjectToTax == 0)
        lblNotSubjectToTax.Text = "";
    else
        lblNotSubjectToTax.Text = Application.FormatCurrency(notSubjectToTax);

    // Assign to global variables
    mySumNotSubjectToTax += notSubjectToTax;
    mySumSubjectToTax += subjectToTax;
    mySumTaxs += tax;
}

using BCE.AutoCount;

private void SummaryBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) 
{
    lblSumTotalWithTax.Text = Application.FormatCurrency(mySumTaxs + mySumSubjectToTax);
    lblSumTax.Text = Application.FormatCurrency(mySumTaxs);
    lblSumSubjectToTax.Text = Application.FormatCurrency(mySumSubjectToTax);
    lblSumSubjectNoTax.Text = Application.FormatCurrency(mySumNotSubjectToTax);
}
*/

private void tableCell133_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
{
    try 
    {
        // Pertama, deklarasikan cell sebelum digunakan
        DevExpress.XtraReports.UI.XRTableCell cell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.DetailBand detailBand = cell.Band as DevExpress.XtraReports.UI.DetailBand;

        object branchCode = detailBand.Report.GetCurrentColumnValue("BranchCode");
        string branch = "";
        string debtor = "";

        if (branchCode != null && !string.IsNullOrEmpty(branchCode.ToString()))
        {
            if (cell.Text != null) 
                branch = cell.Text.ToString().Trim();  
            
            if (!string.IsNullOrEmpty(branch))
            {
                string cleanBranch = string.Join("", System.Text.RegularExpressions.Regex.Split(branch, @"[^\d]"));
                
                if (cleanBranch.All(c => c == '0'))
                {
                    cell.Text = "Tidak ada keduanya Branch Code";
                    return;
                }
                else if (cleanBranch.Length == 15 || (cleanBranch.Length == 16 && cleanBranch[0] == '0'))
                {
                    cell.Text = "NPWP Branch Code"; 
                    return;
                }
                else if (cleanBranch.Length == 16)
                {
                    cell.Text = "NIK Branch Code"; 
                    return;
                }
            }
        }
        else
        {
            if (cell.Tag != null)   
                debtor = cell.Tag.ToString().Trim();    
            if (!string.IsNullOrEmpty(debtor))
            {
                string cleanDebtor = string.Join("", System.Text.RegularExpressions.Regex.Split(debtor, @"[^\d]"));
                
                if (cleanDebtor.All(c => c == '0'))
                {
                    cell.Text = "Tidak ada keduanya Deptor";
                    return;
                }
                else if (cleanDebtor.Length == 15 || (cleanDebtor.Length == 16 && cleanDebtor[0] == '0'))
                {
                    cell.Text = "NPWP Deptor"; 
                    return;
                }
                else if (cleanDebtor.Length == 16)
                {
                    cell.Text = "NIK Deptor"; 
                    return;
                }
            }
        }
        
        cell.Text = "Tidak ada keduanya";
    }
    catch
    {
        ((DevExpress.XtraReports.UI.XRTableCell)sender).Text = "-";
    }
}

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

            if (uom == "BTG" || uom == "BTG1" || uom == "BTL" || uom == "GLN" || 
                uom == "KLG" || uom == "PAK" || uom == "Pcs" || uom == "ROL" || 
                uom == "ROLL" || uom == "TUBE" || uom == "PCS" )
            {
                labelUOM = "UM.0021";
            }
            else if (uom == "BOX" || uom == "DOS" || uom == "OLD" || uom == "DOS1")
            {
                labelUOM = "UM.0022";
            }
            else if (uom == "JS" || uom == "KTG" || uom == "PETI" || uom == "SAK" || uom == "TMB" || uom == "BAL" || uom == "BAL1" || uom == "krg" || uom == "KRG")
            {
                labelUOM = "UM.0033";
            }
            else if (uom == "LBR")
            {
                labelUOM = "UM.0020";
            }
            else if (uom == "DZN")
            {
                labelUOM = "UM.0017";
            }
            else if (uom == "M3" || uom == "MTR")
            {
                labelUOM = "UM.0013";
            }
		else if (uom == "Kg" || uom == "KG")
            {
                labelUOM = "UM.0003";
            }
            else if (uom == "UNIT")
            {
                labelUOM = "UM.0018";
            }
            else if (uom == "KTK" || uom == "PASANG" || uom == "PSG" || uom == "SET"|| uom == "KTK1")
            {
                labelUOM = "UM.0019";
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
        DevExpress.XtraReports.UI.GroupHeaderBand groupHeaderBand = (DevExpress.XtraReports.UI.GroupHeaderBand)tableCell.Band;
        
        object branchCode = groupHeaderBand.Report.GetCurrentColumnValue("BranchCode");
        
        if (branchCode != null && !string.IsNullOrEmpty(branchCode.ToString()) && branchCode != DBNull.Value)
        {
            object registerNo = groupHeaderBand.Report.GetCurrentColumnValue("BranchAddress4");
            if (registerNo != null)
            {
                string noIdentitas = registerNo.ToString().Trim();
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

                if (cleanNumber.All(c => c == '0'))
                {
                    tableCell.Text = "-";
                }
                else if(cleanNumber.Length == 15)
                {
                    tableCell.Text = "TIN";
                }
                else if (cleanNumber.Length == 16 && cleanNumber[0] == '0')
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
            object registerNo = groupHeaderBand.Report.GetCurrentColumnValue("InvAddr4");
            if (registerNo != null)
            {
                string noIdentitas = registerNo.ToString().Trim();
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

                if (cleanNumber.All(c => c == '0'))
                {
                    tableCell.Text = "-";
                }
                else if(cleanNumber.Length == 15)
                {
                    tableCell.Text = "TIN";
                }
                else if (cleanNumber.Length == 16 && cleanNumber[0] == '0')
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
        DevExpress.XtraReports.UI.GroupHeaderBand groupHeaderBand = (DevExpress.XtraReports.UI.GroupHeaderBand)tableCell.Band;
        
        object branchCode = groupHeaderBand.Report.GetCurrentColumnValue("BranchCode");
        
        if (branchCode != null && !string.IsNullOrEmpty(branchCode.ToString()) && branchCode != DBNull.Value)
        {
            object registerNo = groupHeaderBand.Report.GetCurrentColumnValue("BranchAddress4");
            if (registerNo != null)
            {
                string noIdentitas = registerNo.ToString().Trim();
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

                if (cleanNumber.All(c => c == '0'))
                {
                    tableCell.Text = "-";
                }
                else if (cleanNumber.Length == 15)
                {
                    tableCell.Text = "0" + cleanNumber ;
                }
                else if (cleanNumber.Length == 16 && cleanNumber[0] == '0')
                {
                    tableCell.Text = cleanNumber ;
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
            object registerNo = groupHeaderBand.Report.GetCurrentColumnValue("InvAddr4");
            if (registerNo != null)
            {
                string noIdentitas = registerNo.ToString().Trim();
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

                if (cleanNumber.All(c => c == '0'))
                {
                    tableCell.Text = "-";
                }
                else if (cleanNumber.Length == 15)
                {
                    tableCell.Text = "0" + cleanNumber ;
                }
                else if (cleanNumber.Length == 16 && cleanNumber[0] == '0')
                {
                    tableCell.Text = cleanNumber ;
                }
                else if (cleanNumber.Length == 16)
                {
                    tableCell.Text = "0000000000000000" ;
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

private void tableCell7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
    try 
    {
        DevExpress.XtraReports.UI.XRTableCell cell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.GroupHeaderBand groupHeaderBand = (DevExpress.XtraReports.UI.GroupHeaderBand)cell.Band;
        
        object branchNameObj = groupHeaderBand.Report.GetCurrentColumnValue("BranchName");
        string[] prefixes = new[] { "PT.", "PT ", "CV.", "CV ", "PT..", "CV..", "PT .", "CV ." };

        if (branchNameObj != null && !string.IsNullOrEmpty(branchNameObj.ToString().Trim()))
        {
            string nameToProcess = branchNameObj.ToString().Trim();
            foreach (var prefix in prefixes)
            {
                if (nameToProcess.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    string nameWithoutPrefix = nameToProcess.Substring(prefix.Length).Trim();
                    nameWithoutPrefix = nameWithoutPrefix.Replace(".", "").Trim();
                    string cleanPrefix = prefix.TrimEnd('.', ' ');
                    
                    cell.Text = string.Format("{0} {1}", nameWithoutPrefix, cleanPrefix);
                    return;
                }
            }
            cell.Text = nameToProcess.Replace(".", "").Trim();
            return;
        }
        
        object companyNameObj = groupHeaderBand.Report.GetCurrentColumnValue("DebtorName");
        if (companyNameObj != null && !string.IsNullOrEmpty(companyNameObj.ToString().Trim()))
        {
            string nameToProcess = companyNameObj.ToString().Trim();
            foreach (var prefix in prefixes)
            {
                if (nameToProcess.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    string nameWithoutPrefix = nameToProcess.Substring(prefix.Length).Trim();
                    nameWithoutPrefix = nameWithoutPrefix.Replace(".", "").Trim();
                    string cleanPrefix = prefix.TrimEnd('.', ' ');
                    
                    cell.Text = string.Format("{0} {1}", nameWithoutPrefix, cleanPrefix);
                    return;
                }
            }
            cell.Text = nameToProcess.Replace(".", "").Trim();
            return;
        }

        cell.Text = "-";
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
        DevExpress.XtraReports.UI.XRTableCell tableCell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.GroupHeaderBand groupHeaderBand = (DevExpress.XtraReports.UI.GroupHeaderBand)tableCell.Band;
        
        object branchCode = groupHeaderBand.Report.GetCurrentColumnValue("BranchCode");
        
        if (branchCode != null && !string.IsNullOrEmpty(branchCode.ToString()) && branchCode != DBNull.Value)
        {
            object registerNo = groupHeaderBand.Report.GetCurrentColumnValue("BranchAddress4");
            if (registerNo != null)
            {
                string noIdentitas = registerNo.ToString().Trim();
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

                if (cleanNumber.All(c => c == '0'))
                {
                    tableCell.Text = "Tidak Ada Isi";
                }
                else if(cleanNumber.Length == 15)
                {
                    tableCell.Text = "-";
                }
                else if (cleanNumber.Length == 16 && cleanNumber[0] == '0')
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
            object registerNo = groupHeaderBand.Report.GetCurrentColumnValue("InvAddr4");
            if (registerNo != null)
            {
                string noIdentitas = registerNo.ToString().Trim();
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

                if (cleanNumber.All(c => c == '0'))
                {
                    tableCell.Text = "Tidak Ada Isi";
                }
                else if(cleanNumber.Length == 15)
                {
                    tableCell.Text = "-";
                }
                else if (cleanNumber.Length == 16 && cleanNumber[0] == '0')
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

private void tableCell8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) 
{
    try 
    {
        DevExpress.XtraReports.UI.XRTableCell tableCell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.GroupHeaderBand groupHeaderBand = (DevExpress.XtraReports.UI.GroupHeaderBand)tableCell.Band;
        
        object add1 = groupHeaderBand.Report.GetCurrentColumnValue("InvAddr1");
        object add2 = groupHeaderBand.Report.GetCurrentColumnValue("InvAddr2");
        object add3 = groupHeaderBand.Report.GetCurrentColumnValue("InvAddr3");
        string invAddress = add1.ToString() + " " + add2.ToString() + " " + add3.ToString();

        object badd1 = groupHeaderBand.Report.GetCurrentColumnValue("BranchAddress1");
        object badd2 = groupHeaderBand.Report.GetCurrentColumnValue("BranchAddress2");
        object badd3 = groupHeaderBand.Report.GetCurrentColumnValue("BranchAddress3");
        string branchAddress = badd1.ToString() + " " + badd2.ToString() + " " + badd3.ToString();

        object branchCode = groupHeaderBand.Report.GetCurrentColumnValue("BranchCode");
        
        if (branchCode == null || string.IsNullOrEmpty(branchCode.ToString()) || branchCode == DBNull.Value)
            tableCell.Text = invAddress;
        else
            tableCell.Text = branchAddress;
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
        DevExpress.XtraReports.UI.GroupHeaderBand groupHeaderBand = (DevExpress.XtraReports.UI.GroupHeaderBand)tableCell.Band;
        
        object branchCode = groupHeaderBand.Report.GetCurrentColumnValue("BranchCode");
        
        if (branchCode != null && !string.IsNullOrEmpty(branchCode.ToString()) && branchCode != DBNull.Value)
        {
            object registerNo = groupHeaderBand.Report.GetCurrentColumnValue("BranchAddress4");
            if (registerNo != null)
            {
                string noIdentitas = registerNo.ToString().Trim();
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

                if (cleanNumber.All(c => c == '0'))
                {
                    tableCell.Text = "000000";
                }
                else if (cleanNumber.Length == 15)
                {
                    tableCell.Text = "0" + cleanNumber + "000000";
                }
                else if (cleanNumber.Length == 16 && cleanNumber[0] == '0')
                {
                    tableCell.Text = cleanNumber + "000000";
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
            object registerNo = groupHeaderBand.Report.GetCurrentColumnValue("InvAddr4");
            if (registerNo != null)
            {
                string noIdentitas = registerNo.ToString().Trim();
                string cleanNumber = string.Join("", System.Text.RegularExpressions.Regex.Split(noIdentitas, @"[^\d]"));

                if (cleanNumber.All(c => c == '0'))
                {
                    tableCell.Text = "000000";
                }
                else if (cleanNumber.Length == 15)
                {
                    tableCell.Text = "0" +cleanNumber + "000000";                
                }
                else if (cleanNumber.Length == 16 && cleanNumber[0] == '0')
                {
                    tableCell.Text = cleanNumber + "000000";
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

private void tableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
    try {
        DevExpress.XtraReports.UI.XRTableCell tableCell = (DevExpress.XtraReports.UI.XRTableCell)sender;
        DevExpress.XtraReports.UI.GroupHeaderBand groupHeaderBand = (DevExpress.XtraReports.UI.GroupHeaderBand)tableCell.Band;
 
        object docNoObj = groupHeaderBand.Report.GetCurrentColumnValue("DocNo");
        string currentDocNo = (docNoObj != null && docNoObj != DBNull.Value) ? docNoObj.ToString().Trim() : "";

        if (currentDocNo != "") {
            if (currentDocNo == lastDocNo) {
                tableCell.Text = lastCount.ToString();
            } else {
                count1++;
                lastCount = count1;  
                lastDocNo = currentDocNo;  
                tableCell.Text = lastCount.ToString();
            }
        } else {
            tableCell.Text = "0";
        }
    }
    catch (Exception) {
        tableCell42.Text = "0";     }
}

private bool ShouldShowBand(DevExpress.XtraReports.UI.Band band)
{
    try 
    {
        object branchCode = band.Report.GetCurrentColumnValue("BranchCode");
        object branchAddr4 = band.Report.GetCurrentColumnValue("BranchAddress4");
        object invAddr4 = band.Report.GetCurrentColumnValue("InvAddr4");

        if ((branchCode == null || string.IsNullOrWhiteSpace(branchCode.ToString()) || branchCode == DBNull.Value) &&
            (invAddr4 == null || string.IsNullOrWhiteSpace(invAddr4.ToString()) || invAddr4 == DBNull.Value))
        {
            return false;
        }

        if (branchCode != null && !string.IsNullOrWhiteSpace(branchCode.ToString()) && branchCode != DBNull.Value)
        {
            if (branchAddr4 == null || string.IsNullOrWhiteSpace(branchAddr4.ToString()) || branchAddr4 == DBNull.Value)
                return false;

            string cleanBranch = string.Join("", System.Text.RegularExpressions.Regex.Split(branchAddr4.ToString(), @"[^\d]"));
            if (cleanBranch.All(c => c == '0'))
                return false;

            return true;
        }

        if (invAddr4 == null || string.IsNullOrWhiteSpace(invAddr4.ToString()) || invAddr4 == DBNull.Value)
            return false;

        string cleanInv = string.Join("", System.Text.RegularExpressions.Regex.Split(invAddr4.ToString(), @"[^\d]"));
        if (cleanInv.All(c => c == '0'))
            return false;

        return true;
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine("Error in ShouldShowBand: " + ex.Message);
        return false;
    }
}

private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) 
{
    DevExpress.XtraReports.UI.DetailBand detail = (DevExpress.XtraReports.UI.DetailBand)sender;
    e.Cancel = !ShouldShowBand(detail);
}

private void GroupHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) 
{
    DevExpress.XtraReports.UI.GroupHeaderBand groupHeader = (DevExpress.XtraReports.UI.GroupHeaderBand)sender;
    e.Cancel = !ShouldShowBand(groupHeader);
}
