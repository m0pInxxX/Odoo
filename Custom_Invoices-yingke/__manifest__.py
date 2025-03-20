{
    'name': 'Custom Invoices Yingke',
    'version': '1.0',
    'author': 'Mahendra',
    'category': 'Accounting',
    'summary': 'Custom Invoices for Yingke',
    'description': """Custom Invoices for Yingke Matrix Indonesia""",
    'depends': [
        'account',
        'account_accountant',
        'web',
    ],
    'data': [
        'report/sales_receipt_report.xml',
        'report/sales_receipt_template.xml',
        'data/paper_format.xml',
    ],
    'installable': True,
    'application': False,
    'license': 'LGPL-3',
}