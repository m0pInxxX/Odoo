{
    'name': 'LD Custom Journal Entries',
    'version': '1.3.2',
    'author': 'Mahendra',
    'category': 'Accounting',
    'summary': 'Enhanced Journal Entries with Custom Fields',
    'description': """
        Extends Odoo's journal entries with:
        - Custom payment type
        - Additional descriptive fields
        - Enhanced tracking and validation
    """,
    'depends': ['base', 'account'],
    'data': [
        'security/ir.model.access.csv', 
        'data/paper_format_data.xml',
        'views/account_move_view.xml',
        'report/custom_sales_receipt.xml',
        'report/custom_sales_receipt_template.xml',
        'report/bukti_kas_kecil_template.xml',
    ],
    'installable': True,
    'application': False,
    'license': 'LGPL-3',
    'sequence': 1,
}