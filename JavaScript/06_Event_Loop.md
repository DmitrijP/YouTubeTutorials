# JavaScript Event Loop - Warum dein Code nicht macht was du denkst!

Hallo und willkommen zu diesem Tutorial! Heute schauen wir uns eines der wichtigsten aber auch verwirrendsten Themen in JavaScript an: Den Event Loop.

Habt ihr euch schon mal gefragt warum euer JavaScript Code manchmal nicht in der Reihenfolge ausgeführt wird wie ihr es erwartet? Oder warum manche Funktionen "warten" müssen obwohl sie sofort ausgeführt werden sollten?

Das alles hat mit dem Event Loop zu tun - dem Herzstück von JavaScript's Asynchronität.

In diesem Tutorial werden wir Schritt für Schritt verstehen:
- Wie JavaScript wirklich Code ausführt
- Was der Call Stack macht und warum er wichtig ist
- Wie der Event Loop funktioniert
- Was Macrotasks und Microtasks sind
- Und warum das alles für euch als Entwickler wichtig ist
  

Am Ende werdet ihr genau verstehen können warum dieser Code:
```javascript
console.log("Start");
setTimeout(() => console.log("Timeout"), 0);
Promise.resolve().then(() => console.log("Promise"));
console.log("End");
```
nicht das ausgibt was man auf den ersten Blick erwarten würde.

Also, macht es euch gemütlich und lasst uns gemeinsam in die Welt des JavaScript Event Loops eintauchen!

Führt jetzt den Code aus mit `node ./code.js` oder in der Browser console.
<details>
	<summary>
		<h3>Antwort, zum aufklappen hier klicken</h3>
	</summary>
	<p>
	&gt;Start <br/>
	&gt;End<br/>
	&gt;Promise<br/>
	&gt;Timeout<br/>
	</p>
</details>

Warum ist das so? Das werden wir in diesem Tutorial behandeln.

# Der Stack

Javascript so wie viele andere Programmiersprachen verwenden einen Stack um die aktuell gültigen Variablen sowie Funktionen zu halten.
Der Stack wird nach dem Last In First Out Prinzip belegt und geleert. 

```javascript
let a = 7;
let b = 8;
function myFunc(){
	let c = a + b;
	console.log("a + b = " + c);
}
console.log("Hallo");

myFunc();
```

Die Variablen landen Eine nach der Anderen in dem Stack. 
Dann kommt die gesamte `myFunc` Funktion auf den Stack und als letztes `console.log("hallo")`.
Danach wird myFunc(); aufgerufen und es landet `c` auf dem Stack von `myFunc` Es wird `console.log` innerhalb der Funktion aufgerufen und gibt `a + b = 15`  aus.

Wichtig hierbei ist das die Funktionen bei der definition wie Variablen behandelt werden und genau so auf dem Stack landen.
Die Ausführung findet erst statt wenn sie aufgerufen werden.
All das geschieht sequentiell. Es gibt keine Parallelität!
# Der EventLoop und die Macrotask Queue

Der EventLoop und die Queue stellen die Asynchronität bereit. Dabei ist es wichtig zu verstehen das der EventLoop Teil der Runtime ist, also der Browser oder Node. Es ist nicht teil der JS Engine. Die JS Engine ist dafür zuständig Code sequentiell auszuführen und den Stack/Heap zu managen.

Was passiert in diesem Code Block?
```javascript
setTimeout(() => console.log("Timeout"), 0);
let a = 7;
let b = 8;
function myFunc(){
	let c = a + b;
	console.log("a + b = " + c);
}
console.log("Hallo");

myFunc();
```

Als erstes ruft Javascript die `setTimeout`Funktion auf. Dies ist eine Funktion der Runtime API.  In unserem Fall von Node.js. Dabei übergibt es die Funktion `() => console.log("Timeout")` und den Wert 0 an Node. Node startet den Timer.

Danach wird der restliche Code ausgeführt. Währenddessen lauft der Timer und der EventLoop prüft ob auf dem CallStack der JS Engine noch Elemente existieren.  Also der synchrone Code fertig ist. Sobald der Timer bei 0 ist. Legt die Runtime die Funktion `() => console.log("Timeout")` in die Macrotask Queue. Der EventLoop prüft weiterhin ob auf dem CallStack noch was ist. Wenn es leer ist, prüft es die Macrotask Queue, nimmt die Funktion `() => console.log("Timeout")` aus der Queue und legt diese in den Stack der JS Engine. Die JS Engine führt die Funktion aus.
Somit erreichen wir Asynchronität.

Wichtig. Alles geschieht sequentiell. Die Funktion `() => console.log("Timeout")` wartet auf das Ablaufen des Timers. Dann wartet sie auf das Abholen aus der Queue und das Ausführen durch die JS Engine.

# Die Microtask Queue

Das mit der Macrotask Queue ging lange Zeit gut aber irgendwann hat man eine weitere Queue gebraucht um kleinere Aufgaben planen zu können. Dadurch ist die `Microtask Queue` entstanden.

Was passiert in diesem Code Bloack?
```javascript
setTimeout(() => console.log("Timeout"), 0);
let a = 7;
let b = 8;
function myFunc(){
	let c = a + b;
	console.log("a + b = " + c);
}
Promise.resolve().then(() => console.log("Promise"));
myFunc();
```

Das gleiche was wir oben bereits besprochen haben, nur das diesmal `Promises` hinzugekommen sind. Diese legen deren Funktionen auf die `Microtask Queue` ab.
Was bedeutet das für unser Program?
Der Code wird weiterhin sequentiell ausgeführt. 
`setTimeout` Landet in der Runtime. Weiterer Code wird sequentiell ausgeführt.  
Der Timer den die Runtime gesetzt hat ist abgelaufen. Die Funktion landet in der `Macrotask Queue`. Irgendwann erreichen wir den Promise. Die Funktion des Promises landet auf der `Microtask Queue`. Irgendwann ist der Stack der JS Engine leer.  Der `EventLoop` merkt das, schaut zuerst in der `Microtask Queue`vorbei. Sieht die Funktion `() => console.log("Promise")` und legt sie in den Stack der JS Engine. Die JS Engine führt es aus.
Der EventLoop merkt wieder das der Stack leer ist. Schaut in der `Microtask Queue` vorbei, die ist Leer also schaut der EventLoop in der `Macrotask Queue`. Nimmt die Funktion `() => console.log("Timeout")`, legt sie in den Stack. JS Engine führt sie aus und wir sind fertig.

Somit ist wichtig, das Microtasks immer vor Macrotasks laufen.

