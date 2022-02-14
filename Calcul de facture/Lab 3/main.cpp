/*****************************************************
						Lab 3
			    	Par F�lix Cl�ment
					22 f�vrier 2019
*****************************************************/

#include <iostream>
#include <iomanip>
#include <vector>
#include <limits>
#include <Windows.h>
#include "fonctions.h"
#undef max;

using namespace std;

int main() {
	//D�claration du tableau
	vector<double> prixArticle(0); //initialis� � 0 pour emp�cher un erreur dans le programme

	//D�claration des variables
	const int PRIX_MINIMUM(0);
	const int PRIX_MAXIMUM(1000);
	bool finEntreeArticle(false);
	double prix;
	

	//Commande pour garder 2 d�cimales apr�s la virgule.
	cout << setiosflags(ios::fixed | ios::showpoint) << setprecision(2);

	//Pour faire afficher le message d'introduction
	cout << accent("Bienvenue dans ce programme. \n Celui-ci saisi les prix des diff�rents articles d�une facture. \n Les prix sont entre ") << PRIX_MINIMUM << " et " << PRIX_MAXIMUM << accent(" $. \n Veuillez utiliser le point ( . ) pour les nombres d�cimaux. \n Lorsque vous avez saisi tous les articles, \n veuillez-entrer -1.") << endl;

	//Pour que l'utilisateur puisse entrer les prix selon le nombre d'article qu'il ach�te
	for (int i(0); finEntreeArticle == false; i++) {
		cout << "Veuillez entrer le prix (en $) de l'article #" << (i + 1) << " : ";
		cin >> prix;

		if (!cin.fail() && prix >= PRIX_MINIMUM && prix <= PRIX_MAXIMUM) {
			prixArticle.push_back(prix);
		}
		else if (!cin.fail() && prix == -1) {
			finEntreeArticle = true;
		}
		//Pour emp�cher le programme de cr�er un erreur si l'utilisateur entre des donn�es non-valide
		else {
			cin.clear();
			cin.ignore(numeric_limits<streamsize>::max(), '\n');
			cout << accent("Valeur erron�e") << endl;
			i--; //Pour emp�cher l'augmentation de la valeur de i
		}
	}

	//Si l'utilisateur entre -1 comme prix du premier article
	if (prixArticle.size() != 0) {
		cout << accent("\n\n R�sultat pour cette facture : \n ........................................\n") << endl;

		//Pour afficher le prix des articles
		for (int i(0); i < prixArticle.size(); i++) {
			cout << "Article #" << (i + 1) << " : " << right << setw(10) << prixArticle[i] << " $" << endl;
		}

		cout << "\n ........................................\n" << endl;

		//Pour afficher les co�ts (avec l'aide des fonctions)
		cout << left << setw (20) << "Total Avant Taxe : " << right << setw(10) << totalSansTaxe(prixArticle) << " $" << endl;

		cout << left << setw(20) << accent("Co�t moyen : ") << right << setw(10) << moyenne(prixArticle) << " $" << endl;

		cout << left << setw(20) << "TPS : " << right << setw(10) << tps(totalSansTaxe(prixArticle)) << " $" << endl;

		cout << left << setw(20) << "TVQ : " << right << setw(10) << tvq(totalSansTaxe(prixArticle)) << " $" << endl;

		cout << left << setw(20) << "Grand Total : " << right << setw(10) << (totalSansTaxe(prixArticle) + tps(totalSansTaxe(prixArticle)) + tvq(totalSansTaxe(prixArticle))) << " $" << endl;
	}

	cout << accent("\n Merci d'avoir utilis� ce programme.") << endl;

	system("PAUSE");
	return 0;
}