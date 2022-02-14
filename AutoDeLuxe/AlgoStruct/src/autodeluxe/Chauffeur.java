package autodeluxe;

public class Chauffeur {
	private String sLastName; 
	private String sFirstName; 
	private int iHiredYear; 
	private String sAddress; 
	private String sDriverId; 
	private boolean bConstructed;
	
	public Chauffeur(String lastName, String firstName, int hiredYear, String address) {
		bConstructed = false;
		setLastName(lastName);
		setFirstName(firstName);
		setHiredYear(hiredYear);
		setAddress(address);
		bConstructed = true;
		updateId();
	}
	public String getLastName () {
		return sLastName;
	}
	
	public void setLastName (String lastName) {
		sLastName = lastName;
		if (bConstructed)
			updateId();
	}
	
	public String getFirstName () {
		return sFirstName;
	}
	
	public void setFirstName (String firstName) {
		sFirstName = firstName;
		if (bConstructed)
			updateId();
	}
	
	public int getHiredYear () {
		return iHiredYear;
	}
	
	public void setHiredYear (int hiredYear) {
		iHiredYear = hiredYear;
		if (bConstructed)
			updateId();
	}
	
	public String getAddress () {
		return sAddress;
	}
	
	public void setAddress (String address) {
		sAddress = address;
		if (bConstructed)
			updateId();
	}
	
	public String getDriverId () {
		return sDriverId;
	}
	
	//Note, celui dont l'ID est RynL5 devient RynL05.
	//Soit try catch ici (on converti l'anné String en int. Si année < 2000, 
	//on gardde les deux derniers chiffres, sinon seulement le dernier.)
	//Option 2, on demande au correcteur si on peut transformer RynL5 en RynL05.
	private void updateId () {
		String sTempId = "";
		sTempId = sLastName.substring(0, 3) + sFirstName.substring(0, 1) + String.valueOf(iHiredYear).substring(2, 4);
		sDriverId = sTempId;
	}
	
}

/*
Chauffeurs
{"Garfield", "John", "1994","111 boul Joseph"}, 
{"Dunom", "Untel", "1995","555 rue Quelconque"}, 
{"Rubik", "Cube", "1995","333 rue SquareCube"}, 
{"Rynth", "Laby", "2005","123 Maze street"}, 
{"Man", "Pac", "2012","198 Cheezy street"}
 */