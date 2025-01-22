from odoo import models, fields, api

class AccountMoveLine(models.Model):
    _inherit = 'account.move.line'

    invoice_number = fields.Char(related='move_id.name', store=True)
    label = fields.Char(string='Label')
    note = fields.Text(string='Note')
    debit_number = fields.Char(string='Debit Number')
    credit_number = fields.Char(string='Credit Number')

    @api.depends('purchase_line_id', 'sale_line_ids', 'product_id')
    def _compute_name(self):
        for line in self:
            if line.move_id.move_type in ['in_invoice', 'in_refund', 'out_invoice', 'out_refund']:
                original_name = line.name or ''
                
                # Hapus reference code untuk berbagai format
                if ':' in original_name:
                    # Format: "PO/031/MA/IV/24/F: [LL00143] Rental Genset"
                    cleaned_name = ': '.join(original_name.split(':')[1:]).strip()
                    if cleaned_name.startswith('['):
                        cleaned_name = cleaned_name.split(']')[-1].strip()
                    line.name = cleaned_name
                elif '[' in original_name and ']' in original_name:
                    # Format: "[SO001] Product Description"
                    cleaned_name = original_name.split(']')[-1].strip()
                    line.name = cleaned_name
                else:
                    line.name = original_name
            else:
                super(AccountMoveLine, line)._compute_name()
