# Git im Terminal

Dies ist eine Einführung in GIT, wir werden alle Befehle über die Konsole ausführen.
Du benötigst unsere Docker Umgebung. 
[Git Man Pages](https://linux.die.net/man/1/git)
[Git Referenz](https://git-scm.com/docs)

Starte den Docker Container und erstelle ein neues Verzeichnis `gitprojects` sowie `my-c-app`.
Wechsele in `./gitprojects/my-c-app` und führe `git init` aus. Dieser Befehl initialisiert das aktuelle Verzeichnis als eine GIT Repository.

## Setzen des Benutzers
Jetzt setzen wir erstmal den Benutzer für das aktuelle Repository
`git config user.email "vorname.nachname@email.de"`
`git config user.name "Vorname Nachname"`

## Erstes Commit
Erstelle eine readme.md mit einer kurzen Beschreibung des Projektes. Zeige dir den Status deines Repos mit git status an.
Stage die Datei mit `git add .`  und zeige erneut den Status an. 
Erstelle ein erstes Commit mit `git commit -m "Initial Commit"`  und zeige erneut den Status an.
Welche Farben werden wofür verwendet?

## Log / Reflog
Git besitzt zwei möglichkeiten den Verlauf anzuzeigen. Die meisten verwenden den git log Befehl. Dieser zeigt die letzten Commits an.
Es gibt aber auch noch  git reflog es zeigt den Verlauf des Git Zeigers. Reflog kann sogar verwendet werden, um zerstörte git repositories wieder her zu stellen. Weil jede Bewegung des Zeigers hier referenziert wird.

## Branches
Ein Branch erstellt man mit `git switch -c feature/first-code` und wechselt direkt in diesen Branch.
Erstelle eine neue Datei `main.c` und füge den Code in diese Datei ein:
```c
#include <stdio.h>

int main(){
  printf("Hello World \n");
}
```

## .gitignore
Als nächstes erstellen wir eine Datei zum ignorieren von Dateien die nicht in das Repository eingecheckt werden sollen
Lege die Datei `.gitignore` an und befülle diese mit:
 
```gitignore
#C build files
*.s
*.o
myapp
```


## Kompilation
Als nächstes erstellen wir erstmal eine Zwischencode Datei.
`gcc -w -S main.c -o main.s`
Sowie eine ausführbare Datei mit GCC.
`gcc -w main.c -o myapp`
Wenn man jetzt `git status` aufruft, sollte nur die `main.c`  angezeigt werden.
Erstelle füge die Dateien zu dem Stage hinzu und erstelle einen weiteren Commit.

## Merging
Wechsele zu dem `master` Branch mit git switch master  und führe git merge feature/first-code aus. 
Dadurch sollte jetzt ein neuer Commit in dem Master Branch erscheinen. 

Zeige das Log an.
Zeige das Reflog an.

## Commit Details anzeigen
Rufe `git log` auf, um die letzten Commits zu zeigen. Am Anfang jeder Zeile steht ein Commit Hash. Dieser kann für weitere Aktionen verwendet werden wie zb. die Details des Commits.
Kopiere ein Hash und rufen diesen mit `git show 49889fd` auf. Du siehst die Änderungen dieses Commits.
Mit `git diff 49889fd` kann man die Differenz zu dem ausgewählten Commit anzeigen.

## Mergekonflikte
Stelle sicher das du auf dem `master` Branch bist und erweitere die `main.c` um folgenden Code:

```c
#include <stdio.h>

void print(int i){
        printf("Hallo Welt %d\n", i);
}

int main(){
        for(int i = 0; i < 23; i++){
                print(i);
        }
}
```

Erstelle einen weiteren Commit und wechsele zu dem Branch `feature/first-code` 
Erweitere dieselbe Datei um:

```c
#include <stdio.h>

int main(){
        for(int i = 0; i < 23; i++){
                print(i);
        }
}

void print(int i){
        printf(getHelloString(), i);
}

char* getHelloString(){
        return "Hallo Welt %d\n";
}
```

Erstelle einen Commit und wechsele zurück zu dem master Branch zurück.
Führe ein Merge des `feature/first-code` Branches in den master Branch aus.
Es sollte ein Konflikt gemeldet werden:
```c
Auto-merging main.c
CONFLICT (content): Merge conflict in main.c
Automatic merge failed; fix conflicts and then commit the result.
```
Lass dir den Status anzeigen. Du wirst sehen das es ein Konflikt in der Datei main.c aus.
Öffne die entsprechende Datei innerhalb von VIM. Du wirst erkennen das die Datei in folgende Bereiche eingeteilt ist:  <<<<<<< HEAD, ============  und >>>>>>> branch-name diese Marker zeigen die Bereiche, in denen sich der Code geändert hat. 

```c
#include <stdio.h>

void print(int i){
        printf("Hallo Welt %d\n", i);
}

int main(){
        for(int i = 0; i < 23; i++){
                print(i);
        }
}

<<<<<<< HEAD
=======
void print(int i){
        printf(getHelloString(), i);
}

char* getHelloString(){
        return "Hallo Welt %d\n";
}
>>>>>>> feature/first-code


Löse den Konflikt auf 

#include <stdio.h>

char* getHelloString(){
        return "Hallo Welt %d\n";
}

void print(int i){
        printf(getHelloString(), i);
}

int main(){
        for(int i = 0; i < 23; i++){
                print(i);
        }
}
```

speichere und verlasse die Datei. Kompiliere die Datei und führe das Programm aus. Es sollte noch immer funktionieren.
Erstelle einen neuen Commit und lass dir den GIT-Status anzeigen es sollte keine weiteren Konflikte geben. Somit hast du den Konflikt gelöst.

## Restore
Lösche die `main.c` mit `rm main.c`  und rufe `git status` auf. Was wird dir angezeigt? Stelle die Datei mit `git restore main.c` wieder her. Prüfe nach, ob die Datei wieder da ist. 

## Rebase
Oft gibt es den Usecase das man Commits aus einem Branch unter die bereits bestehenden Commits aus einem anderen Branch einreihen möchte. Dazu ist das `git rebase` gut. Wogegen `git merge` die Commits aus einem anderen Branch auf die bereits bestehenden Commits des aktuellen Branches anwendet.
In dieser Aufgabe werden wir Commits die im `master` nachträglich hinzugefügt wurden dem `feature/`  Branch vorschieben. Wir setzen also den `feature/` Branch auf den letzen Commit des master Branches

Aus diesem Baum soll:
```c

      A---B feature
     /
D---E---F master

Folgender Baum entstehen:
 
          A'--B' feature
         /
D---E---F master
```

Dazu wechseln wir in den Branch `feature/first-code` und erweitern die `main.c` um die Funktion `getIntroduction()` und erstellen ein Commit mit dieser Funktion.

```
#include <stdio.h>

int main(){
    print(getIntroduction());
    for(int i = 0; i < 23; i++){
        print(i);
    }
}

void print(int i){
    printf(getHelloString(), i);
}

char[] getHelloString(){
    return "Hallo Welt %d\n";
}

char[] getIntroduction(){
   return "Dies ist eine Testanwendung \n\r Die Ausgabe ist: \n\r";
}
```

Danach erstellen eine weitere Funktion und einen weiteren Commit.

```
#include <stdio.h>

int main(){
    print(getIntroduction());
    for(int i = 0; i < 23; i++){
        print(i);
    }
}

void print(int i){
    printf(getHelloString() + getName(), i);
}

char[] getHelloString(){
    return "Hallo Welt %d\n";
}

char[] getIntroduction(){
       return "Dies ist eine Testanwendung \n\r Die Ausgabe ist: \n\r";
}

char() getName(){
    return "Dmitrij.";
}
```

Jetzt wechseln wir zurück in den `master` Branch und erweitern die `readme.md` und erstellen einen Commit.


Jetzt haben wir zwei Commits auf dem feature-Branch und ein Commit auf dem master-Branch. Was wir jetzt erreichen wollen, ist das der Commit aus dem master-Branch vor den beiden Commits im feature-Branch erscheint. Dafür benötigen wir ein `git rebase master feature/first-code`. Dadurch wird der master unter den feature/first-code gelegt.
Dies sorgt für Merge conflicts die wir auflösen müssen.

Die Datei in Vim öffnen
```bash
#include <stdio.h>

char[] getHelloString(){
  return "Hallo Welt %d\n";
}

void print(int i){
    printf(getHelloString() + getName(), i);
}

int main(){
    print(getIntroduction());
    for(int i = 0; i < 23; i++){
        print(i);
    }
}

<<<<<<< HEAD
=======
void print(int i){
    printf(getHelloString(), i);
}

char[] getHelloString(){
  return "Hallo Welt %d\n";
}


char[] getIntroduction(){
       return "Dies ist eine Testanwendung \n\r Die Ausgabe ist: \n\r";
}
>>>>>>> 1620d0e (getIntroduction funktion hinzugefuegt)
```

und die Konflikte lösen.

```bash
#include <stdio.h>

char[] getHelloString(){
  return "Hallo Welt %d\n";
}

void print(int i){
    printf(getHelloString(), i);
}

char[] getIntroduction(){
       return "Dies ist eine Testanwendung \n\r Die Ausgabe ist: \n\r";
}

int main(){
    print(getIntroduction());
    for(int i = 0; i < 23; i++){
        print(i);
    }
}
```

Dann führt man ein `git add main.c` aus um die Konfliktlösung an Git zu melden und `git rebase --continue` um den Rebase weiterzuführen. Dies zeigt die originale Commit Message an, die kann man akzeptieren oder neu schreiben.

Jetzt müssen wir noch den Konflikt des zweiten Commits auflösen. 

Hier gehen wir wie gewohnt vor:

Dann führt man ein git add main.c aus, um die Konfliktlösung an Git zu melden und git rebase --continue um den Rebase weiterzuführen. Dies zeigt die originale Commit Message an, die kann man akzeptieren oder neu schreiben.

Wenn man jetzt das git log ausgibt, bekommt man die Commits in erwarteter Reihenfolge. Ein Merge hätte den "readme erweitert" Commit nach dem "getName() funktion hinzugefuegt" Commit gelegt.

Hier noch die Ausgabe aus dem reflog

