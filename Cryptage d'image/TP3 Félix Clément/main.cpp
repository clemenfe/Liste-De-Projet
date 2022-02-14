#include <stdlib.h>
#include <stdio.h>
#include <sys/timeb.h>
#include <time.h>



/****************
Félix Clément
17 Décembre
TP-3
****************/

//Définition des structures utilisées dans le programme
struct _timeb start, finish;
unsigned long duration;


typedef unsigned char  UCHAR;
struct IMAGERAW {
	int hauteur;
	int largeur;
	UCHAR* image;
};
typedef struct IMAGERAW IMAGE;


/**************************************************************************************
Entrée : Aucune
Rôle   : Permettre de faire choisir l'image qu'on veut utiliser dans le programme.
Sortie : Un pointeur qui pointe sur l'image (L'image étant de type IMAGE)
**************************************************************************************/
IMAGE* lire_image() {
	//OUVERTURE/LECTURE /FERMETURE  DE FICHIER 
	int h, l; //Vont servir pour obtenir la taille de l'image
	char nomfichier[80]; //Va contenir le nom du fichier
	IMAGE* im; //Le pointeur sur l'image
	FILE* fpI; //Pointeur sur un fichier
	bool validate = true; //Un boolean qui va définir si l'entrée est valide(true) ou non(false).
	errno_t err; //Variable pour vérifier les erreurs lors de l'ouverture du fichier.
	
	do {
		//Demander nom de fichier, ouvrir le fichier en lecture  et valider
		printf("Veuillez entrer le nom du fichier : ");
		
		gets_s(nomfichier); 
		
		err = fopen_s(&fpI, nomfichier, "rb");
		
		//On valide le fichier 
		if (err == 0) {
			validate = true;
			printf("\nLe fichier c'est ouvert correctement\n");
		}

		//Une erreur est survenue dans l'ouverture du fichier
		else { // fpI != NULL ???
			
			validate = false; //Pour permettre de recommencer la boucle
			printf("Une erreur est survenue lors de l'ouverture du fichier.\n Veuillez entrer un nom de fichier valide.\n");
		}

	}while (validate == false);

	//Reserver un espace memoire pour  im
	im = (IMAGE*)malloc(sizeof(IMAGE));
	
	

	//Pour entrer la hauteur de l'image
	do {

		printf("\nVeuillez entrer la hauteur de l'image : ");
		int userAnswer = scanf_s("%i", &h);


		if (userAnswer != NULL) {

			im->hauteur = h; //On dit que la hauteur est la varriable h (pour benji.raw c'est 1690)
			validate = true;
		}
		//Ceci va permettre de redemender à l'utilisateur d'entrer une nouvelle valeur (Arrive s'il entre une lettre)
		else {
			while (fgetc(stdin) != '\n');
			printf("\nENTRÉE INVALIDE!\n\n");
			validate = false;
		}


	} while (validate == false);



	//Pour entrer la largeur de l'image
	do {

		printf("\nVeuillez entrer la largeur de l'image : ");
		int userAnswer = scanf_s("%i", &l);


		if (userAnswer != NULL) {

			im->largeur = l; //On dit que la largeur est la varriable l (pour benji.raw c'est 1409)
			validate = true;
		}
		//Ceci va permettre de redemender à l'utilisateur d'entrer une nouvelle valeur (Arrive s'il entre une lettre)
		else {
			while (fgetc(stdin) != '\n');
			printf("\nENTRÉE INVALIDE!\n\n");
			validate = false;
		}


	} while (validate == false);

	 
	
	im->image = (UCHAR*)malloc(im->hauteur * im->largeur);

	//Lire l’image sur le disque     
	fread(im->image, sizeof(UCHAR), im->hauteur * im->largeur, fpI); // voir si fread() retourne une valeur valide
	fclose(fpI);
	/* RETOURNER LE POINTEUR SUR IMAGE */
	return(im);

}





/**************************************************************************************
Entrée : Un pointeur qui pointe sur l'image (L'image étant de type IMAGE)
Rôle   : crypter et décrypter l'image selon la clef de cryptage
Sortie : Un pointeur qui pointe sur l'image (L'image étant de type IMAGE)
**************************************************************************************/
IMAGE* crypter_decrypter(IMAGE* im) {

	int L, H, m, n;
	UCHAR cle; //Variable pour la clef de codification

	/* Acces avec positionnement spatial dans une image */
	L = im->largeur;   // largeur de l’image
	H = im->hauteur; // hauteur de l’image
	bool entreeValide = true; //Un boolean qui va définir si l'entrée est valide(true) ou non(false).
	char parcours; //Variable qui va contenir le type de parcours que l'utilisateur veut effectuer
	char entree[80]; //Entrée de l'utilisateur pour obtenir la clef
	//cle = 15;

	//Boucle pour vérifier les entrées de l'utilisateur
	do {

		printf("\n\nVeuillez entrer la clef de décryptage : ");
		
		while (fgetc(stdin) != '\n'); //Parfois le gets_s est "ignoré"... Ceci permet de régler le problème
		char* result = gets_s(entree);
		cle = (UCHAR)entree; //On converti la valeur en UCHAR

		//Si le résultat est valide (si l'utilisateur à entrée un chiffre entre 0 et 255)
		if (result != NULL) {
			
			do {
				printf("\nVeuillez entrer le mode de parcours\nr pour rangée\nc pour colonne\ns pour Accès Sans positionnement spatial\nVotre entrée : ");
				char result2 = scanf_s("%c", &parcours);

				if (result2 != NULL) {
					
					entreeValide = true; //On dit que l'entrée est valide

					//Sans positionnement spatial
					if (parcours == 's') {
						_ftime64_s(&start); //Pour commencer le chronomètre
						for (m = 0; m < L * H; m++) // Acces sans positionnement spatial 
							im->image[m] = im->image[m] ^ cle;  /* XOR */
						_ftime64_s(&finish); //Pour arrêter le chronomètre
					}


					//Parcours par rangée
					else if (parcours == 'r') {
						_ftime64_s(&start); //Pour commencer le chronomètre
						for (m = 0; m < H; m++) // Acces avec positionnement spatial
							for (n = 0; n < L; n++) // Parcours par rangée
								im->image[m * L + n] = im->image[m * L + n] ^ cle; /*XOR*/
						_ftime64_s(&finish); //Pour arrêter le chronomètre
					}


					//Parcours par colonne
					else if (parcours == 'c') {
						_ftime64_s(&start); //Pour commencer le chronomètre
						for (n = 0; n < L; n++) // Acces avec positionnement spatial
							for (m = 0; m < H; m++) // Parcours par colonne
								im->image[m * L + n] = im->image[m * L + n] ^ cle; /*XOR*/
						_ftime64_s(&finish); //Pour arrêter le chronomètre
					}

					//S'il entre autre chose que s r ou c
					else {

						entreeValide = false; //On dit que l'entrée n'est pas valide
						printf("\nENTRÉE INVALIDE!\n\n");
						while (fgetc(stdin) != '\n'); //Parfois le gets_s est "ignoré"... Ceci permet de régler le problème

					}

					//Va s'afficher seulement si entreeValide est vrai
					if (entreeValide) {
						duration = ((unsigned long)finish.time * 1000L + (unsigned long)finish.millitm) - ((unsigned long)start.time * 1000L + (unsigned long)start.millitm);
						printf("\nTemps d'execution : %d ms\n", duration); // On affiche la durée du parcours en ms.
					}
					
				}

				//Si le résultat n'est pas valide
				else {

					while (fgetc(stdin) != '\n');
					entreeValide = false;
					printf("\nENTRÉE INVALIDE!\n\n");
				}
			}while (entreeValide == false);

			entreeValide = true;
		}

		//Si le résultat n'est pas valide
		else {

			while (fgetc(stdin) != '\n');
			entreeValide = false;
			printf("\nENTRÉE INVALIDE!\n\n");
			printf("\nVeuillez entrer un chiffre entre 0 et 255 : ");

		}
	} while (entreeValide == false);

	/* RETOURNER LE POINTEUR SUR IMAGE */
	return(im);
}

/**************************************************************************************
Entrée : Un pointeur qui pointe sur l'image (L'image étant de type IMAGE)
Rôle   : Créer un nouveau fichier (une image) sur l'ordinateur
Sortie : Aucune
**************************************************************************************/
void ecrire_image(IMAGE* imageD) {
	/* OUVERTURE, ECRITURE ET  FERMETURE DE FICHIER */
	char nomfichier[80];
	FILE* fpD;
	bool validate;
	errno_t err;

	do {


		/* demander nom de fichier a ecrire*/
		printf("Veuillez entrer le nom du nouveau fichier : ");

		while (fgetc(stdin) != '\n'); //Parfois le gets_s est "ignoré"... Ceci permet de régler le problème
		gets_s(nomfichier); //On demande le nom de fichier 

		err = fopen_s(&fpD, nomfichier, "wb");

		//On vérifie s'il y a des erreurs
		if (err == 0) {
			validate = true;
			printf("\nLe fichier c'est écrit correctement\n");
		}

		//Une erreur est survenue dans l'ouverture du fichier
		else { // fpI != NULL ???

			validate = false; //Pour permettre de recommencer la boucle
			printf("Une erreur est survenue!\n");
		}

	} while (validate == false);

	//On écrit le fichier
	fwrite(imageD->image, sizeof(UCHAR), imageD->hauteur * imageD->largeur, fpD); // voir si fwrite() retourne une valeur valide

	fclose(fpD); //On ferme le fichier

}

/*****************************************
Entrée : Aucune
Rôle : fonction principal du programme
Sortie : int 0
*****************************************/
int main() {

	//DECLARATION D’UN POINTEUR SUR UNE 
// STRUCTURE IMAGE 
	IMAGE* im;

	// LECTURE DE L’IMAGE A CRYPTER

	im = lire_image();

	// CRYPTAGE DE L’IMAGE

	crypter_decrypter(im);

	// SAUVEGARDE DE L’IMAGE CRYPTEE

	ecrire_image(im);

	// LIBERER LA  RAM ATTRIBUEE DYNAMIQUEMENT

	free(im->image);
	free(im);

}