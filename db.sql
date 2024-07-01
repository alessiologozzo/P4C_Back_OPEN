-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               10.4.28-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             12.7.0.6850
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for p4c_open_db
DROP DATABASE IF EXISTS `p4c_open_db`;
CREATE DATABASE IF NOT EXISTS `p4c_open_db` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `p4c_open_db`;

-- Dumping structure for table p4c_open_db.tbl_abilitazioni
DROP TABLE IF EXISTS `tbl_abilitazioni`;
CREATE TABLE IF NOT EXISTS `tbl_abilitazioni` (
  `Utente` varchar(50) NOT NULL,
  `NomeUtente` varchar(50) NOT NULL,
  `FlgAttivo` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`Utente`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Dumping data for table p4c_open_db.tbl_abilitazioni: 1 rows
DELETE FROM `tbl_abilitazioni`;
/*!40000 ALTER TABLE `tbl_abilitazioni` DISABLE KEYS */;
INSERT INTO `tbl_abilitazioni` (`Utente`, `NomeUtente`, `FlgAttivo`) VALUES
	('Alessio', 'Alessio Logozzo', b'1');
/*!40000 ALTER TABLE `tbl_abilitazioni` ENABLE KEYS */;

-- Dumping structure for table p4c_open_db.tbl_canale
DROP TABLE IF EXISTS `tbl_canale`;
CREATE TABLE IF NOT EXISTS `tbl_canale` (
  `IdCanale` int(11) NOT NULL,
  `NomeCanale` varchar(50) DEFAULT NULL,
  `DescCanale` varchar(500) DEFAULT NULL,
  `fk_Piattaforma` int(11) DEFAULT NULL,
  PRIMARY KEY (`IdCanale`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Dumping data for table p4c_open_db.tbl_canale: 10 rows
DELETE FROM `tbl_canale`;
/*!40000 ALTER TABLE `tbl_canale` DISABLE KEYS */;
INSERT INTO `tbl_canale` (`IdCanale`, `NomeCanale`, `DescCanale`, `fk_Piattaforma`) VALUES
	(1, 'Vendite', NULL, 1),
	(2, 'Richieste di attivazione', NULL, 1),
	(3, 'Vigilanza', NULL, 2),
	(4, 'Call center', NULL, 2),
	(5, 'Marketing', NULL, 3),
	(6, 'Sede legale', NULL, 3),
	(7, 'Istanze di gestione', NULL, 4),
	(8, 'Rimborsi', NULL, 4),
	(9, 'Bonus di servizio', NULL, 5),
	(10, 'Contatti', NULL, 5);
/*!40000 ALTER TABLE `tbl_canale` ENABLE KEYS */;

-- Dumping structure for table p4c_open_db.tbl_criteri
DROP TABLE IF EXISTS `tbl_criteri`;
CREATE TABLE IF NOT EXISTS `tbl_criteri` (
  `IdCriterio` int(11) NOT NULL AUTO_INCREMENT,
  `TipoCriterio` enum('Filtro','Misura') DEFAULT NULL COMMENT 'Indica se identifica una misura o un filtro applicato',
  `DescCriterio` varchar(500) DEFAULT NULL,
  `DettaglioCriterio` text DEFAULT NULL,
  `KpiOrigine` varchar(100) DEFAULT NULL COMMENT 'Lista dei KPI sorgente',
  `DataInserimento` datetime DEFAULT NULL,
  `UtenteInserimento` char(7) DEFAULT NULL,
  `DataAggiornamento` datetime DEFAULT NULL,
  `UtenteAggiornamento` char(7) DEFAULT NULL,
  PRIMARY KEY (`IdCriterio`)
) ENGINE=MyISAM AUTO_INCREMENT=21 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Dumping data for table p4c_open_db.tbl_criteri: 20 rows
DELETE FROM `tbl_criteri`;
/*!40000 ALTER TABLE `tbl_criteri` DISABLE KEYS */;
INSERT INTO `tbl_criteri` (`IdCriterio`, `TipoCriterio`, `DescCriterio`, `DettaglioCriterio`, `KpiOrigine`, `DataInserimento`, `UtenteInserimento`, `DataAggiornamento`, `UtenteAggiornamento`) VALUES
	(1, 'Misura', 'Imposizione numeri autoritari', 'CODE HG88LF', '5,8', '2024-03-01 12:14:10', 'Alessio', '2024-04-01 13:00:54', 'Alessio'),
	(2, 'Filtro', 'Estrazione programmata falsi codici enumerati', 'CODE RT28JH', '3', '2024-03-17 12:15:12', 'Alessio', '2024-04-01 13:01:00', 'Alessio'),
	(3, 'Misura', 'Generazione programmata interferenze', 'CODE 53HO21', '7,4,8', '2024-03-17 12:15:57', 'Alessio', '2024-04-10 13:01:06', 'Alessio'),
	(4, 'Misura', 'Richieste delle chat gestite dalla aziende', 'CODE 76TR32', NULL, '2024-04-01 12:17:16', 'Alessio', NULL, NULL),
	(5, 'Misura', 'Visite enumerate dei proprietari dei servizi di accesso', 'CODE 68MW27', NULL, '2024-04-01 12:19:00', 'Alessio', NULL, NULL),
	(6, 'Misura', 'Rapporto tra creditori e responsabili di divieto', 'CODE NB43RY', NULL, '2024-04-01 12:20:45', 'Alessio', NULL, NULL),
	(7, 'Filtro', 'Somma dei rapporti di bilancio', 'CODE GF83EV', NULL, '2024-05-01 12:21:27', 'Alessio', NULL, NULL),
	(8, 'Misura', 'Abbandoni telefonici totali', 'CODE 43HI56', NULL, '2024-05-01 12:25:20', 'Alessio', NULL, NULL),
	(9, 'Filtro', 'Identificazione processo iterativo', 'CODE 19BB73', '8', '2024-05-01 12:26:00', 'Alessio', '2024-04-10 13:01:15', 'Alessio'),
	(10, 'Filtro', 'Insieme degli stati di offerta da considerare', 'CODE 91ME59', NULL, '2024-05-01 12:26:40', 'Alessio', NULL, NULL),
	(11, 'Filtro', 'Esclusione delle iterazioni di interfaccia', 'CODE 17TZ84', NULL, '2024-05-01 12:27:58', 'Alessio', NULL, NULL),
	(12, 'Misura', 'Processi considerati gestionali', 'CODE YE11NV', '3', '2024-06-01 12:29:11', 'Alessio', '2024-04-10 13:01:20', 'Alessio'),
	(13, 'Misura', 'Quozienti estrattivi di enumerazione stringhe ordinate e composte', 'CODE FL72VN', '2', '2024-06-10 12:30:41', 'Alessio', '2024-04-10 13:01:29', 'Alessio'),
	(14, 'Misura', 'Rapporto numeri estratti dai processi di ordinamento', 'CODE ZX12OI', NULL, '2024-06-10 12:31:52', 'Alessio', NULL, NULL),
	(15, 'Misura', 'Tempo di attesa in coda operatore', 'CODE VM89AP', NULL, '2024-06-11 12:32:45', 'Alessio', '2024-05-01 12:38:11', 'Alessio'),
	(16, 'Filtro', 'Piani gestionali di approvvigionamento', 'CODE UA41AK', NULL, '2024-07-01 12:33:58', 'Alessio', '2024-05-01 12:38:17', 'Alessio'),
	(17, 'Filtro', 'Indice di estrazione', 'CODE IS25UB', '10,2', '2024-07-01 12:35:58', 'Alessio', '2024-05-01 13:01:38', 'Alessio'),
	(18, 'Misura', 'Percentuale richieste sommate e soddisfatte', 'CODE PX43VQ', '5', '2024-07-01 12:36:58', 'Alessio', '2024-06-01 13:02:09', 'Alessio'),
	(19, 'Misura', 'Media buoni processati', 'CODE DS27JL', '8,4,1', '2024-07-01 12:38:02', 'Alessio', '2024-06-01 13:01:53', 'Alessio'),
	(20, 'Filtro', 'Insieme visti operativi', 'CODE BR82ZI', NULL, '2024-07-01 12:39:21', 'Alessio', NULL, NULL);
/*!40000 ALTER TABLE `tbl_criteri` ENABLE KEYS */;

-- Dumping structure for table p4c_open_db.tbl_kpi
DROP TABLE IF EXISTS `tbl_kpi`;
CREATE TABLE IF NOT EXISTS `tbl_kpi` (
  `IdKpi` int(11) NOT NULL AUTO_INCREMENT,
  `NomeKpi` varchar(50) DEFAULT NULL,
  `DescKpi` varchar(500) DEFAULT NULL,
  `CategoriaKpi` enum('Semplice','Composto') DEFAULT 'Semplice' COMMENT '''Composto'' quando deriva da altri KPI ''Semplici''',
  `UMKpi` enum('Intero','Decimale','Sommato','Percentuale','Estratto','Logaritmico') DEFAULT 'Intero' COMMENT 'Unità di misura',
  `Benchmark` varchar(500) DEFAULT NULL COMMENT 'Report di riferimento',
  `DataInserimento` datetime DEFAULT NULL,
  `UtenteInserimento` char(7) DEFAULT NULL,
  `DataAggiornamento` datetime DEFAULT NULL,
  `UtenteAggiornamento` char(7) DEFAULT NULL,
  PRIMARY KEY (`IdKpi`)
) ENGINE=MyISAM AUTO_INCREMENT=11 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Dumping data for table p4c_open_db.tbl_kpi: 10 rows
DELETE FROM `tbl_kpi`;
/*!40000 ALTER TABLE `tbl_kpi` DISABLE KEYS */;
INSERT INTO `tbl_kpi` (`IdKpi`, `NomeKpi`, `DescKpi`, `CategoriaKpi`, `UMKpi`, `Benchmark`, `DataInserimento`, `UtenteInserimento`, `DataAggiornamento`, `UtenteAggiornamento`) VALUES
	(1, 'Nominativi totali', 'Lettura valore di campo calcolato', 'Composto', 'Sommato', 'AGGREGATE 8730', '2024-03-01 12:44:06', 'Alessio', '2024-03-01 19:50:06', 'Alessio'),
	(2, 'Estrattori generati', 'Numero di estrattori generati dal contesto di servizio', 'Semplice', 'Estratto', 'AGGREGATE 1243', '2024-03-01 12:45:13', 'Alessio', '2024-04-01 19:50:06', 'Alessio'),
	(3, 'Contatori di servizio enumerati', 'Funzione logaritmica di asserzione', 'Composto', 'Percentuale', 'AGGREGATE 5487', '2024-03-01 12:46:39', 'Alessio', '2024-04-01 19:50:06', 'Alessio'),
	(4, 'Matrici operative', 'Indice matriciale di confronto', 'Semplice', 'Estratto', 'AGGREGATE 3989', '2024-03-01 12:50:09', 'Alessio', '2024-05-01 19:50:06', 'Alessio'),
	(5, 'Indici testuali', 'Visti di nozione alfabetica', 'Semplice', 'Intero', 'AGGREGATE 3354', '2024-03-01 12:51:29', 'Alessio', '2024-05-01 19:50:06', 'Alessio'),
	(6, 'Insieme di contesto', 'Numeri di insiemi interi e definiti', 'Semplice', 'Intero', 'AGGREGATE 7320', '2024-04-01 12:52:30', 'Alessio', '2024-05-01 19:50:06', 'Alessio'),
	(7, 'Richieste di processi organizzativi', 'Valore totale di richieste ordinate, processate e verificate', 'Semplice', 'Decimale', 'AGGREGATE 4327', '2024-05-01 12:54:44', 'Alessio', '2024-07-01 14:04:28', 'Alessio'),
	(8, 'Matrici di origine elementare', 'Verifiche richieste di processo', 'Composto', 'Logaritmico', 'AGGREGATE 1989', '2024-05-01 12:57:16', 'Alessio', '2024-07-01 14:04:33', 'Alessio'),
	(9, 'Percezione cliente', 'Ordinamento valori di indicizzazione', 'Composto', 'Decimale', 'AGGREGATE 6682', '2024-05-01 12:59:04', 'Alessio', '2024-05-01 19:50:06', 'Alessio'),
	(10, 'Conversioni di stringhe alfanumeriche', 'Valore ridotto e scalato', 'Semplice', 'Intero', 'AGGREGATE 2500', '2024-07-01 13:00:21', 'Alessio', '2024-06-01 19:50:06', 'Alessio');
/*!40000 ALTER TABLE `tbl_kpi` ENABLE KEYS */;

-- Dumping structure for table p4c_open_db.tbl_kpicriteri
DROP TABLE IF EXISTS `tbl_kpicriteri`;
CREATE TABLE IF NOT EXISTS `tbl_kpicriteri` (
  `IdKpi` int(11) NOT NULL,
  `IdCriterio` int(11) NOT NULL,
  KEY `IdCriterio` (`IdCriterio`) USING BTREE,
  KEY `IdKpi` (`IdKpi`) USING BTREE
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Dumping data for table p4c_open_db.tbl_kpicriteri: 10 rows
DELETE FROM `tbl_kpicriteri`;
/*!40000 ALTER TABLE `tbl_kpicriteri` DISABLE KEYS */;
INSERT INTO `tbl_kpicriteri` (`IdKpi`, `IdCriterio`) VALUES
	(1, 8),
	(1, 11),
	(3, 1),
	(3, 4),
	(3, 18),
	(4, 9),
	(7, 2),
	(8, 1),
	(8, 17),
	(9, 5);
/*!40000 ALTER TABLE `tbl_kpicriteri` ENABLE KEYS */;

-- Dumping structure for table p4c_open_db.tbl_piattaforma
DROP TABLE IF EXISTS `tbl_piattaforma`;
CREATE TABLE IF NOT EXISTS `tbl_piattaforma` (
  `IdPiattaforma` int(11) NOT NULL AUTO_INCREMENT,
  `NomePiattaforma` varchar(50) DEFAULT NULL,
  `DescPiattaforma` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`IdPiattaforma`)
) ENGINE=MyISAM AUTO_INCREMENT=6 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Dumping data for table p4c_open_db.tbl_piattaforma: 5 rows
DELETE FROM `tbl_piattaforma`;
/*!40000 ALTER TABLE `tbl_piattaforma` DISABLE KEYS */;
INSERT INTO `tbl_piattaforma` (`IdPiattaforma`, `NomePiattaforma`, `DescPiattaforma`) VALUES
	(1, 'FRONT OFFICE', NULL),
	(2, 'BACK OFFICE', NULL),
	(3, 'SERVIZI WEB', NULL),
	(4, 'SPORTELLO CLIENTI', NULL),
	(5, 'GESTIONE PROCESSI', NULL);
/*!40000 ALTER TABLE `tbl_piattaforma` ENABLE KEYS */;

-- Dumping structure for table p4c_open_db.tbl_report
DROP TABLE IF EXISTS `tbl_report`;
CREATE TABLE IF NOT EXISTS `tbl_report` (
  `IdReport` int(11) NOT NULL AUTO_INCREMENT,
  `TipoOggetto` enum('Dashboard','File','Vista') NOT NULL DEFAULT 'Dashboard',
  `LivelloAccessibilita` enum('Pubblico','Interno','Ristretto') DEFAULT NULL,
  `NomeReport` varchar(50) DEFAULT NULL,
  `DescReport` varchar(500) DEFAULT NULL,
  `PathReport` varchar(500) DEFAULT NULL,
  `Link` varchar(500) DEFAULT NULL,
  `Dataset/ReportPadre` varchar(500) DEFAULT NULL,
  `DataInserimento` datetime DEFAULT NULL,
  `UtenteInserimento` char(7) DEFAULT NULL,
  `DataAggiornamento` datetime DEFAULT NULL,
  `UtenteAggiornamento` char(7) DEFAULT NULL,
  PRIMARY KEY (`IdReport`)
) ENGINE=MyISAM AUTO_INCREMENT=6 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Dumping data for table p4c_open_db.tbl_report: 5 rows
DELETE FROM `tbl_report`;
/*!40000 ALTER TABLE `tbl_report` DISABLE KEYS */;
INSERT INTO `tbl_report` (`IdReport`, `TipoOggetto`, `LivelloAccessibilita`, `NomeReport`, `DescReport`, `PathReport`, `Link`, `Dataset/ReportPadre`, `DataInserimento`, `UtenteInserimento`, `DataAggiornamento`, `UtenteAggiornamento`) VALUES
	(1, 'Dashboard', 'Pubblico', 'Iterazione principale', 'Dashboard di atterraggio degli utenti', NULL, 'https://www.google.it/', 'Generato', '2024-04-01 14:42:05', 'Alessio', '2024-06-01 19:51:47', 'Alessio'),
	(2, 'Vista', 'Ristretto', 'Pagina di oggetto', 'Valore di richiesta effettuata', 'PDKEO 11043', 'https://www.google.it/', NULL, '2024-05-01 14:55:28', 'Alessio', '2024-06-01 19:51:47', NULL),
	(3, 'File', 'Interno', 'Checkout', 'Denominazione privata dei resoconti', 'HTBVQ 49993', NULL, 'Automatico,Enumerato', '2024-05-01 14:56:58', 'Alessio', '2024-06-01 19:51:47', 'Alessio'),
	(4, 'File', 'Pubblico', 'Controllo risorse', 'Riparatore istantaneo di indicizzazione', 'UHUBD 98161', NULL, 'Automatico', '2024-06-01 14:58:20', 'Alessio', '2024-06-08 20:00:18', 'Alessio'),
	(5, 'File', 'Pubblico', 'Sommatoria generale', 'Requisiti finali di acquisto', 'WYBDI 40831', 'https://www.google.it/', 'Generato', '2024-07-01 20:03:47', 'Alessio', '2024-06-18 20:04:52', 'Alessio');
/*!40000 ALTER TABLE `tbl_report` ENABLE KEYS */;

-- Dumping structure for table p4c_open_db.tbl_reportcanale
DROP TABLE IF EXISTS `tbl_reportcanale`;
CREATE TABLE IF NOT EXISTS `tbl_reportcanale` (
  `IdReport` int(11) DEFAULT NULL,
  `IdCanale` int(11) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Dumping data for table p4c_open_db.tbl_reportcanale: 9 rows
DELETE FROM `tbl_reportcanale`;
/*!40000 ALTER TABLE `tbl_reportcanale` DISABLE KEYS */;
INSERT INTO `tbl_reportcanale` (`IdReport`, `IdCanale`) VALUES
	(1, 1),
	(1, 2),
	(2, 10),
	(3, 3),
	(3, 4),
	(3, 6),
	(5, 5),
	(4, 10),
	(4, 1);
/*!40000 ALTER TABLE `tbl_reportcanale` ENABLE KEYS */;

-- Dumping structure for table p4c_open_db.tbl_reportkpi
DROP TABLE IF EXISTS `tbl_reportkpi`;
CREATE TABLE IF NOT EXISTS `tbl_reportkpi` (
  `IdReport` int(11) DEFAULT NULL,
  `IdKpi` int(11) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Dumping data for table p4c_open_db.tbl_reportkpi: 6 rows
DELETE FROM `tbl_reportkpi`;
/*!40000 ALTER TABLE `tbl_reportkpi` DISABLE KEYS */;
INSERT INTO `tbl_reportkpi` (`IdReport`, `IdKpi`) VALUES
	(1, 9),
	(2, 8),
	(3, 3),
	(3, 10),
	(4, 4),
	(5, 2);
/*!40000 ALTER TABLE `tbl_reportkpi` ENABLE KEYS */;

-- Dumping structure for table p4c_open_db.tbl_valorienum
DROP TABLE IF EXISTS `tbl_valorienum`;
CREATE TABLE IF NOT EXISTS `tbl_valorienum` (
  `NomeCampoEnum` varchar(50) DEFAULT NULL,
  `ValoriCampoEnum` varchar(1000) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- Dumping data for table p4c_open_db.tbl_valorienum: 6 rows
DELETE FROM `tbl_valorienum`;
/*!40000 ALTER TABLE `tbl_valorienum` DISABLE KEYS */;
INSERT INTO `tbl_valorienum` (`NomeCampoEnum`, `ValoriCampoEnum`) VALUES
	('CategoriaKpi', 'Semplice|Composto'),
	('Dataset/ReportPadre', 'Automatico|Enumerato|Generato'),
	('TipoCriterio', 'Filtro|Misura'),
	('TipoOggetto', 'Dashboard|File|Vista'),
	('LivelloAccessibilita', 'Pubblico|Interno|Ristretto'),
	('UMKpi', 'Intero|Decimale|Sommato|Percentuale|Estratto|Logaritmico');
/*!40000 ALTER TABLE `tbl_valorienum` ENABLE KEYS */;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
