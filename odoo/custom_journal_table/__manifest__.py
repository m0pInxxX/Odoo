{
    'name': 'Custom Journal Table View',
    'version': '1.0.0',
    'summary': 'Adjust table column widths for Journal Entries',
    'author' : 'Mahendra',
    'category': 'Accounting',
    'depends': ['account'],
    'assets': {
        'web.assets_backend': [
            'custom_journal_table/static/src/css/custom_journal_table.css',
        ],
    },
    'installable': True,
    'application': False,
    'license': 'LGPL-3',
}
