/*
Logiciel Autodeluxe réalisé par : 
Félix Clément 
Jacques Lavallée
*/

package autodeluxe;
import javax.swing.JOptionPane; //Pour l'interface
import javax.swing.JTextField; //Pour créer plusieurs zones de textes
import java.util.*; //Pour les List


public class program {

	public static void main(String[] args) throws ExceptionChauffeur {
		// TODO Auto-generated method stub
		
		final int ANNEEFONDEE = 1994;
		
		boolean quitter = false;
		boolean switchTrueFalse = false;

		
		String message = "";
		List <Integer> listOfInt = new ArrayList<Integer>();
		String option[] = {"Trouver toutes les limousines conduites par un chauffeur, sélectionné à partir de son numéro d’identification.", "Afficher toutes les caractéristiques des trajets effectués ainsi que les caractéristiques des limousines utilisées", "Ajouter des conducteurs","Quitter"};
		String choixUtilisateur;
		JTextField txtFirstName = new JTextField(30);
		JTextField txtLastName = new JTextField(30);
		JTextField txtHiredYear = new JTextField(30);
		JTextField txtAddress = new JTextField(30);
		
		Chauffeur conducteur = new Chauffeur("Default", "Default", 9999, "1 default street"); //Cette est utilisée lors de l'ajout de chauffeur.
		
		//Tableau qui contient les champs d'entrées ainsi que des String permettant d'itifier les champs pour l'utilisateur
		Object[] fieldsArray = {"Prénom : ", txtFirstName, "Nom de famille : ", txtLastName, "L'année de l'embauche : ", txtHiredYear, "Adresse : ", txtAddress};
		
		//Données Limousine Fournies
		String[][] arrLimousine =  
			{
					{"SSZG0T", "79", "rouge"}, 
					{"GRWE7I", "80", "jaune"}, 
					{"HW8HXT", "81", "blanc"}, 
					{"LWMFRX", "62", "jaune"}, 
					{"7AR19N", "86", "rose"}, 
					{"8D6HWL", "63", "jaune"}, 
					{"T6PG5C", "59", "jaune"}, 
					{"EEB9Y1", "72", "gris"}, 
					{"N06EVT", "72", "orange"}, 
					{"E18PIH", "97", "jaune"}, 
					{"G4JSD4", "100", "jaune"}, 
					{"ID3ZE4", "55", "bleu"}, 
					{"N59QBR", "68", "noir"}, 
					{"S6N4WM", "78", "gris"}, 
					{"NEIA1T", "80", "orange"}, 
					{"SCA1IJ", "96", "blanc"}, 
					{"SLAIMH", "51", "rose"}, 
					{"F6VW2G", "65", "rouge"}, 
					{"T34EA3", "71", "gris"}, 
					{"503HHR", "64", "rose"}
			};
		
		//Données Chauffeur Fournies
		String[][] arrChauffeur =  
			{
				{"Garfield", "John", "1994","111 boul Joseph"}, 
				{"Dunom", "Untel", "1995","555 rue Quelconque"}, 
				{"Rubik", "Cube", "1995","333 rue SquareCube"}, 
				{"Rynth", "Laby", "2005","123 Maze street"}, 
				{"Man", "Pac", "2012","198 Cheezy street"}
			};
		
		//Données Trajet Fournies
		String[][] arrTrajet =  
			{
				{"DunU95", "Québec", "Montréal", "25000", "25300", "GRWE7I"}, 
				{"GarJ94", "Trois-Rivières", "Sept-Iles", "26498", "26985", "GRWE7I"}, 
				{"RubC95", "Bout-du-Monde", "Nulle-Part", "64598", "64733", "LWMFRX"}, 
				{"RynL05", "Flatland", "Spaceland", "92403", "92676", "7AR19N"}, 
				{"ManP12", "Levis", "Warwick", "166096", "166188", "T6PG5C"}, 
				{"GarJ94", "Nulle-Part", "Spaceland", "91586", "91638", "EEB9Y1"}, 
				{"DunU95", "Warwick", "Flatland", "68439", "68501", "N06EVT"}, 
				{"RubC95", "Shawinigan", "Emptyland", "268982", "269100", "SSZG0T"}
			};
		
		// On vide les 2 dimensions array en remplissant un tableau d'objet
		Limousine fleet[] = new Limousine[arrLimousine.length] ;
		for(int j=0; j<arrLimousine.length; j++) {
			Limousine limousineTemp = new Limousine(arrLimousine[j][0], Integer.parseInt(arrLimousine[j][1]), arrLimousine[j][2]);
			fleet[j] = limousineTemp;
		}
		
		//On envoie les ID dans une liste.
		//Ceci va permettre de créer de nouveaux ID dans le programme et de les ajouter aux menus
		List <String> rosterID = new ArrayList<String>(); 
		Chauffeur roster[] = new Chauffeur[arrChauffeur.length] ;
		for(int j=0; j<arrChauffeur.length; j++) {
			Chauffeur chauffeurTemp = new Chauffeur(arrChauffeur[j][0], arrChauffeur[j][1], Integer.parseInt(arrChauffeur[j][2]), arrChauffeur[j][3]);
			roster[j] = chauffeurTemp;
			rosterID.add(roster[j].getDriverId());
		}
		
		Trajet itinerary[] = new Trajet[arrTrajet.length] ;
		for(int j=0; j<arrTrajet.length; j++) {
			Trajet trajetTemp = new Trajet(arrTrajet[j][0], arrTrajet[j][1], arrTrajet[j][2], Integer.parseInt(arrTrajet[j][3]), Integer.parseInt(arrTrajet[j][4]), arrTrajet[j][5]);
			itinerary[j] = trajetTemp;
		}
		
		Compagnie autoDeLuxe = new Compagnie("AutodeLuxe", ANNEEFONDEE); //1994, car c'est l'année la plus ancienne dans les données fournis.
		
		//Message d'ouverture du programme
		JOptionPane.showMessageDialog(null, "Bienvenue chez " + autoDeLuxe.get_Name() + "!\nFondée en " + autoDeLuxe.get_Year(), autoDeLuxe.get_Name(), JOptionPane.PLAIN_MESSAGE);
		
		//Cette boucle sera le menu
		do { 
			//On affciche un interface menu. (Liste déroulante)
			choixUtilisateur = (String) JOptionPane.showInputDialog(null, "Veuillez choisir une option :", "Menu", JOptionPane.QUESTION_MESSAGE, null, option, option[0]);
		
			
			//Trouver les limouisines conduites par un chauffeur.
			if (choixUtilisateur == option[0]) {
				

				//On converti la liste en Objet pour qu'elle soit accepté par JOptionPane.
				Object[] rosterID_Array = rosterID.toArray(); //La mettre ici permet d'ajouter de nouveau chauffeur dans une autre section.
				
				//Affiche le message permettant de choisir le conducteur
				choixUtilisateur = (String) JOptionPane.showInputDialog(null, "Veuillez sélectionner l'ID du chauffeur", "Sélection du chauffeur", JOptionPane.QUESTION_MESSAGE, null, rosterID_Array, rosterID_Array[0]);
				
				//Si l'utilisateur clique sur X ou Cancel
				if (choixUtilisateur != null) {
				
					listOfInt.clear(); //On vide la liste, car on a besoin qu'elle soit vide pour le bon déroulement de la prochaine section
					switchTrueFalse = false; // Va devenir true si le conducteur conduit une limousine.
					//Puisque le choix est sous forme de liste déroulante, il est LOGIQUEMENT obligatoire que le choix soit valide.
					for (int i = 0; i < itinerary.length; i++) {
						
						//On regarde si l'ID d'un chauffeur est le même que celui d'un itinéraire
						if (choixUtilisateur.equals(itinerary[i].getDriverId())) {
	
							switchTrueFalse = true; //Il conduit une limousine :)
							
							listOfInt.add(i); //On ajoute la position de L'ID à la liste.
							
						}
						
						//Arrive si le conducteur ne conduit pas de limousine
						if ((!switchTrueFalse ) && (i == itinerary.length - 1)) {
							JOptionPane.showMessageDialog(null, "Malheureusement, le conducteur " + choixUtilisateur + "\n ne conduit pas de limousine.", "Limousines conduites", JOptionPane.PLAIN_MESSAGE);
						}
						
						//Lorsque nous sommes à la dernière itération de la boucle et qu'il conduit une limousine
						else if (i == itinerary.length - 1) {
							message = "Le conducteur " + choixUtilisateur + " conduit les limousines suivantes : \n";
							
							//On ajoute les numéros de plaques dans la variables message.
							for(int compteurZ = 0; compteurZ < listOfInt.size(); compteurZ++) {
								message += itinerary[listOfInt.get(compteurZ)].getPlateNumber() + "\n"; 
							}
							//On affiche le message.
							JOptionPane.showMessageDialog(null, message, "Limousines conduites", JOptionPane.PLAIN_MESSAGE);
							
						}
						
					}
					
				}
	
			}
			
			//S'il veut voir les données des trajets.
			else if (choixUtilisateur == option[1]) {
				
				//On ajoute les données dans un String
				message = "Voici les caractéristiques des trajets effectués : ";
				//On boucle pour obtenir chaque trajet
				for (int i = 0; i < itinerary.length; i++) {
					message += "\n l'ID du conducteur est : "; //On change de ligne
					message += itinerary[i].getDriverId();
					message += " | ville de départ : ";
					message += itinerary[i].getStartLocation();
					message += " | ville d'arrivée : ";
					message += itinerary[i].getEndLocation();
					message += " | km départ : ";
					message += itinerary[i].getStartMilage();
					message += " | km arrivée : ";
					message += itinerary[i].getEndMilage();
					message += " | immatriculation";
					message += itinerary[i].getPlateNumber();
					
					for (int j = 0; j < fleet.length; j++) {
						//On trouve les données de la limousine conduite
						if (itinerary[i].getPlateNumber().equals(fleet[j].getPlateNo())) {
							message += " | réservoir : ";
							message += fleet[j].getFuelCapacity();
							message += "L | couleur : ";
							message += fleet[j].getColor();
						}
					}
				}
				
				//Message sur les infos des trajets
				JOptionPane.showMessageDialog(null, message, "Caractéristiques", JOptionPane.PLAIN_MESSAGE);
			}
			
			//S'il veut ajouter un conducteur
			else if (choixUtilisateur == option[2]) {
				boolean bAjout = false;
				int userAnswerInteger;
				while (!(bAjout)) {
					//On affiche un message avec la possibilité d'entrer des valeurs.
					JOptionPane.showMessageDialog(null, fieldsArray, "Ajout d'un conducteur", JOptionPane.PLAIN_MESSAGE);
					
					//On essaie le code suivant :
					try {
						//On convertit la valeur de l'année en int.
						userAnswerInteger = Integer.parseInt(txtHiredYear.getText());
						
							//On regarde si la compagnie existait au momment où on veut ajouter l'employer et on vérifie que l'utilisateur n'essaie pas d'entrer une année qui est dans le futur.
							if ((userAnswerInteger >= autoDeLuxe.get_Year()) && (userAnswerInteger <= Calendar.getInstance().get(Calendar.YEAR))) {
										if (!(txtFirstName.getText().isBlank() || txtLastName.getText().isBlank() || txtAddress.getText().isBlank()) ) {
											//On modifie les attributs de Chauffeur conducteur pour pouvoir obtenir son ID
											conducteur.setFirstName(txtFirstName.getText());
											conducteur.setLastName(txtLastName.getText());
											conducteur.setHiredYear(userAnswerInteger);
											conducteur.setAddress(txtAddress.getText());
									
											//on ajoute l'id du nouveau chauffeur à la list qui contient les ID.
											rosterID.add(conducteur.getDriverId());
											
											//On efface les entrées des Inputs
											txtFirstName.setText("");
											txtLastName.setText("");
											txtHiredYear.setText("");
											txtAddress.setText("");
											bAjout = true;
											JOptionPane.showMessageDialog(null, "Le chauffeur a été ajouté avec succès", "Information", JOptionPane.INFORMATION_MESSAGE);
											
										}
										else {
	
											try {
												throw new ExceptionChauffeur("Les informations du chauffeur sont incorrectes, SVP remplir à nouveau.");
												}
												catch (ExceptionChauffeur error1) {
													JOptionPane.showMessageDialog(null, ExceptionChauffeur.getMsg(), "ERREUR", JOptionPane.ERROR_MESSAGE);
												}
										}
									}
													
							//Si suppérieur à l'année actuelle
							else if (userAnswerInteger > Calendar.getInstance().get(Calendar.YEAR)){
								try {
									throw new ExceptionChauffeur("Vous devez entrer une année Valide dans l'année d'embauche\nNous sommes en " + Calendar.getInstance().get(Calendar.YEAR));
									}
									catch (ExceptionChauffeur error1) {
										JOptionPane.showMessageDialog(null, ExceptionChauffeur.getMsg(), "ERREUR", JOptionPane.ERROR_MESSAGE);
									}
							}
							
							//Si la valeur est inférieur à l'année de Création
							else if (userAnswerInteger < autoDeLuxe.get_Year()){
								try {
									throw new ExceptionChauffeur("Vous devez entrer une année Valide dans l'année d'embauche\nLa compagnie a été fondée en " + autoDeLuxe.get_Year());
									}
									catch (ExceptionChauffeur error1) {
										JOptionPane.showMessageDialog(null, ExceptionChauffeur.getMsg(), "ERREUR", JOptionPane.ERROR_MESSAGE);
									}
							}
					}
						
					//Lorsqu'une erreur survient (
					catch (java.lang.NumberFormatException error) {
						
						//S'il est vide, on suppose que l'utilisateur veut seulement retourner au menu.
						if (txtHiredYear.getText().equals("")) {
							JOptionPane.showMessageDialog(null, "Vous devez entrer un chiffre dans l'année d'embauche", "ERREUR", JOptionPane.ERROR_MESSAGE);
							try {
							throw new ExceptionChauffeur("Les informations du chauffeur sont incorrectes, SVP remplir à nouveau.");
							}
							catch (ExceptionChauffeur error1) {
								JOptionPane.showMessageDialog(null, ExceptionChauffeur.getMsg(), "ERREUR", JOptionPane.ERROR_MESSAGE);
							}
						}
						
					}
					
					//Lorsqu'un autre type d'erreur survient
					catch (Exception error) {
						
						; //Équivalent du pass Statement en Python. 
						//On n'est pas obligé de mettre le ;, mais ça montre que l'on veut qu'il ne se passe rien dans cette partie du code
						//(L'app va retourner au menu sans rien faire)
						
					}
					
				}
			}
			//Si l'utilisateur clique sur X, Cancel ou choisit l'option de quitter
			else {
				quitter = true; //Va permettre de sortir de la boucle
			}
			
			
		}while(!quitter); //Va boucler tant que quitter n'est pas vrai (Tant qu'il est faux)
		
	}
}

