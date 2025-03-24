from odoo import models, fields, api

class StockPicking(models.Model):
    _inherit = 'stock.picking'

    sale_id = fields.Many2one('sale.order', string='Sales Order', compute='_compute_sale_id', store=True)
    note = fields.Text('Notes')

    @api.depends('origin')
    def _compute_sale_id(self):
        for picking in self:
            if picking.origin:
                sale_order = self.env['sale.order'].search([('name', '=', picking.origin)], limit=1)
                picking.sale_id = sale_order.id if sale_order else False

    @api.model
    def _get_report_values(self, docids, data=None):
        docs = self.browse(docids)
        return {
            'doc_ids': docids,
            'doc_model': 'stock.picking',
            'docs': docs,
            'data': data,
            'datetime': fields.Datetime,
        }

class StockMoveLine(models.Model):
    _inherit = 'stock.move.line'

    description_picking = fields.Text(related='move_id.description_picking', string='Description', readonly=False)
    move_id = fields.Many2one('stock.move', 'Stock Move', ondelete='cascade', check_company=True, index=True)

    # Tidak perlu mendefinisikan ulang field yang sudah ada di model asli
    # Field-field ini sudah ada di stock.move.line:
    # - product_id
    # - location_id
    # - location_dest_id
    # - product_uom_qty (quantity)
    # - product_uom_id
    # - qty_done

    @api.model
    def _get_report_values(self, docids, data=None):
        docs = self.browse(docids)
        return {
            'doc_ids': docids,
            'doc_model': 'stock.picking',
            'docs': docs,
            'data': data,
            'datetime': fields.Datetime,
        } 