CREATE TABLE IF NOT EXISTS 
	`airport` (
				`id` INT NOT NULL, 
				`name` VARCHAR(512) NOT NULL, 
				`iata` CHAR(3) NULL, 
				`icao` CHAR(4) NULL, 
				`latitude` DOUBLE(8,5) NOT NULL,
				`longitude` DOUBLE(8,5) NOT NULL,
		PRIMARY KEY (`id`));