/*
Logiciel Autodeluxe r?alis? par : 
F?lix Cl?ment 
Jacques Lavall?e
*/

package autodeluxe;

public class Limousine {
	private String sPlateNumber; 
	private int iFuelCapacity;
	private String sColor;
	
	public Limousine(String plateNumber, int fuelCapacity, String color) {
		setPlateNo(plateNumber);
		setFuelCapacity(fuelCapacity);
		setColor(color);
	}
	
	public String getPlateNo () {
		
		return sPlateNumber;
	}
	
	public void setPlateNo (String plateNumber) {
		sPlateNumber = plateNumber;
	}
	
	public int getFuelCapacity () {
		
		return iFuelCapacity;
	}
	
	public void setFuelCapacity (int fuelCapacity) {
		iFuelCapacity = fuelCapacity;
	}
	public String getColor () {
		
		return sColor;
	}
	
	public void setColor (String color) {
		sColor = color;
	}
}

/*
Limousines
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
*/