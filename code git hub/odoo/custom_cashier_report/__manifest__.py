{
    'name': 'Custom Sales Receipt',
    'version': '1.1.1',
    'author': 'Mahendra',
    'category': 'Point of Sale',
    'summary': 'Export custom POS Orders to XLSX and PDF ',
    'description': 'This module allows exporting POS Orders to XLSX and PDF with customized fields.',
    'depends': ['base', 'point_of_sale', 'web'],
    'data': [
        'security/ir.model.access.csv',
        'views/pos_order_views.xml',
        'reports/pos_order_listing_report.xml',
        'reports/pos_order_listing_template.xml',
    ],
    'installable': True,
    'application': False,
    'license': 'LGPL-3',
}
