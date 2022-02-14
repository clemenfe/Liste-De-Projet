/*
Logiciel Autodeluxe réalisé par : 
Félix Clément 
Jacques Lavallée
*/

package autodeluxe;

public class Compagnie {
	private String _name; //Nom de la compagnie
	private int _foundationYear; //Année que la compagnie a été fondé. (Les employées ne peuvent pas avoir été engagée
								//avant que la compagnie soit créée ;p )
	
	
	//Constructeur 
	//ENTRÉE : le nom de la compagnie (String) et l'année de fondation de la compagnie (int)
	public Compagnie(String _name, int _foundationYear) {
		this._name = _name;
		this._foundationYear = _foundationYear;
	}
	
	/*********************************************
	Entrée : Aucune
	Rôle   : Pour recevoir le nom de la compagnie
	Sortie : nom de la compagnie (String)
	*********************************************/
	public String get_Name() {
		return _name;
	}
	
	/*********************************************
	Entrée : Aucune
	Rôle   : Pour recevoir l'année de la création
	Sortie : année de la création (int)
	*********************************************/
	public int get_Year() {
		return _foundationYear;
	}
	
	
	
}
