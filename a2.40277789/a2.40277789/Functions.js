

//Exercise 3


// A. Function: addNumbers
function addNumbers(numbers) {
  let sum=0;
  for(let i=0;i<numbers.length;i++){
    sum+=numbers[i];
  }
  return sum;
}

const numbers1=[1,2,3,4,5,6,7,8,9,10];
document.getElementById("addNumbers").innerHTML = addNumbers(numbers1);

  
// B. Function: findMaxNumber
function findMaxNumber() {
  if (arguments.length === 0) {
    return undefined; // Check if no arguments are passed,    do we have to???
  }

  let max = arguments[0]; 
  for (let i = 1; i < arguments.length; i++) {
    if (arguments[i] > max) {
      max = arguments[i];
    }
  }
  return max;
}

document.getElementById("findMaxNumber").innerHTML = findMaxNumber(1,2,3,4,5,6,7,8,9,10); // why doesn't it take arrays as arguments???

  
// C. Function: addOnlyNumbers
function addOnlyNumbers(mixedData) {
  let sum = 0;

  for (let i = 0; i < mixedData.length; i++) {
    if (!isNaN(parseFloat(mixedData[i]))) {
      sum += parseFloat(mixedData[i]); // Add the parsed number to sum if it's not NaN
    }
  }

  return sum;
}

const mixedData = [4, 5, "3.0 birds", true, "birds2"];
document.getElementById("addOnlyNumbers").innerHTML = addOnlyNumbers(mixedData);



// D. Function: getDigits
function getDigits(str) {
  let digits = str.replace(/\D/g, '');
  return digits;
}

document.getElementById("getDigits").innerHTML = getDigits("I have 2 apples and 3 pineapples");



  
// E. Function: reverseString
function reverseString(str) {
  let reversed = '';

  for (let i = str.length - 1; i >= 0; i--) {
    reversed += str.charAt(i);
  }

  return reversed;
}

document.getElementById("reverseString").innerHTML = reverseString("Hello World!");

  
// F. Function: getCurrentDate
/*
function getCurrentDate() {
  const currentDate = new Date();
  const options = { weekday: 'long', month: 'short', day: 'numeric', year: 'numeric' };
  return currentDate.toLocaleDateString('en-US', options);
}
*/

function getCurrentDate() {
  const currentDate = new Date();

  const days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
  const months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

  const dayOfWeek = days[currentDate.getDay()];
  const month = months[currentDate.getMonth()];
  const date = currentDate.getDate();
  const year = currentDate.getFullYear();

  const formattedDate = dayOfWeek + ", " + month + " " + date + ", " + year;

  return formattedDate;
}

document.getElementById("getCurrentDate").innerHTML = getCurrentDate();
