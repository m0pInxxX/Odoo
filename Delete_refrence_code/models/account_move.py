from odoo import models, api, fields

class AccountMoveLine(models.Model):
    _inherit = 'account.move.line'

    def _get_computed_name(self):
        # Override method untuk menghapus reference code dari deskripsi
        self.ensure_one()
        
        if self.product_id:
            name = self.product_id.display_name
            if self.product_id.description_sale and self.move_id.move_type in ('out_invoice', 'out_refund'):
                name += '\n' + self.product_id.description_sale
            elif self.product_id.description_purchase and self.move_id.move_type in ('in_invoice', 'in_refund'):
                name += '\n' + self.product_id.description_purchase
            return name
        return self.name 