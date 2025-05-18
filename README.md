<a name="readme-top"></a>

<p align="center">
  <img src="https://forthebadge.com/images/badges/made-with-c-sharp.svg"/>
  <img src="https://forthebadge.com/images/badges/0-percent-optimized.svg"/>
  <img src="https://forthebadge.com/images/badges/built-with-love.svg"/>
  <img src="https://forthebadge.com/images/badges/works-on-my-machine.svg"/>
</p>

<div align="center">
  <img src="https://i.imgur.com/Vzp4ZN7.png" alt="InullKit"/>
  <br />
  <p><b>Launcher STUdio + Java + Pilote Lunii — Tout-en-un</b></p>
  <br />
  <p align="center">
    <img src="https://img.shields.io/github/v/release/Seph29/LuniiKit_App?label=Version&style=for-the-badge" alt="Release"/>
    <img src="https://img.shields.io/github/downloads/Seph29/LuniiKit_App/total?label=Downloads&style=for-the-badge" alt="Downloads"/>
    <img src="https://img.shields.io/github/issues/Seph29/LuniiKit_App?label=Issues&style=for-the-badge" alt="Issues"/>
    <img src="https://img.shields.io/github/stars/Seph29/LuniiKit_App?label=Stars&style=for-the-badge" alt="Stars"/>
  </p>
  <br />
</div>

---

## 📖 Présentation

**InullKit** est un lanceur personnalisé pour le logiciel **STUdio** pour Lunii. Il simplifie l’installation des dépendances (Java 11, pilotes USB Lunii) et permet de lancer plusieurs versions du logiciel STUdio.

---

## 📷 Captures d’écran

<p align="center">
  <img src="https://i.imgur.com/NhB6DQN.png" width="300"/>
  <img src="https://i.imgur.com/9ScXZfR.png" width="300"/>
</p>

---

## ⭐️ Fonctionnalités

- Interface graphique simple
- Support de plusieurs versions de STUdio
- Installation automatique du pilote Lunii
- Installation automatique de Java via winget
- Option pour désactiver la suppression du fichier `official.json`

---

## 📦 Structure du dossier

```
project-folder/
│
├── agent/           # STUdio 0.3.1
├── driver/          # Pilote Lunii (depuis Luniistore)
├── jre11/           # Java Runtime 11
├── lib/             # Librairies STUdio 1.0.2
├── app/             # Application STUdio 1.0.2
├── quarkus/         # Framework STUdio 1.0.2
├── lib-0.3.1/       # Librairies STUdio 0.3.1 (renommées)
├── lib-0.4.2/       # Librairies STUdio 0.4.2 (renommées)
├── spg/             # Studio Pack Generator
│
├── quarkus-run.jar
├── studio-web-ui-0.3.1.jar
├── studio-web-ui-0.4.2.jar
└── Lunii-Admin
```

---

## ⚙️ Installation

1. Télécharger la dernière version ici :  
   [📦 Version 2.3.1](https://github.com/Seph29/LuniiKit_App/releases/latest)
2. Décompresser le package
3. Lancer le fichier exécutable fourni

---

## 📈 Journal des modifications

- Changement de nom : InullKit
- Ajout du support STUdio 1.0.2
- Suppression de la version 0.4.3
- Amélioration de l’interface utilisateur
- Ajout de l’option pour conserver `official.json`

📄 [Consulter toutes les versions](https://github.com/Seph29/LuniiKit_App/tags)

---

## 👥 Contributeurs

<table>
  <tr>
    <td align="center">
      <a href="https://github.com/royto">
        <img src="https://avatars.githubusercontent.com/u/6990995?v=4" width="80" alt="royto"/>
        <br /><sub><b>royto</b></sub>
      </a>
    </td>
  </tr>
</table>

---

## 📄 Licence

Projet sous licence **GPL-3.0** — voir le fichier [LICENSE](LICENSE)

### Licences tierces

- [STUdio 0.3.1](https://github.com/marian-m12l/studio) — MPL 2.0  
- [STUdio 0.4.2 / 1.0.2](https://github.com/kairoh/studio) — MPL 2.0  
- [SPG](https://github.com/jersou/studio-pack-generator) — MIT  
- [Lunii-Admin](https://github.com/olup/lunii-admin)

---

<p align="right"><a href="#readme-top">⬆️ Retour en haut</a></p>
