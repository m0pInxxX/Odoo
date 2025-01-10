from odoo import models, fields, api

class SaleOrder(models.Model):
    _inherit = 'sale.order'  # Extend model sale.order

    def get_quotation_details(self):
        """Method to fetch quotation details for the report."""
        result = []
        for line in self.order_line:
            result.append({
                'product_code': line.product_id.default_code,
                'product_name': line.product_id.name,
                'quantity': line.product_uom_qty,
                'price_unit': line.price_unit,
                'subtotal': line.price_subtotal,
                'uom': line.product_uom.name,
            })
        return result
    
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
    
    @api.depends('order_line.price_total')
    def _compute_amount_total(self):
        for order in self:
            order.amount_total = sum(line.price_subtotal for line in order.order_line)