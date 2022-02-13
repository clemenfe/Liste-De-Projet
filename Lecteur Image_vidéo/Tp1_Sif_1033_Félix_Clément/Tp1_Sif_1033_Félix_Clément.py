import cv2
from tkinter import * #Doit être installé lors de l'installation de Python
from tkinter import filedialog
from tkinter import messagebox
import matplotlib.pyplot #Pour lire l'image (voir https://stackoverflow.com/questions/68716321/how-to-use-absolute-path-in-cv2-imread)
import os
import numpy

tresholdVal = 1 #Ceci empêche une erreur de valeur non initialisé si on n'a pas touché au Slider avant d'activer le treshold

"""
Entrée : Aucune
Rôle   : Sauvegarder une image sur le disque
Sortie : Aucune
"""
def SaveImg() :
       
    #Si currentImg[0] est None, l'image ne peut pas être enregistré. currentImg[1] correspond au "type" d'image (ex. : les contours)
    if currentImg[0] is None : 
        messagebox.showerror("Erreur", f"Il est malheureusement impossible d'enregistrer une image {currentImg[1]}") #Affichage d'un message d'erreur
        return None #Pour sortir de la fonction


    ext = (["jpeg", "*.jpeg*"], ["png", "*.png*"]) #Les extensions de fichiers

    #On affiche un dialog permettant d'enregistrer une image (sous format jpeg par défaut)
    fileDialog = filedialog.asksaveasfile(filetypes = ext, defaultextension = ext)
    
    #Si l'utilisateur ferme la fenêtre, fileDialog sera None...
    if fileDialog is not None :

        #On essaie de sauvegarder
        try :
            
            #cv2.imwrite(fileDialog.name, currentImg) #On enregistre l'image #Pour une raison obscure, le fichier sauvegarder par imwrite fait 0 bytes sur le disque et n'est donc pas lisible
            
            #Car matplotlib sauvegarde sous RGB et non GBR...
            currentImg2 = cv2.cvtColor(currentImg, cv2.COLOR_BGR2RGB) #si on met currentImg = cv2..., le programme plante. Il faut que la varriable passé en paramêtre soit différente de celle qui est affecté
           
            matplotlib.pyplot.get(currentImg2) #On importe l'image dans pyplot
            matplotlib.pyplot.imshow(currentImg2) #Sans ceci, l'image reste blanche
            
            matplotlib.pyplot.axis('off') #On supprime les axes de matplotlib présente dans l'image
           
            shape = currentImg2.shape
            
            #Le dpi a été mis à 500 pour conserver une bonne résolution d'image
            matplotlib.pyplot.savefig(fileDialog.name, dpi = 500, transparent = True, bbox_inches='tight') #Pour enregister l'image
            messagebox.showinfo("Sauvegarde", "L'image a été enregistré avec succès!") #Affichage d'un message de confirmation
               
        #En cas d'erreur    
        except : 
            
            messagebox.showerror("ERREUR", "Type de fichier invalide!\nL'image n'a pas pu être sauvegardé")

        fileDialog.close() #Pour fermer la fenêtre


#Fonction qui utilise une fonction lambda pour appeler une autre fonction
#Cette façon de faire empêche que les méthodes associer au clic des bouttons s'exécute avant le clic
def FuncCall(funct, window) :
    return lambda x : funct(window)

#En utilisant une variable global, on peut obtenir la valeur actuelle. (En y allant par return, la valeur restait à 0...)
#Pour appliquer aux variables la nouvelle valeur du Treshold
def ApplyTresholdValue(self) :
    global tresholdVal
    tresholdVal = self
    CV2_IMGModif()

#Permet d'exécuter la fonction CV2_IMGModif à l'aide des sliders de couleurs.
def CV2_IMGModif2(self) :
    CV2_IMGModif()
        



"""
Entrée : Aucune
Rôle   : Permet l'affichage de l'image selon le mode de couleur désiré. Cette fonction permet de pouvoir exécuter les modifications sur l'image en temps réel.
Sortie : Aucune
"""
def CV2_IMGModif() :

    img_color = img #On évite de modifier la variable représentant l'image
    global currentImg #Le terme global va permettre d'accéder à la varriable dans la fonction SaveImg()
    
    #Si les contours ne sont pas actifs et le treshold non plus.
    if (not (btn_TresholdActive.get() or btn_contourValue.get())) :
        if (colorModeValue.get() == 0) : 
            img_color = cv2.cvtColor(img, cv2.COLOR_RGB2BGR) #(On convertit l'image qui est en RGB sous format BGR pour pouvoir avoir les bonnes couleurs)

        elif (colorModeValue.get() == 1) :
            img_color = img #matplotlib lit une image sous frome RGB. Il n'y a donc pas de conversion à faire.

        elif (colorModeValue.get() == 2) : 
            img_color = cv2.cvtColor(img, cv2.COLOR_RGB2HSV) #On convertit l'image qui est en RGB sous format HSV

        elif (colorModeValue.get() == 3) : 
            img_color = cv2.cvtColor(img, cv2.COLOR_RGB2GRAY) #On convertit l'image qui est en RGB en noir et blanc

        cv2.namedWindow("Mon Image", cv2.WINDOW_NORMAL) #ON crée une fenêtre avec l'image
        cv2.imshow("Mon Image", img_color) #On affiche l'image
        # cv2.waitKey(0) #On attend jusqu'à ce qu'on appuie sur une touche du clavier ou le X
        
        currentImg = img_color #On affecte la varriable de la valeur de l'image affiché présentement à l'écran.

    #Si on veut voir du Treshold ou du contour. (1 des deux boutons est coché.)
    else :

        img_color = cv2.cvtColor(img, cv2.COLOR_RGB2GRAY)

        #Cv2.treshold retourne 2 valeurs. Celle qui nous intéresse est la seconde
        ret, thresholdImg = cv2.threshold(img_color, float(tresholdVal), 255, cv2.THRESH_BINARY)
        
        if not btn_contourValue.get() :
            cv2.namedWindow("Mon Image", cv2.WINDOW_NORMAL)
            cv2.imshow("Mon Image", thresholdImg) #On affiche l'image
            currentImg = thresholdImg #On affecte la varriable de la valeur de l'image affiché présentement à l'écran.
        
        else :
            
            #On essait de trouver les contours (ne fonctionne pas sur les PNG)
            try :
                #On trouve les contours (Si on ne met pas hierarchy, contours est considéré comme un tuple...)
                contours, hierarchy = cv2.findContours(thresholdImg, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)
                

            #En cas d'erreur
            except :
                messagebox.showerror("ERROR", "Il est malheureusement impossible\nd'afficher le contour pour ce type d'image.")
                return None #On sort de la fonction

            #Création d'une image vide
            img_contours = numpy.zeros(img.shape)

            #On dessine les contours. -1 signifie de dessiner tous les contours.
            cv2.drawContours(img_contours, contours, -1, (rgb_B.get(), rgb_G.get(), rgb_R.get()), 3)

            #On affiche les contours
            cv2.imshow("Mon Image", img_contours) #On affiche l'image

            #Donne une erreur lors de la sauvegarde, donc on ne sauvegardera pas le contour
            #currentImg = img_contours #On affecte la varriable de la valeur de l'image affiché présentement à l'écran.
            currentImg = (None, "contour") #Pour empêcher la sauvegarde et expliquer pourquoi. (Voir la fonction pour la sauvegarde)

            #print("Shape of the loaded image is", img.shape)  #Pour avoir la taille de l'image

        
     
        
    


            
"""
Entrée : Nom de la fenêtre
Rôle   : Permettre à l'utilisateur d'effectuer des modifications sur l'image/vidéo
Sortie : Aucune
"""
def ModifWindow(windowName, allOptions = 'On') :

    windowModif = Toplevel() #Création d'une nouvelle fenêtre
   
    #Varriable qui va permettre de choisir le mode de couleur. Global pour pouvoir y accéder dans d'autres fonctions
    global colorModeValue
    colorModeValue = IntVar() 

    if allOptions.capitalize() == 'On' :

        #On affiche du texte dans la fenêtre
        winLabel = Label(windowModif, text="Treshold : ", fg='black', font=("Calibri", 16))
        winLabel.place(x = 180, y = 305)

        #On affiche un slider permettant de choisir une valeur pour le treshold
        tresholdSlide = Scale(windowModif, from_=1, to=255, orient=HORIZONTAL, command = ApplyTresholdValue)
        tresholdSlide.place(x = 280, y = 292.5)
    
        #Varriable qui vont contenir les valeurs des couleurs du contour
        global rgb_R, rgb_G, rgb_B
        rgb_R = IntVar()
        rgb_G = IntVar()
        rgb_B = IntVar()

        #Slider qui permettent de changer les valeurs des contours (Positionner sous format RGB, car il est plus populaire que GBR...)
        Scale(windowModif, from_=0, to=255, variable = rgb_R, command = CV2_IMGModif2).place(x = 325, y = 155)
        Scale(windowModif, from_=0, to=255, variable = rgb_G, command = CV2_IMGModif2).place(x = 375, y = 155)
        Scale(windowModif, from_=0, to=255, variable = rgb_B, command = CV2_IMGModif2).place(x = 425, y = 155)

        #Texte pour les sliders RGB
        Label(windowModif, text="R", fg='black', font=("Calibri", 12)).place( x = 350, y = 125)
        Label(windowModif, text="G", fg='black', font=("Calibri", 12)).place( x = 400, y = 125)
        Label(windowModif, text="B", fg='black', font=("Calibri", 12)).place( x = 450, y = 125)

    
        #Variables qui vont contenir la valeur des checkbutton
        global btn_contourValue, btn_TresholdActive
        btn_contourValue = IntVar()
        btn_TresholdActive = IntVar()

        #Des Check button qui vont permettre de voir si on veut du treshold où les contours...
        Checkbutton(windowModif, text="Contour", variable = btn_contourValue, font=("Calibri", 16), command = CV2_IMGModif).place(x = 180, y = 245)
        Checkbutton(windowModif, text="Treshold", variable = btn_TresholdActive, font=("Calibri", 16), command = CV2_IMGModif).place(x = 180, y = 275)

        #Des Radio bouttons permettants de choisir le mode d'image
        Radiobutton(windowModif, text='BGR', variable = colorModeValue, value = 0, font=("Calibri", 16), command = CV2_IMGModif).place(x = 180, y = 125)
        Radiobutton(windowModif, text='RGB', variable = colorModeValue, value = 1, font=("Calibri", 16), command = CV2_IMGModif).place(x = 180, y = 155)
        Radiobutton(windowModif, text='HSV', variable = colorModeValue, value = 2, font=("Calibri", 16), command = CV2_IMGModif).place(x = 180, y = 185)
        Radiobutton(windowModif, text='GREY', variable = colorModeValue, value = 3, font=("Calibri", 16), command = CV2_IMGModif).place(x = 180, y = 215)

        #Bouttons
        Button(windowModif, text = "Menu", font=("Calibri", 16), command = windowModif.destroy).place(x = 200, y = 400)
        Button(windowModif, text = "Save", font=("Calibri", 16), command = SaveImg).place(x = 350, y = 400)

    elif allOptions.capitalize() == 'Off' :

        #Des Radio bouttons permettants de choisir le mode d'image (Le paramêtre command à été enlevé par rapport à allOptions = "on"
        Radiobutton(windowModif, text='BGR', variable = colorModeValue, value = 0, font=("Calibri", 16)).place(x = 180, y = 125)
        Radiobutton(windowModif, text='RGB', variable = colorModeValue, value = 1, font=("Calibri", 16)).place(x = 180, y = 155)
        Radiobutton(windowModif, text='HSV', variable = colorModeValue, value = 2, font=("Calibri", 16)).place(x = 180, y = 185)
        Radiobutton(windowModif, text='GREY', variable = colorModeValue, value = 3, font=("Calibri", 16)).place(x = 180, y = 215)
        #On met un boutton pour passer à la prochaine étape après cette fenêtre
        Button(windowModif, text = "Next", font=("Calibri", 16), command = windowModif.destroy).place(x = 250, y = 400)   

    windowModif.title(windowName) #Nom de la fenêtre
    windowModif.geometry("600x500+10+20") #Taille de la fenêtre et la position initiale
    
    windowModif.wait_window() #Permet à la fenêtre de rester ouverte et d'exécuter les commandes des boutons
    

"""
Entrée : Aucune
Rôle   : Lire une image sur Disque et l'ouvrir avec la librairie CV2
Sortie : Aucune
"""
def ReadIMG(window) :

    #On masque la fenêtre du menu
    window.withdraw()

    #On permet à l'utilisateur de chercher une image sur son disque
    path = os.path.normpath(filedialog.askopenfilename(initialdir = "/", title = "Veuillez sélectionner un fichier", 
                                  filetypes = (("Image", "*.jpg*"), #Il existe d'autres format d'image que cv2 peut ouvrir, mais on peut les sélectionner en choisissant tous les types de fichier dans la fenêtre
                                               ("Image", "*.jpeg*"),
                                               ("Image", "*.bmp*"),
                                               ("all files", "*.*"))))
    
    #Si l'utilisateur clique sur x au lieu de choisir un fichier (On évite un message d'erreur disant que le système n'a pas pu lire l'image
    if (path == '.') :
        window.deiconify() #Pour faire afficher la fenêtre du menu
        return None #On retourne au Menu


    #CV2 utilise le format GBR tandis que matplotlip utile RGB

    #On essaie de lire l'image
    try :
        global img
        #img = cv2.imread(path, cv2.IMREAD_COLOR) L'image ne peut pas être chargé si la path est trop longue
        img = matplotlib.pyplot.imread(path) #Permet de lire l'image (Lit sous format RGB)
        

    #Si l'image ne peut pas être lu
    except :
        #print("The system was unable to read the file.")
        messagebox.showerror("ERROR", "The system was unable to read the image")
        #On affiche à nouveau la fenêtre du menu
        window.deiconify() #On affiche à nouveau la fenêtre du menu
        return None #On sort de la fonction

    imgGBR = cv2.cvtColor(img, cv2.COLOR_RGB2BGR) #(On convertit l'image qui est en RGB sous format BGR pour pouvoir avoir les bonnes couleurs)
    
    cv2.namedWindow("Mon Image", cv2.WINDOW_NORMAL) #ON crée une fenêtre avec l'image
    cv2.imshow("Mon Image", imgGBR) #On affiche l'image

    #J'affiche une fenêtre permmetant d'effectuer des modifications à une image
    ModifWindow("Modification d'image")
    
    cv2.destroyAllWindows() #On ferme les fenêtres de CV2

    #On affiche à nouveau la fenêtre du menu
    window.deiconify()




"""
Entrée : Aucune
Rôle   : Lire une vidéo sur Disque et l'ouvrir avec la librairie CV2
Sortie : Aucune
"""
def ReadVideo(window) :

    #On masque la fenêtre du menu
    window.withdraw()

    #On demande si l'utilisateur veut utiliser la webcam
    if(messagebox.askyesno("Question", "Voulez-vous utiliser votre webcam?")) :
        path = 0 #cv2.VideoCapture(0) fait la webcam

    #L'utilisateur parcours les fichiers de son ordinateur pour trouver la vidéo désiré.
    else :
        path = os.path.normpath(filedialog.askopenfilename(initialdir = "/", title = "Veuillez sélectionner un fichier", 
                                  filetypes = (("Video", "*.mp4*"),
                                               ("Video", "*.webm*"), #Il existe d'autres format d'image que cv2 peut ouvrir, mais on peut les sélectionner en choisissant tous les types de fichier dans la fenêtre
                                               ("Video", "*.mov*"),
                                               ("Video", "*.avi*"),
                                               ("all files", "*.*"))))
    
    
    #On essaie
    try :
        video = cv2.VideoCapture(path) #On référence la vidéo à la path de celle-ci

    #Si on est incapable de lire le vidéo
    except : 
        messagebox.showerror("ERROR", "The system was unable to read the viedo") #Message d'erreur
        window.deiconify() #Pour faire afficher la fenêtre du menu
        return None #On sort de la fonction

    ModifWindow("Modification Vidéo", allOptions = 'off')


    while(video.isOpened() or path == 0):
        ret, frame = video.read()
        
        if (colorModeValue.get() == 0) :
            colorMod = frame #Il n'y a pas de conversion à faire, car l'image est déjà en GBR
        
        elif (colorModeValue.get() == 1) :
            colorMod = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB) #On convertit l'image de la vidéo qui est en BGR sous format RGB

        elif (colorModeValue.get() == 2) : 
            colorMod = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV) #On convertit l'image de la vidéo qui est en BGR sous format HSV

        elif (colorModeValue.get() == 3) : 
            colorMod = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)#On convertit l'image de la vidéo qui est en BGR en noir et blanc

        #POUR AFFICHER LA VIDÉO
        if ret :
            cv2.imshow('Vidéo', colorMod)
            #windowVideo = cv2.imshow('Vidéo', frame)

        #Si l'utilisateur appuie sur q ou Esc, ça va quitter la vidéo
        keyPress = cv2.waitKey(1)
        if keyPress == 27 or keyPress == ord('q') or keyPress == ord('Q') :
            break
       
        #On essaie de lire la propriété de la fenêtre. (Lorsqu'on pèse sur le X pour fermer la fenêtre, le programme envoie une erreur pour dire qu'il ne trouve pas la fenêtre)
        try :
            cv2.getWindowProperty('Vidéo', 1)
        
        #En faisant le break, on peut ainsi fermer la fenêtre en utilisant le bouton x
        except :
            break;


    video.release() #On libère la vidéo
    cv2.destroyAllWindows() #On détruit les fenêtres de CV2

    #On affiche à nouveau la fenêtre du menu
    window.deiconify()



window = Tk() #On crée une nouvelle fenêtre tkinter

#On affiche du texte dans la fenêtre
winLabel = Label(window, text="Veuillez sélectionner le type\nde fichier à ouvrir", fg='black', font=("Calibri", 16))
winLabel.place(x = 180, y = 125)

#On crée un bouton pour la sélection d'une image
winBtnImage = Button(window, text="Image", fg='black', bg = "lightblue", font=("Calibri", 16))
winBtnImage.place(x = 100, y = 300)
winBtnImage.bind('<Button-1>', FuncCall(ReadIMG, window)) #On appel la fonction ReadIMG() lorsqu'on fait un click gauche

#On crée un bouton pour la sélection d'une vidéo
winBtnVideo = Button(window, text="Video", fg='black', bg = "lightblue", font=("Calibri", 16))
winBtnVideo.place(x = 400, y = 300)
winBtnVideo.bind('<Button-1>', FuncCall(ReadVideo, window)) #On appel la fonction ReadVideo() lorsqu'on fait un click gauche

window.title("Projet de session Phase 1") #Nom de la fenêtre
window.geometry("600x500+10+20") #Taille de la fenêtre et la position initiale

window.mainloop() #Permet à la fenêtre de rester ouverte et d'exécuter les commandes des boutons

