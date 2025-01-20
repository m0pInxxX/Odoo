from odoo import models, fields, api

class AccountMoveDebitCredit(models.Model):
    _name = 'account.move.debit.credit'
    _description = 'Account Move Debit Credit Lines'
    _inherit = ['mail.thread', 'mail.activity.mixin']

    move_id = fields.Many2one('account.move', string='Journal Entry', required=True, ondelete='cascade', tracking=True)
    account_id = fields.Many2one('account.account', string='Account', required=True, tracking=True)
    debit = fields.Monetary(string='Debit Amount', default=0.0, tracking=True)
    credit = fields.Monetary(string='Credit Amount', default=0.0, tracking=True)
    currency_id = fields.Many2one('res.currency', related='move_id.currency_id')
    company_id = fields.Many2one('res.company', string='Company', related='move_id.company_id', store=True)
    name = fields.Char(string='Label', tracking=True) 