from odoo import models, api, fields, _
from odoo.exceptions import UserError

class ProductTemplate(models.Model):
    _inherit = 'product.template'

    @api.model
    def _get_dummy_product(self):
        """Mendapatkan atau membuat produk dummy untuk pengganti produk yang dihapus"""
        dummy_product = self.env['product.product'].search([
            ('name', '=', '[Deleted Product Reference]'),
            ('active', '=', False)
        ], limit=1)
        
        if not dummy_product:
            dummy_product = self.env['product.product'].create({
                'name': '[Deleted Product Reference]',
                'type': 'service',
                'active': False,
                'sale_ok': False,
                'purchase_ok': False,
                'list_price': 0,
                'standard_price': 0,
            })
        
        return dummy_product

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
                raise UserError(_('Produk ini memiliki transaksi terkait. Gunakan tombol "Hapus Paksa" untuk menghapus produk ini.'))
        return super(ProductTemplate, self).unlink() 