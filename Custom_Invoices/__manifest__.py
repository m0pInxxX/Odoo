{
    'name': 'Invoices',
    'version': '1.0',
    'author': 'Mahendra',
    'category': 'Accounting',
    'summary': 'Custom Invoices',
    'description': """Custom Invoices""",
    'depends': [
        'account',
        'account_accountant',
        'web',
    ],
    'data': [
        'data/paper_format.xml',
        'views/account_move_views.xml',
        'report/sales_receipt_report.xml',
        'report/sales_receipt_template.xml',
    ],
    'installable': True,
    'application': False,
    'license': 'LGPL-3',
}