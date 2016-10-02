//
//  ViewController.h
//  CCQrCode
//
//  Created by Ayush Sanyal on 10/2/16.
//  Copyright © 2016 Ayush Sanyal. All rights reserved.
//

//
//  AMScanViewController.h
//
//
//  Created by Alexander Mack on 11.10.13.
//  Copyright (c) 2013 ama-dev.com. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <AVFoundation/AVFoundation.h>
#import "QRCodeReaderDelegate.h"

@protocol ViewControllerDelegate;

@interface ViewController : UIViewController <AVCaptureMetadataOutputObjectsDelegate,QRCodeReaderDelegate>

@property (nonatomic, weak) id<ViewControllerDelegate> delegate;

@property (assign, nonatomic) BOOL touchToFocusEnabled;

- (BOOL) isCameraAvailable;
- (void) startScanning;
- (void) stopScanning;
- (void) setTorch:(BOOL) aStatus;

@end

@protocol ViewControllerDelegate <NSObject>

@optional

- (void) scanViewController:(ViewController *) aCtler didTapToFocusOnPoint:(CGPoint) aPoint;
- (void) scanViewController:(ViewController *) aCtler didSuccessfullyScan:(NSString *) aScannedValue;

@end