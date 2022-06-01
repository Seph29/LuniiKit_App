# LuniiKit
[![forthebadge](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://forthebadge.com)[![forthebadge](https://forthebadge.com/images/badges/0-percent-optimized.svg)](https://forthebadge.com)[![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com)

[![forthebadge](https://forthebadge.com/images/badges/works-on-my-machine.svg)](https://forthebadge.com)

Ma 1re application sur un langage que je ne maîtrise pas du tout !
LuniiKit est un launcher pour le logiciel STUdio, il permet aussi d'installer le driver lunii nécessaire a son fonctionnement et Java (via winget).

Mon code est sûrement moche mais fonctionnel 😜

![screeenshot](https://i.imgur.com/tlZIEL9.png)
![screeenshot](https://i.imgur.com/iVgnYL7.png)
![screeenshot](https://i.imgur.com/t3BCJYE.png)
![screeenshot](https://i.imgur.com/gfgcrv5.png)

## Pour commencer

vous pouvez telecharger le Package complet ici : [LuniiKit 2.2.0](https://github.com/Seph29/LuniiKit_App/releases/tag/2.2.0)

Vous pouvez aussi générer la solution vous-même via le code source et faire votre propre package en respectant la stucture
![screeenshot](https://i.imgur.com/wKJd5qn.png)

* Folder
  * agent (studio 0.3.1)
  * driver (disponible avec le Luniistore ``C:\Program Files\Luniistore\app\driver``)
  * jre11 (Java JRE 11)
  * lib-0.3.1 (studio 0.3.1 : renommer le dossier lib en lib-0.3.1)
  * lib-0.4.2 (studio 0.4.2 : renommer le dossier lib en lib-0.4.2)
  * lib-0.4.3 (studio 0.4.3 : renommer le dossier lib en lib-0.4.3-SNAPSHOT)
  * spg (Studio-pack-generator)
* Fichiers
  * studio-web-ui-0.3.1.jar
  * studio-web-ui-0.4.2.jar
  * studio-web-ui-0.4.3-SNAPSHOT.jar


La version SNAPSHOT est optionnelle, si elle n'existe pas dans le dossier le bouton est desactivé.
![screeenshot](https://i.imgur.com/oMG3SMA.png)

## Fabriqué avec

Visual Studio 2022

## Versions

- **Dernière version stable :** 2.2.2
- **Dernière version :** 2.2.0

## Changelog

- Ajout de WinSparkle pour la gestion des mises à jour
- Ajout de Studio Pack Generator
- Création d'un installer avec NSIS
- Sauvegarde des réglages au format JSON dans le dossier de l'application
- Ajout d'un bouton raccourci vers le dossier de l'application
- Ajout d'un bouton raccourci vers le dossier library de STUdio
- Modification graphique des boutons
- Ajout d'un bouton pour copier LuniiKit vers une clé USB (ou un autres dossier)
- Nettoyage du code
- Modification du bouton Github
- Dernière version du SNAPSHOT Studio 0.4.3

Liste des versions : [Cliquer pour afficher](https://github.com/Seph29/LuniiKit_App/tags)

## License

Ce projet est sous licence ``GPL-3.0 License`` - voir le fichier [LICENSE.md](LICENSE.md) pour plus d'informations
