{
    'name': 'Bukti Kas Kecil',
    'version': '1.0',
    'author': 'Mahendra',
    'category': 'Accounting',
    'summary': 'Bukti Kas Kecil Format for Enterprise',
    'description': """Bukti Kas Kecil with specific format""",
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