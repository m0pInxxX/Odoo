{
    'name': 'Custom Sales Receipt',
    'version': '1.2.0',
    'author': 'Mahendra',
    'category': 'Accounting',
    'summary': 'Custom Sales Receipt Format for Enterprise',
    'description': """Custom Sales Receipt with specific format""",
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