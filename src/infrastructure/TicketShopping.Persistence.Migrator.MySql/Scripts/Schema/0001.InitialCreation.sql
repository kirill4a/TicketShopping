CREATE TABLE IF NOT EXISTS 
	`airport` (
				`id` INT NOT NULL AUTO_INCREMENT, 
				`name` VARCHAR(512) NOT NULL, 
				`iata` CHAR(3) NULL, 
				`icao` CHAR(4) NULL, 
				`latitude` DOUBLE(8,5) NOT NULL,
				`longitude` DOUBLE(8,5) NOT NULL,
		PRIMARY KEY (`id`));

CREATE UNIQUE INDEX `UIX_Airport_Iata` ON `airport` (`iata`);
CREATE UNIQUE INDEX `UIX_Airport_Icao` ON `airport` (`icao`);