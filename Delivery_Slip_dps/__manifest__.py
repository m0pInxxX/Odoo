# -*- coding: utf-8 -*-
{
    'name': 'Delivery Slip DPS',
    'version': '1.0',
    'author': 'LD Arengga',
    'category': 'Inventory',
    'summary': 'Custom delivery slip templates for DPS',
    'description': """
        Custom delivery slip templates for Delta Persada Solusi
        - Custom delivery slip report from stock picking
        - Custom paper format
        - Additional fields for delivery slip
    """,
    'depends': [
        'base',
        'stock',
        'sale_management',
    ],
    'data': [
        'security/ir.model.access.csv',
        'views/stock_picking_views.xml',
        'report/delivery_slip_report.xml',
        'report/delivery_slip_template.xml',
    ],
    'installable': True,
    'application': False,
    'auto_install': False,
} 