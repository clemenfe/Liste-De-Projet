/*
Logiciel Autodeluxe r�alis� par : 
F�lix Cl�ment 
Jacques Lavall�e
*/

package autodeluxe;

public class ExceptionChauffeur extends Exception { 
		private static String ErrorMsg;
	    public ExceptionChauffeur(String errorMessage) {
	    	setMsg(errorMessage);
	    }
	    private void setMsg (String Msg) {
	    	ErrorMsg = Msg;
	    }
	    public static String getMsg () {
	    	return ErrorMsg;
	    }
	}
