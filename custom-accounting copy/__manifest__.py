{
    'name': 'Custom Accounting',
    'version': '1.3.0',
    'category': 'Accounting',
    'summary': 'Custom accounting module for Odoo',
    'description': """
        Custom accounting module for Odoo.
    """,
    'depends': ['base', 'account'],
    'data': [
        'security/ir.model.access.csv',
        'views/menu_items.xml',
        'views/sales_receipt_view.xml',
        'reports/sales_receipt_report.xml',
    ],
    'installable': True,
    'application': True,
}
