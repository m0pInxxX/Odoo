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

            if (uom == "DRM" || uom == "DRUM")
            {
                labelUOM = "UM.0008";
            }
            else if (uom == "MTH" || uom == "MTR")
            {
                labelUOM = "UM.0012";
            }
            else if (uom == "DZN")
            {
                labelUOM = "UM.0017";
            }
            else if (uom == "BT" || uom == "BTG" || uom == "BTH")
            {
                labelUOM = "UM.0018";
            }
            else if (uom == "IKAT" || uom == "IKET" || uom == "PASANG" || uom == "SET")
            {
                labelUOM = "UM.0019";
            }
            else if (uom == "LBR" || uom == "LEMBAR")
            {
                labelUOM = "UM.0020";
            }
            else if (uom == "PCA" || uom == "PCS")
            {
                labelUOM = "UM.0021";
            }
            else if (uom == "BALL" || uom == "BKS" || uom == "BOX" || uom == "BOXX" || 
                     uom == "DOS" || uom == "DOS1" || uom == "DOS2" || uom == "DOSS" || 
                     uom == "DOZ" || uom == "DUS" || uom == "DUSS" || uom == "KOTAK" || 
                     uom == "KT" || uom == "KTK" || uom == "KTK15" || uom == "PACK" || 
                     uom == "PAK" || uom == "PETI" || uom == "ROL" || uom == "ROLL")
            {
                labelUOM = "UM.0022";
            }
            else if (uom == "HANK" || uom == "JS" || uom == "KG" || uom == "MSP")
            {
                labelUOM = "UM.0033";
            }
            else if (uom == "T")
            {
                labelUOM = "UM.0034";
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
