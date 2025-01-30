from odoo import models, fields, api

class AccountMove(models.Model):
    _inherit = 'account.move'

    custom_field = fields.Char(string='Custom Field')