from odoo import models, fields, api

class PosOrder(models.Model):
    _inherit = 'pos.order'

    def get_pos_details(self):
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

    def get_subtotal(self, doc):
        if doc.state == 'cancel':
            return 0.0
        return sum(line.price_subtotal for line in doc.lines)

    def get_grand_total(self, docs):
        total = 0.0
        for doc in docs:
            if doc.state != 'cancel':
                for line in doc.lines:
                    total += line.price_subtotal
        return total

    @api.depends('lines', 'lines.price_subtotal', 'state')
    def _compute_filtered_total(self):
        for order in self:
            if order.state == 'cancel':
                order.filtered_total = 0.0
            else:
                order.filtered_total = sum(line.price_subtotal for line in order.lines)

    filtered_total = fields.Float(
        string='Filtered Total',
        compute='_compute_filtered_total',
        store=True,
    ) 