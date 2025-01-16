from odoo import models, api, _
from odoo.exceptions import UserError

class ProductTemplate(models.Model):
    _inherit = 'product.template'

    def action_unarchive_product(self):
        self.write({
            'active': True,
            'sale_ok': True,
            'purchase_ok': True,
            'name': self.name.replace('[Archived] ', '')
        })
        return True

    def unlink(self):
        if self.env.context.get('force_delete'):
            # Hapus stock moves terkait
            moves = self.env['stock.move'].search([
                ('product_id.product_tmpl_id', 'in', self.ids)
            ])
            moves.sudo().unlink()
            return super(ProductTemplate, self).unlink()
            
        for product in self:
            pos_lines = self.env['pos.order.line'].search([
                ('product_id.product_tmpl_id', '=', product.id)
            ])
            if pos_lines:
                raise UserError(_('Produk ini memiliki transaksi POS terkait. Gunakan tombol "Arsipkan Produk" untuk mengarsipkan produk ini.'))
        return super(ProductTemplate, self).unlink() 