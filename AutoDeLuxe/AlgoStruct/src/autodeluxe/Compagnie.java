/*
Logiciel Autodeluxe r�alis� par : 
F�lix Cl�ment 
Jacques Lavall�e
*/

package autodeluxe;

public class Compagnie {
	private String _name; //Nom de la compagnie
	private int _foundationYear; //Ann�e que la compagnie a �t� fond�. (Les employ�es ne peuvent pas avoir �t� engag�e
								//avant que la compagnie soit cr��e ;p )
	
	
	//Constructeur 
	//ENTR�E : le nom de la compagnie (String) et l'ann�e de fondation de la compagnie (int)
	public Compagnie(String _name, int _foundationYear) {
		this._name = _name;
		this._foundationYear = _foundationYear;
	}
	
	/*********************************************
	Entr�e : Aucune
	R�le   : Pour recevoir le nom de la compagnie
	Sortie : nom de la compagnie (String)
	*********************************************/
	public String get_Name() {
		return _name;
	}
	
	/*********************************************
	Entr�e : Aucune
	R�le   : Pour recevoir l'ann�e de la cr�ation
	Sortie : ann�e de la cr�ation (int)
	*********************************************/
	public int get_Year() {
		return _foundationYear;
	}
	
	
	
}
