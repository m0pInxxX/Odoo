{
    'name': 'Custom Quotation Report',
    'version': '1.1.2',
    'author': 'hendra',
    'category': 'Sales',
    'summary': 'Custom Quotation Report with details(fix)',
    'depends': ['sale'],  
    'data': [
        'report/report_action.xml',  
        'report/report_sale_quotation.xml',
        'views/sale_order_view.xml'  
    ],
    'installable': True,
    'application': False,
    'auto_install': False,
}
