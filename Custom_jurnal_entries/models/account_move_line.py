from odoo import models, fields, api

class AccountMoveLine(models.Model):
    _inherit = 'account.move.line'

    invoice_number = fields.Char(related='move_id.name', store=True)
    label = fields.Char(string='Label')
    note = fields.Text(string='Note')
    debit_number = fields.Char(string='Debit Number')
    credit_number = fields.Char(string='Credit Number')
