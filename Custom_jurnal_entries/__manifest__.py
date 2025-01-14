{
    'name': 'Custom Journal Entries',
    'version': '1.3.1',
    'author': 'Mahendra',
    'category': 'Accounting',
    'summary': 'Custom Sales Receipt and Bukti Kas Kecil for Journal Entries',
    'description': """
        This module adds custom report formats for journal entries:
        - Sales Receipt
        - Bukti Kas Kecil Keluar
    """,
    'depends': ['base', 'account'],
    'data': [
        'data/paper_format_data.xml',
        'views/account_move_view.xml',
        'report/custom_sales_receipt.xml',
        'report/custom_sales_receipt_template.xml',
        'report/bukti_kas_kecil_template.xml',
    ],
    'installable': True,
    'application': False,
    'license': 'LGPL-3',
}
