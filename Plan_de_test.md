# Plan de test

### Pour les (#) suivre le plan pour voir la fonctionnalité.

## Interaction avec clavier/souris

<ol>
    <li> Touches W A S D pour faire marcher le personnage</li>
    <li> Bouger la souris pour faire bouger la direction que regarde le personnage</li>
    <li> Click gauche de la souris pour tirer l'arme en main (pour cacher l'icône de la souris faire soit le click droit ou gauche)</li>
    <li> Touche R pour recharger l'arme en main</li>
    <li> Si vous avez la grenade en inventaire, vous pouvez la lancer avec la touche G</li>
    <li> Touches E et Q pour changer l'arme en main si vous avez déjà ramassé une autre arme</li>
    <li> Une fois dans le jeu, touche ESC pour ouvrir le menu du jeu</li>
</ol>

## Fonctionnalité navigations des menus

<ol>
    <li>On peut naviguer dans le menu en cliquant sur les boutons</li>
    <li>Une fois dans le jeu, on peut appuyer sur ESC pour ouvrir le menu du jeu et cliquer sur les boutons pour naviguer</li>
    <li>Une fois dans le jeu, on peut bouger la souris pour changer la perspective du personnage et utiliser les touches W A S D pour le déplacer</li>
</ol>

## Fonctionnalité du tire du pistolet

<p>Quand on spawn dans le jeu, le personnage a un pistolet qu'il peut utiliser. Viser un ennemi et tirer avec le clic gauche de la souris jusqu'à ce que l'ennemi meure celui-ci vas disparaitre.</p>

## Fonctionnalité de detections du personnage et du degas que font les ennemis.

<p>Chaque ennemi a une zone de détection. Si le personnage entre dedans, l'ennemi va aller vers le personnage. Pour que l'ennemi tire sur le personnage, il doit s'arrêter devant lui. Comme l'ennemi n'a pas 100% de chance de toucher le personnage, il faut attendre et être chanceux d'être touché. Une fois touché, le personnage perd de la vie, cela se voit en bas à gauche de l'écran. Le personnage prend toujours 25 dégâts. Alors, le personnage doit être touché un total de 4 fois avant de mourir. Si la vie atteint 0, le personnage meurt, alors le jeu s'arrête et la partie est perdue. L'ennemi perd de vue le personnage s'il sort de la zone de détection ou bien si le personnage se cache de l'ennemi derrière un mur. L'ennemi ira se placer à la position où il a vu le personnage la dernière fois, puis il reprendra sa zone de patrouille. La vie du personnage diminue lorsqu'il se fait toucher</p>

### pour tester plusieur ennemis en meme temps, se plasser entre eux.

## Fonctionnalité de ramassage des objets et de gagnier la partie

<p>Pour gagner, le personnage doit ramasser trois objets et se diriger vers la porte de sortie : deux documents et une clé. Il doit les trouver dans la scène. Si le personnage essaie de sortir avant d'avoir tout ramassé, un message lui indiquant ce qu'il lui reste à ramasser va apparaître.Si je gange un message doit apparaitre me disant cela</p>

## Fonctionnalité d'appliquer le degats adequat

<p>Un ennemi a plusieurs zones de dégâts disponibles à tirer : la tête, le corps, les deux bras et les deux jambes. En fonction de l'endroit où on tire, l'ennemi va prendre les dégâts adéquats.</p>

### Pour le tester adequantement, commencer par le pistolet:

<ol>
    <li>Tirer sur la tete tu en un coup</li>
    <li>Tirer sur le corp tu en 4 coups</li>
    <li>Tirer sur les membres tu en 16 coups</li>
</ol>



## Fonctionnalité du ramassage de la grenade lancement et degats applique par la grenade.

<p>Une grenade peut être ramassée pour être ajoutée dans l'inventaire. Une fois trouvée, on peut appuyer sur G pour la lancer. Une seule grenade est disponible. Le dégât de la grenade est fixé à 100, permettant de tuer n'importe qui au contact.</p>

## Fonctionnalité de ramasser et de tirer avec les differentes armes.

<p> 3 Ars et 3 smgs pouront etre ramasses quand on jou. Deux de chaque pouront etre vu des qu'on spawn et les autres vont etre echappe par des ennemis lorsqu'ils vont mourir. On peut ramasser seulement un de chaque. Les copies qu'on retrouvent au debu sont la pour montrer que un doublon ne peut pas etre ramasse et ceux qui sont echapper par un ennemi pour demontrer que ya des ennemis qui peuvent echapper.</p>

### faire le test pour le AR

<ol>
    <li>Tirer sur la tete tu en un coup</li>
    <li>Tirer sur le corp tu en 4 coups</li>
    <li>Tirer sur les membres tu en 14 coups</li>
</ol>

### faire le test pour la smg

<ol>
    <li>Tirer sur la tete tu en 3 coups</li>
    <li>Tirer sur le corp tu en 10 coups</li>
    <li>Tirer sur les membres tu en 40 coups</li>
</ol>

## Fonctionnalité de ramasser le ammo box et de le faire apparaitre lorsqu'on tue un ennemi

<p> On commence la partie avec 10 balles et a chaque fois qu'on ramasse une boite de munition on obtien 10 balles de plus dans notre inventaire. Les balles dans l'inventaire sont partagees dans l'inventaire.</p>

## Fonctionnalité de recharger les arms et de changer les armes

<p>Sur le click du boutton R on recharge l'arme actuellement selectionnee et appuyer sur soit Q ou E pour changer l'arme actuellement selectionne. Il n y a pas d annimation, seulement le nombre de munitions change</p>
