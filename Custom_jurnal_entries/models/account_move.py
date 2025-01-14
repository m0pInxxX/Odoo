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
       store=True)

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