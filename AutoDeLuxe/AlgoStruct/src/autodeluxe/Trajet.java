package autodeluxe;

public class Trajet {
	private String sDriverId; 
	private String sStartLocation;
	private String sEndLocation;
	private int iStartMilage;
	private int iEndMilage;
	private String sPlateNumber;

	public Trajet (String driverId, String startLocation, String endLocation, int startMilage, int endMilage, String plateNumber) {
		setDriverId(driverId);
		setStartLocation(startLocation);
		setEndLocation(endLocation);
		setStartMilage(startMilage);
		setEndMilage(endMilage);
		setPlateNumber(plateNumber);
	}
	
	public String getDriverId () {
		return sDriverId;
	}
	public void setDriverId (String driverId) {
		sDriverId = driverId;
	}
	
	public String getStartLocation () {
		return sStartLocation;
	}
	public void setStartLocation (String startLocation) {
		sStartLocation = startLocation;
	}
	
	public String getEndLocation () {
		return sEndLocation;
	}
	public void setEndLocation (String endLocation) {
		sEndLocation = endLocation;
	}
	
	public int getStartMilage () {
		return iStartMilage;
	}
	public void setStartMilage (int startMilage) {
		iStartMilage = startMilage;
	}
	
	public int getEndMilage () {
		return iEndMilage;
	}
	public void setEndMilage (int endMilage) {
		iEndMilage = endMilage;
	}
	
	public String getPlateNumber () {
		return sPlateNumber;
	}
	public void setPlateNumber (String plateNumber) {
		sPlateNumber = plateNumber;
	}
}
/*
{"DunU95", "Québec", "Montréal", "25000", "25300", "GRWE7I"}, 
{"GarJ94", "Trois-Rivières", "Sept-Iles", "26498", "26985", "GRWE7I"}, 
{"RubC95", "Bout-du-Monde", "Nulle-Part", "64598", "64733", "LWMFRX"}, 
{"RynL5", "Flatland", "Spaceland", "92403", "92676", "7AR19N"}, 
{"ManP12", "Levis", "Warwick", "166096", "166188", "T6PG5C"}, 
{"GarJ94", "Nulle-Part", "Spaceland", "91586", "91638", "EEB9Y1"}, 
{"DunU95", "Warwick", "Flatland", "68439", "68501", "N06EVT"}, 
{"RubC95", "Shawinigan", "Emptyland", "268982", "269100", "SSZG0T"}
*/