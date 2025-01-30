from odoo import models, fields, api, _

class SalesReceipt(models.Model):
    _name = 'sales.receipt'
    _description = 'Sales Receipt'

    name = fields.Char(string='Receipt Number', required=True, copy=False, readonly=True, index=True, default=lambda self: _('New'))
    amount = fields.Monetary(string='Amount', required=True)
    invoice_number = fields.Char(string='Invoice Number')
    total_invoice = fields.Monetary(string='Total Invoice')
    payment_amount = fields.Monetary(string='Payment Amount')
    debit_account = fields.Many2one('account.account', string='Debit Account')
    debit_amount = fields.Monetary(string='Debit Amount')
    credit_account = fields.Many2one('account.account', string='Credit Account')
    credit_amount = fields.Monetary(string='Credit Amount')
    received_by = fields.Char(string='Received By')
    approved_by = fields.Char(string='Approved By')
    description = fields.Text(string='Description')
    currency_id = fields.Many2one('res.currency', string='Currency')

    @api.model
    def create(self, vals):
        if vals.get('name', _('New')) == _('New'):
            vals['name'] = self.env['ir.sequence'].next_by_code('sales.receipt') or _('New')
        return super(SalesReceipt, self).create(vals)