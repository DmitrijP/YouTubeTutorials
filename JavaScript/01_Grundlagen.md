## Grundlagen

Video: https://youtu.be/X6kGSpmeX7A

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

## Variablen

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
