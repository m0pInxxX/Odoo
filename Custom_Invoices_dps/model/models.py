from odoo import models, fields, api

class AccountMove(models.Model):
    _inherit = 'account.move'

    delivery_date = fields.Date(string='Delivery Date')
    sale_order_id = fields.Many2one('sale.order', string='Related Sale Order')
    
    def get_sale_order_details(self):
        """Method untuk mendapatkan detail dari sales order terkait"""
        self.ensure_one()
        if not self.sale_order_id:
            return {}
        
        return {
            'name': self.sale_order_id.name,
            'date_order': self.sale_order_id.date_order,
            'commitment_date': self.sale_order_id.commitment_date,
            'user_id': self.sale_order_id.user_id.name,
            'client_order_ref': self.sale_order_id.client_order_ref,
            'validity_date': self.sale_order_id.validity_date,
            'payment_term_id': self.sale_order_id.payment_term_id.name,
            'fiscal_position_id': self.sale_order_id.fiscal_position_id.name,
        }

    def get_tax_computation(self):
        """Method untuk mendapatkan perhitungan pajak"""
        self.ensure_one()
        
        pph_taxes = []
        other_taxes = []
        
        for line in self.line_ids:
            if line.tax_line_id:
                tax_name = line.tax_line_id.name
                tax_amount = abs(line.balance)
                
                if 'PPH' in tax_name.upper() or 'pph' in tax_name.lower():
                    pph_taxes.append({
                        'name': tax_name,
                        'amount': tax_amount
                    })
                else:
                    other_taxes.append({
                        'name': tax_name,
                        'amount': tax_amount
                    })
        
        return {
            'subtotal': self.amount_untaxed,
            'pph_taxes': pph_taxes,
            'other_taxes': other_taxes,
            'total': self.amount_total
        } 