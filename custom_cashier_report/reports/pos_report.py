from odoo import models, fields, api
from datetime import datetime

class PosReport(models.AbstractModel):
    _name = 'report.custom_cashier_report.report_pos_order_listing'
    _description = 'Custom POS Order Report'

    def _get_report_values(self, docids, data=None):
        docs = self.env['pos.order'].browse(docids)
        
        # Mengelompokkan order berdasarkan cashier
        grouped_orders = {}
        for order in docs:
            if order.user_id not in grouped_orders:
                grouped_orders[order.user_id] = []
            grouped_orders[order.user_id].append(order)
        
        # Mengurutkan orders per cashier
        sorted_docs = []
        for cashier in sorted(grouped_orders.keys(), key=lambda x: x.name or ''):
            sorted_docs.extend(grouped_orders[cashier])

        return {
            'doc_ids': docids,
            'docs': sorted_docs,
            'all_docs': docs,
            'user': self.env.user,
            'today_date': fields.Date.today(),
        }