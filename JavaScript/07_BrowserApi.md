## Browser APIs
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
