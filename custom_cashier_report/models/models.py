from odoo import models, fields

class PosReport(models.AbstractModel):
    _name = 'report.custom_cashier_report.report_pos_order_listing'
    _description = 'Custom POS Order Report'

    def _get_report_values(self, docids, data=None):
        docs = self.env['pos.order'].browse(docids)
        return {
            'docs': docs,
            'today_date': fields.Date.today(),
            'user': self.env.user,
            'get_grand_total': self.get_grand_total,
            'get_subtotal': self.get_subtotal,
        }
    
    def get_subtotal(self, doc):
        return sum(line.price_subtotal for line in doc.lines)

    def get_grand_total(self, docs):
        return sum(doc.amount_total for doc in docs) 