#include <iostream>
#include <iomanip>
#include <string>
#include <vector>
#include <Windows.h>
#include "fonctions.h"


using namespace std;

/***********************************************************
Permet d'afficher les accents correctement dans la console
***********************************************************/
string accent(char tab[]) {
	char buffer[256];
	CharToOemA(tab, buffer);
	string str(buffer);
	return str;
}

/*******************************
Entr�e : un tableau contenant des doubles
Sortie : un double
R�le   : Calculer le total avant taxe de la facture
*******************************/
double totalSansTaxe(vector<double> const& tab) {
	double total (0);
	for (int i(0); i < tab.size(); i++) {
		total += tab[i];
	}
	return total;
}



/*******************************
Entr�e : un tableau contenant des doubles
Sortie : un double
R�le   : Calculer une moyenne
*******************************/
double moyenne(vector<double> const& tab) {
	double total(0);
	for (int i(0); i < tab.size(); i++) {
		total += tab[i];
	}
	return total / tab.size();

}

/****************************
	Entr�e : Un double
	Sortie : Un double
	R�le   : Calculer la TPS
	****************************/
double tps(double total) {
	double tps(0.05); //0.05, car la TPS est de 5%
	tps *= total;

	return tps;
}

/****************************
	Entr�e : Un double
	Sortie : Un double
	R�le   : Calculer la TVQ
	****************************/
double tvq(double total) {
	double tvq(0.09975);
	tvq *= total; // 0.09975, car la TVQ est de 9.975%

	return tvq;
}