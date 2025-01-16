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
