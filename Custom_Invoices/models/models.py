from odoo import models, fields, api

class AccountMove(models.Model):
    _inherit = 'account.move'

    @api.model
    def _get_tax_totals(self, tax_lines):
        """Helper method untuk menghitung total pajak"""
        pph_taxes = []
        other_taxes = []
        
        for line in tax_lines:
            if line.tax_line_id:
                tax_name = line.tax_line_id.name
                tax_amount = abs(line.balance)
                
                if 'PPH' in tax_name.upper() or 'pph' in tax_name.lower():
                    pph_taxes.append({
                        'name': tax_name,
                        'amount': tax_amount
                    })
                else:
                    other_taxes.append({
                        'name': tax_name,
                        'amount': tax_amount
                    })
        
        return pph_taxes, other_taxes

    def get_tax_computation(self):
        """Method untuk mendapatkan perhitungan pajak"""
        self.ensure_one()
        
        pph_taxes, other_taxes = self._get_tax_totals(self.line_ids)
        
        return {
            'subtotal': self.amount_untaxed,
            'pph_taxes': pph_taxes,
            'other_taxes': other_taxes,
            'total': self.amount_total
        }

    def _compute_tax_totals(self):
        for move in self:
            move.tax_totals = move._prepare_tax_totals()
