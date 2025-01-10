using System.Data;
using System.Drawing;
using DevExpress.XtraReports.UI;
using System.Windows.Forms;
using BCE.AutoCount;
using BCE.AutoCount;


int count = 0;

private void DetailReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) 
{
  count = 0;
}

private void lblCount_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
  lblCount.Text = (++count).ToString();
}
/*

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


private void SummaryBand1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) 
{
    lblSumTotalWithTax.Text = Application.FormatCurrency(mySumTaxs + mySumSubjectToTax);
    lblSumTax.Text = Application.FormatCurrency(mySumTaxs);
    lblSumSubjectToTax.Text = Application.FormatCurrency(mySumSubjectToTax);
    lblSumSubjectNoTax.Text = Application.FormatCurrency(mySumNotSubjectToTax);
}
*/

private void tableCell18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
object obj = Report.GetCurrentColumnValue("DebtorRegisterNo");

//MessageBox.Show(obj.ToString());
if (obj != null)
    {
        string debtorRegisterNo = obj.ToString();

        if (debtorRegisterNo.Length == 20)
        {
            tableCell18.Text = "oke";
        }
            else if (debtorRegisterNo.Length == 15)
        {
            tableCell18.Text = "oke ya";
        }
}
//if(obj != null && obj.ToString().Length == 20)
//tableCell18.Text="oke";

}


private void tableCell123_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) 
{
object obj = setting.ExecuteScalar(
        "SELECT UOM FROM IVDTL WHERE DocKey = ?", 
        Report.GetCurrentColumnValue("DocKey")
    );

    if (obj != null)
    {
        string uom = obj.ToString().Trim();
        string labelUOM;

        if (uom == "KG")
        {
            labelUOM = "UM.003";
        }
        else if (uom == "PCS")
        {
            labelUOM = "UM.001";
        }
        else
        {
            labelUOM = "Unknown";
        }

        tableCell123.Text = labelUOM;
    }
    else
    {
        tableCell123.Text = "UOM Missing";
    }}

