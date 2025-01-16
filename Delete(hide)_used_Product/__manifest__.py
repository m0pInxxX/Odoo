{
    'name': 'Archive Used Product',
    'version': '1.1.1',
    'author': 'Mahendra',
    'category': 'Point of Sale',
    'summary': 'Archive Used Product in POS',
    'description': '''
        This module allows:
        - Archive products that have POS transactions
        - Safely handle stock moves and quants
        - Track archiving history in chatter
    ''',
    'depends': ['base', 'point_of_sale', 'stock', 'web'],
    'data': [
        'views/actions.xml',
        'views/product_views.xml',
    ],
    'installable': True,
    'application': False,
    'license': 'LGPL-3',
}
