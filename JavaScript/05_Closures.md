## Closures
Youtube Video:
https://youtu.be/kYSD8S2sCVM?list=PLtFURTtAiZIlQ1o4bpxYIGq7yIdvChxMo&t=511

Closures ist eine Programmiertechnik die es uns im Funktionellen Kontext erlaubt variablen innerhalb von Funktionen zwischen zu speichern.

Dabei gibt die Funktion die ein Closure generiert immer eine neue Funktion zurück die die zwischenzuspeichernde Variable referenziert. In dem Fall `value` Meist wird eine weitere Variable außerhalb der zurückgegebenen Funktion gehalten (`a`).  Diese Variable ist der Speicher in dem die Funktion ihre Werte hält. 

Wenn `return function () {}` in `function incrementBy(value){}` aufgerufen wird, wird ein neuer Kontext erzeugt der die Variable `value` und `a` in einer Art Rucksack übergeben bekommt. Bei Aufruf der zurückgegebenen Funktion prüft JS erstmal ob die Variable in dem Kontext der Funktion vorzufinden ist, wenn nicht dann schaut es in den Rucksack den es bekommen hat und danach im globalen Kontext.

```js
function incrementBy(value){
	//Closure kontext
	let a = 0;
	return function() {
		return (a += value);
	}
	//Closure kontext
}
// incrementBy5 hällt im Rucksack den Wert von a und value
var incrementBy5 = incrementBy(5);
// incrementBy2 hällt im Rucksack den Wert von a und value
var incrementBy2 = incrementBy(2);
console.log(incrementBy5()); // Prints 5
console.log(incrementBy5()); // Prints 10
console.log(incrementBy5()); // Prints 15
//=====
console.log(incrementBy2()); // Prints 2
console.log(incrementBy2()); // ?rints 4
```

Ein weiteres Beispiel ist eine Art Singleton die es uns erlaubt eine Funktion ein einziges Mal auszuführen. Hierbei haben wir die closure `let executed = false;` die beim ersten Aufruf auf True gesetzt wird und danach das erneute Ausführen durch die if Abfrage verhindert.
```js
function executeOnceGenerator(){
	let executed = false;
	return function(){
		if(!executed){
			console.log("Executing...");
			executed = true;
		}
	}
}

var toExecuteOnce = executeOnceGenerator();
toExecuteOnce();
toExecuteOnce();
toExecuteOnce();
```
