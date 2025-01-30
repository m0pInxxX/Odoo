from odoo import models, fields

class AccountMove(models.Model):
    _inherit = 'account.move'

    custom_header = fields.Text(string='Custom Header')
    custom_footer = fields.Text(string='Custom Footer')
    custom_field = fields.Char(string='Custom Field')