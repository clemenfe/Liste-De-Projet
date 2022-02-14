using Assets.Script.Conception_scripts;
using Assets.Script.Visual_scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ProjetDeSession {
    public class GameController : MonoBehaviour {
        [SerializeField] GameObject casePrefab;
        [SerializeField] GameObject colorPanel;

        private static GameController _instance;
        private List<VisualCase> cases = new List<VisualCase>();


        private SudokuBoard gameBoard;
        private CommandInvoker commandInvoker;
        private CaseMemory caseMemory;

        private MultipleCommand MultipleCommands = new MultipleCommand();

        public bool inColorMode { get; private set; } = false;

        public SudokuBoard getGameBoard() {
            return gameBoard;
        }

        private void Awake() {
            if (_instance != null)
                Destroy(this);
            gameBoard = new SudokuBoard();

            commandInvoker = CommandInvoker.Instance; //Get l'instance du commandInvoker lors d'une action, soit à l'input
            caseMemory = FindObjectOfType<CaseMemory>();

            ColorPanelOff();
        }

        public void WakeMeUp()//Uniquement pour simulé le Awake() dans les tests
        {

            gameBoard = new SudokuBoard();

            commandInvoker = CommandInvoker.Instance; //Get l'instance du commandInvoker lors d'une action, soit à l'input
            caseMemory = FindObjectOfType<CaseMemory>();
        }

        private GameController() {

        }

        //Semblable à Instance, mais avec un new
        public static GameController Instance2 {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<GameController>();
                    if (_instance == null) {
                        _instance = new GameController(); //Le constructeur a été déclaré privé.
                    }
                }
                return _instance;
            }
        }

        public static GameController Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<GameController>();
                    if (_instance == null) {
                        _instance = new GameObject().AddComponent<GameController>();
                    }
                }
                return _instance;
            }
        }

        public void addCase(VisualCase newCase) {
            cases.Add(newCase);
        }

        public SudokuBoard GetSudokuBoard() {
            return gameBoard;
        }

        public GameObject getCasePrefab() {
            return casePrefab;
        }

        public void NumberEntry(int newNum, ICase caseSelectionne, AbstractInteractableCase caseVisuelle) {
            /*caseSelectionne.changeValue(newNum);
            if (caseSelectionne.GetType().Equals(typeof(CaseValue))) //Si on a modifié une case
            {
                gameBoard.verifyDuplicate(); //Vérifie l'ensemble des éléments sur la grille afin d'en noter ceux qui ont un doublon
            }*/
            if (caseMemory.selectMultiple) {

                MultipleCommands.addCommand(new NumberEntryCommand(newNum, caseSelectionne, caseVisuelle));

                if (MultipleCommands.getCommandCount() == caseMemory.MultipleInterations.Count)
                    commandInvoker.execute(MultipleCommands);
            }
            else {
                NumberEntryCommand command = new NumberEntryCommand(newNum, caseSelectionne, caseVisuelle); //Créer une command d'entrée
                commandInvoker.execute(command); //Demande à l'invoker d'executer la commande, ce qui permet de 'stacker' la command en cas d'un undo ou redo.
            }
        }

        public void NumberEntry(int newNum, ICase caseSelectionne) {
            caseSelectionne.changeValue(newNum);
            if (caseSelectionne.GetType().Equals(typeof(CaseValue))) //Si on a modifié une case
            {
                gameBoard.verifyDuplicate(); //Vérifie l'ensemble des éléments sur la grille afin d'en noter ceux qui ont un doublon
            }

            //NumberEntryCommand command = new NumberEntryCommand(newNum, caseSelectionne, caseVisuelle); //Créer une command d'entrée
            //commandInvoker.execute(command); //Demande à l'invoker d'executer la commande, ce qui permet de 'stacker' la command en cas d'un undo ou redo.


        }

        public void ColorEntry(Color newColor, AbstractInteractableCase caseVisuelle) {
            if (caseMemory.selectMultiple) {

                MultipleCommands.addCommand(new ColorEntryCommand(newColor, caseVisuelle));

                if (MultipleCommands.getCommandCount() == caseMemory.MultipleInterations.Count)
                    commandInvoker.execute(MultipleCommands);
            }
            else {
                ColorEntryCommand command = new ColorEntryCommand(newColor, caseVisuelle);
                commandInvoker.execute(command);
            }
        }

        public void CmModeOn() {
            foreach (VisualCase c in cases) {
                c.setOnCm();
            }
        }

        public void CmModeOff() {
            foreach (VisualCase c in cases) {
                c.setOffCm();
            }
        }

        public void Reset() {
            gameBoard.ResetData();
        }

        public void Undo() {
            commandInvoker.undo();
        }

        public void Redo() {
            commandInvoker.redo();
        }

        public void switchSelectMultiple() {
            caseMemory.switchSelectMultiple();
        }

        public void switchColorMode() {
            inColorMode = !inColorMode;
        }

        //On affiche le panel
        public void ColorPanelOn() {

            inColorMode = true;
            colorPanel.SetActive(true);

        }

        //On enlève le panel
        public void ColorPanelOff() {

            inColorMode = false;
            colorPanel.SetActive(false);

        }

        public void ChooseColor(Image colorButton) {
            PinceauCouleur.nouvelleCouleur = colorButton.color;

            var caseMemory = FindObjectOfType<CaseMemory>();

            if (caseMemory.selectMultiple) {
                foreach (var caseVisuelle in caseMemory.MultipleInterations) {
                    ColorEntry(PinceauCouleur.nouvelleCouleur, caseVisuelle);
                }
                caseMemory.removeLastMultipleInteractions();
            }
            else {
                if (caseMemory.lastInteration != null) {
                    ColorEntry(PinceauCouleur.nouvelleCouleur, caseMemory.lastInteration);
                    caseMemory.removeLastInteraction();
                }
            }
        }

        public void orderNewMultipleCommand() {
            MultipleCommands = new MultipleCommand();
        }
    }
}
