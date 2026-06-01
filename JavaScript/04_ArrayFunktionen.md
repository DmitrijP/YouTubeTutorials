## Kapitel 4: Array Functions

Video: https://youtu.be/MKBXHSHB8v8 

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
