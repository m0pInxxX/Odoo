from odoo import models, fields, api

class AccountMoveLine(models.Model):
    _inherit = 'account.move.line'

    invoice_number = fields.Char(
        related='move_id.name', 
        string='Invoice Number', 
        store=True
    )
    
    label = fields.Char(
        string='Line Label', 
        help='Specific label for this journal entry line'
    )
    
    description = fields.Char(
        string='Line Description', 
        help='Detailed description for this line'
    )

    note = fields.Text(
        string='Line Notes', 
        help='Additional notes for this journal entry line'
    )

    line_total = fields.Monetary(
        string='Line Total', 
        currency_field='company_currency_id', 
        compute='_compute_line_total', 
        store=True
    )

    @api.depends('debit', 'credit')
    def _compute_line_total(self):
        """Compute total amount for the line"""
        for line in self:
            line.line_total = line.debit - line.credit