//
//  LocationsController.m
//  CCQrCode
//
//  Created by Ayush Sanyal on 10/2/16.
//  Copyright © 2016 Ayush Sanyal. All rights reserved.
//

#import "LocationsController.h"

@implementation LocationsController

NSMutableSet *selectedCellIndexes;
- (IBAction)submitButton:(id)sender {
    
}

-(void)viewDidLoad{
    [self.tableView reloadData];
}

-(NSInteger)numberOfSectionsInTableView:(UITableView *)tableView{
    return 1;
}

-(NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section{
    return 2;
}

-(UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath{
    
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier:@"test"];

    if (cell == nil) {
        cell = [[UITableViewCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:@"test"];
    }
    if (indexPath.row == 0) {
        cell.textLabel.text = @"Pheonix";
    }
    else{
        cell.textLabel.text = @"Messa";
    }
    
    return cell;
}

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath  *)indexPath
{
    [tableView cellForRowAtIndexPath:indexPath].accessoryType = UITableViewCellAccessoryCheckmark;
}

-(void)tableView:(UITableView *)tableView didDeselectRowAtIndexPath:(NSIndexPath *)indexPath
{
    [tableView cellForRowAtIndexPath:indexPath].accessoryType = UITableViewCellAccessoryNone;
}

@end
