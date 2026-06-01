## Currying

Video: https://youtu.be/lvlZAYFvToY

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
