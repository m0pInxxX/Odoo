{
    'name': 'Delete Used Product',
    'version': '1.1.1',
    'author': 'Mahendra',
    'category': 'Point of Sale',
    'summary': 'Delete Used Product',
    'description': 'This module allows deleting used product',
    'depends': ['base', 'point_of_sale', 'web'],
    'data': [
        'views/actions.xml',
        'views/product_views.xml',
    ],
    'installable': True,
    'application': False,
    'license': 'LGPL-3',
}
