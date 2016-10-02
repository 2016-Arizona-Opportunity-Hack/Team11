//
//  InventoryInput.h
//  CCQrCode
//
//  Created by Ayush Sanyal on 10/2/16.
//  Copyright © 2016 Ayush Sanyal. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "XLFormViewController.h"

@interface InventoryInput:UITableViewController

@property(readwrite) NSString* qrItem;
@property(readwrite) NSString* qrCategoryName;
@property(readwrite) NSString* qrQuantity;


@end
