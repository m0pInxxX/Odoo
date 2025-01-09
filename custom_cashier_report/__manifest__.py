{
    'name': 'Custom Sales Receipt',
    'version': '1.1.1',
    'author': 'Mahendra',
    'category': 'Point of Sale',
    'summary': 'Export custom POS Orders to XLSX and PDF ',
    'description': 'This module allows exporting POS Orders to XLSX and PDF with customized fields such as Doc No, Cashier, and more.',
    'depends': ['base', 'point_of_sale', 'web'],
    'data': [
        'reports/pos_order_listing_report.xml',
        'reports/pos_order_listing_template.xml',
        'views/pos_order_views.xml',
    ],
    'installable': True,
    'application': False,
    'license': 'LGPL-3',
}
