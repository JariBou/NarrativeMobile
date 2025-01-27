---------------------------------------
	Text Editor Package
---------------------------------------

Pour ouvrir la TextEditor Window :

Window -> TextEditor

Une fois la window créée :

--- Personnaliser un texte --- 

Ecrire le texte souhaité, sélectionner la partie du texte à modifier (Dans le TextField ou sur le visuel du texte)
Appuyer sur Select Text

Faire les modification nécessaire puis appuyer sur Apply to Selected Text

--- Sauvegarde de Texte Stylisés ---

Une fois satisfait du texte stylisé, Donner le nom de fichier voulu puis Appuyer sur Save Text to File 

Un Scriptable Object est créé et sélectionné. Dans l'inspector, il y a la possibilité de le regarder, l'ouvrir dans la TextEditor Window en appuyant sur Open ou Copier le texte en format RichText avec le bouton Copy RichTextFormat to clipboard, 
ensuite ouvrir un TextMeshPro et Ctrl + V dans la partie texte, pour faire apparaître le texte.

Pour modifier un fichier existant, renseigner le nom du fichier et appuyer sur Modify File dans la TextEditor Window

Enfin pour Importer un Fichier existant, renseigner le nom du fichier et appuyer sur Import File.

--- UI Generator ---

Le Script UI Generator, TextEditorPackage/Runtime/UIGenerator,
sert à créer un TextMeshPro contenant le texte stylisé à partir d'un nom de fichier. 

Pour cela, placer le fichier sur un Canvas, 
mettre dans le TextField "Saved Text File Name" le nom du fichier souhaité. 
Configurer le Background (Sprite et Size) comme voulu.
Appuyer sur Generate UI Element.



