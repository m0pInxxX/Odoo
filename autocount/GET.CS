using System.Data;
using System;
using System.Data;
using System.Drawing;
using System.Collections.Generic;

/*private void Report_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) 
{
	DataSet ds = (DataSet) Report.DataSource;
	
	DataTable optionTable = ds.Tables["Report Option"];
	
	if (optionTable.Rows[0]["ShowCriteria"].ToString() == "No")
	{
		PanelCriteria.Visible = false;
	}

	if (optionTable.Rows[0]["GroupBy"].ToString() == "None")
	{
		GroupHeader3.Visible = false;
	}

	new BCE.AutoCount.Report.ReportDrillDown(Report, "DocType", "DocKey", new string[] {"xrLabel24", "xrLabel25", "xrLabel26", "xrLabel27", "xrInvPrice","xrInOutQty", "xrBalQty", "xrLabel4", "xrLabel31", "xrLabel32", "xrLabel37"});
}*/

private decimal _totalIN2 = 0;
private HashSet<long> _processedKeys = new HashSet<long>();

private void Report_StartPrint(object sender, EventArgs e)
{
    _totalIN2 = 0;
    _processedKeys.Clear();
    System.Diagnostics.Debug.WriteLine("Reset _totalIN2 ke 0");
}

private void Detail1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) 
{
  decimal inQty = BCE.AutoCount.Application.RoundQuantity(DetailReport.GetCurrentColumnValue("InQty"));

  if (inQty > 0)
    xrInOutQty.Text = BCE.AutoCount.Application.FormatQuantity(inQty);
  else
  {
    decimal outQty = BCE.AutoCount.Application.RoundQuantity(DetailReport.GetCurrentColumnValue("OutQty"));

    xrInOutQty.Text = BCE.AutoCount.Application.FormatQuantity(outQty);
  }

  decimal balQty = BCE.AutoCount.Application.RoundQuantity(DetailReport.GetCurrentColumnValue("Balance"));

  if (balQty < 0)
    xrBalQty.ForeColor = Color.Red;
  else
    xrBalQty.ForeColor = Color.Black;
  
  DataRow row = null;
  long dtlKey = 0;
  xrInvPrice.Text = "";

  object obj = DetailReport.GetCurrentColumnValue("DocType");

  if (obj == null) return;
  string docType = obj.ToString();

  if (docType=="IV" || docType=="DO" || docType=="GR" || docType=="PI" || docType=="CN" || docType=="DN" ||
      docType=="DR" || docType=="CS" || docType=="PR")
  {
    dtlKey = BCE.Data.Convert.ToInt64(DetailReport.GetCurrentColumnValue("DtlKey"));
    row = BCE.AutoCount.Data.SqlDataAccess.LoadSingleDataRow(String.Format("SELECT {0}.CurrencyRate*{0}DTL.UnitPrice AS UnitPrice FROM {0}, {0}DTL WHERE {0}.DocKey={0}DTL.DocKey AND {0}DTL.DtlKey=", docType.Trim())+dtlKey.ToString());
  }
  else if (docType=="OS")
  {
    dtlKey = BCE.Data.Convert.ToInt64(DetailReport.GetCurrentColumnValue("DtlKey"));
    try
    {
      row = BCE.AutoCount.Data.SqlDataAccess.LoadSingleDataRow("SELECT POSDTL.UnitPrice FROM POSDTL WHERE POSDTL.DtlKey="+dtlKey.ToString());
    }
    catch (Exception) 
    {
    }
  }

  if (row != null)
  {
    decimal unitPrice;
    if (row["UnitPrice"] == DBNull.Value)
      unitPrice = 0;
    else
      unitPrice = System.Convert.ToDecimal(row["UnitPrice"]);

    if (inQty == 0)
      xrInvPrice.ForeColor = Color.Blue;
    else
      xrInvPrice.ForeColor = Color.Brown;

    xrInvPrice.Text = BCE.AutoCount.Application.FormatPrice(unitPrice);
  }
  
  xrIN2.Text = "";
  xrUOM2.Text = "";
  
  if (docType == "PI")
  {
    try
    {
      DataRow rowUDF = BCE.AutoCount.Data.SqlDataAccess.LoadSingleDataRow(
        "SELECT UDF_Qty2, UDF_UOM2 FROM PIDTL WHERE DtlKey=" + dtlKey.ToString());
      
      if (rowUDF != null)
      {
        if (rowUDF["UDF_Qty2"] != DBNull.Value)
        {
          decimal in2 = System.Convert.ToDecimal(rowUDF["UDF_Qty2"]);
          xrIN2.Text = BCE.AutoCount.Application.FormatQuantity(in2);
          
          if (!_processedKeys.Contains(dtlKey))
          {
            _totalIN2 += in2;
            _processedKeys.Add(dtlKey);
            System.Diagnostics.Debug.WriteLine("DtlKey: " + dtlKey + " | Menambahkan " + in2 + " ke _totalIN2, total sekarang: " + _totalIN2);
          }
          else
          {
            System.Diagnostics.Debug.WriteLine("DtlKey " + dtlKey + " sudah diproses sebelumnya, tidak ditambahkan ke total");
          }
        }
        
        if (rowUDF["UDF_UOM2"] != DBNull.Value)
        {
          xrUOM2.Text = rowUDF["UDF_UOM2"].ToString();
        }
      }
    }
    catch (Exception ex) 
    {
      System.Diagnostics.Debug.WriteLine("Error di Detail1_BeforePrint: " + ex.Message);
    }
  }
}

private void GroupHeader3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) 
{
  xrGroupBy.Text = "Group By  " + GetCurrentColumnValue("GroupIDName").ToString() + " " + GetCurrentColumnValue("GroupIDDescription").ToString();
}

private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
{
    try
    {
        xr10.Text = BCE.AutoCount.Application.FormatQuantity(_totalIN2);
        System.Diagnostics.Debug.WriteLine("Menampilkan _totalIN2 di footer: " + _totalIN2 + " dari " + _processedKeys.Count + " baris");
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine("Error di ReportFooter_BeforePrint: " + ex.Message);
        xr10.Text = BCE.AutoCount.Application.FormatQuantity(0);
    }
}
