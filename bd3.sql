CREATE DATABASE  IF NOT EXISTS `vulrill` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci */;
USE `vulrill`;
-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: vulrill
-- ------------------------------------------------------
-- Server version	5.5.5-10.4.26-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `client`
--

DROP TABLE IF EXISTS `client`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `client` (
  `id_client` int(11) NOT NULL AUTO_INCREMENT,
  `surname` varchar(45) NOT NULL,
  `name` varchar(45) NOT NULL,
  `patronymic` varchar(45) DEFAULT NULL,
  `phone_number` bigint(20) NOT NULL,
  `age` int(11) NOT NULL,
  PRIMARY KEY (`id_client`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `client`
--

LOCK TABLES `client` WRITE;
/*!40000 ALTER TABLE `client` DISABLE KEYS */;
INSERT INTO `client` VALUES (1,'Негирова','Анастасия','Михайловна',87894561230,21),(2,'Березова','Ольга','Ивановна',88745511644,66),(8,'Царевич','Иван','Колыванович',89506304578,23),(11,'Золотов','Александр','Владимирович',87845616573,32),(12,'Прунина','Анна','Олеговна',84765834768,40),(13,'Плесникова','Ольга','Ивановна',89789441515,19),(14,'Беляков','Виктор','Сергееивч',89012341242,19),(15,'Шумилов','Валерий','Михайлович',82342354234,23),(16,'Смирнова','Дарья','Павловна',84358347593,18),(17,'Юрева','Ульяна','Анатольевна',81554235232,28),(18,'Обухина','Виктория','Сергеевна',85234423423,31),(19,'Рябиков','Дмитрий','Алексеевич',86745634534,19),(20,'Пахтусов','Алексей','Алексеевич',83546423556,19),(21,'Охлопков','Андрей','Анатольевич',85235434534,20),(22,'Смирнов','Олег','Сергеевич',84635423412,22),(23,'Шипкова','Виктория','Олеговна',85345634523,23),(24,'Удалова','Мария','Михайловна',84634563245,28),(25,'Петров','Михаил','Андреевич',84856634534,48),(27,'Песков','Андрей','Артемович',85234234214,26),(28,'Красильникова','Арина','Олеговна',82342342342,48),(29,'Верхорубов','Данил','Михайлович',85564345345,33),(30,'Удалова','Евгения','Михайловна',86533452354,19),(31,'Пеньков','Никита','Андреевич',82342534532,19),(32,'Беляков','Вадим','Александрович',89566345325,19),(33,'Сенцов','Кирилл','Валерьевич',86345342532,35),(34,'Конкин','Никита','Александрович',85764563453,19),(35,'Кузнецов','Илья','Александрович',86536743524,19),(36,'Сизанкова','Кира','Олеговна',88567456435,19),(37,'Перунин','Илья','Андреевич',88896576456,44),(38,'Мишутова','Виктория','Павловна',89856456345,58),(39,'Смирнов','Глеб','Никитич',86746745634,25),(40,'Смирнова','Ульяна','Дмитриевна',89463452323,18),(41,'Доронин','Матвей','Вячеславович',89676735658,21),(42,'Мальцев','Антон','Антонович',87654365324,27),(43,'Дремин','Иван','Андреевич',89885634523,32),(44,'Кудряшов','Ефим','Евгеньевич',85687463453,18),(45,'Романова','Ева','Дмитриевна',89856745634,24),(46,'Семенов','Дмитрий','Олегович',88569856747,68),(47,'Ширкунов','Сергей','Анатольеивич',89567456345,27),(48,'Мудров','Максим','Алексееивч',86456345324,24),(49,'Рожов','Кирилл','Петрович',85634523423,27),(50,'Бодров','Константин','Михайлович',89047964234,34),(51,'Мелентьев','Максим','Андреевич',87634523423,18),(52,'Вербин','Павел','Анатольевич',82342112312,30);
/*!40000 ALTER TABLE `client` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employee`
--

DROP TABLE IF EXISTS `employee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employee` (
  `id_employee` int(11) NOT NULL AUTO_INCREMENT,
  `login` varchar(45) NOT NULL,
  `password` varchar(100) NOT NULL,
  `role_id` int(11) NOT NULL,
  `surname` varchar(45) NOT NULL,
  `name` varchar(45) NOT NULL,
  `patronymic` varchar(45) DEFAULT NULL,
  `phone_number` bigint(20) NOT NULL,
  PRIMARY KEY (`id_employee`),
  KEY `fk_employee_role_idx` (`role_id`),
  CONSTRAINT `fk_employee_role` FOREIGN KEY (`role_id`) REFERENCES `role` (`id_role`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employee`
--

LOCK TABLES `employee` WRITE;
/*!40000 ALTER TABLE `employee` DISABLE KEYS */;
INSERT INTO `employee` VALUES (1,'admin','D033E22AE348AEB5660FC2140AEC35850C4DA997',1,'Смирнов','Никита','Сергеевич',89624582655),(2,'manager','1A8565A9DC72048BA03B4156BE3E569F22771F23',2,'Вербин','Антон','Александрович',87456652189),(5,'manager2','D2C9A46B3870E03E3C45C6A6BA0D7A574F50C698',2,'Бабуинов','Лев','Петрович',89595454454),(12,'manager3','DB8B07FAD20CCC4EAC5F54CE0AF56A994E2059B3',2,'Копылов','Владислав','Максимович',89567456345),(13,'manager4','F4518CA20F753790DD4F0B5B167E9E1AE2ED9E16',2,'Кудряшов','Евгений','Сергеевич',89430238482),(14,'manager5','4411060FAA23F7B02AC5A4350224935007E76624',2,'Сергин','Петр','Олегович',88967896653);
/*!40000 ALTER TABLE `employee` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master`
--

DROP TABLE IF EXISTS `master`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `master` (
  `id_master` int(11) NOT NULL AUTO_INCREMENT,
  `surname` varchar(45) NOT NULL,
  `name` varchar(45) NOT NULL,
  `patronymic` varchar(45) DEFAULT NULL,
  `experience` int(11) NOT NULL,
  `phone_number` bigint(20) NOT NULL,
  PRIMARY KEY (`id_master`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master`
--

LOCK TABLES `master` WRITE;
/*!40000 ALTER TABLE `master` DISABLE KEYS */;
INSERT INTO `master` VALUES (1,'Сутулов','Андрей','Викторович',8,89542457155),(2,'Андропов','Петр','Васильевич',3,89515145151),(3,'Генералов','Дмитрий','Дмитриевич',11,89895515515),(6,'Курсаков','Иван','Олегович',1,89535544422),(7,'Заренкова','Анастасия','Евгеньевна',3,83214645465),(8,'Арцыбашева','Александра','Александровна',4,82154544488),(9,'Каширин','Даниил','Олегович',5,89552958484),(10,'Повелайтис','Владислав','Петрович',2,89848444422),(11,'Солунин','Никита','Владиславович',9,84515115211),(15,'Иванов','Иван','Александрвич',12,89678567456);
/*!40000 ALTER TABLE `master` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `order`
--

DROP TABLE IF EXISTS `order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `order` (
  `id_order` int(11) NOT NULL AUTO_INCREMENT,
  `sketch_id` int(11) NOT NULL,
  `master_id` int(11) NOT NULL,
  `client_id` int(11) NOT NULL,
  `employee_id` int(11) NOT NULL,
  `date` date NOT NULL,
  PRIMARY KEY (`id_order`),
  KEY `fk_order_sketch1_idx` (`sketch_id`),
  KEY `fk_order_master1_idx` (`master_id`),
  KEY `fk_order_client1_idx` (`client_id`),
  KEY `fk_order_employee1_idx` (`employee_id`),
  CONSTRAINT `fk_order_client1` FOREIGN KEY (`client_id`) REFERENCES `client` (`id_client`),
  CONSTRAINT `fk_order_employee1` FOREIGN KEY (`employee_id`) REFERENCES `employee` (`id_employee`),
  CONSTRAINT `fk_order_master1` FOREIGN KEY (`master_id`) REFERENCES `master` (`id_master`),
  CONSTRAINT `fk_order_sketch1` FOREIGN KEY (`sketch_id`) REFERENCES `sketch` (`id_sketch`)
) ENGINE=InnoDB AUTO_INCREMENT=88 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order`
--

LOCK TABLES `order` WRITE;
/*!40000 ALTER TABLE `order` DISABLE KEYS */;
INSERT INTO `order` VALUES (1,1,1,1,2,'2024-11-10'),(2,42,8,8,5,'2024-11-11'),(3,22,1,12,2,'2024-11-12'),(4,14,10,2,2,'2024-11-13'),(7,9,11,2,2,'2024-11-09'),(8,2,1,1,2,'2024-11-10'),(9,40,7,8,2,'2024-11-11'),(10,30,1,13,2,'2024-11-12'),(11,41,7,2,2,'2024-11-13'),(12,31,1,11,2,'2024-10-14'),(13,5,1,24,2,'2024-11-14'),(14,21,9,17,14,'2024-10-18'),(15,5,1,51,2,'2024-10-20'),(16,5,6,45,2,'2024-10-23'),(17,5,6,44,2,'2024-10-17'),(18,15,9,51,2,'2024-10-11'),(19,27,11,18,2,'2024-11-12'),(20,47,3,37,2,'2024-11-14'),(21,35,8,51,2,'2024-11-01'),(22,37,6,14,2,'2024-11-03'),(23,33,6,45,2,'2024-11-15'),(24,48,7,46,2,'2024-11-01'),(25,20,8,12,2,'2024-11-02'),(26,19,7,34,2,'2024-11-03'),(27,39,6,11,2,'2024-11-04'),(28,40,3,47,2,'2024-11-05'),(29,27,3,50,2,'2024-11-06'),(30,15,3,40,2,'2024-11-07'),(31,44,7,48,2,'2024-11-08'),(32,1,7,38,2,'2024-11-10'),(33,1,1,51,2,'2024-11-09'),(34,1,1,16,2,'2024-11-08'),(35,24,2,33,2,'2024-11-07'),(36,4,9,23,2,'2024-11-02'),(37,6,7,18,2,'2024-11-03'),(38,5,9,44,2,'2024-11-04'),(39,45,2,27,2,'2024-11-05'),(40,14,6,41,2,'2024-11-06'),(41,43,3,43,2,'2024-10-12'),(42,29,6,48,2,'2024-10-15'),(43,2,7,25,2,'2024-10-03'),(44,13,11,8,2,'2024-11-03'),(45,24,3,45,2,'2024-11-04'),(46,28,9,39,2,'2024-11-15'),(47,23,8,12,2,'2024-11-05'),(48,11,8,45,2,'2024-10-18'),(49,10,7,8,2,'2024-10-15'),(50,25,11,19,2,'2024-10-18'),(51,22,8,32,2,'2024-11-14'),(52,8,3,33,2,'2024-11-14'),(53,7,8,31,2,'2024-11-11'),(54,38,10,24,2,'2024-11-12'),(55,36,7,50,2,'2024-11-12'),(56,32,9,35,2,'2024-11-12'),(57,26,11,30,2,'2024-11-13'),(58,16,11,28,2,'2024-11-15'),(59,31,6,8,2,'2024-11-13'),(60,39,9,15,2,'2024-11-08'),(61,17,8,37,2,'2024-11-06'),(62,46,8,32,2,'2024-11-03'),(63,8,1,33,2,'2024-11-05'),(64,10,10,52,2,'2024-11-10'),(65,5,1,52,2,'2024-11-15'),(66,6,7,38,2,'2024-11-17'),(67,15,11,51,2,'2024-11-17'),(68,16,11,51,2,'2024-11-17'),(69,45,3,40,2,'2024-11-17'),(70,47,2,45,2,'2024-11-17'),(71,40,1,22,2,'2024-11-17'),(72,45,1,52,2,'2024-11-17'),(73,45,1,39,2,'2024-11-17'),(74,40,1,52,2,'2024-11-17'),(75,40,1,52,2,'2024-11-17'),(76,40,1,52,2,'2024-11-17'),(77,48,3,47,2,'2024-11-17'),(78,40,6,48,2,'2024-11-17'),(79,15,6,47,2,'2024-11-17'),(80,15,6,47,2,'2024-11-17'),(81,40,6,47,2,'2024-11-17'),(82,48,2,46,2,'2024-11-17'),(83,36,6,49,2,'2024-11-17'),(84,47,2,48,2,'2024-11-17'),(85,42,6,49,2,'2024-11-17'),(86,40,1,49,2,'2024-11-17'),(87,40,2,48,2,'2024-11-17');
/*!40000 ALTER TABLE `order` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role` (
  `id_role` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  PRIMARY KEY (`id_role`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,'Администратор'),(2,'Менеджер');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sketch`
--

DROP TABLE IF EXISTS `sketch`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sketch` (
  `id_sketch` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `cost` int(11) NOT NULL,
  `image` varchar(45) NOT NULL,
  PRIMARY KEY (`id_sketch`)
) ENGINE=InnoDB AUTO_INCREMENT=59 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sketch`
--

LOCK TABLES `sketch` WRITE;
/*!40000 ALTER TABLE `sketch` DISABLE KEYS */;
INSERT INTO `sketch` VALUES (1,'Бабочка',5400,'babochka.png'),(2,'Бык',3500,'bull.png'),(3,'Череп с розами',7800,'cherep.jpg'),(4,'Роза',4000,'rose.jpg'),(5,'Солнце',2000,'sun.png'),(6,'Петля',2300,'traibal.png'),(7,'Пламя',6100,'twiner.jpg'),(8,'Купола',5400,''),(9,'Крылья',7500,'wings.png'),(10,'Волк',4900,'woolf.png'),(11,'Ястреб',4900,'yastreb.jpg'),(13,'Вектор',3600,'vector.png'),(14,'Молния с лицом',3000,'face.png'),(15,'Девочка',5500,'girl.jpg'),(16,'Огенный глаз',10000,'glas.jpg'),(17,'Змей',12000,'zmei.jpg'),(18,'Скульптура',14000,'sculpture.jpg'),(19,'Демон',13000,'demon.jpg'),(20,'Маска страшная',13500,'maska.jpg'),(21,'Корабль',18000,'korab.jpg'),(22,'Ангел',5000,'angel.jpg'),(23,'Герой',4700,'geroi.jpg'),(24,'Бандит',3700,'bandit.jpg'),(25,'Монстор',5000,'monstr.jpg'),(26,'Клоун',10000,'clown.jpg'),(27,'Рыцарь',7400,'ruc.jpg'),(28,'Сокол',4600,'sokol.jpg'),(29,'Колючая прововолка',3200,'uzor.jpg'),(30,'Сова',15200,'sova.jpg'),(31,'Человек маяк',11600,'nn.jpg'),(32,'Чудовище',9600,'chudo.jpg'),(33,'Портрет',20000,'portret.jpg'),(35,'Глаз демона',8700,'demonglas.jpg'),(36,'Собака с чаем',9350,'dog.jpg'),(37,'Дракон',23000,''),(38,'Горы',6400,''),(39,'Череп №2',12000,'golova.jpg'),(40,'Кот скелет',6490,'cat.jpg'),(41,'Анаконда',19100,'anakonda.jpg'),(42,'Портрет мужчины',25000,'port.jpg'),(43,'Машина',3000,''),(44,'Самолет в небе',5400,''),(45,'Паук',2800,'pauk.jpg'),(46,'Медведь',12500,'bear.jpg'),(47,'Персонаж из мультика',7700,'mult.jpg'),(48,'Пожилой человек',30000,'deduchka.jpg');
/*!40000 ALTER TABLE `sketch` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'vulrill'
--

--
-- Dumping routines for database 'vulrill'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-11-17  4:19:35
