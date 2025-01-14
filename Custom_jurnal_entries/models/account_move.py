from odoo import models, fields, api

class AccountMove(models.Model):
    _inherit = 'account.move'

    payment_type = fields.Selection([
        ('in_receipt', 'In Receipt'),
        ('out_receipt', 'Out Receipt'),
    ], string='Payment Type', 
       help='Type of payment for journal entries',
       tracking=True)  # Tambahkan tracking untuk audit trail

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

    @api.onchange('payment_type')
    def _onchange_payment_type(self):
        """Optional: Add any specific logic when payment type changes"""
        if self.payment_type == 'in_receipt':
            # Contoh: set default journal for incoming receipt
            default_journal = self.env['account.journal'].search([
                ('type', '=', 'cash'), 
                ('company_id', '=', self.company_id.id)
            ], limit=1)
            if default_journal:
                self.journal_id = default_journal