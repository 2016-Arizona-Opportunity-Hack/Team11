CREATE database `ccdb`; 

CREATE TABLE `Categories` (
  `CategoryId` int(11) NOT NULL,
  `CategoryName` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`CategoryId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `CCUserRoles` (
  `UserRole` int(11) NOT NULL,
  `RoleId` int(11) DEFAULT NULL,
  PRIMARY KEY (`UserRole`),
  KEY `RoleId_idx` (`RoleId`),
  CONSTRAINT `RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Roles` (`RoleId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `UserRole` FOREIGN KEY (`UserRole`) REFERENCES `CCUsers` (`CcUserId`) ON DELETE NO ACTION ON UPDATE NO ACTION
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
) ENGINE=InnoDB AUTO_INCREMENT=1004 DEFAULT CHARSET=big5 COMMENT='Table containing user login information\n';

CREATE TABLE `Departments` (
  `DepartmentId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`DepartmentId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `Inventory` (
  `InventoryId` int(11) NOT NULL AUTO_INCREMENT,
  `ItemId` int(11) DEFAULT NULL,
  `LocationId` int(11) DEFAULT NULL,
  `Quantity` int(11) DEFAULT NULL,
  PRIMARY KEY (`InventoryId`),
  KEY `ItemId_idx` (`ItemId`),
  KEY `LocationId_idx` (`LocationId`),
  CONSTRAINT `ItemId` FOREIGN KEY (`ItemId`) REFERENCES `Items` (`ItemId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `LocationId` FOREIGN KEY (`LocationId`) REFERENCES `Locations` (`LocationId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `Items` (
  `ItemId` int(11) NOT NULL AUTO_INCREMENT,
  `CategoryId` int(11) DEFAULT NULL,
  `Gender` varchar(45) DEFAULT NULL,
  `Size` varchar(45) DEFAULT NULL,
  `Price` decimal(2,0) DEFAULT NULL,
  `LowLimit` int(11) DEFAULT NULL,
  `Age` varchar(45) DEFAULT NULL,
  `ModifiedBy` int(11) DEFAULT NULL,
  `Timestamp` datetime DEFAULT NULL,
  `IsDeleted` tinyint(1) DEFAULT NULL,
  `Name` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ItemId`),
  KEY `ModifiedBy_idx` (`ModifiedBy`),
  CONSTRAINT `ModifiedBy` FOREIGN KEY (`ModifiedBy`) REFERENCES `CCUsers` (`CcUserId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `Locations` (
  `LocationId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `Address` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`LocationId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE `Roles` (
  `RoleId` int(11) NOT NULL,
  `Name` varchar(45) NOT NULL,
  `Permissions` varchar(45) NOT NULL,
  PRIMARY KEY (`RoleId`),
  UNIQUE KEY `user_id_UNIQUE` (`RoleId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COMMENT='Roles for the users';

