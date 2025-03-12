from odoo import models, fields, api

class AccountMoveDebitCredit(models.Model):
    _name = 'x_account_move_debit_credit'
    _description = 'Account Move Debit Credit Lines'
    _inherit = ['mail.thread', 'mail.activity.mixin']

    move_id = fields.Many2one('account.move', string='Journal Entry', required=True, ondelete='cascade', tracking=True)
    debit_account_id = fields.Many2one('account.account', string='Debit Account', tracking=True)
    credit_account_id = fields.Many2one('account.account', string='Credit Account', tracking=True)
    amount = fields.Monetary(string='Amount', tracking=True)
    currency_id = fields.Many2one('res.currency', related='move_id.currency_id')
    company_id = fields.Many2one('res.company', string='Company', related='move_id.company_id', store=True)
    name = fields.Char(string='Label', tracking=True) 