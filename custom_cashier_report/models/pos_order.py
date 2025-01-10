from odoo import models, fields, api
from io import BytesIO
import base64
import xlsxwriter
from datetime import datetime

class PosOrder(models.Model):
    _inherit = 'pos.order'

    def export_to_excel(self):
        """Export POS orders to Excel file"""
        output = BytesIO()
        workbook = xlsxwriter.Workbook(output)
        worksheet = workbook.add_worksheet('POS Orders')
        
        # Formats
        header = workbook.add_format({'bold': True, 'align': 'center', 'border': 1, 'bg_color': '#D3D3D3'})
        text = workbook.add_format({'align': 'left', 'border': 1})
        number = workbook.add_format({'align': 'right', 'border': 1, 'num_format': '#,##0.00'})
        
        # Headers
        headers = ['No', 'Date', 'Order', 'Customer', 'Product', 'Quantity', 'Price', 'Discount', 'Tax', 'Total']
        for col, header_text in enumerate(headers):
            worksheet.write(0, col, header_text, header)
        
        # Data
        row = 1
        for order in self:
            for line in order.lines:
                worksheet.write(row, 0, row, text)
                worksheet.write(row, 1, order.date_order.strftime('%Y-%m-%d %H:%M:%S'), text)
                worksheet.write(row, 2, order.name, text)
                worksheet.write(row, 3, order.partner_id.name or 'Anonymous', text)
                worksheet.write(row, 4, line.product_id.name, text)
                worksheet.write(row, 5, line.qty, number)
                worksheet.write(row, 6, line.price_unit, number)
                worksheet.write(row, 7, line.discount, number)
                worksheet.write(row, 8, sum(tax.amount for tax in line.tax_ids), number)
                worksheet.write(row, 9, line.price_subtotal_incl, number)
                row += 1
        
        # Column widths
        widths = [5, 20, 15, 20, 30, 10, 12, 10, 10, 12]
        for i, width in enumerate(widths):
            worksheet.set_column(i, i, width)
        
        workbook.close()
        
        # Create attachment
        filename = f'POS_Orders_{datetime.now().strftime("%Y%m%d_%H%M%S")}.xlsx'
        attachment = self.env['ir.attachment'].create({
            'name': filename,
            'type': 'binary',
            'datas': base64.b64encode(output.getvalue()),
            'mimetype': 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
        })
        
        return {
            'type': 'ir.actions.act_url',
            'url': f'/web/content/{attachment.id}?download=true',
            'target': 'self',
        }

    def print_pos_order(self):
        """Print POS order PDF report"""
        return self.env.ref('custom_cashier_report.action_report_pos_order_listing').report_action(self) 

    def _compute_filtered_total(self):
        for order in self:
            if order.state == 'cancel':
                order.filtered_total = 0.0
            else:
                order.filtered_total = order.amount_total

    filtered_total = fields.Monetary(
        string='Filtered Total',
        compute='_compute_filtered_total',
        currency_field='currency_id',
    ) 