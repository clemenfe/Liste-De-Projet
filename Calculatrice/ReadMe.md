Ce projet consistait à faire une calculatrice (sans priorité des opérations à l'exception des parenthèses (et sans gérer les parenthèses imbriqués)) tout en respectant les patrons *Façade*, *Composite*, *Builder*, *visiteur* et le principe *DRY (Don't Repeat Yourself)*. J'ai perdu quelques points pour le patron *Visiteur*, car ma logique du ***PrintVisitor*** était situé dans la fonction ***ToString()*** de la classe ***Expression*** au lieu d'être dans la classe ***PrintVisitor***. Le devis de ce travail est le document *travail3.md*

Pour le dossier test, j'ai seulement inclus les fichiers *.cs* et *.csproj*, car les dossier *bin* et *obj* contenait trop de fichier...

*Le projet a été fait à l'automne 2021*
