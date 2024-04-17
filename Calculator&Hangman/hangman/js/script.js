let hintBtn = document.getElementById('hint');
let playAgainBtn = document.getElementById('playAgain');
let categoryContent = document.getElementById('chosenCategory');
let wordField = document.getElementById('chosenWord');
let sketch = document.getElementById('sketch');
let currentLives = document.getElementById('lives');
let clue = document.getElementById('clue');

let wordArray = [];
let revealedWord = '';
let hiddenWordArray = [];

let words = [
    {
        category: 'Time',
        word: 'afternoon',

        clue: function() {
            let result = 'The time between morning and evening, often associated with a relaxing break.';
            return result;
        }
    },
    {
        category: 'Feeling',
        word: 'awkward',

        clue: function() {
            let result = 'Socially _ __ _____ ____.';
            return result;
        }
    },
    {
        category: 'Brain Teasers',
        word: 'puzzling',

        clue: function() {
            let result = 'Confusing or challenging.';
            return result;
        }
    },
    {
        category: 'Fishing Gear',
        word: 'fishhook',

        clue: function() {
            let result = 'A curved and pointed device used for catching fish.';
            return result;
        }
    },
    {
        category: 'Texture',
        word: 'fluffiness',

        clue: function() {
            let result = 'The quality of being soft, light, and airy.';
            return result;
        }
    },
    {
        category: 'Sounds',
        word: 'squawk',

        clue: function() {
            let result = 'A loud, harsh cry or noise, often associated with birds or other animals.';
            return result;
        }
    },
    {
        category: 'Size',
        word: 'jumbo',

        clue: function() {
            let result = 'Exceptionally large or oversized, often used to describe something of considerable size.';
            return result;
        }
    },
    {
        category: 'Beverages',
        word: 'schnapps',

        clue: function() {
            let result = 'A strong alcoholic drink, typically flavored and often served as a shot or in cocktails.';
            return result;
        }
    },
    {
        category: 'Motion',
        word: 'zigzagging',

        clue: function() {
            let result = 'Moving in a pattern characterized by sharp turns or angles.';
            return result;
        }
    },
    {
        category: 'Biblical Events',
        word: 'exodus',

        clue: function() {
            let result = 'The mass departure or journey of a group of people, especially the Israelites, as described in the Bible.';
            return result;
        }
    },
    {
        category: 'Hobbies',
        word: 'bookworm',

        clue: function() {
            let result = 'You get lost in the pages of most books.';
            return result;
        }
    },
    {
        category: 'Fashion',
        word: 'snazzy',

        clue: function() {
            let result = 'Stylish and elegant in a lively, eye-catching way.';
            return result;
        }
    },
    {
        category: 'Architecture',
        word: 'stronghold',

        clue: function() {
            let result = 'A heavily fortified place.';
            return result;
        }
    },
    {
        category: 'Food',
        word: 'flapjack',

        clue: function() {
            let result = 'A sweet and delicous treat made from rolled oats.';
            return result;
        }
    },
    {
        category: 'Mythology',
        word: 'sphinx',

        clue: function() {
            let result = 'It has the body of a lion and a head of a human/animal, known for posing riddles..';
            return result;
        }
    },
    {
        category: 'Games',
        word: 'boggle',

        clue: function() {
            let result = 'A game where players search for words hidden in a grid of letters, competing to find the most words within a time limit.';
            return result;
        }
    },
    {
        category: 'Transportation',
        word: 'rickshaw',

        clue: function() {
            let result = 'A two-wheeled vehicle, often pulled by a poerson on foot.';
            return result;
        }
    },
    {
        category: 'Appliance',
        word: 'microwave',

        clue: function() {
            let result = 'I forgot to make my popcorn....';
            return result;
        }
    },
    {
        category: 'Self-Reflection',
        word: 'unworthy',

        clue: function() {
            let result = 'A feeling of not deserving.';
            return result;
        }
    }
]
console.log(`Objects: ${words.length}`);

function randomNumber (){
    let result = Math.floor(Math.random() * ((1 + 20 - 1)) + 1) - 1;
    if(result > 0 || result <= 19) {
        return result;
    }
}
console.log(`Number generated: ${randomNumber()}`);


let chosenWord = words[randomNumber()];
console.log(chosenWord);


function categoryName() {
    let category = Object.keys(chosenWord);

    let categoryKey = category[0];
    let categoryValue = chosenWord[categoryKey];

    console.log(`The chosen category is: ${categoryValue}`);

    return categoryValue;
}

function getWordArray(word) {
    for(let i = 0; i < word.length; i++) {
        wordArray.push(word[i]);
    }
    console.log(wordArray);
}

function guessWord() {
    let word = Object.keys(chosenWord);

    let wordKey = word[1];
    let wordValue = chosenWord[wordKey];
    console.log(`The chosen word is: ${wordValue}`);

   let hiddenWord = '';

    for(let i = 0; i < wordValue.length; i++) {
        hiddenWordArray.push('_');
        hiddenWord += `${hiddenWordArray[i]} `;
    }

    console.log(hiddenWordArray);


    getWordArray(wordValue);

    return hiddenWord;
}

function revealLetter(revealIteration, letter) {
    for(let i = 0; i < wordArray.length; i++) {
        if (i === revealIteration) {
                hiddenWordArray[i] = letter;
        }
    }

    let revealedWord = '';
    for (let i = 0; i < hiddenWordArray.length; i++) {
        revealedWord += `${hiddenWordArray[i]} `;
    }
    wordField.innerText = `Word: ${revealedWord}`;
    console.log(revealedWord);

    let dashCounter = 0;
    for(let i = 0; i < wordArray.length; i++) {
        if (hiddenWordArray[i] === '_') {
            dashCounter++;
        }
    }
    if (dashCounter === 0) {
        console.log('You won..cheater');
    }
}

function checkWordLetters(letter) {
    if (lives > 0) {
        let counter = 0;
        for(let i = 0; i < wordArray.length; i++) {
            if (letter.value === wordArray[i]) {
                console.log(`The letter ${letter.value} was found in the word!`);
                revealLetter(i, letter.value);
                counter++;
            }
        }
        if (counter === 0) {
            lives--;
            currentLives.innerHTML = `Lives:&nbsp;${lives}`;
            switch(lives) {
                case 5:
                    sketch.innerHTML = '<img src="img/hangman5.png" alt="hangman">';
                    break;
                case 4:
                    sketch.innerHTML = '<img src="img/hangman4.png" alt="hangman">';
                    break;
                case 3:
                    sketch.innerHTML = '<img src="img/hangman3.png" alt="hangman">';
                    break;
                case 2:
                    sketch.innerHTML = '<img src="img/hangman2.png" alt="hangman">';
                    break;
                case 1:
                    sketch.innerHTML = '<img src="img/hangman1.png" alt="hangman">';
                    break;
                case 0:
                    sketch.innerHTML = '<img src="img/hangmanWhole.png" alt="hangman">';
                    break;
            }
        }
    }
    else {
        alert("You ran out of lives!");
    }
}

function getClue(){
    let result = chosenWord.clue();

    return result;
}



categoryContent.innerHTML = `Category:&nbsp;${categoryName()}`;

wordField.innerText = `Word: ${guessWord()}`;

sketch.innerHTML = '<img src="img/hangman6.png" alt="hangman">';

let lives = 6;

currentLives.innerHTML = `Lives:&nbsp;${lives}`;



// THIS WAS A LIFE SAVER 
// (In each iteration of the loop below, the index number is converted to the UTF-16 equivalent of a letter in the English alphabet(in this case the numbers are in the range 65 ... <= 90) and then makes the letter lowercase. 
// The letter is then used to find an element in the html document with an id that matches the letter, declares it along with adding an event listener to it).
for (let i = 65; i <= 90; i++) {
    let letter = String.fromCharCode(i).toLowerCase();
    let element = document.getElementById(letter);
    
    if (element) {
        element.addEventListener('click', function() {
            checkWordLetters(this);
        });
    }
}

hintBtn.addEventListener('click', function() {
    clue.innerHTML = `Clue&nbsp;-&nbsp;${getClue()}`;
})

playAgainBtn.addEventListener('click', function() {
    window.location.reload();
})

