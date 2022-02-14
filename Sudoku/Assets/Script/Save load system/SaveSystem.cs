using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;



public static class SaveSystem {

    

    public static void Save(SudokuBoard sudoku) {

        BinaryFormatter _formatter = new BinaryFormatter();

        //Trouve une path qui sera valide peut importe l'OS de la machine.
        string _path = Application.persistentDataPath + "/Sudoku Grid.sudgd"; //On crée la path du fichier
                                                                              //L'emplacement ressemblera à C:/Users/Félix/AppData/LocalLow/DefaultCompany/Sudoku - Projet session Advanced Object/Sudoku Grid.sudgd

        FileStream fichier = new FileStream(_path, FileMode.Create); //On crée un nouveau fichier dans la path précédemment défini

        SudokuData data = new SudokuData(sudoku);

        _formatter.Serialize(fichier, data);  //On écrit les données du Sudoku dans le fichier créer précédemment
        fichier.Close(); //On ferme le fichier
        
    }



    public static SudokuData Load() {

        string _path = Application.persistentDataPath + "/Sudoku Grid.sudgd";

        if (File.Exists(_path)) {

            BinaryFormatter formatter = new BinaryFormatter();

            FileStream fichier = new FileStream(_path, FileMode.Open);

            fichier.Position = 0;

            SudokuData sudokuLoad = formatter.Deserialize(fichier) as SudokuData;
            fichier.Close();

            return sudokuLoad;
            


        }

        //On ne trouve pas le fichier
        else {

            Debug.LogError("Save file not found in " + _path);
            return null;
        }
    }
    /*
    //Pour loader un fichier différent (Test Unitaire)
    public static SudokuData Load(string nomFichier) {

        string _path = Application.persistentDataPath + "/" + nomFichier; //On choisit le nom du fichier qu'on veut ouvrir

        if (File.Exists(_path)) {

            BinaryFormatter formatter = new BinaryFormatter();

            FileStream fichier = new FileStream(_path, FileMode.Open);

            fichier.Position = 0;

            SudokuData sudokuLoad = formatter.Deserialize(fichier) as SudokuData;
            fichier.Close();

            return sudokuLoad;



        }

        //On ne trouve pas le fichier
        else {

            Debug.LogError("Save file not found in " + _path);
            return null;
        }
    }*/

}
