import { ExportedMethod, ExportedMethod2 } from '../JavaScript/objectOrientedJS'


function jsDemo(){
    // Versitile programming language, Used to make web page interactive
    // JS can be run on servers using Runtime enviroments like Node.js (comes with npm which is the world largest software registry)
}

jsDemo.prototype.Basics = function () {
    
    // var is Function scoped
    // let is block scoped
    // const is block scoped
    
    // Hoisting -> i can of var the defincation is moved to the top of the scope

    let a = [1,3,4,5];
    const b = 3;

    a.map(x => {
        // var c = 10; c is function-scoped, accessible throughout this function
        console.log(x);
    })

    // console.log('Var is function scoped :  ' + c);

    // Operators and Experssion
    // Arithmetic Operators  ->  + - / %
    // Logical Operators    -> || , &&
    // Comparator Operators -> != , == , ===
    // Assignment Operators -> =, += , -=

    // === checks if value and type both are same or not.

    let c1 = 3;
    let c2 = '3';

    if(c1 == c2){ // true

    }

    if(c1 === c2){ // false

    }
    
    // Loops and Interation

    a.forEach(x => {

    });

    for(var z = 0; z < a.length; z++){ 

    }
    
    a.map(x => { // Iterate over each element of the array

    });

    while(false){ // Runs till the condition fails

    }

    do{

    }while(false); // Runs atleast once


    // Functions

    function testFunc() {
        
    }
    
    // Arrow Functions
    let testFunc2 = (x) => x + 2;

    console.log('testFunc2 with input as 2 : ' + testFunc2(2));

    // Error Handling
    try{
        throw new Error("Test Error");
    }
    catch(ex){
        console.log("Error : " + ex);
    }

    // DOM Manipulation

    // Adding onclick
    document.getElementById("JSDemo").addEventListener("click", jsDemo.prototype.JSDemoClick);

    //Adding colour to text
    var element = document.getElementById("JSDemo");
    element.style.color = '#FFFFFF';
    setTimeout(() => {
        element.style.color = '#000000';
    }, 1000);

    
    // Event Propogation
    // JSDemoButton Button is present inside a div having a onclick of its own.
    // onClick both of them are getting triggered.
    document.getElementById('JSDemoButton').addEventListener('click', jsDemo.prototype.ButtonClickMethod);

    // Event Bubbling : Start from target and goes to the root level (upward direction)
    // Event Capturing : Staring from the root level down to that target (downward direction)

    const e = { a  : 2, b : 4};
    e.a = 3; // we can change the content inside a const variable

    // Template Literal
    console.log(`This is Template Literal ${e.a}`) // `` is used and ${}

    // Destructing
    const { g , h } = e;
    console.log('Values after destructing : ' + g + ' ' + h);

    // Spread and Rest Operators
    function mergeNumbers(...list){  // This is called Rest
        console.log(list); // [1,3,4]
        console.log(...list); // This is spread 1 3 4
    }

    mergeNumbers(1,3,4);
}

jsDemo.prototype.JSDemoClick = function(){
    console.log("DOM Manipulation Working");
}

jsDemo.prototype.ButtonClickMethod = function(e){
    console.log("Test (method");
    e.stopPropagation(); // Prevents from bubbling
}

jsDemo.prototype.TestMethod = function() {
    jsDemo.prototype.Basics();
}

ExportedMethod();
ExportedMethod2();