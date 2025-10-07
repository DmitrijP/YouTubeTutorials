
## Einleitung
Dies soll eine Einleitung in JS Grundlagen sein.
Folgt der Tutorialserie: *JavaScript Deep Dive* 
https://www.youtube.com/playlist?list=PLtFURTtAiZIlQ1o4bpxYIGq7yIdvChxMo 

## Kapitel 0: Grundlagen

### Variablendeklaration:
```js
var theVar;
console.log("The variable is: " + theVar);
theVar = 1;
console.log("The variable is: " + theVar);
```

### Schleifen
```js
for(var i; i < 10; i++){ }
while(var i < 10){ i++; }

let tiere = ["Hund", "Katze", "Maus"];
for (let tier of tiere) {
  console.log(tier);
}

let person = { name: "Anna", alter: 25, stadt: "Berlin" };
for (let key in person) {
  console.log(key + ": " + person[key]);
}
```

### If
```js
if(i < 1){ } else { }
```

### Funktionen
```js
function f() {}
function f2(item){ console.log(item) }
function f3(item){ return item + 1 }
```

### Objekte 
Variablen werden mit `:` von der Zuweisung getrennt und mit `,` voneinander
```js
var obj = {
	myVar: 5,
	myName: 'dmitrij',
	myFunc: function () { 
		console.log(`The Var is: ${this.myVar} The name is ${this.myName}`) 
	}
}
```
### Scope
- `function scope`: Gültig innerhalb der Funktion in der es definiert wurde.
- `block scope`: Gültig innerhalb des `{}` Blocks in dem es definiert wurde.
- `lexical scope`: Gültig dort wo es bei der definition hin geschrieben wurde.

## Kapitel 1: Variablen Details

Es gibt drei Möglichkeiten Variablen zu initialisieren. Mit den Keywörtern: `var`, `let` und `const`.

### var
Diese Variablen sind innerhalb von Funktionen im `function scope` gültig. Während sie außerhalb von Funktionen `globally scoped` sind.  Sie können mehrfach deklariert werden.
```js
var theVar = 0;
console.log("The variable is: " + theVar);
theVar = 1;
var theVar = 1;
console.log("The variable is: " + theVar);
```

### const 
Konstanten können wenn diese mit  `primitives` belegt wurden nicht neu zugewiesen werden. Bei `objects` kann der Inhalt der Variable verändert werden. 
Sind im `block scope` gültig und können nicht mehrfach deklariert werden.
```js
const myConst = 0;
console.log("My Const variable is: ", myConst);
myConst = 0; //> Assignment to constant variable

const myConst = 1; //> Identifier 'a' has already been declared
console.log("My Const variable is: ", myConst);

const arr = []
arr.push(1); // valide
```

### let
Diese Variablen sind im `block scope` gültig und können nicht mehrfach deklariert werden.
```js
let myLet = 0;
console.log("My Let Variable is: ", myLet);
let myLet = 1; //> Identifier 'a' has already been declared
```
### FunctionScope VS BlockScope
 `var` ist function scoped, während es außerhalb von Funktionen `gloablly scoped`ist. 
 `let` und `const` sind aber block scoped. Das heißt mit `let` und `const` definierte Variablen sind nur innerhalb des aktuellen `{}`-blocks gültig.

```javascript
function f() {
	{
		var myBlockVar = 1
		let myBlockLet = 1
		console.log("myBlockVar: " + myBlockVar)
		//>myBlockVar: 1
		console.log("myBlockLet: " + myBlockLet)
		//>myBlockLet: 1
	}
	console.log("myBlockVar: " + myBlockVar)
	//>myBlockVar: 1
	console.log("myBlockLet: " + myBlockLet)
	//>Reference Error...
}
```

### Variable hoisting
`var` unterliegen dem sogenannten `hoisting`. Das heißt das diese an den Anfang des `scopes` gehoben wird. Man kann somit auf die Variable zugreifen bevor diese definiert wurde. Sie ist dann `undefined`
```js
function varHoisting() {
	console.log(myVar)
	//> undefined
	var myVar = "hello"
	console.log(myVar)
	//> hello
}
varHoisting();
```

Ein Zugriff auf eine mit `let / const` definierte Variable vor ihrer definition schlägt mit einem `Reference Error` fehl. Diese unterliegen auch dem `hoisting` landen aber in der `Temporal Dead Zone (TDZ)`.
```js
function letHoisting() {
	console.log(myLet)
	//> Reference error
	//die Variable existiert aber du darfst sie nicht verwenden
	let myLet = "hello"
	console.log(myLet)
	//> hello
}
letHoisting(); 
```


### Achtung bei Schleifen
Da var `function scoped`ist ist das `i` und das `j` auch vor sowie nach verlassen der Schleife gültig. 
```javascript
i = 0;
for(var i; i < 3; i++){ var j = i * 2; }
console.log(i);
//> 3
console.log(j);
//> 4
```

Bei `let / const` verhält es sich anders. Da es `block scoped` ist sind die Variablen außerhalb des `for{}`blocks nicht mehr gültig
```javascript
for(let l = 0; l < 3; l++){ let k = l * 2; const a = 3; }
console.log(l);
//> reference error
console.log(k);
//> reference error
console.log(a);
//> reference error
```

## Kapitel 2: Gleichheit in JavaScript

Es gibt zwei arten von Vergleichen in JavaScript, `=== (strict equality)` und `== (loose equality)`.

Dabei prüft `strict equality` auf den Typ und den Wert der Variablen. Es führt keine automatische Typconversion aus.
Dies ist bei `loose equality` anders. Es verwendet nur den Wert der Variablen. Deshalb führt es automatische Typconversion aus, bis es bei beiden Seiten des Vergleiches auf den gleichen Typ kommt oder runter auf ein primitive. 
Regel: `object` wird zu einem `primitive` konvertiert mit dem Aufruf von `valueof()`, wenn es dann ein `primitive` ist dann wird verglichen sonst ein `toString()` Aufruf. 
`boolean` und `string` wird zur `number` konvertiert. 

`string` wird zu einem `int` konvertiert mit `Number()`:
```js
'5' == 5      // true da Number('5') → 5 == 5
'5.5' == 5.5  // true da Number('5.5') → 5.5 == 5.5
'five' == 5   // false da Number('five') → NaN
```

`boolean` wird zu einem `int` konvertiert:
```js
true == 1     // true da Number(true) → 1 == 1
false == 0    // true Number(false) → 0 == 0
true == 2     // false Number(true) → 1 ≠ 2
```

Leere Strings werden zu einem `int` Konvertiert das ergibt den Default Wert von 0
```js
'' == 0       // true da Number('') → 0 == 0
' ' == 0      // true da Number(' ') → 0 == 0
```

Unterschiedliche Datentypen werden beide zu `int` konvertiert. Dabei wird auf dem Array zuerst `toString()` aufgerufen und dann ein konvertiert zu `int`
```js
[] == false     // true ([] → '' → 0, false → 0)
[0] == false    // true ([0] → '0' → 0, false → 0)
[null] == false // true ([null] → '' → 0)
```

Jetzt kommen wir zum meme:
![[Pasted image 20251001213919.png|500]]
Warum gibt uns JS ein false aus bei Vergleicht zwischen "0" und []?
```js
console.log(0 == "0"); 
// 0 == Number("0") -> 0 == 0
console.log(0 == []);
// [].valueOf() -> [] also [].toString() -> "" -> 0 == "" -> 0 == Number("") 
console.log("0" == []);
// "0" == [].toString() -> [] also [].toString() -> "" also "0" == ""
```

Arrays verwenden `toString()` dann `Number()`.
```js
[1] == 1
// [1].toString() → '1' → '1' == 1 → 1 == 1 → true

[] == 0
// [].toString() → '' → '' == 0 → 0 == 0 → true

[1,2] == '1,2'
// [1,2].toString() → '1,2' → '1,2' == '1,2' → true
```

Bei `objecs` wird zuerst `valueOf()` verwendet.
```js
const obj = {
  valueOf() { return 42; }
};

obj == 42
// valueOf() → 42 → 42 == 42 → true
```

Wenn `valueOf()` ein `object` zurück gibt dann wird stattdessen `toString()` verwendet:
```js
const obj = {
  valueOf() { return {}; },
  toString() { return '123'; }
};

obj == 123
// valueOf() → not a primitive
// toString() → '123' → '123' == 123 → 123 == 123 → true
```

## Kapitel 3: Funktionen

In `javascript` gibt es mehrere Arten der Funktionsdefinition.

#### Normale Funktionsdefinition

Wird sofort an den Anfang des `scopes` gehoisted und kann verwendet werden bevor sie definiert wurde:
```js
halloFunc(`dmitirj`)
//> hallo dmitrij
function halloFunc(name){
  console.log(`hallo ${name}`)
}
```

Die Funktion hat ein `this` - Identifier über den man auf weitere Variablen des aufrufenden Scopes zugreifen kann. 
Im Browser würde sowas funktionieren da `name` in den `global scope` kommt in `Node` oder `strict mode` würde es mit `undefined`abbrechen.
```js
var name = "Dmitrij"

function halloFunc(){
  console.log(`hallo ${this.name}`);
}
halloFunc() //> hallo Dmitrij
```

In Objekten übernimmt es den `objekt scope`
```js
var obj = {
	i: 0,
	myFunc: function () {
		console.log(`hallo ${this.i}`);
	}
}
obj.myFunc(); //> hallo 0
```

Somit kommen wir zu

#### Expression Functions 
Zuweisung einer anonym definierten Funktion zu einer variable. Wird an den Anfang des `scopes` gehoisted aber gibt bei Aufruf `undefined` zurück. Verhält sich entsprechend dem Art der Variablen.
```js
halloFunc('dmitrij') //> undefined
var/let/const halloFunc = function (name) {
  console.log(`hallo from halloFunc ${name}`)
}
halloFunc(`dmitirj`)
```

#### Arrow Functions
Zuweisung einer `lambda` definierten Funktion zu einer variable.  Hat kein eigenes `this.`.
```js
var/let/const halloFunc = (name) => {
  console.log(`hallo from halloFuncLambda ${name}`)
}
halloFunc(`dmitirj`)
```

Übernimmt den `lexical scope` des Blocks in dem es definiert wurde. Hier wurde es im global scope definiert somit ist `i` `undefined` da es das i im `global scope` nicht gibt. 
```js
var obj = {
	i: 0,
	myFunc: () => {
		console.log(`hallo ${this.i}`);
	}
}
obj.myFunc(); //> hallo undefinde
```

Hier wird das **i** aus dem `global scope` verwendet nicht das **i** des Objektes
```js
var i = 1 //(Browser: window.i = 1)
var obj = {
	i: 0,
	myFunc: () => {
		console.log(`hallo ${this.i}`);
	}
}
obj.myFunc(); //> hallo 1 (Browser: window.i = 1)
```
Wird bei komplexeren Funktionen und Callbacks sehr wichtig.

### Funktionen als Variablen

Funktionen können Variablen zugewiesen werden und an andere Funktionen übergeben werden.
Eine Funktion wird durch das Anhängen von `(parameter1, parameter2, ...)` ausgeführt. Sonst wird es wie eine normale Variable behandelt.

```js
var myFunc = (nameFormatter, name) => {
	console.log(nameFormatter(name))
}

var upperCase = (value) => {
	return `The name is ${value.toUppercase()}`;
}

var lowerCase = function (value) {
	return `The name is ${value.toLowercase()}`;
}

myFunc(upperCase, `Dmitrij`
//> The name is DMITRIJ
myFunc(lowerCase, `Dmitrij`)
//> The name is dmitrij
```


## Kapitel 4: Array Functions
In diesem Kapitel werden wir unsere eigenen Array Funktionen bauen.

### forEach()
Dabei fangen wir mit foreach an. Diese Funktion wird für alle weiteren Funktionen als Grundbaustein verwendet.
Diese Funktion soll ein Array entgegen nehmen und auf jedem Element des Arrays ein Callback aufrufen.
```js
//=====forEach=============================
var forEach = function(array, callback){
  for(let i = 0; i < array.length; i++){
    callback(array[i])
  }
}

var arr = [1,2,3,4]
forEach(arr, (item) => {
  console.log(`Logging: ${item}`)
})
```

### map()
Als nächstes implementieren wir die Map Funktion. Diese Funktion wird verwendet um ein Array an Objekten zu Transformieren. Sie erwartet ein Array und eine Callback Funktion. Dabei wird die Callback Funktion genau so wie bei ForEach auf jedes Element angewendet. Das Ergebnis der Funktion wird dann in ein neues Array geschrieben.  
```js
//=====map=============================
var map = function(array, callback){
  const res = []
  forEach(array, (item) => {
    res.push(callback(item))
  })
  return res;
}

var incrCallBack = (item) => { return ++item };
var arr = [1,2,3,4,5,6,7]
console.log(map(arr, incrCallBack))
```

### flat()
Die Flat Funktion wird genutzt um ein verschachteltes Array abzuflachen. Dabei erwartet die Funktion ein Array und eine Tiefe um die man das Array reduzieren soll. 
```js
//=====flat=============================
isIterable = function (item) {
	return !!item[Symbol.iterator];
};
var flat = function (array, depth = 1) {
	let res = [];
	function flatten(arr, dep) {
		//depth is already at 0 so we just push what ever is in the var
		if (dep < 0) {
			console.log(`Depth was zero, pushing`);
			res.push(arr);
			return;
		}
		//item is not iterable so we just push what ever is in the var
		if (!isIterable(arr)) {
			console.log(`Not Iterable, pushing`);
			res.push(arr);
			return;
		}
		// now we need to iterate over every item and pass it recursively to this function
		console.log(`Calling flatten, on`);
		forEach(arr, (item) => {
			console.log(item);
			flatten(item, dep - 1);
		});
	}
	
		flatten(array, depth);
		return res;
};
var toFlatten = [1, 2, [13, 3, 4], [[8, 7, 6, [45, 4564, 56756]], 847], 1231];
console.log(flat(toFlatten, 2));
```

### flatMap()
FlatMap vereint die Funktionsweise von Map und Flat. Dabei wird zuerst der Map Schritt angewendet und danach der Flat. Dies wird gemacht weil viele Map Schritte oft ein Array erzeugen und man ein Array von Arrays hätte aber ein einfaches Array benötigt. 
```js
//=====flatMap=============================
var flatMap = function (array, mapper){
	return flat(map(array, mapper))
}
var stringArray = ["Hallo", "Wie geht es dir?"];
var splitToChar = (item) => {
	let arr = [];
	forEach(item, (i) => {
		arr.push(i);
	});
	return arr;
};
console.table(flatMap(stringArray, splitToChar))
```

### reduce()
Reduce ist eine Funktion die ein Array auf einen einfachen Wert reduziert, in dem sie z.B.: alle Elemente aufeinander aufaddiert oder irgendwie anders vereint. 
```js
//=====reduce=============================
var reduce = function(array, reducer, initialValue){
    forEach(array, (item) => {
      console.log(`item: ${item} value: ${initialValue}`)
      initialValue = reducer(initialValue, item)
    })
  return initialValue
}
var sum = (initialValue, item) => {
  return initialValue + item
}
var arr = [12,3,433,234,234,23,4234,234];
console.log(reduce(arr, sum, 0))
```

### filter()
Eine Filter Funktion erstellt ein neues Array mit den gefilterten Elementen
```js
//=====filter=============================
var filter = function(array, filterFunc) {
  const res = []
  forEach(array, (item) => {
    if (filterFunc(item)){
      res.push(item)
    }
  })
  return res
}

var arr2 = [1,2,3,4,5,6,7,8,9,4,5,44,44,22,11,5,6645]
console.log(filter(arr2, (item) => {
  return (item % 2) == 0
}))
```

## Kapitel 5 Closures
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

## Kapitel 6: Currying
In diesem Kapitel sprechen wir über das Currying. Dies ist eine Programmiertechnik die es uns erlaubt einige Parameter einer Funktion vor zu belegen und somit eine Konfiguration vorzunehmen.
Man nennt es auch Teilausführung einer Funktion. Sie nutzt Closures um diese vor belegten Parameter zu speichern.

### Beispiel 1:
Wie man an dem Beispiel sieht ist es beim Currying so das man die Funktionen ineinander verschachtelt. Dabei wird die Variable `a` zu einer Closure. Das was an sie übergeben wird, bleibt im Kontext der zurückgegebenen inneren Funktion erhalten. Dadurch benötigt die Innere Funktion nur noch einen einzelnen Wert.
```js
function curriedAdd(a) {
  return function(b) {
    return a + b;
  };
}

console.log(curriedAdd(2)(3)); // 5
const add5 = curriedAdd(5);
console.log(add5(10)); // 15
console.log(add5(3));  // 8
```

### Beispiel 2:
Hier sieht man eine etwas produktivnäheres Beispiel. Man verwendet wiederum eine Closure um den `LogLevel` zwischen zu speichern und sich mehrere Funktionen zu generieren die einen anderen Prefix ausgeben. 
```js
function createLogger(level) {
  return function(message) {
    console.log(`[${level}] ${message}`);
  };
}

const error = createLogger("ERROR");
const info  = createLogger("INFO");

error("Datenbank nicht erreichbar!");
info("Server gestartet.");
```

### Beispiel 3:
Meist verwendet man das Currying im Zusammenhang mit konfigurierbaren Funktionen um wie im folgenden Beispiel den Request Stück für Stück aufzubauen.  
```js 
// Curry-style API request helper
function apiRequest(baseUrl) {
  return function(endpoint) {
    return function(options = {}) {
      return fetch(`${baseUrl}${endpoint}`, options)
        .then(res => res.json());
    };
  };
}

// Preconfigured clients
const githubApi = apiRequest("https://api.github.com");
const getUsers  = githubApi("/users");
const getRepos  = githubApi("/repositories");

// Usage
getUsers().then(console.log);
getRepos().then(console.log);

```
### Beispiel 4:
Wein weiteres weit verbreitetes Beispiel ist der CurrencyConverter der für unterscheidliche Currencies konfiguriert werden kann.
```js
// Curry-style currency formatter
function formatCurrency(locale) {
  return function(currency) {
    return function(amount) {
      return new Intl.NumberFormat(locale, {
        style: "currency",
        currency
      }).format(amount);
    };
  };
}

// Preconfigured formatters
const formatEUR = formatCurrency("de-DE")("EUR");
const formatUSD = formatCurrency("en-US")("USD");

// Usage
console.log(formatEUR(1000)); // "1.000,00 €"
console.log(formatUSD(1000)); // "$1,000.00"

```

### Beispiel 5:
Auch beliebt ist das Styling welches es erlaubt CSS einfacher zu stylen.
```js
function setStyle(property) {
  return function(value) {
    return { [property]: value };
  };
}

const setColor = setStyle("color");
console.log(setColor("red")); // { color: "red" }
```


## Kapitel 7: Browser APIs
Browser bieten Funktionen an die über Javascript aufgerufen werden können. Dies sind keine Funktionalitäten die Javascript von sich aus mit bringt. Es sind Teile des ausführenden Browsers (Chrome, Firefox, Opera) oder anderer Laufzeitumgebungen wie `Node.js`

### console.log(msg)
`console.log()` ist eine dieser Funktionen. Sie erwartet ein String oder Objekt mit einer ToString() Methode. Gibt diese dann auf der Console aus. Da die Console Teil des Browsers ist und Javascript keine Ahnung hat, was es ist wird dieser Aufruf an den Browser weitergeleitet. Der Browser gibt dann die Nachricht auf seiner Console aus.
```js
console.log("Hello, world!");
console.info("Info message");
console.warn("Warning message"); 
console.error("Error message");

console.assert(2 + 2 === 5, "Math is broken!"); 
const user = { name: "Alice", age: 25 };

console.dir(user);    // Shows an interactive object
console.table([       // Displays arrays/objects in table format
  { name: "Alice", age: 25 },
  { name: "Bob", age: 30 }
]);

console.count("Click");
console.count("Click");
console.countReset("Click");
console.count("Click");
```
### setTimeout(func, time)
`setTimeout(func, time)` ist eine dieser Funktionen. Es erwartet eine Funktion die vom Browser zu einem Späteren Zeitpunkt aufgerufen werden soll. Der Zeitpunkt wird in Millisekunden angegeben.

Wichtig dabei ist das die Zeit erst anfängt zu laufen, nach dem der gesamte synchrone Teil des Programs ausgeführt wurde.
```js
console.log('Start')
setTimeout(() => {
	console.log('hallo')
}, 0)
console.log('Ende')

//>Start
//>Ende
//>hallo
```


### document
Das `document` Objekt wird vom Browser zur Verfügung gestellt und bietet eine Vielzahl an Funktionen an die darauf ausgeführt werden können.  
```js
const title = document.getElementById("myTitle");
const firstButton = document.querySelector(".btn");
const allButtons = document.querySelectorAll(".btn");
```
### fetch()
Ist eine der modernen asynchronen Funktionen die uns ermöglich Netzwerkanfragen abzusenden. 

Asynchron:
```js
async function getData() {
  try {
    const response = await fetch("https://jsonplaceholder.typicode.com/posts/1");
    
    if (!response.ok) {
      throw new Error("Network response was not ok " + response.statusText);
    }

    const data = await response.json();
    console.log("Fetched data:", data);
  } catch (error) {
    console.error("Fetch error:", error);
  }
}

getData();
```
Synchron:
```js
fetch("https://jsonplaceholder.typicode.com/posts/1")
  .then(response => {
    if (!response.ok) {
      throw new Error("Network response was not ok " + response.statusText);
    }
    return response.json();
  })
  .then(data => {
    console.log("Fetched data:", data);
  })
  .catch(error => {
    console.error("There was a problem with the fetch operation:", error);
  });
```
### xhr()
Ist eine ältere synchrone Funktion die es uns ermöglich Netzwerkanfragen abzusenden. 
```js
const xhr = new XMLHttpRequest();
xhr.open("GET", "https://jsonplaceholder.typicode.com/posts/1", true);

xhr.onreadystatechange = function () {
  if (xhr.readyState === 4) { 
    if (xhr.status >= 200 && xhr.status < 300) {
      const data = JSON.parse(xhr.responseText);
      console.log("Fetched data:", data);
    } else {
      console.error("Request failed with status:", xhr.status);
    }
  }
};

xhr.onerror = function () {
  console.error("There was a network error.");
};

xhr.send();
```
