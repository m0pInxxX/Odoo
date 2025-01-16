{
    'name': 'Delete Reference Code',
    'version': '1.1.2',
    'author': 'Mahendra',
    'category': 'Accounting',
    'summary': 'Delete Reference Code on preview description in vendor bills and customer invoices',
    'description': '''
        This module removes reference codes from:
        - Vendor Bill line descriptions
        - Customer Invoice line descriptions
    ''',
    'depends': ['account', 'sale_management'],
    'data': [
        'report/invoice_report_templates.xml',
    ],
    'installable': True,
    'application': False,
    'auto_install': False,
    'license': 'LGPL-3',
}
