from odoo import models, fields

class SaleReport(models.AbstractModel):
    _name = 'report.custom_quotation_report.report_sale_quotation'
    _description = 'Custom Quotation Report'

    def _get_report_values(self, docids, data=None):
        docs = self.env['sale.order'].browse(docids)
        return {
            'docs': docs,
            'today_date': fields.Date.today(),
            'user': self.env.user,
            'get_grand_total': self.get_grand_total,
            'get_subtotal': self.get_subtotal,
        }
    
    def get_subtotal(self, doc):
        if doc.state == 'cancel':
            return 0.0
        return sum(line.price_subtotal for line in doc.order_line)

    def get_grand_total(self, docs):
        total = 0.0
        for doc in docs:
            if doc.state != 'cancel':
                for line in doc.order_line:
                    total += line.price_subtotal
        return total
