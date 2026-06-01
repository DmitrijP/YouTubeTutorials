## Funktionen

In `javascript` gibt es mehrere Arten der Funktionsdefinition.

#### Normale Funktionsdefinition

Video: https://youtu.be/kYSD8S2sCVM

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
