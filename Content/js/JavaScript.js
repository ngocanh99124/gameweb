var slideIndex = 0;
//0:boy 1:girl 2:robot

function showSlides(n) {
  
    document.getElementById("boy").style.display = "none";
    document.getElementById("girl").style.display = "none";
    if (n == 0)
       {
            document.getElementById("boy").style.display = "inline";
            return;
       }
    if (n == 1)
        {
            document.getElementById("girl").style.display = "inline";
            return;
        }
}
showSlides(slideIndex);


function increase() {
    slideIndex = slideIndex+1;
    if (slideIndex < 0) slideIndex = 1;
    if (slideIndex > 1) slideIndex = 0;
    showSlides(slideIndex);
}
function decrease() {
    slideIndex = slideIndex-1;
    if (slideIndex < 0) slideIndex = 1;
    if (slideIndex > 1) slideIndex = 0;
    showSlides(slideIndex);
}


var socket = new WebSocket("ws://localhost:8080/WebApplication5/endpoint");
    var json = { "status": 0, "player": "3" };
socket.onopen = function (message) { processOnOpen(message); }
function chon() {
    socket.send("abc");
}




