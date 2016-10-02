//
//  InventoryInput.m
//  CCQrCode
//
//  Created by Ayush Sanyal on 10/2/16.
//  Copyright © 2016 Ayush Sanyal. All rights reserved.
//

#import "InventoryInput.h"
#import "XLFormRowDescriptor.h"
#import "XLForm.h"

@interface InventoryInput ()
@property (weak, nonatomic) IBOutlet UITextField *itemName;
@property (weak, nonatomic) IBOutlet UITextField *categoryName;
@property (weak, nonatomic) IBOutlet UITextField *quantity;
@property (weak, nonatomic) IBOutlet UITextField *ages;
@property (weak, nonatomic) IBOutlet UITextField *sizes;

@property (weak, nonatomic) IBOutlet UITextField *genders;

@end

@implementation InventoryInput

-(void)viewDidLoad{
    //do a get and pull the data.

    _itemName.text = self.qrItem;
    _categoryName.text = self.qrCategoryName;
    _quantity.text = self.qrQuantity;
    [self.tableView reloadData];
}

@end
