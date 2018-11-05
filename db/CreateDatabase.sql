CREATE SCHEMA `DB_CARRINHO_CHOPPEIRA` ;

CREATE TABLE `DB_CARRINHO_CHOPPEIRA`.`Address`
(
  `Key`			CHAR(36)		NOT NULL,
  `Street`	    VARCHAR(75)		NOT NULL,
  `Complement`	VARCHAR(100)	NULL,
  `Number`		INT				NOT NULL,
  `PostalCode`	VARCHAR(9)		NOT NULL,
  `City`		VARCHAR(75)		NOT NULL,
  `State`		VARCHAR(75)	    NOT NULL,
  `Country`		VARCHAR(75)	    NOT NULL,
  
  PRIMARY KEY (`Key`)
);

CREATE TABLE `DB_CARRINHO_CHOPPEIRA`.`Client`
(
  `Key`			CHAR(36)		NOT NULL,
  `Name`	    VARCHAR(100)	NOT NULL,
  `AddressKey`	CHAR(36)		NOT NULL,
  `DocumentId`	VARCHAR(14)		NOT NULL,
  `Phone`		VARCHAR(13)		NOT NULL,
  `SecondPhone`	VARCHAR(13)	    NULL,
  
  PRIMARY KEY (`Key`),
  CONSTRAINT `FK_Client_Address` FOREIGN KEY (`AddressKey`) REFERENCES `Address` (`Key`)
);

CREATE TABLE `DB_CARRINHO_CHOPPEIRA`.`Cart`
(
  `Key`			CHAR(36)	    NOT NULL,
  `ExternalKey`	VARCHAR(255)	NOT NULL,
  `ChargeValue`	FLOAT		    NOT NULL,
  `ClientKey`	CHAR(36)		NOT NULL,
  `IsActive`	BIT				NOT NULL,
  
  PRIMARY KEY (`Key`),
  CONSTRAINT `FK_Cart_Client` FOREIGN KEY (`ClientKey`) REFERENCES `Client` (`Key`)
);