# DataProject

Scène

La scène se compose d’un bureau d’ordinateur avec des icônes que l’on peut interagir avec.
Chaque icône à un niveau de protection et ne peut être ouverte que s’il est neutre.
Les outils ou programmes peuvent changer le niveau de protection.

Scripts

GameManager : Instancie les icônes et fenêtres sur le bureau à l’aide des données du DataManager
DataManager : Créer deux dictionnaires à partir des fichiers json
Interactable : Contient la majeure partie des informations des fichiers json lors de son instanciation. Chaque type d’icône (image, dossier, programme) devrait contenir un script qui dérive de Interactable pour modifier le comportement lors de l’ouverture d’un fichier.

Interactions externes

Ce qu’il est possible de faire:

	- Ajouter des icônes sur le bureau
	- Modifier les coordonnées des icônes
	- Modifier le nom,l’image et le niveau de protections des icônes
	- Ajouter/Retirer des interactions permettant de modifier le niveau de protection

Ce qu’il n’est pas possible de faire:
	- Modifier le fond d’écran
	- Afficher des messages avec un programme
	- Créer des interactions autres que le changement de protection.	

Ces interactions sont réparties dans deux fichiers json.
