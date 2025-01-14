from odoo import models, fields, api, _
from odoo.exceptions import UserError, ValidationError

class AccountMove(models.Model):
    _inherit = 'account.move'

    payment_type = fields.Selection([
        ('in_receipt', 'In Receipt'),
        ('out_receipt', 'Out Receipt'),
    ], string='Payment Type', 
       help='Type of payment for journal entries',
       tracking=True,
       required=True)

    description = fields.Char(
        string='Description', 
        tracking=True, 
        help='Additional description for the journal entry'
    )

    reference = fields.Char(
        string='Reference', 
        tracking=True, 
        help='External reference number'
    )

    label = fields.Char(
        string='Label', 
        tracking=True, 
        help='Short label for the journal entry'
    )

    note = fields.Text(
        string='Internal Note', 
        help='Internal notes or comments'
    )

    total_debit = fields.Monetary(
        string='Total Debit', 
        compute='_compute_total_amount', 
        store=True,
        currency_field='company_currency_id'
    )

    total_credit = fields.Monetary(
        string='Total Credit', 
        compute='_compute_total_amount', 
        store=True,
        currency_field='company_currency_id'
    )

    @api.depends('line_ids.debit', 'line_ids.credit')
    def _compute_total_amount(self):
        """Compute total debit and credit amounts"""
        for move in self:
            move.total_debit = sum(move.line_ids.mapped('debit'))
            move.total_credit = sum(move.line_ids.mapped('credit'))

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
            self.env['ir.logging'].create({
                'name': 'Payment Type Change Error',
                'type': 'server',
                'dbname': self.env.cr.dbname,
                'level': 'ERROR',
                'message': str(e)
            })

    @api.constrains('payment_type', 'journal_id')
    def _check_payment_type_journal(self):
        """
        Validate journal type for payment type
        """
        for record in self:
            if record.payment_type == 'in_receipt' and record.journal_id.type != 'cash':
                raise ValidationError(_("Cash receipt must use a cash journal type."))