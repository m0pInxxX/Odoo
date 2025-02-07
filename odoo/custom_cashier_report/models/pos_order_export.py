import io
import base64
from odoo import models, fields, api
from odoo.tools.misc import xlsxwriter
from datetime import datetime

class POSOrderExportWizard(models.TransientModel):
    _name = 'pos.order.export.wizard'
    _description = 'Export POS Orders to XLSX'

    export_file = fields.Binary(readonly=True)
    export_filename = fields.Char(readonly=True)

    def export_to_xlsx(self):
        # Fetch POS orders to export
        pos_orders = self.env['pos.order'].browse(self.env.context.get('active_ids', []))

        # Create an in-memory file
        output = io.BytesIO()
        workbook = xlsxwriter.Workbook(output, {'in_memory': True})
        worksheet = workbook.add_worksheet('POS Orders')

        # Define headers
        headers = [
            'Doc No', 'Date', 'Code', 'Description', 'Cashier', 'UOM', 'Qty',
            'Delivered', 'Remaining', 'Unit Price', 'Taxes', 'Curr.', 'Disc.', 'Amount'
        ]

        # Add headers to the sheet
        for col, header in enumerate(headers):
            worksheet.write(0, col, header)

        # Add data rows
        row = 1
        for order in pos_orders:
            for line in order.lines:
                worksheet.write(row, 0, order.pos_reference)  # Doc No
                worksheet.write(row, 1, order.date_order.strftime('%d/%m/%Y'))  # Date
                worksheet.write(row, 2, line.product_id.default_code or '')  # Code
                worksheet.write(row, 3, line.product_id.display_name or '')  # Description
                worksheet.write(row, 4, order.user_id.name)  # Cashier
                worksheet.write(row, 5, line.product_uom_id.name or '')  # UOM
                worksheet.write(row, 6, line.qty)  # Qty
                worksheet.write(row, 7, 0.0)  # Delivered
                worksheet.write(row, 8, 0.0)  # Remaining
                worksheet.write(row, 9, line.price_unit)  # Unit Price
                worksheet.write(row, 10, '')  # Taxes
                worksheet.write(row, 11, order.currency_id.name or '')  # Currency
                worksheet.write(row, 12, '0.00%')  # Discount
                worksheet.write(row, 13, line.price_subtotal)  # Amount
                row += 1

        workbook.close()
        output.seek(0)

        # Attach the file to the wizard
        self.export_file = base64.b64encode(output.read())
        self.export_filename = f'POS_Orders_{datetime.today().strftime("%Y%m%d")}.xlsx'
        output.close()

        return {
            'type': 'ir.actions.act_window',
            'res_model': 'pos.order.export.wizard',
            'view_mode': 'form',
            'res_id': self.id,
            'target': 'new',
        }
