{
    'name': 'Custom Journal Entries',
    'version': '1.0.0',
    'author': 'Mahendra',
    'category': 'Accounting',
    'summary': 'Custom Journal Entries Format for Enterprise',
    'description': """
        Enterprise-ready module for customized journal entries printing:
        - Sales Receipt (A5 Landscape)
        - Bukti Kas Kecil (A5 Landscape)
    """,
    'depends': [
        'base',
        'account',
        'account_accountant',  # Enterprise dependency
        'web_enterprise',      # Enterprise dependency
        'mail',
    ],
    'data': [
        'security/ir.model.access.csv',
        'data/paper_format_data.xml',
        'views/account_move_views.xml',
        'report/sales_receipt_report.xml',
        'report/sales_receipt_template.xml',
        'report/bukti_kas_kecil_report.xml',
        'report/bukti_kas_kecil_template.xml',
    ],
    'assets': {
        'web.assets_backend': [
            'Custom_jurnal_entries/static/src/scss/style.scss',
        ],
        'web.report_assets_common': [
            'Custom_jurnal_entries/static/src/scss/report_style.scss',
        ],
    },
    'installable': True,
    'application': False,
    'auto_install': False,
    'license': 'LGPL-3',
}