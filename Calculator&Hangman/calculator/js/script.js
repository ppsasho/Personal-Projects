let display = document.getElementById('display');
let clear = document.getElementById('clear');
let backSpc = document.getElementById('backSpace');
let divide = document.getElementById('divide');
let seven = document.getElementById('seven');
let eight = document.getElementById('eight');
let nine = document.getElementById('nine');
let multiply = document.getElementById('multiply');
let four = document.getElementById('four');
let five = document.getElementById('five');
let six = document.getElementById('six');
let subtract = document.getElementById('subtract');
let one = document.getElementById('one');
let two = document.getElementById('two');
let three = document.getElementById('three');
let sum = document.getElementById('sum');
let decimal = document.getElementById('decimal');
let zero = document.getElementById('zero');
let equals = document.getElementById('equals');


display.innerHTML = '';


let operators = [
    '/',
    'x',
    '-',
    '+' 
];

let firstOperand = '';
let secondOperand = '';
let operator = '';
let operatorCounter = 0;

function checkOperators(input) {
    operator = input.value;
    if (display.innerHTML === '' || display.innerHTML === '0') {
        console.log(0);
    }else {
        let checkArray = display.innerHTML.split('');

        if (operatorCounter > 0) {
            for (let i = 0; i < operators.length;i++) {
                for(let b = 0; b < checkArray.length; b++)
                if (checkArray[b] === operators[i]) {
                    checkArray[b] = operator;
                    display.innerHTML = '';
                    for(let j = 0; j < checkArray.length; j++) {
                        display.innerHTML += checkArray[j];
                    }
                }
            }
        }else {
            operatorCounter++;
            display.innerHTML += operator;
        }
    }
}


function returnBackSpace() {
    if (display.innerHTML.length > 0) {
        if (display.innerHTML === '0') {
            console.log(0);
        }else {
        let checkArray = display.innerHTML.split('');
        for (let i = 0; i < checkArray.length; i++) {
            if (checkArray[i] === 'x' || checkArray[i] === '/' || checkArray[i] === '-' || checkArray[i] === '+') {
                operator = '';
                operatorCounter = 0;
            }
        }
            let equationArray = display.innerHTML.split('');
            console.log(equationArray);
    
            equationArray.splice(-1, 1);            
    
            console.log(equationArray);
    
            let element = '';
            for (let i = 0; i < equationArray.length; i++) {
                element += equationArray[i];
            }
            if (element) {
                display.innerHTML = element;
            }else {
                display.innerHTML = '0';
            }
        }
    }else {
        console.log('No values entered!');
    }
}

function addDecimal() {
    if (display.innerHTML) {
        if (!display.innerHTML.includes('.')) {
            display.innerHTML += decimal.value;
        }
    }else{
        console.log('No numbers');
    }
}

function addNumber(number) {
    if (display.innerHTML === '0') {
        display.innerHTML = number.value;
    }else {
        display.innerHTML += number.value;
    }
}

function result() {
    if (operatorCounter > 0) {
        let resultArray = display.innerHTML.split('');

        let mark;
        for (let b = 0; b < operators.length; b++) {
            for (let i = 0; i < resultArray.length; i++) {
                if (resultArray[i] === operators[b]) {
                    firstOperand = parseFloat(firstOperand);
                    mark = i + 1;
                    break;
                } else {
                    firstOperand += resultArray[i];
                }
            }
            if (mark !== undefined) {
                break;
            }
        }

        for (let i = mark; i < resultArray.length; i++) {
            if (i === resultArray.length - 1) {
                secondOperand += resultArray[i];
                secondOperand = parseFloat(secondOperand);
                break;

            } else {
                secondOperand += resultArray[i];
            }
        } 

        let result;
        switch(operator) {
            case '/':
                result = firstOperand / secondOperand;
                Math.trunc(result);
                break;

            case 'x':
                result = firstOperand * secondOperand;
                break;

            case '-':
                result = firstOperand - secondOperand;
                break;
            
            case '+':
                result = firstOperand + secondOperand;
                break;

            default:
                console.log('If you see this you probably did black magic.... Or you\'re just reading the source code.');
                break;
        }
        console.log(result);
        display.innerHTML = `${result}`;
        firstOperand = '';
        secondOperand = '';
        operatorCounter = 0;
    }
}


        


one.addEventListener('click', function () {
    addNumber(this);    
})
two.addEventListener('click', function () {
    addNumber(this);
})
three.addEventListener('click', function () {
    addNumber(this);
})
four.addEventListener('click', function () {
    addNumber(this);
})
five.addEventListener('click', function () {
    addNumber(this);
})
six.addEventListener('click', function () {
    addNumber(this);
})
seven.addEventListener('click', function () {
    addNumber(this);
})
eight.addEventListener('click', function () {
    addNumber(this);
})
nine.addEventListener('click', function () {
    addNumber(this);
})
zero.addEventListener('click', function () {
    addNumber(this);
})



clear.addEventListener('click', function () {
    display.innerHTML = '0';
    operatorCounter = 0;
})

backSpc.addEventListener('click', function () {
    returnBackSpace();
})

divide.addEventListener('click', function() {
    checkOperators(this);
})

multiply.addEventListener('click', function() {
    checkOperators(this);
})

subtract.addEventListener('click', function() {
    checkOperators(this);
})

sum.addEventListener('click', function() {
    checkOperators(this);
})

decimal.addEventListener('click', function() {
    addDecimal();
})


equals.addEventListener('click', function() {
    result();
})
