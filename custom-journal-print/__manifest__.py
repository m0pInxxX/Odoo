{
    'name': 'Custom Journal Print',
    'version': '1.0',
    'author': 'Mahendra',
    'website': 'https://github.com/m0pInxxX/Odoo',
    'category': 'Accounting',
    'summary': 'Custom print report for journal entries',
    'description': """
        Custom print report for journal entries in Odoo.
    """,
    'depends': ['base', 'account'],
    'data': [
        'security/ir.model.access.csv',
        'models/account_move.py',
        'report/custom_journal_report.xml',
        'report/custom_journal_report_template.xml',
    ],
    'installable': True,
    'application': True,
    'license': 'LGPL-3',
}