CREATE TABLE `Categories` (
  `CategoryId` int(11) NOT NULL AUTO_INCREMENT,
  `CategoryName` varchar(100) NOT NULL,
  PRIMARY KEY (`CategoryId`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

CREATE TABLE `CCUserRoles` (
  `UserRole` int(11) NOT NULL,
  `RoleId` int(11) DEFAULT NULL,
  PRIMARY KEY (`UserRole`),
  KEY `RoleId_idx` (`RoleId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `CCUsers` (
  `CcUserId` int(11) NOT NULL AUTO_INCREMENT,
  `LoginId` varchar(45) NOT NULL,
  `Password` varchar(45) NOT NULL,
  `ExpirationDate` date NOT NULL,
  `Name` varchar(100) DEFAULT NULL,
  `Email` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`CcUserId`),
  UNIQUE KEY `user_id_UNIQUE` (`CcUserId`)
) ENGINE=InnoDB AUTO_INCREMENT=1006 DEFAULT CHARSET=big5 COMMENT='Table containing user login information\n';

CREATE TABLE `Departments` (
  `DepartmentId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`DepartmentId`)
) ENGINE=InnoDB AUTO_INCREMENT=112 DEFAULT CHARSET=latin1;

CREATE TABLE `Inventory` (
  `InventoryId` int(11) NOT NULL AUTO_INCREMENT,
  `ItemId` int(11) NOT NULL,
  `LocationId` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL,
  PRIMARY KEY (`InventoryId`),
  KEY `ItemId_idx` (`ItemId`),
  KEY `LocationId_idx` (`LocationId`),
  CONSTRAINT `ItemId` FOREIGN KEY (`ItemId`) REFERENCES `Items` (`ItemId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `LocationId` FOREIGN KEY (`LocationId`) REFERENCES `Locations` (`LocationId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=100021 DEFAULT CHARSET=latin1;

CREATE TABLE `Items` (
  `ItemId` int(11) NOT NULL AUTO_INCREMENT,
  `CategoryId` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Gender` varchar(45) NOT NULL DEFAULT '',
  `Size` varchar(45) NOT NULL DEFAULT '',
  `Price` decimal(2,0) NOT NULL DEFAULT '0',
  `LowLimit` int(11) NOT NULL DEFAULT '0',
  `Age` varchar(45) NOT NULL,
  `ModifiedBy` int(11) DEFAULT NULL,
  `Timestamp` datetime DEFAULT NULL,
  `IsDeleted` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`ItemId`),
  KEY `ModifiedBy_idx` (`ModifiedBy`),
  KEY `Category_idx` (`CategoryId`),
  CONSTRAINT `ItemCategory` FOREIGN KEY (`CategoryId`) REFERENCES `Categories` (`CategoryId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `ModifiedBy` FOREIGN KEY (`ModifiedBy`) REFERENCES `CCUsers` (`CcUserId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=latin1;

CREATE TABLE `Locations` (
  `LocationId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `Address` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`LocationId`)
) ENGINE=InnoDB AUTO_INCREMENT=122 DEFAULT CHARSET=latin1;

CREATE TABLE `OrderDetails` (
  `OrderDetailId` int(11) NOT NULL AUTO_INCREMENT,
  `CategoryId` int(11) DEFAULT NULL,
  `ItemId` int(11) DEFAULT NULL,
  `Quantity` int(11) DEFAULT NULL,
  `OrderId` int(11) DEFAULT NULL,
  PRIMARY KEY (`OrderDetailId`),
  KEY `Item_idx` (`ItemId`),
  KEY `Order_idx` (`OrderId`),
  KEY `Category_idx` (`CategoryId`),
  CONSTRAINT `Category` FOREIGN KEY (`CategoryId`) REFERENCES `Categories` (`CategoryId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Item` FOREIGN KEY (`ItemId`) REFERENCES `Items` (`ItemId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Order` FOREIGN KEY (`OrderId`) REFERENCES `Orders` (`OrderId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

CREATE TABLE `Orders` (
  `OrderId` int(11) NOT NULL AUTO_INCREMENT,
  `NeedByDate` date DEFAULT NULL,
  `IsFulfilled` bit(1) DEFAULT NULL,
  `LocationId` int(11) DEFAULT NULL,
  `DepartmentId` int(11) DEFAULT NULL,
  `CreationDate` date DEFAULT NULL,
  PRIMARY KEY (`OrderId`),
  KEY `location_id_idx` (`LocationId`),
  KEY `program_id_idx` (`DepartmentId`),
  CONSTRAINT `location` FOREIGN KEY (`LocationId`) REFERENCES `Locations` (`LocationId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `program_id` FOREIGN KEY (`DepartmentId`) REFERENCES `Departments` (`DepartmentId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

CREATE TABLE `Roles` (
  `RoleId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `Permissions` varchar(45) NOT NULL,
  PRIMARY KEY (`RoleId`),
  UNIQUE KEY `role_id_UNIQUE` (`RoleId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1 COMMENT='Roles for the users';

INSERT INTO `ccadb`.`Categories`
(`CategoryId`,
`CategoryName`)
VALUES
(<{CategoryId: }>,
<{CategoryName: }>);

INSERT INTO `ccadb`.`CCUserRoles`
(`UserRole`,
`RoleId`)
VALUES
(<{UserRole: }>,
<{RoleId: }>);

INSERT INTO `ccadb`.`CCUsers`
(`CcUserId`,
`LoginId`,
`Password`,
`ExpirationDate`,
`Name`,
`Email`)
VALUES
(<{CcUserId: }>,
<{LoginId: }>,
<{Password: }>,
<{ExpirationDate: }>,
<{Name: }>,
<{Email: }>);

INSERT INTO `ccadb`.`Departments`
(`DepartmentId`,
`Name`)
VALUES
(<{DepartmentId: }>,
<{Name: }>);

INSERT INTO `ccadb`.`Inventory`
(`InventoryId`,
`ItemId`,
`LocationId`,
`Quantity`)
VALUES
(<{InventoryId: }>,
<{ItemId: }>,
<{LocationId: }>,
<{Quantity: }>);

INSERT INTO `ccadb`.`Items`
(`ItemId`,
`CategoryId`,
`Name`,
`Gender`,
`Size`,
`Price`,
`LowLimit`,
`Age`,
`ModifiedBy`,
`Timestamp`,
`IsDeleted`)
VALUES
(<{ItemId: }>,
<{CategoryId: }>,
<{Name: }>,
<{Gender: }>,
<{Size: }>,
<{Price: 0}>,
<{LowLimit: 0}>,
<{Age: }>,
<{ModifiedBy: }>,
<{Timestamp: }>,
<{IsDeleted: }>);

INSERT INTO `ccadb`.`Locations`
(`LocationId`,
`Name`,
`Address`)
VALUES
(<{LocationId: }>,
<{Name: }>,
<{Address: }>);

INSERT INTO `ccadb`.`OrderDetails`
(`OrderDetailId`,
`CategoryId`,
`ItemId`,
`Quantity`,
`OrderId`)
VALUES
(<{OrderDetailId: }>,
<{CategoryId: }>,
<{ItemId: }>,
<{Quantity: }>,
<{OrderId: }>);

INSERT INTO `ccadb`.`Orders`
(`OrderId`,
`NeedByDate`,
`IsFulfilled`,
`LocationId`,
`DepartmentId`,
`CreationDate`)
VALUES
(<{OrderId: }>,
<{NeedByDate: }>,
<{IsFulfilled: }>,
<{LocationId: }>,
<{DepartmentId: }>,
<{CreationDate: }>);

INSERT INTO `ccadb`.`Roles`
(`RoleId`,
`Name`,
`Permissions`)
VALUES
(<{RoleId: }>,
<{Name: }>,
<{Permissions: }>);


UPDATE `ccadb`.`Categories`
SET
`CategoryId` = <{CategoryId: }>,
`CategoryName` = <{CategoryName: }>
WHERE `CategoryId` = <{expr}>;

UPDATE `ccadb`.`CCUserRoles`
SET
`UserRole` = <{UserRole: }>,
`RoleId` = <{RoleId: }>
WHERE `UserRole` = <{expr}>;

UPDATE `ccadb`.`CCUsers`
SET
`CcUserId` = <{CcUserId: }>,
`LoginId` = <{LoginId: }>,
`Password` = <{Password: }>,
`ExpirationDate` = <{ExpirationDate: }>,
`Name` = <{Name: }>,
`Email` = <{Email: }>
WHERE `CcUserId` = <{expr}>;

UPDATE `ccadb`.`Departments`
SET
`DepartmentId` = <{DepartmentId: }>,
`Name` = <{Name: }>
WHERE `DepartmentId` = <{expr}>;

UPDATE `ccadb`.`Inventory`
SET
`InventoryId` = <{InventoryId: }>,
`ItemId` = <{ItemId: }>,
`LocationId` = <{LocationId: }>,
`Quantity` = <{Quantity: }>
WHERE `InventoryId` = <{expr}>;

UPDATE `ccadb`.`Items`
SET
`ItemId` = <{ItemId: }>,
`CategoryId` = <{CategoryId: }>,
`Name` = <{Name: }>,
`Gender` = <{Gender: }>,
`Size` = <{Size: }>,
`Price` = <{Price: 0}>,
`LowLimit` = <{LowLimit: 0}>,
`Age` = <{Age: }>,
`ModifiedBy` = <{ModifiedBy: }>,
`Timestamp` = <{Timestamp: }>,
`IsDeleted` = <{IsDeleted: }>
WHERE `ItemId` = <{expr}>;

UPDATE `ccadb`.`Locations`
SET
`LocationId` = <{LocationId: }>,
`Name` = <{Name: }>,
`Address` = <{Address: }>
WHERE `LocationId` = <{expr}>;

UPDATE `ccadb`.`OrderDetails`
SET
`OrderDetailId` = <{OrderDetailId: }>,
`CategoryId` = <{CategoryId: }>,
`ItemId` = <{ItemId: }>,
`Quantity` = <{Quantity: }>,
`OrderId` = <{OrderId: }>
WHERE `OrderDetailId` = <{expr}>;

UPDATE `ccadb`.`Orders`
SET
`OrderId` = <{OrderId: }>,
`NeedByDate` = <{NeedByDate: }>,
`IsFulfilled` = <{IsFulfilled: }>,
`LocationId` = <{LocationId: }>,
`DepartmentId` = <{DepartmentId: }>,
`CreationDate` = <{CreationDate: }>
WHERE `OrderId` = <{expr}>;

UPDATE `ccadb`.`Roles`
SET
`RoleId` = <{RoleId: }>,
`Name` = <{Name: }>,
`Permissions` = <{Permissions: }>
WHERE `RoleId` = <{expr}>;


DELETE FROM `ccadb`.`Categories`
WHERE <{where_expression}>;

DELETE FROM `ccadb`.`CCUserRoles`
WHERE <{where_expression}>;

DELETE FROM `ccadb`.`CCUsers`
WHERE <{where_expression}>;

DELETE FROM `ccadb`.`Departments`
WHERE <{where_expression}>;

DELETE FROM `ccadb`.`Inventory`
WHERE <{where_expression}>;

DELETE FROM `ccadb`.`Items`
WHERE <{where_expression}>;

DELETE FROM `ccadb`.`Locations`
WHERE <{where_expression}>;

DELETE FROM `ccadb`.`OrderDetails`
WHERE <{where_expression}>;

DELETE FROM `ccadb`.`Orders`
WHERE <{where_expression}>;

DELETE FROM `ccadb`.`Roles`
WHERE <{where_expression}>;
