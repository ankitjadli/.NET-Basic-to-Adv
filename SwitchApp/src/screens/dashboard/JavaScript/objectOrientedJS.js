
// Functions

function Animal(x){
    this.x = x;
    this.animalName = x;
}

Animal.prototype.Speak = function(){
    console.log(this.x);
    console.log(this.animalName);
}   

const dog = new Animal('doggy');
dog.Speak();

// Classes

class Cars{

    constructor(name){
        this.carName = name;
    }

    TellCarName = function(){
        console.log(this.carName);
    }
}

const Audi = new Cars('Audi');
Audi.TellCarName();

// this keyword:
// 1. In case of global context, it refers to the window object
// 2. In case of object method, it refers to the object that called that method.
// 3. In case of constructor functions, it refers to the instance of the object created.

function BMSingleScreen(){

}

BMSingleScreen.prototype.CallBackFunc = function(func){ // Callback function here function is passed as an argument.
    func();
}

BMSingleScreen.prototype.CallBackFunc(() => { new Cars("Lambo").TellCarName()});

// Promises :
//In real-world applications, you often make requests to a server (like asking for data). 
//A promise ensures you don’t proceed with something until you know if the data has successfully arrived (resolved) or failed (rejected).
BMSingleScreen.prototype.PromiseTest = new Promise((resolve, revoke) => {
    var passed = false;

    passed ? resolve() : revoke();
});

BMSingleScreen.prototype.PromiseTest.then(() => console.log("Resolved")).catch(() => console.log("Revoked"));

// Async functions
BMSingleScreen.prototype.AsyncTest = async function(){
    await console.log("Test");
    const response = await fetch('https://jsonplaceholder.typicode.com/users/1');
    console.log(response);
}

BMSingleScreen.prototype.AsyncTest();

//The error Uncaught SyntaxError: Unexpected token 'export' usually happens because the export keyword is part of ES6 modules. 
//Browsers or environments that don’t support ES6 modules by default (or are not configured correctly) will throw this error.
//<script type="module" src="/src/screens/dashboard/objectOrientedJS.js"></script>
export const ExportedMethod = () => {
    console.log("ExportedMethod called")
}

export const ExportedMethod2 = () => {
    console.log("ExportedMethod2 called")
}


// Best Practices ->
// Separarte JS logic from HTML and CSS

// Js will handle the functionality
// HTML will provide the structore
// CSS to manage the styling

//Search inputs: If you have a search bar and you don’t want to send a request to the server every time the user types a letter, 
//you can debounce the input event
BMSingleScreen.prototype.DeBouncing = function(func, delay) {
    let timeout;  // This will hold the timer for the delay
    return function(...args) {  // Returns a function that will be debounced
        clearTimeout(timeout);  // Clears any previously set timer
        timeout = setTimeout(() => func.apply(this, args), delay);  // Sets a new timer for the function
    }
}

function searchQuery(query) {
    console.log('Searching for:', query);
}

const debouncedSearch = BMSingleScreen.prototype.DeBouncing(searchQuery, 1000);

// This simulates rapid typing; the function will only log "Searching for: Hello" after the user stops typing for 1 second.
debouncedSearch('H');
debouncedSearch('He');
debouncedSearch('Hel');
debouncedSearch('Hell');
debouncedSearch('Hello');

// Call 1: debouncedSearch('H')

// Timeout is cleared: (none yet)
// Set new timeout: Starts a timer for 1 second to call searchQuery('H').
// Call 2: debouncedSearch('He')

// Clear previous timeout: The timer from Call 1 is cleared.
// Set new timeout: Starts a new 1-second timer for searchQuery('He').
// Call 3: debouncedSearch('Hel')

// Clear previous timeout: The timer from Call 2 is cleared.
// Set new timeout: Starts a new 1-second timer for searchQuery('Hel').
// Call 4: debouncedSearch('Hell')

// Clear previous timeout: The timer from Call 3 is cleared.
// Set new timeout: Starts a new 1-second timer for searchQuery('Hell').
// Call 5: debouncedSearch('Hello')

// Clear previous timeout: The timer from Call 4 is cleared.
// Set new timeout: Starts a new 1-second timer for searchQuery('Hello').

BMSingleScreen.prototype.Throttling = function(func, cd){

    let cooldown;

    return function(...args){
        if(!cooldown){
            cooldown = true;
            func.apply(this,args);
            setTimeout(() => {
                cooldown = false
            }, cd);
        }
    }
}

// Uses:
// Handling Scroll Events:
 
// When a user scrolls a webpage, the scroll event can fire rapidly. 
//Throttling helps to limit how often you execute a function (like updating a UI element) based on the scroll position.

// Resizing Windows:
// Similar to scroll events, resizing a window can trigger many events quickly. Throttling can be used to improve performance by limiting how often the resizing logic runs.


// Lazy Loading 
// use of defer keyword in script tag
//Scripts with the defer attribute are downloaded in the background while the HTML document is being parsed. 
//This means that the browser doesn't have to stop parsing HTML to download the script.