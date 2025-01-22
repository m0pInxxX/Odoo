{
    'name': 'Custom Journal Entries',
    'version': '1.3.1',
    'author': 'Mahendra',
    'category': 'Accounting',
    'summary': 'Custom Journal Entries Format',
    'description': """
        This module customizes:
        - Sales Receipt Format
        - Bukti Kas Kecil Format
        - Invoice/Bill Description Format
    """,
    'depends': [
        'base',
        'account',
        'purchase',
        'sale',
        'mail',
        'web_enterprise',
    ],
    'data': [
        'security/security_rules.xml',
        'security/access_rights.xml',
        'data/paper_format_data.xml',
        'views/account_move_views.xml',
        'report/sales_receipt_report.xml',
        'report/sales_receipt_template.xml',
        'report/bukti_kas_kecil_report.xml',
        'report/bukti_kas_kecil_template.xml',
    ],
    'installable': True,
    'application': False,
    'license': 'LGPL-3',
}