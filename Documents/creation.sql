-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema epyks
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema epyks
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `epyks` DEFAULT CHARACTER SET utf8 ;
USE `epyks` ;

-- -----------------------------------------------------
-- Table `epyks`.`utilisateur`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `epyks`.`utilisateur` (
  `id_utilisateur` INT(11) NOT NULL AUTO_INCREMENT COMMENT '',
  `username` VARCHAR(45) NOT NULL COMMENT '',
  `password` VARCHAR(45) NOT NULL COMMENT '',
  `prenom` VARCHAR(60) NOT NULL COMMENT '',
  `nom` VARCHAR(60) NOT NULL COMMENT '',
  `sexe` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `image` BLOB NULL DEFAULT NULL COMMENT '',
  `email` VARCHAR(255) NOT NULL COMMENT '',
  `imgFile_name` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `imgFile_size` INT(11) NULL DEFAULT NULL COMMENT '',
  PRIMARY KEY (`id_utilisateur`)  COMMENT '',
  UNIQUE INDEX `username_UNIQUE` (`username` ASC)  COMMENT '')
ENGINE = InnoDB
AUTO_INCREMENT = 6
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `epyks`.`contact`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `epyks`.`contact` (
  `id_utilisateur` INT(11) NOT NULL COMMENT '',
  `id_amis` INT(11) NOT NULL COMMENT '',
  INDEX `fk_id_utilisateur_idx` (`id_utilisateur` ASC)  COMMENT '',
  INDEX `fk_id_amis_idx` (`id_amis` ASC)  COMMENT '',
  PRIMARY KEY (`id_utilisateur`, `id_amis`)  COMMENT '',
  CONSTRAINT `fk_id_utilisateur`
    FOREIGN KEY (`id_utilisateur`)
    REFERENCES `epyks`.`utilisateur` (`id_utilisateur`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_id_amis`
    FOREIGN KEY (`id_amis`)
    REFERENCES `epyks`.`utilisateur` (`id_utilisateur`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
