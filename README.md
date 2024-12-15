# 🎮 **Guide d'installation et de test du jeu**

## **Prérequis**
Pour tester ce projet, vous devez avoir **Unity** installé sur votre ordinateur.

1. **Unity Hub** (pour gérer les versions d'Unity et les projets).
2. **Git** (pour cloner le projet depuis le dépôt).

---

## **Étape 1 : Installer Unity**

1. **Télécharger Unity Hub :**
   - Rendez-vous sur [Unity Download](https://unity.com/download).
   - Téléchargez et installez Unity Hub.

2. **Installer la version Unity utilisée dans ce projet :**
   - Ouvrez Unity Hub.
   - Cliquez sur **"Installs"** pour ajouter une nouvelle version.
   - Sélectionnez la version **`<INSÉRER VERSION UNITY>`** utilisée pour ce projet (remplacez par la version exacte).

3. **Installer les modules nécessaires :**
   - Assurez-vous d'ajouter les modules **"Windows Build Support"** ou **"Mac Build Support"**, selon votre plateforme.

---

## **Étape 2 : Cloner le projet depuis GitHub**

1. **Installer Git :**
   - Téléchargez et installez Git : [Git Download](https://git-scm.com/).

2. **Cloner le projet :**
   - Ouvrez un terminal ou Git Bash.
   - Exécutez la commande suivante pour cloner le projet :
     ```bash
     git clone <URL_DU_REPO>
     ```
   - Remplacez `<URL_DU_REPO>` par l'URL de votre projet Git.

3. **Accéder au dossier cloné :**
   ```bash
   cd <NOM_DU_PROJET>
   ```

---

## **Étape 3 : Ouvrir le projet avec Unity**

1. **Ouvrez Unity Hub.**
2. Cliquez sur **"Add"** pour ajouter un projet existant.
3. Sélectionnez le dossier cloné contenant le projet Unity.
4. Unity va importer et charger les fichiers du projet. Cela peut prendre quelques minutes.

---

## **Étape 4 : Tester le jeu**

1. Dans Unity, assurez-vous de sélectionner la scène principale :
   - Ouvrez le dossier **`Assets`**, puis cherchez la scène principale (souvent nommée **`MainScene`** ou **`GameScene`**).
2. Cliquez sur **Play** (le bouton ▶️ en haut) pour tester le jeu directement dans l'éditeur.

---

## **Problèmes courants**

- **Erreur de version Unity :**
   Assurez-vous que la version Unity installée correspond à celle indiquée pour le projet.

- **Assets manquants :**
   - Vérifiez que tous les fichiers ont bien été clonés depuis GitHub.
   - Si besoin, supprimez le dossier **`Library`** et relancez Unity.

---

## **Contribuer**

Pour signaler un bug ou proposer une amélioration :
1. Forkez ce projet.
2. Créez une branche : 
   ```bash
   git checkout -b feature/nom-de-la-fonctionnalité
   ```
3. Faites vos modifications et un **pull request**.

---

## **Licence**

Ce projet est sous licence **MIT** (ou autre, selon votre choix).

---

## 🎉 **Merci d'avoir testé le jeu !**
N'hésitez pas à laisser vos retours dans les issues du dépôt. 😊

---

![Aperçu du jeu](chemin/vers/image.png)

---

> **Note :** Remplacez `<INSÉRER VERSION UNITY>`, `<URL_DU_REPO>` et `chemin/vers/image.png` par les valeurs appropriées pour votre projet.
