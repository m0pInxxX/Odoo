from odoo import models, fields, api, _
from odoo.exceptions import UserError, ValidationError
import logging

_logger = logging.getLogger(__name__)

class AccountMove(models.Model):
    _inherit = 'account.move'

    payment_type = fields.Selection([
        ('in_receipt', 'In Receipt'),
        ('out_receipt', 'Out Receipt'),
    ], string='Payment Type', 
       help='Type of payment for journal entries',
       tracking=True,
       compute='_compute_payment_type',
       store=True,
       index=True)

    debit_account_id = fields.Many2one(
        'account.account', 
        string='Debit Account',
        domain=[('deprecated', '=', False)],
        tracking=True
    )
    credit_account_id = fields.Many2one(
        'account.account', 
        string='Credit Account',
        domain=[('deprecated', '=', False)],
        tracking=True
    )

    debit_number = fields.Char(string='Debit Account Number', related='debit_account_id.code', store=True)
    credit_number = fields.Char(string='Credit Account Number', related='credit_account_id.code', store=True)

    amount = fields.Monetary(string='Amount', tracking=True)

    x_debit_credit_ids = fields.One2many(
        'x_account_move_debit_credit',
        'move_id',
        string='Debit Credit Lines'
    )

    @api.depends('move_type')
    def _compute_payment_type(self):
        """
        Compute payment type based on move type
        """
        for move in self:
            if move.move_type in ['in_receipt', 'out_receipt']:
                move.payment_type = move.move_type
            else:
                move.payment_type = False

    @api.onchange('payment_type')
    def _onchange_payment_type(self):
        """
        Set default journal based on payment type
        """
        try:
            if self.payment_type == 'in_receipt':
                default_journal = self.env['account.journal'].search([
                    ('type', '=', 'cash'), 
                    ('company_id', '=', self.company_id.id)
                ], limit=1)
                
                if not default_journal:
                    raise UserError(_("No cash journal found for this company."))
                
                self.journal_id = default_journal
        except Exception as e:
            _logger.error(f"Error changing payment type: {str(e)}")

    @api.onchange('debit_account_id', 'credit_account_id', 'amount')
    def _onchange_accounts(self):
        if self.move_type in ['out_receipt', 'in_receipt']:
            lines = []
            if self.debit_account_id:
                lines.append((0, 0, {
                    'account_id': self.debit_account_id.id,
                    'debit': self.amount or 0.0,
                    'credit': 0.0,
                }))
            if self.credit_account_id:
                lines.append((0, 0, {
                    'account_id': self.credit_account_id.id,
                    'debit': 0.0,
                    'credit': self.amount or 0.0,
                }))
            if lines:
                self.line_ids = [(5, 0, 0)] + lines