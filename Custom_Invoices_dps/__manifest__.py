# -*- coding: utf-8 -*-
{
    'name': 'Custom Invoices DPS',
    'version': '1.0',
    'author': 'LD Mahendra',
    'category': 'Accounting',
    'summary': 'Custom invoice templates for DPS',
    'description': """
        Custom invoice templates for Delta Persada Solusi
        - Custom invoice report from sales order
        - Custom paper format
        - Additional fields for invoice
    """,
    'depends': [
        'base',
        'account',
        'sale_management',
    ],
    'data': [
        'security/ir.model.access.csv',
        'views/account_move_views.xml',
        'report/invoice_report.xml',
        'report/invoice_report_template.xml',
    ],
    'installable': True,
    'application': False,
    'auto_install': False,
} 