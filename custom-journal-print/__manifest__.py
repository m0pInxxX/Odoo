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
        'views/journal_entry_views.xml',
        'report/journal_entry_report.xml',
    ],
    'installable': True,
    'application': True,
    'license': 'LGPL-3',
}