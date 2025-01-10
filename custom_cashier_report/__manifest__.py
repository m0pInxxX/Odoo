{
    'name': 'Custom Sales Receipt',
    'version': '1.2.2',
    'author': 'Mahendra',
    'category': 'Point of Sale',
    'summary': 'Custom reports for POS',
    'description': """Custom reports for Point of Sale""",
    'depends': ['point_of_sale'],
    'data': [
        'security/ir.model.access.csv',
        'views/pos_order_action_views.xml',
        'reports/pos_order_listing_report.xml',
        'reports/pos_order_listing_template.xml',
    ],
    'installable': True,
    'application': False,
    'license': 'LGPL-3',
}
