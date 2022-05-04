# LuniiKit
[![forthebadge](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://forthebadge.com)[![forthebadge](https://forthebadge.com/images/badges/0-percent-optimized.svg)](https://forthebadge.com)[![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com)

[![forthebadge](https://forthebadge.com/images/badges/works-on-my-machine.svg)](https://forthebadge.com)

Ma 1re application sur un langage que je ne maîtrise pas du tout !
LuniiKit est un launcher pour le logiciel STUdio, il permet aussi d'installer le driver lunii nécessaire a son fonctionnement et Java (via winget).

Mon code est sûrement moche mais fonctionnel 😜

![screeenshot](https://i.imgur.com/uOhujL5.png)
![screeenshot](https://i.imgur.com/CWbfr3Q.png)
![screeenshot](https://i.imgur.com/jqKeFWG.png)

## Pour commencer

vous pouvez telecharger le Package complet ici : [LuniiKit 2.1.3](https://github.com/Seph29/LuniiKit_App/releases/download/2.1.1/LuniiKit-v2.1.2.zip)

Vous pouvez aussi générer la solution vous-même via le code source et faire votre propre package en respectant la stucture
![screeenshot](https://i.imgur.com/wKJd5qn.png)

* Folder
  * agent (studio 0.3.1)
  * driver (disponible avec le Luniistore ``C:\Program Files\Luniistore\app\driver``)
  * jre11 (Java JRE 11)
  * lib-0.3.1 (studio 0.3.1 : renommer le dossier lib en lib-0.3.1)
  * lib-0.4.2 (studio 0.4.2 : renommer le dossier lib en lib-0.4.2)
  * lib-0.4.3 (studio 0.4.3 : renommer le dossier lib en lib-0.4.3-SNAPSHOT)
* Fichiers
  * studio-web-ui-0.3.1.jar
  * studio-web-ui-0.4.2.jar
  * studio-web-ui-0.4.3-SNAPSHOT.jar


La version SNAPSHOT est optionnelle, si elle n'existe pas dans le dossier le bouton est desactivé.
![screeenshot](https://i.imgur.com/fRQUiUW.png)

## Fabriqué avec

Visual Studio 2022

## Versions

- **Dernière version stable :** 2.1.3
- **Dernière version :** 2.1.3

## Changelog

- Ajout du choix de la version Java à installer
- Ajout option " -DskipTests"
- Ajout option choix du nombre de logs conservés (entre 1 et 99)

Liste des versions : [Cliquer pour afficher](https://github.com/Seph29/LuniiKit_App/tags)

## License

Ce projet est sous licence ``GPL-3.0 License`` - voir le fichier [LICENSE.md](LICENSE.md) pour plus d'informations
