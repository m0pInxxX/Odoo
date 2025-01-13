from odoo import models, fields

class AccountMove(models.Model):
    _inherit = 'account.move'

    payment_type = fields.Selection([
        ('receipt', 'Receipt'),
        ('payment', 'Payment')
    ], string='Payment Type') 