function calculateTotal() {
    var item1 = document.getElementById("BasicWebProgramming").value;
    var item2 = document.getElementById("IntrotoPHP").value;
    var item3 = document.getElementById("AdvancedJQuery").value;

    var price1 = 19.99;
    var price2 = 86.00;
    var price3 = 55.00;
  
    if (item1 === '' || item2 === '' || item3 === '' || !Number.isInteger(Number(item1)) || !Number.isInteger(Number(item2)) || !Number.isInteger(Number(item3))) {
      alert('Please enter integer values for all items.');
      return;
    }
  
    
    var totalCost = (parseInt(item1) * price1) + (parseInt(item2) * price2) + (parseInt(item3) * price3);
    document.getElementById('totalCost').innerHTML =
    " <span style='font-weight: bold;'> Basic Web Programming (Quantity= "  +item1 +"): <span style='font-weight: normal;'> $"+item1*price1 +"<br>" +
    " <span style='font-weight: bold;'> Intro to PHP (Quantity= " +item2+"): <span style='font-weight: normal;'> $"+item2*price2+ "<br>" +
    "<span style='font-weight: bold;'>  Advanced JQuery (Quantity= " +item3+"): <span style='font-weight: normal;'> $"+item3*price3+ "<br>" +
    "<span style='font-weight: bold;'> Final total: <span style='font-weight: normal;'>$" + totalCost.toFixed(2); 
    

        var button = document.getElementById("button");
        button.style.borderColor = "blue";
    
   
  }
  
