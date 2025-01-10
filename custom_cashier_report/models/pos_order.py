from odoo import models
from datetime import datetime
import xlsxwriter
import base64
from io import BytesIO

class PosOrder(models.Model):
    _inherit = 'pos.order'

    def get_pos_details(self):
        """Method to fetch POS details for the report."""
        result = []
        for line in self.lines:
            result.append({
                'product_code': line.product_id.default_code,
                'product_name': line.product_id.name,
                'quantity': line.qty,
                'price_unit': line.price_unit,
                'subtotal': line.price_subtotal,
                'uom': line.product_uom_id.name,
            })
        return result

    def action_export_xlsx(self):
        output = BytesIO()
        workbook = xlsxwriter.Workbook(output)
        worksheet = workbook.add_worksheet('POS Orders')

        # Formats
        header_format = workbook.add_format({
            'bold': True,
            'align': 'center',
            'border': 1,
            'font_size': 11
        })
        
        data_format = workbook.add_format({
            'align': 'left',
            'border': 1,
            'font_size': 10
        })
        
        number_format = workbook.add_format({
            'align': 'right',
            'border': 1,
            'font_size': 10,
            'num_format': '#,##0.00'
        })

        # Headers
        headers = ['Doc No', 'Date', 'Code', 'Description', 'Cashier Name', 
                  'UOM', 'Qty', 'Discount', 'Remaining', 'Unit Price', 
                  'Tax', 'Exch. Rate', 'Amount']
        for col, header in enumerate(headers):
            worksheet.write(0, col, header, header_format)

        row = 1
        for order in self:
            for line in order.lines:
                worksheet.write(row, 0, order.name, data_format)  # Doc No
                worksheet.write(row, 1, order.date_order.strftime('%d/%m/%Y'), data_format)  # Date
                worksheet.write(row, 2, line.product_id.default_code or '', data_format)  # Code
                worksheet.write(row, 3, line.product_id.name, data_format)  # Description
                worksheet.write(row, 4, order.user_id.name, data_format)  # Cashier Name
                worksheet.write(row, 5, line.product_uom_id.name, data_format)  # UOM
                worksheet.write(row, 6, line.qty, number_format)  # Qty
                worksheet.write(row, 7, line.discount, number_format)  # Discount
                worksheet.write(row, 8, 0, number_format)  # Remaining
                worksheet.write(row, 9, line.price_unit, number_format)  # Unit Price
                worksheet.write(row, 10, line.tax_ids_after_fiscal_position.amount or 0, number_format)  # Tax
                worksheet.write(row, 11, 1, number_format)  # Exch. Rate
                worksheet.write(row, 12, line.price_subtotal, number_format)  # Amount
                row += 1

            # Add subtotal per order
            worksheet.write(row, 0, f"Subtotal for {order.user_id.name}", header_format)
            worksheet.write(row, 12, order.amount_total, number_format)
            row += 1

        # Add grand total
        worksheet.write(row, 0, "Grand Total", header_format)
        worksheet.write(row, 12, sum(order.amount_total for order in self), number_format)

        workbook.close()
        
        # Create attachment
        filename = f'POS_Orders_{datetime.now().strftime("%Y%m%d_%H%M%S")}.xlsx'
        attachment = self.env['ir.attachment'].create({
            'name': filename,
            'datas': base64.b64encode(output.getvalue()),
            'type': 'binary'
        })

        return {
            'type': 'ir.actions.act_url',
            'url': f'/web/content/{attachment.id}?download=true',
            'target': 'self',
        }

    def action_export_pdf(self):
        return self.env.ref('custom_cashier_report.action_report_pos_order_listing').report_action(self) 